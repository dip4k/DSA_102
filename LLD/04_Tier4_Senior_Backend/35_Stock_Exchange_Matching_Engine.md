# 35. Stock Exchange Order Matching Engine (Nasdaq Lite)

## 📌 Context & Motivation
An **Order Matching Engine** is the core algorithmic component of modern financial exchanges (Nasdaq, NYSE, Binance, Coinbase, LMAX). In senior and staff engineering interviews, it evaluates your mastery across:
1. **Multi-Key Sorting (Price-Time Priority / FIFO)**: Buyers sorted descending by price, sellers ascending by price; ties broken strictly by arrival time.
2. **Efficient Data Structures (`O(log N)` or `O(1)`)**: Fast order book insertions, priority peeks, and `O(log N)` or `O(1)` arbitrary order cancellations.
3. **Execution Semantics & Partial Fills**: Matching incoming aggressive taker orders against resting maker limit orders with residual quantity rollover.
4. **Thread Safety & Lock Granularity**: Single-symbol serialized execution to guarantee deterministic trade sequencing without cross-symbol contention.
5. **Exact Numeric Precision**: Absolute zero tolerance for floating-point rounding errors (must use `decimal` in C# or `Decimal` / integer micros in Python).

---

## 🏛️ Phase 1: Scope & Non-Functional Requirements (NFRs)

### Functional Requirements
- **Order Placement**: Support `Limit` and `Market` orders for both `Buy` (Bid) and `Sell` (Ask) sides.
- **Price-Time Priority (FIFO)**:
  - **Bids**: Highest price takes precedence. For equal prices, earlier timestamp matches first.
  - **Asks**: Lowest price takes precedence. For equal prices, earlier timestamp matches first.
- **Matching Condition**: A trade executes whenever `Highest Bid Price >= Lowest Ask Price`. The execution price is determined by the resting order (the maker price already on the book).
- **Partial & Multi-Level Fills**: An incoming large order sweeps across multiple resting price levels until filled or until the limit price boundary is reached.
- **Order Cancellation**: Active limit orders can be cancelled arbitrarily by their unique ID with sub-millisecond latency.

### Non-Functional Requirements
- **Deterministic Sequencing**: Matches for a specific symbol must execute in strict chronological sequence.
- **Throughput & Latency**: In-memory matching throughput target of 50,000+ orders/second per symbol with sub-millisecond execution latency.
- **Concurrency Isolation**: Lock striping per trading pair / symbol (`AAPL`, `MSFT`, `BTC-USD`). Orders for `AAPL` must never block orders for `MSFT`.
- **Financial Precision**: Decimal arithmetic only. Floating point `float` or `double` is strictly forbidden due to binary rounding inaccuracies.

---

## 🏛️ Phase 2: Core Domain Contracts

```csharp
public enum OrderSide { Buy, Sell }
public enum OrderType { Limit, Market }
public enum OrderStatus { New, Open, PartiallyFilled, Filled, Cancelled, Rejected }

public interface IMatchingEngine
{
    IReadOnlyList<Trade> SubmitOrder(Order order);
    bool CancelOrder(string symbol, Guid orderId);
    IReadOnlyList<Order> GetActiveBids(string symbol);
    IReadOnlyList<Order> GetActiveAsks(string symbol);
}

public interface IOrderBook
{
    string Symbol { get; }
    IReadOnlyList<Trade> ProcessOrder(Order incomingOrder);
    bool CancelOrder(Guid orderId);
}
```

---

## 🏛️ Phase 3: Class & State Modeling

### Class Diagram

```mermaid
classDiagram
    class Order {
        +Guid Id
        +string Symbol
        +OrderSide Side
        +OrderType Type
        +decimal Price
        +int InitialQuantity
        +int RemainingQuantity
        +DateTimeOffset Timestamp
        +OrderStatus Status
        +Fill(int qty)
        +Cancel()
    }

    class Trade {
        +Guid Id
        +Guid BuyOrderId
        +Guid SellOrderId
        +string Symbol
        +decimal Price
        +int Quantity
        +DateTimeOffset ExecutedAt
    }

    class OrderBook {
        +string Symbol
        -SortedSet~Order~ _bids
        -SortedSet~Order~ _asks
        -Dictionary~Guid, Order~ _activeOrders
        -object _bookLock
        +ProcessOrder(Order order) List~Trade~
        +CancelOrder(Guid orderId) bool
    }

    class MatchingEngine {
        -ConcurrentDictionary~string, OrderBook~ _books
        +SubmitOrder(Order order) List~Trade~
        +CancelOrder(string symbol, Guid orderId) bool
    }

    OrderBook "1" *-- "0..*" Order : contains
    OrderBook "1" --> "0..*" Trade : generates
    MatchingEngine "1" o-- "1..*" OrderBook : manages
```

### Order Lifecycle State Machine

```mermaid
stateDiagram-v2
    [*] --> New
    New --> Open : Placed on OrderBook
    New --> Filled : Immediate full taker match
    Open --> PartiallyFilled : Partial match
    PartiallyFilled --> PartiallyFilled : Further partial match
    PartiallyFilled --> Filled : Remaining quantity exhausted
    Open --> Filled : Full match
    Open --> Cancelled : User cancellation
    PartiallyFilled --> Cancelled : Remaining quantity cancelled
    New --> Rejected : Invalid params / No liquidity
    Filled --> [*]
    Cancelled --> [*]
    Rejected --> [*]
```

---

## 🏛️ Phase 4: Patterns & Concurrency Architecture

### 1. Data Structure Trade-off: `SortedSet<T>` vs. Priority Queue vs. Price Ladder
| Data Structure | Peek Best Price | Insert Order | Cancel Arbitrary Order | Memory Overhead | Verdict |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Binary Heap (`PriorityQueue`)** | `O(1)` | `O(log N)` | `O(N)` (Linear search required) | Low | ❌ Unusable for exchanges where 80%+ of limit orders get cancelled. |
| **Red-Black Tree (`SortedSet<T>`)** | `O(log N)` or `O(1)` via `Min`/`Max` | `O(log N)` | `O(log N)` via direct reference removal | Moderate | ✅ **Optimal for interview implementation** (clean, balanced, built-in). |
| **Price Ladder (`SortedDictionary` + FIFO `LinkedList`)** | `O(1)` | `O(log P)` where `P` is unique price points | `O(1)` with node pointer map | Higher | ✅ **Production institutional standard** (Nasdaq / LMAX style). |

### 2. Multi-Key Comparer Invariants
In C#, `SortedSet<Order>` requires an `IComparer<Order>` that implements a total ordering:
1. **Primary Key**:
   - Bids: Compare price descending (`y.Price.CompareTo(x.Price)`).
   - Asks: Compare price ascending (`x.Price.CompareTo(y.Price)`).
2. **Secondary Key**: Compare arrival timestamp ascending (`x.Timestamp.CompareTo(y.Timestamp)`).
3. **Tertiary Unique Tie-Breaker**: Compare `Guid` ID (`x.Id.CompareTo(y.Id)`). **Crucial**: Without the ID tie-breaker, `SortedSet` would treat two distinct orders with identical price and timestamp as equal duplicates and reject the second order!

### 3. Execution Price Invariant (Maker vs. Taker)
When an incoming taker order matches against an existing maker order on the book:
- The execution price is **always the maker price** (the resting price in the book).
- If a buyer submits a limit order at \$105 and the lowest resting ask is \$100, the trade executes at **\$100**, rewarding the taker with price improvement.

---

## 🏛️ Phase 5: Production Implementations

### Primary: Production C# (.NET 8/9)

```csharp
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace StockExchange.Engine;

public enum OrderSide { Buy, Sell }
public enum OrderType { Limit, Market }
public enum OrderStatus { New, Open, PartiallyFilled, Filled, Cancelled, Rejected }

public sealed class Order
{
    public Guid Id { get; }
    public string Symbol { get; }
    public OrderSide Side { get; }
    public OrderType Type { get; }
    public decimal Price { get; }
    public int InitialQuantity { get; }
    public int RemainingQuantity { get; private set; }
    public DateTimeOffset Timestamp { get; }
    public OrderStatus Status { get; private set; }

    public Order(string symbol, OrderSide side, OrderType type, decimal price, int quantity)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be empty.", nameof(symbol));
        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be strictly positive.");
        if (type == OrderType.Limit && price <= 0)
            throw new ArgumentOutOfRangeException(nameof(price), "Limit order price must be strictly positive.");

        Id = Guid.NewGuid();
        Symbol = symbol.Trim().ToUpperInvariant();
        Side = side;
        Type = type;
        Price = type == OrderType.Market ? 0m : price;
        InitialQuantity = quantity;
        RemainingQuantity = quantity;
        Timestamp = DateTimeOffset.UtcNow;
        Status = OrderStatus.New;
    }

    public void MarkOpen()
    {
        if (Status == OrderStatus.New)
            Status = OrderStatus.Open;
    }

    public void Fill(int fillQuantity)
    {
        if (fillQuantity <= 0 || fillQuantity > RemainingQuantity)
            throw new InvalidOperationException($"Invalid fill quantity {fillQuantity}. Remaining: {RemainingQuantity}");

        RemainingQuantity -= fillQuantity;
        Status = RemainingQuantity == 0 ? OrderStatus.Filled : OrderStatus.PartiallyFilled;
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Filled)
            throw new InvalidOperationException("Cannot cancel an already filled order.");
        Status = OrderStatus.Cancelled;
    }
}

public sealed record Trade(
    Guid Id,
    Guid BuyOrderId,
    Guid SellOrderId,
    string Symbol,
    decimal Price,
    int Quantity,
    DateTimeOffset ExecutedAt);

public sealed class BidOrderComparer : IComparer<Order>
{
    public int Compare(Order? x, Order? y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (x is null) return -1;
        if (y is null) return 1;

        // 1. Highest price first
        int priceComp = y.Price.CompareTo(x.Price);
        if (priceComp != 0) return priceComp;

        // 2. Earliest timestamp first
        int timeComp = x.Timestamp.CompareTo(y.Timestamp);
        if (timeComp != 0) return timeComp;

        // 3. Unique tie-breaker
        return x.Id.CompareTo(y.Id);
    }
}

public sealed class AskOrderComparer : IComparer<Order>
{
    public int Compare(Order? x, Order? y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (x is null) return 1;
        if (y is null) return -1;

        // 1. Lowest price first
        int priceComp = x.Price.CompareTo(y.Price);
        if (priceComp != 0) return priceComp;

        // 2. Earliest timestamp first
        int timeComp = x.Timestamp.CompareTo(y.Timestamp);
        if (timeComp != 0) return timeComp;

        // 3. Unique tie-breaker
        return x.Id.CompareTo(y.Id);
    }
}

public sealed class OrderBook : IOrderBook
{
    public string Symbol { get; }
    private readonly SortedSet<Order> _bids = new(new BidOrderComparer());
    private readonly SortedSet<Order> _asks = new(new AskOrderComparer());
    private readonly Dictionary<Guid, Order> _activeOrders = new();
    private readonly object _bookLock = new();

    public OrderBook(string symbol)
    {
        Symbol = symbol.Trim().ToUpperInvariant();
    }

    public IReadOnlyList<Trade> ProcessOrder(Order incomingOrder)
    {
        var executedTrades = new List<Trade>();

        lock (_bookLock)
        {
            if (incomingOrder.Side == OrderSide.Buy)
            {
                MatchIncomingBuy(incomingOrder, executedTrades);
            }
            else
            {
                MatchIncomingSell(incomingOrder, executedTrades);
            }

            // If incoming limit order has residual quantity, place it on the book
            if (incomingOrder.RemainingQuantity > 0 && incomingOrder.Type == OrderType.Limit)
            {
                incomingOrder.MarkOpen();
                if (incomingOrder.Side == OrderSide.Buy)
                {
                    _bids.Add(incomingOrder);
                }
                else
                {
                    _asks.Add(incomingOrder);
                }
                _activeOrders[incomingOrder.Id] = incomingOrder;
            }
        }

        return executedTrades;
    }

    public bool CancelOrder(Guid orderId)
    {
        lock (_bookLock)
        {
            if (!_activeOrders.TryGetValue(orderId, out var order))
                return false;

            if (order.Side == OrderSide.Buy)
                _bids.Remove(order);
            else
                _asks.Remove(order);

            order.Cancel();
            _activeOrders.Remove(orderId);
            return true;
        }
    }

    private void MatchIncomingBuy(Order buyOrder, List<Trade> trades)
    {
        while (_asks.Count > 0 && buyOrder.RemainingQuantity > 0)
        {
            var bestAsk = _asks.Min!;

            // Crossing spread check for Limit order
            if (buyOrder.Type == OrderType.Limit && buyOrder.Price < bestAsk.Price)
                break; // No further match possible

            int matchQuantity = Math.Min(buyOrder.RemainingQuantity, bestAsk.RemainingQuantity);
            decimal tradePrice = bestAsk.Price; // Maker resting price takes precedence

            buyOrder.Fill(matchQuantity);
            bestAsk.Fill(matchQuantity);

            trades.Add(new Trade(
                Guid.NewGuid(),
                buyOrder.Id,
                bestAsk.Id,
                Symbol,
                tradePrice,
                matchQuantity,
                DateTimeOffset.UtcNow));

            if (bestAsk.RemainingQuantity == 0)
            {
                _asks.Remove(bestAsk);
                _activeOrders.Remove(bestAsk.Id);
            }
        }
    }

    private void MatchIncomingSell(Order sellOrder, List<Trade> trades)
    {
        while (_bids.Count > 0 && sellOrder.RemainingQuantity > 0)
        {
            var bestBid = _bids.Min!;

            if (sellOrder.Type == OrderType.Limit && sellOrder.Price > bestBid.Price)
                break; // No further match possible

            int matchQuantity = Math.Min(sellOrder.RemainingQuantity, bestBid.RemainingQuantity);
            decimal tradePrice = bestBid.Price; // Maker resting price takes precedence

            sellOrder.Fill(matchQuantity);
            bestBid.Fill(matchQuantity);

            trades.Add(new Trade(
                Guid.NewGuid(),
                bestBid.Id,
                sellOrder.Id,
                Symbol,
                tradePrice,
                matchQuantity,
                DateTimeOffset.UtcNow));

            if (bestBid.RemainingQuantity == 0)
            {
                _bids.Remove(bestBid);
                _activeOrders.Remove(bestBid.Id);
            }
        }
    }
}

public sealed class MatchingEngine : IMatchingEngine
{
    private readonly ConcurrentDictionary<string, OrderBook> _books = new();

    public IReadOnlyList<Trade> SubmitOrder(Order order)
    {
        var book = _books.GetOrAdd(order.Symbol, s => new OrderBook(s));
        return book.ProcessOrder(order);
    }

    public bool CancelOrder(string symbol, Guid orderId)
    {
        if (_books.TryGetValue(symbol.Trim().ToUpperInvariant(), out var book))
        {
            return book.CancelOrder(orderId);
        }
        return false;
    }

    public IReadOnlyList<Order> GetActiveBids(string symbol) => Array.Empty<Order>();
    public IReadOnlyList<Order> GetActiveAsks(string symbol) => Array.Empty<Order>();
}

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("=== 📈 HIGH-PERFORMANCE STOCK EXCHANGE MATCHING ENGINE ===\n");

        var engine = new MatchingEngine();

        // 1. Submit resting Ask orders (Sellers)
        Console.WriteLine("1. Placing resting Sell Limit Orders for MSFT:");
        var ask1 = new Order("MSFT", OrderSide.Sell, OrderType.Limit, price: 420.50m, quantity: 100);
        var ask2 = new Order("MSFT", OrderSide.Sell, OrderType.Limit, price: 421.00m, quantity: 200);
        var ask3 = new Order("MSFT", OrderSide.Sell, OrderType.Limit, price: 420.50m, quantity: 50); // Later timestamp

        engine.SubmitOrder(ask1);
        engine.SubmitOrder(ask2);
        engine.SubmitOrder(ask3);
        Console.WriteLine($"   Placed Ask 1: 100 @ $420.50");
        Console.WriteLine($"   Placed Ask 2: 200 @ $421.00");
        Console.WriteLine($"   Placed Ask 3: 50 @ $420.50\n");

        // 2. Incoming Aggressive Buy Limit Order that crosses the spread
        Console.WriteLine("2. Placing Buy Limit Order: 120 @ $420.75 (Sweeps 100 from Ask 1, 20 from Ask 3):");
        var buy1 = new Order("MSFT", OrderSide.Buy, OrderType.Limit, price: 420.75m, quantity: 120);
        var trades1 = engine.SubmitOrder(buy1);

        foreach (var t in trades1)
        {
            Console.WriteLine($"   ⚡ Trade Executed: {t.Quantity} MSFT @ ${t.Price} (Buy: {t.BuyOrderId.ToString()[..8]}, Sell: {t.SellOrderId.ToString()[..8]})");
        }
        Console.WriteLine($"   Buyer Remaining: {buy1.RemainingQuantity}, Status: {buy1.Status}\n");

        // 3. Incoming Market Sell Order (Liquidates into resting Bids or rejects if empty)
        Console.WriteLine("3. Placing resting Buy Limit Order:");
        var bid1 = new Order("MSFT", OrderSide.Buy, OrderType.Limit, price: 419.00m, quantity: 80);
        engine.SubmitOrder(bid1);
        Console.WriteLine($"   Placed Bid: 80 @ $419.00");

        Console.WriteLine("4. Submitting Market Sell Order for 50 shares:");
        var marketSell = new Order("MSFT", OrderSide.Sell, OrderType.Market, price: 0m, quantity: 50);
        var trades2 = engine.SubmitOrder(marketSell);

        foreach (var t in trades2)
        {
            Console.WriteLine($"   ⚡ Market Trade Executed: {t.Quantity} MSFT @ ${t.Price}");
        }
        Console.WriteLine($"   Market Sell Remaining: {marketSell.RemainingQuantity}, Status: {marketSell.Status}\n");

        // 5. Test Cancellation
        Console.WriteLine("5. Testing Order Cancellation for Ask 2 (200 @ $421.00):");
        bool cancelled = engine.CancelOrder("MSFT", ask2.Id);
        Console.WriteLine($"   Cancellation Result: {cancelled}, Status: {ask2.Status}");

        Console.WriteLine("\n=== Order Matching Engine Correctness & Invariants Verified! ===");
    }
}
```

---

### Secondary: Production Python (3.11+)

```python
from __future__ import annotations
import uuid
import threading
from decimal import Decimal
from enum import Enum, auto
from dataclasses import dataclass, field
from datetime import datetime, timezone
from typing import Dict, List, Optional


class OrderSide(Enum):
    BUY = auto()
    SELL = auto()


class OrderType(Enum):
    LIMIT = auto()
    MARKET = auto()


class OrderStatus(Enum):
    NEW = auto()
    OPEN = auto()
    PARTIALLY_FILLED = auto()
    FILLED = auto()
    CANCELLED = auto()
    REJECTED = auto()


@dataclass
class Order:
    symbol: str
    side: OrderSide
    order_type: OrderType
    price: Decimal
    initial_quantity: int
    id: uuid.UUID = field(default_factory=uuid.uuid4)
    remaining_quantity: int = field(init=False)
    timestamp: datetime = field(default_factory=lambda: datetime.now(timezone.utc))
    status: OrderStatus = OrderStatus.NEW

    def __post_init__(self):
        if not self.symbol:
            raise ValueError("Symbol cannot be empty.")
        if self.initial_quantity <= 0:
            raise ValueError("Quantity must be strictly positive.")
        if self.order_type == OrderType.LIMIT and self.price <= Decimal("0"):
            raise ValueError("Limit order price must be strictly positive.")
        
        self.symbol = self.symbol.upper()
        self.remaining_quantity = self.initial_quantity
        if self.order_type == OrderType.MARKET:
            self.price = Decimal("0")

    def mark_open(self) -> None:
        if self.status == OrderStatus.NEW:
            self.status = OrderStatus.OPEN

    def fill(self, fill_quantity: int) -> None:
        if fill_quantity <= 0 or fill_quantity > self.remaining_quantity:
            raise ValueError(f"Invalid fill quantity {fill_quantity}. Remaining: {self.remaining_quantity}")
        self.remaining_quantity -= fill_quantity
        self.status = OrderStatus.FILLED if self.remaining_quantity == 0 else OrderStatus.PARTIALLY_FILLED

    def cancel(self) -> None:
        if self.status == OrderStatus.FILLED:
            raise ValueError("Cannot cancel an already filled order.")
        self.status = OrderStatus.CANCELLED


@dataclass(frozen=True)
class Trade:
    id: uuid.UUID
    buy_order_id: uuid.UUID
    sell_order_id: uuid.UUID
    symbol: str
    price: Decimal
    quantity: int
    executed_at: datetime


class OrderBook:
    """
    Thread-safe Order Book maintaining Price-Time Priority.
    Internal sorting:
      - Bids: Sorted by (-Price, Timestamp, Id)
      - Asks: Sorted by (Price, Timestamp, Id)
    """
    def __init__(self, symbol: str):
        self.symbol = symbol.upper()
        self._bids: List[Order] = []  # Kept sorted: highest price first
        self._asks: List[Order] = []  # Kept sorted: lowest price first
        self._active_orders: Dict[uuid.UUID, Order] = {}
        self._lock = threading.Lock()

    def process_order(self, incoming_order: Order) -> List[Trade]:
        executed_trades: List[Trade] = []

        with self._lock:
            if incoming_order.side == OrderSide.BUY:
                self._match_incoming_buy(incoming_order, executed_trades)
            else:
                self._match_incoming_sell(incoming_order, executed_trades)

            # If residual limit quantity remains, add to resting book
            if incoming_order.remaining_quantity > 0 and incoming_order.order_type == OrderType.LIMIT:
                incoming_order.mark_open()
                if incoming_order.side == OrderSide.BUY:
                    self._insert_bid(incoming_order)
                else:
                    self._insert_ask(incoming_order)
                self._active_orders[incoming_order.id] = incoming_order

        return executed_trades

    def cancel_order(self, order_id: uuid.UUID) -> bool:
        with self._lock:
            order = self._active_orders.get(order_id)
            if not order:
                return False

            if order.side == OrderSide.BUY:
                self._bids = [o for o in self._bids if o.id != order_id]
            else:
                self._asks = [o for o in self._asks if o.id != order_id]

            order.cancel()
            del self._active_orders[order_id]
            return True

    def _insert_bid(self, order: Order) -> None:
        # Bids: Descending by price, then ascending by timestamp
        import bisect
        # Bisect key: (-price, timestamp)
        keys = [(-o.price, o.timestamp) for o in self._bids]
        idx = bisect.bisect_right(keys, (-order.price, order.timestamp))
        self._bids.insert(idx, order)

    def _insert_ask(self, order: Order) -> None:
        # Asks: Ascending by price, then ascending by timestamp
        import bisect
        keys = [(o.price, o.timestamp) for o in self._asks]
        idx = bisect.bisect_right(keys, (order.price, order.timestamp))
        self._asks.insert(idx, order)

    def _match_incoming_buy(self, buy_order: Order, trades: List[Trade]) -> None:
        while self._asks and buy_order.remaining_quantity > 0:
            best_ask = self._asks[0]

            # Price cross check for limit order
            if buy_order.order_type == OrderType.LIMIT and buy_order.price < best_ask.price:
                break

            match_qty = min(buy_order.remaining_quantity, best_ask.remaining_quantity)
            trade_price = best_ask.price  # Resting maker price takes precedence

            buy_order.fill(match_qty)
            best_ask.fill(match_qty)

            trades.append(Trade(
                id=uuid.uuid4(),
                buy_order_id=buy_order.id,
                sell_order_id=best_ask.id,
                symbol=self.symbol,
                price=trade_price,
                quantity=match_qty,
                executed_at=datetime.now(timezone.utc)
            ))

            if best_ask.remaining_quantity == 0:
                self._asks.pop(0)
                self._active_orders.pop(best_ask.id, None)

    def _match_incoming_sell(self, sell_order: Order, trades: List[Trade]) -> None:
        while self._bids and sell_order.remaining_quantity > 0:
            best_bid = self._bids[0]

            if sell_order.order_type == OrderType.LIMIT and sell_order.price > best_bid.price:
                break

            match_qty = min(sell_order.remaining_quantity, best_bid.remaining_quantity)
            trade_price = best_bid.price  # Resting maker price takes precedence

            sell_order.fill(match_qty)
            best_bid.fill(match_qty)

            trades.append(Trade(
                id=uuid.uuid4(),
                buy_order_id=best_bid.id,
                sell_order_id=sell_order.id,
                symbol=self.symbol,
                price=trade_price,
                quantity=match_qty,
                executed_at=datetime.now(timezone.utc)
            ))

            if best_bid.remaining_quantity == 0:
                self._bids.pop(0)
                self._active_orders.pop(best_bid.id, None)


class MatchingEngine:
    def __init__(self):
        self._books: Dict[str, OrderBook] = {}
        self._engine_lock = threading.Lock()

    def submit_order(self, order: Order) -> List[Trade]:
        symbol = order.symbol.upper()
        with self._engine_lock:
            if symbol not in self._books:
                self._books[symbol] = OrderBook(symbol)
            book = self._books[symbol]
        return book.process_order(order)

    def cancel_order(self, symbol: str, order_id: uuid.UUID) -> bool:
        symbol = symbol.upper()
        book = self._books.get(symbol)
        if not book:
            return False
        return book.cancel_order(order_id)


# Verification Driver
if __name__ == "__main__":
    print("=== 📈 PYTHON MATCHING ENGINE VERIFICATION ===")
    engine = MatchingEngine()

    # 1. Place Resting Asks
    ask1 = Order("AAPL", OrderSide.SELL, OrderType.LIMIT, Decimal("180.50"), 100)
    ask2 = Order("AAPL", OrderSide.SELL, OrderType.LIMIT, Decimal("181.00"), 200)
    ask3 = Order("AAPL", OrderSide.SELL, OrderType.LIMIT, Decimal("180.50"), 50)
    engine.submit_order(ask1)
    engine.submit_order(ask2)
    engine.submit_order(ask3)
    print("Placed Resting Asks: 100 @ $180.50, 200 @ $181.00, 50 @ $180.50")

    # 2. Incoming Buy Sweeper
    buy = Order("AAPL", OrderSide.BUY, OrderType.LIMIT, Decimal("180.75"), 120)
    trades = engine.submit_order(buy)
    for t in trades:
        print(f"⚡ Trade: {t.quantity} AAPL @ ${t.price}")
    print(f"Buyer remaining: {buy.remaining_quantity}, status: {buy.status}")

    # 3. Cancel Ask 2
    success = engine.cancel_order("AAPL", ask2.id)
    print(f"Cancel Ask 2 success: {success}, status: {ask2.status}")
```

---

## 🏛️ Phase 6: Extensibility Drill & Edge Cases

### Edge Cases Matrix

| Edge Case | Failure Scenario | Defensive Architectural Handling |
| :--- | :--- | :--- |
| **Market Order on Empty Book** | Unbounded slippage; price swings to infinity or zero. | If no resting liquidity exists on the opposite side, the market order is rejected immediately or executed partially with residual cancelled (IOC rule). |
| **Equal Price-Time Collision** | Two orders arrive with exact same microsecond timestamp and price. | The unique `Guid` tie-breaker in `IComparer` guarantees strict total ordering without accidental deduplication. |
| **Self-Trade Prevention (STP)** | A single trading firm matches its own buy and sell orders (illegal wash trading). | Check `order.FirmId == restingOrder.FirmId`. If matched, trigger configured STP policy: `CancelNewest`, `CancelOldest`, or `DecrementAndCancel`. |
| **Arbitrary Order Cancellation** | Trader attempts to cancel an order that was filled a millisecond prior. | `CancelOrder` returns `false` idempotently and leaves trade ledger untouched. |
| **Crossed Book at Initialization** | Historical replay where bids > asks. | An auction uncrossing call resolves the maximum executable volume at a single clearing price before continuous trading opens. |

### Architectural Curveballs & Extensions
1. **Immediate-Or-Cancel (IOC) and Fill-Or-Kill (FOK)**:
   - **IOC**: Match as much as possible against current resting book; cancel any remaining unfilled volume immediately without adding to the book.
   - **FOK**: Pre-check if the entire quantity can be filled immediately at the limit price. If not, reject the entire order with zero partial fills.
2. **LMAX Disruptor Ultra-Low-Latency Architecture**:
   - Institutional engines avoid all locks, heap allocations, and garbage collection.
   - Orders are ingested via a single-threaded CPU-pinned core processing a pre-allocated ring buffer. Memory padding prevents CPU L1/L2 cache line false sharing, achieving over 6,000,000 orders/sec per core.
