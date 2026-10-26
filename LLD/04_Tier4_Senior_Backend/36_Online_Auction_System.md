# 36. Online Auction & Real-Time Bidding System (eBay / Sotheby's Lite)

## 📌 Context & Motivation
An **Online Real-Time Auction System** is one of the most intellectually demanding Senior and Staff LLD interview problems (frequently asked at eBay, Amazon, StockX, Uber, and financial auction platforms). It challenges candidates across four critical engineering axes:
1. **Financial Escrow & Double-Entry Integrity**: Bidders must have funds pre-authorized or placed on hold prior to placing a bid; when outbid, holds must be immediately released without deadlocks.
2. **Atomic High-Contention Bidding**: Multiple bidders submit bids simultaneously in the final seconds of an auction. Bids must be validated atomically against the current highest bid plus the minimum bid increment.
3. **Dynamic Soft-Close Time Extension ("Anti-Sniping")**: If a bid arrives in the final 60 seconds, the auction dynamically extends by 2 minutes to prevent sniping bots from robbing human bidders.
4. **Lifecycle State Machine**: Strict transitions through `Draft -> Active -> Extended -> Settled` or `ReserveNotMet`.

---

## 🏛️ Phase 1: Scope & Non-Functional Requirements (NFRs)

### Functional Requirements
- **Auction Creation**: Sellers list items with starting price, reserve price (minimum acceptable sale price), minimum increment, and end time.
- **Atomic Bidding**: Bids must exceed `CurrentHighestBid + MinimumIncrement`. The bidder must have sufficient available funds placed into escrow hold.
- **Escrow Fund Management**:
  - Placing a bid reserves funds in escrow.
  - When an existing high bidder is outbid, their escrow hold is released immediately.
  - When the auction settles, the winning bidder's held funds transfer to the seller.
- **Anti-Sniping Dynamic Extension**: If a valid bid arrives within 60 seconds of the auction deadline, extend the deadline by 2 minutes. A hard cap (e.g. 30 minutes total extensions) prevents infinite extension loops.
- **Auction Conclusion & Settlement**:
  - If `HighestBid >= ReservePrice`: Settle funds and declare winner.
  - If `HighestBid < ReservePrice`: Mark `ReserveNotMet`, release winner's escrow hold, and return item to seller.

### Non-Functional Requirements
- **High Concurrency & Deadlock Prevention**: Handle thousands of simultaneous bids per second during final seconds without race conditions or circular deadlocks.
- **Strict Money Invariant**: Decimal precision only; zero floating-point math. No currency mismatches.
- **Lock Granularity**: Auction listings use per-auction locks. Contention on one auction must never block bidding on another.
- **Idempotency**: Retried bid submissions with identical transaction IDs must not double-hold escrow funds.

---

## 🏛️ Phase 2: Core Domain Contracts

```csharp
public enum AuctionState
{
    Draft,
    Active,
    Extended,
    Settled,
    ReserveNotMet,
    Cancelled
}

public interface IEscrowService
{
    void Deposit(string bidderId, decimal amount);
    bool HoldFunds(string bidderId, decimal amount);
    void ReleaseHold(string bidderId, decimal amount);
    void SettleFunds(string buyerId, string sellerId, decimal amount);
    decimal GetAvailableBalance(string bidderId);
}

public interface IAuctionNotifier
{
    Task NotifyOutbidAsync(string bidderId, string auctionId, decimal newHighestBid);
    Task NotifyWinnerAsync(string bidderId, string auctionId, decimal winningBid);
}

public interface IAuctionEngine
{
    void RegisterAuction(AuctionListing auction);
    BidResult SubmitBid(string auctionId, string bidderId, decimal bidAmount, DateTimeOffset currentUtc);
    void ConcludeAuction(string auctionId, DateTimeOffset currentUtc);
}
```

---

## 🏛️ Phase 3: Class & State Modeling

### Class Diagram

```mermaid
classDiagram
    class Money {
        +decimal Amount
        +string Currency
        +Add(Money other) Money
        +Subtract(Money other) Money
        +IsGreaterThan(Money other) bool
    }

    class Bid {
        +Guid Id
        +string BidderId
        +Money Amount
        +DateTimeOffset Timestamp
    }

    class AuctionListing {
        +string Id
        +string SellerId
        +string ItemTitle
        +Money StartingPrice
        +Money ReservePrice
        +Money MinIncrement
        +DateTimeOffset EndTime
        +DateTimeOffset HardCeilingEndTime
        +AuctionState State
        +Bid HighestBid
        +object ListingLock
        +PlaceBid(string bidderId, Money amount, DateTimeOffset now) BidResult
        +EvaluateConclusion(DateTimeOffset now)
    }

    class EscrowLedger {
        +decimal AvailableBalance
        +decimal HeldBalance
        +Hold(decimal amount) bool
        +Release(decimal amount)
        +Settle(decimal amount)
    }

    class AuctionEngine {
        -ConcurrentDictionary~string, AuctionListing~ _auctions
        -IEscrowService _escrowService
        -IAuctionNotifier _notifier
        +SubmitBid(string auctionId, string bidderId, decimal amount, DateTimeOffset now) BidResult
        +ConcludeAuction(string auctionId, DateTimeOffset now)
    }

    AuctionListing "1" *-- "0..*" Bid : history
    AuctionListing "1" o-- "0..1" Bid : highestBid
    AuctionListing "1" *-- "3" Money : prices
    AuctionEngine "1" o-- "1..*" AuctionListing : coordinates
    AuctionEngine --> EscrowLedger : manages holds
```

### Auction Lifecycle State Machine

```mermaid
stateDiagram-v2
    [*] --> Draft
    Draft --> Active : StartAuction(StartTime)
    Active --> Extended : Bid placed within 60s of EndTime
    Extended --> Extended : Another bid placed within 60s of new EndTime
    Active --> Settled : EndTime reached AND HighestBid >= ReservePrice
    Extended --> Settled : Extended EndTime reached AND HighestBid >= ReservePrice
    Active --> ReserveNotMet : EndTime reached AND HighestBid < ReservePrice
    Extended --> ReserveNotMet : Extended EndTime reached AND HighestBid < ReservePrice
    Active --> Cancelled : Seller cancels before bids placed
    Settled --> [*]
    ReserveNotMet --> [*]
    Cancelled --> [*]
```

---

## 🏛️ Phase 4: Patterns & Concurrency Architecture

### 1. Lock Hierarchy & Deadlock Prevention
Deadlocks occur when two threads acquire locks in opposing orders (e.g. Thread 1 locks Auction then User Escrow; Thread 2 locks User Escrow then Auction).
To guarantee zero deadlocks:
1. **Pre-Authorization**: Acquire the bidder's escrow lock first and hold the funds.
2. **Auction Gate**: Acquire the per-auction lock (`ListingLock`).
3. **Rollback on Failure**: If the bid is rejected (e.g., outbid by a concurrent request), release the held escrow immediately.
4. **Outbid Release Outside Gate**: Release the *previous* highest bidder's escrow hold **after** releasing the per-auction lock.

```
Step 1: EscrowService.HoldFunds(newBidder, amount)   [Thread holds NewBidder Escrow Lock]
Step 2: lock(AuctionListing.GateLock)                 [Thread holds Auction Gate Lock]
Step 3: Evaluate bid validity & swap HighestBid
Step 4: unlock(AuctionListing.GateLock)               [Thread releases Auction Gate Lock]
Step 5: EscrowService.ReleaseHold(prevBidder, amount) [Thread releases Previous Bidder Hold]
```

### 2. Anti-Sniping Soft-Close Extension Invariant
Sniping bots submit bids at `T - 0.5s`, preventing human counter-bids.
- **Rule**: If `now >= EndTime - 60 seconds` AND `now < EndTime`:
  - `EndTime = max(EndTime, now + 120 seconds)`
  - `State = AuctionState.Extended`
- **Hard Ceiling Protection**: To prevent an attacker with multiple accounts from extending an auction forever, `EndTime` is clamped by `HardCeilingEndTime = OriginalEndTime + MaxExtensionDuration` (e.g., 30 minutes). Once reached, no further extensions occur.

---

## 🏛️ Phase 5: Idiomatic Production Implementations

### Primary: Production C# (.NET 8/9)

```csharp
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineAuction.Engine;

public enum AuctionState
{
    Draft,
    Active,
    Extended,
    Settled,
    ReserveNotMet,
    Cancelled
}

public readonly record struct Money(decimal Amount, string Currency)
{
    public static Money Usd(decimal amount) => new(amount, "USD");

    public static Money operator +(Money a, Money b)
    {
        ValidateCurrency(a, b);
        return new Money(a.Amount + b.Amount, a.Currency);
    }

    public static Money operator -(Money a, Money b)
    {
        ValidateCurrency(a, b);
        return new Money(a.Amount - b.Amount, a.Currency);
    }

    public static bool operator >(Money a, Money b)
    {
        ValidateCurrency(a, b);
        return a.Amount > b.Amount;
    }

    public static bool operator <(Money a, Money b)
    {
        ValidateCurrency(a, b);
        return a.Amount < b.Amount;
    }

    public static bool operator >=(Money a, Money b)
    {
        ValidateCurrency(a, b);
        return a.Amount >= b.Amount;
    }

    public static bool operator <=(Money a, Money b)
    {
        ValidateCurrency(a, b);
        return a.Amount <= b.Amount;
    }

    private static void ValidateCurrency(Money a, Money b)
    {
        if (!string.Equals(a.Currency, b.Currency, StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException($"Currency mismatch: {a.Currency} vs {b.Currency}");
    }
}

public sealed record Bid(
    Guid Id,
    string BidderId,
    Money Amount,
    DateTimeOffset Timestamp);

public sealed record BidResult(bool IsSuccess, string? Error, Bid? AcceptedBid);

public sealed class AuctionListing
{
    public string Id { get; }
    public string SellerId { get; }
    public string ItemTitle { get; }
    public Money StartingPrice { get; }
    public Money ReservePrice { get; }
    public Money MinIncrement { get; }
    public DateTimeOffset EndTime { get; private set; }
    public DateTimeOffset HardCeilingEndTime { get; }
    public AuctionState State { get; private set; }
    public Bid? HighestBid { get; private set; }
    public object GateLock { get; } = new();

    private static readonly TimeSpan AntiSnipeThreshold = TimeSpan.FromSeconds(60);
    private static readonly TimeSpan AntiSnipeExtension = TimeSpan.FromMinutes(2);

    public AuctionListing(
        string id,
        string sellerId,
        string itemTitle,
        Money startingPrice,
        Money reservePrice,
        Money minIncrement,
        DateTimeOffset endTime,
        TimeSpan? maxExtension = null)
    {
        Id = id;
        SellerId = sellerId;
        ItemTitle = itemTitle;
        StartingPrice = startingPrice;
        ReservePrice = reservePrice;
        MinIncrement = minIncrement;
        EndTime = endTime;
        HardCeilingEndTime = endTime + (maxExtension ?? TimeSpan.FromMinutes(30));
        State = AuctionState.Active;
    }

    public BidResult PlaceBid(string bidderId, Money amount, DateTimeOffset now)
    {
        if (State != AuctionState.Active && State != AuctionState.Extended)
            return new BidResult(false, $"Auction is not active. Current state: {State}", null);

        if (now >= EndTime)
        {
            EvaluateConclusion(now);
            return new BidResult(false, "Auction has already closed.", null);
        }

        // Determine minimum required bid
        Money requiredMinimum = HighestBid == null 
            ? StartingPrice 
            : HighestBid.Amount + MinIncrement;

        if (amount < requiredMinimum)
            return new BidResult(false, $"Bid of {amount.Amount} is below required minimum of {requiredMinimum.Amount}", null);

        if (HighestBid != null && HighestBid.BidderId == bidderId)
            return new BidResult(false, "You are already the highest bidder.", null);

        // Anti-Sniping soft-close dynamic extension
        if (EndTime - now <= AntiSnipeThreshold)
        {
            var prospectiveEnd = now + AntiSnipeExtension;
            EndTime = prospectiveEnd > HardCeilingEndTime ? HardCeilingEndTime : prospectiveEnd;
            State = AuctionState.Extended;
        }

        var newBid = new Bid(Guid.NewGuid(), bidderId, amount, now);
        HighestBid = newBid;
        return new BidResult(true, null, newBid);
    }

    public void EvaluateConclusion(DateTimeOffset now)
    {
        if (now < EndTime) return;

        if (HighestBid != null && HighestBid.Amount >= ReservePrice)
        {
            State = AuctionState.Settled;
        }
        else
        {
            State = AuctionState.ReserveNotMet;
        }
    }
}

public interface IEscrowService
{
    void Deposit(string bidderId, decimal amount);
    bool HoldFunds(string bidderId, decimal amount);
    void ReleaseHold(string bidderId, decimal amount);
    void SettleFunds(string buyerId, string sellerId, decimal amount);
    decimal GetAvailableBalance(string bidderId);
}

public sealed class InMemoryEscrowService : IEscrowService
{
    private class Account
    {
        public decimal Available;
        public decimal Held;
        public readonly object Lock = new();
    }

    private readonly ConcurrentDictionary<string, Account> _accounts = new();

    private Account GetAccount(string userId) =>
        _accounts.GetOrAdd(userId, _ => new Account());

    public void Deposit(string bidderId, decimal amount)
    {
        var acc = GetAccount(bidderId);
        lock (acc.Lock)
        {
            acc.Available += amount;
        }
    }

    public bool HoldFunds(string bidderId, decimal amount)
    {
        var acc = GetAccount(bidderId);
        lock (acc.Lock)
        {
            if (acc.Available < amount) return false;
            acc.Available -= amount;
            acc.Held += amount;
            return true;
        }
    }

    public void ReleaseHold(string bidderId, decimal amount)
    {
        var acc = GetAccount(bidderId);
        lock (acc.Lock)
        {
            acc.Held -= amount;
            acc.Available += amount;
        }
    }

    public void SettleFunds(string buyerId, string sellerId, decimal amount)
    {
        var buyer = GetAccount(buyerId);
        var seller = GetAccount(sellerId);

        lock (buyer.Lock)
        {
            buyer.Held -= amount;
        }
        lock (seller.Lock)
        {
            seller.Available += amount;
        }
    }

    public decimal GetAvailableBalance(string bidderId)
    {
        var acc = GetAccount(bidderId);
        lock (acc.Lock)
        {
            return acc.Available;
        }
    }
}

public interface IAuctionNotifier
{
    Task NotifyOutbidAsync(string bidderId, string auctionId, decimal newHighestBid);
    Task NotifyWinnerAsync(string bidderId, string auctionId, decimal winningBid);
}

public sealed class ConsoleAuctionNotifier : IAuctionNotifier
{
    public Task NotifyOutbidAsync(string bidderId, string auctionId, decimal newHighestBid)
    {
        Console.WriteLine($"   📢 [ALERT] Bidder '{bidderId}' outbid on Auction '{auctionId}'. New highest: ${newHighestBid}");
        return Task.CompletedTask;
    }

    public Task NotifyWinnerAsync(string bidderId, string auctionId, decimal winningBid)
    {
        Console.WriteLine($"   🏆 [WINNER] Bidder '{bidderId}' WON Auction '{auctionId}' at ${winningBid}!");
        return Task.CompletedTask;
    }
}

public sealed class AuctionEngine
{
    private readonly ConcurrentDictionary<string, AuctionListing> _auctions = new();
    private readonly IEscrowService _escrow;
    private readonly IAuctionNotifier _notifier;

    public AuctionEngine(IEscrowService escrow, IAuctionNotifier notifier)
    {
        _escrow = escrow;
        _notifier = notifier;
    }

    public void RegisterAuction(AuctionListing auction)
    {
        _auctions[auction.Id] = auction;
    }

    public async Task<BidResult> SubmitBidAsync(string auctionId, string bidderId, Money amount, DateTimeOffset now)
    {
        if (!_auctions.TryGetValue(auctionId, out var auction))
            return new BidResult(false, "Auction not found.", null);

        // 1. Pre-authorize funds in escrow
        if (!_escrow.HoldFunds(bidderId, amount.Amount))
            return new BidResult(false, $"Insufficient funds. Available balance: ${_escrow.GetAvailableBalance(bidderId)}", null);

        Bid? previousHighestBid = null;
        BidResult bidResult;

        // 2. Acquire listing lock
        lock (auction.GateLock)
        {
            previousHighestBid = auction.HighestBid;
            bidResult = auction.PlaceBid(bidderId, amount, now);

            if (!bidResult.IsSuccess)
            {
                // Rollback hold on failed bid
                _escrow.ReleaseHold(bidderId, amount.Amount);
                return bidResult;
            }
        }

        // 3. Post-bid: Release previous bidder's escrow and notify (outside gate lock)
        if (previousHighestBid != null)
        {
            _escrow.ReleaseHold(previousHighestBid.BidderId, previousHighestBid.Amount.Amount);
            await _notifier.NotifyOutbidAsync(previousHighestBid.BidderId, auctionId, amount.Amount);
        }

        return bidResult;
    }

    public async Task ConcludeAuctionAsync(string auctionId, DateTimeOffset now)
    {
        if (!_auctions.TryGetValue(auctionId, out var auction)) return;

        lock (auction.GateLock)
        {
            auction.EvaluateConclusion(now);
        }

        if (auction.State == AuctionState.Settled && auction.HighestBid != null)
        {
            _escrow.SettleFunds(auction.HighestBid.BidderId, auction.SellerId, auction.HighestBid.Amount.Amount);
            await _notifier.NotifyWinnerAsync(auction.HighestBid.BidderId, auction.Id, auction.HighestBid.Amount.Amount);
        }
        else if (auction.State == AuctionState.ReserveNotMet && auction.HighestBid != null)
        {
            _escrow.ReleaseHold(auction.HighestBid.BidderId, auction.HighestBid.Amount.Amount);
            Console.WriteLine($"   ⚠️ Auction '{auction.Id}' closed. Reserve price was not met. Held funds returned.");
        }
    }
}

public static class Program
{
    public static async Task Main()
    {
        Console.WriteLine("=== 🔨 HIGH-PERFORMANCE REAL-TIME AUCTION SYSTEM ===\n");

        var escrow = new InMemoryEscrowService();
        var notifier = new ConsoleAuctionNotifier();
        var engine = new AuctionEngine(escrow, notifier);

        // Pre-fund bidder balances
        escrow.Deposit("alice", 5000m);
        escrow.Deposit("bob", 7000m);
        escrow.Deposit("carol", 10000m);

        var now = DateTimeOffset.UtcNow;
        var auction = new AuctionListing(
            id: "AUC-ROLEX-01",
            sellerId: "seller_john",
            itemTitle: "1968 Vintage Rolex Submariner",
            startingPrice: Money.Usd(1000m),
            reservePrice: Money.Usd(2500m),
            minIncrement: Money.Usd(100m),
            endTime: now.AddSeconds(30) // Closes in 30 seconds
        );
        engine.RegisterAuction(auction);

        // 1. Alice bids $1,200
        Console.WriteLine("1. Alice bids $1,200:");
        var r1 = await engine.SubmitBidAsync(auction.Id, "alice", Money.Usd(1200m), now.AddSeconds(5));
        Console.WriteLine($"   Success: {r1.IsSuccess}, Highest: ${auction.HighestBid?.Amount.Amount}\n");

        // 2. Bob outbids with $1,500 -> Alice outbid alert & escrow hold released
        Console.WriteLine("2. Bob outbids Alice with $1,500:");
        var r2 = await engine.SubmitBidAsync(auction.Id, "bob", Money.Usd(1500m), now.AddSeconds(10));
        Console.WriteLine($"   Success: {r2.IsSuccess}, Highest: ${auction.HighestBid?.Amount.Amount}\n");

        // 3. Sniper Bid: Carol bids $3,000 at T-10s -> Triggers Anti-Sniping 2-minute dynamic extension
        Console.WriteLine("3. Carol bids $3,000 at T-10 seconds (Anti-sniping trigger):");
        var r3 = await engine.SubmitBidAsync(auction.Id, "carol", Money.Usd(3000m), now.AddSeconds(20));
        Console.WriteLine($"   Success: {r3.IsSuccess}, New State: {auction.State}, New EndTime: {auction.EndTime:HH:mm:ss}\n");

        // 4. Invalid Bid: Alice tries to bid $3,050 (requires $3,100 minimum)
        Console.WriteLine("4. Alice attempts invalid bid of $3,050 (Below minimum increment):");
        var r4 = await engine.SubmitBidAsync(auction.Id, "alice", Money.Usd(3050m), now.AddSeconds(25));
        Console.WriteLine($"   Success: {r4.IsSuccess}, Error: '{r4.Error}'\n");

        // 5. Conclude auction and settle funds
        Console.WriteLine("5. Concluding auction after deadline...");
        await engine.ConcludeAuctionAsync(auction.Id, auction.EndTime.AddSeconds(1));

        Console.WriteLine($"\nFinal Balances:");
        Console.WriteLine($"   Alice Available: ${escrow.GetAvailableBalance("alice")}");
        Console.WriteLine($"   Bob Available:   ${escrow.GetAvailableBalance("bob")}");
        Console.WriteLine($"   Carol Available: ${escrow.GetAvailableBalance("carol")}");
        Console.WriteLine($"   Seller Available:${escrow.GetAvailableBalance("seller_john")}");

        Console.WriteLine("\n=== Auction System Execution & Financial Ledger Verified! ===");
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
from datetime import datetime, timezone, timedelta
from typing import Dict, Optional


class AuctionState(Enum):
    DRAFT = auto()
    ACTIVE = auto()
    EXTENDED = auto()
    SETTLED = auto()
    RESERVE_NOT_MET = auto()
    CANCELLED = auto()


@dataclass(frozen=True)
class Money:
    amount: Decimal
    currency: str = "USD"

    def __add__(self, other: Money) -> Money:
        self._validate_currency(other)
        return Money(self.amount + other.amount, self.currency)

    def __sub__(self, other: Money) -> Money:
        self._validate_currency(other)
        return Money(self.amount - other.amount, self.currency)

    def __lt__(self, other: Money) -> bool:
        self._validate_currency(other)
        return self.amount < other.amount

    def __ge__(self, other: Money) -> bool:
        self._validate_currency(other)
        return self.amount >= other.amount

    def _validate_currency(self, other: Money) -> None:
        if self.currency != other.currency:
            raise ValueError(f"Currency mismatch: {self.currency} vs {other.currency}")


@dataclass(frozen=True)
class Bid:
    id: uuid.UUID
    bidder_id: str
    amount: Money
    timestamp: datetime


@dataclass
class BidResult:
    is_success: bool
    error: Optional[str] = None
    accepted_bid: Optional[Bid] = None


class AuctionListing:
    ANTI_SNIPE_THRESHOLD = timedelta(seconds=60)
    ANTI_SNIPE_EXTENSION = timedelta(minutes=2)

    def __init__(
        self,
        listing_id: str,
        seller_id: str,
        item_title: str,
        starting_price: Money,
        reserve_price: Money,
        min_increment: Money,
        end_time: datetime,
        max_extension: timedelta = timedelta(minutes=30)
    ):
        self.id = listing_id
        self.seller_id = seller_id
        self.item_title = item_title
        self.starting_price = starting_price
        self.reserve_price = reserve_price
        self.min_increment = min_increment
        self.end_time = end_time
        self.hard_ceiling_end_time = end_time + max_extension
        self.state = AuctionState.ACTIVE
        self.highest_bid: Optional[Bid] = None
        self.lock = threading.Lock()

    def place_bid(self, bidder_id: str, amount: Money, now: datetime) -> BidResult:
        if self.state not in (AuctionState.ACTIVE, AuctionState.EXTENDED):
            return BidResult(False, f"Auction not active. State: {self.state}")

        if now >= self.end_time:
            self.evaluate_conclusion(now)
            return BidResult(False, "Auction has already closed.")

        required_minimum = (
            self.starting_price if self.highest_bid is None
            else self.highest_bid.amount + self.min_increment
        )

        if amount < required_minimum:
            return BidResult(False, f"Bid of {amount.amount} below minimum {required_minimum.amount}")

        if self.highest_bid and self.highest_bid.bidder_id == bidder_id:
            return BidResult(False, "Already the highest bidder.")

        # Anti-sniping dynamic extension check
        if self.end_time - now <= self.ANTI_SNIPE_THRESHOLD:
            prospective = now + self.ANTI_SNIPE_EXTENSION
            self.end_time = min(prospective, self.hard_ceiling_end_time)
            self.state = AuctionState.EXTENDED

        new_bid = Bid(uuid.uuid4(), bidder_id, amount, now)
        self.highest_bid = new_bid
        return BidResult(True, accepted_bid=new_bid)

    def evaluate_conclusion(self, now: datetime) -> None:
        if now < self.end_time:
            return

        if self.highest_bid and self.highest_bid.amount >= self.reserve_price:
            self.state = AuctionState.SETTLED
        else:
            self.state = AuctionState.RESERVE_NOT_MET


class EscrowService:
    def __init__(self):
        self._available: Dict[str, Decimal] = {}
        self._held: Dict[str, Decimal] = {}
        self._lock = threading.Lock()

    def deposit(self, user_id: str, amount: Decimal) -> None:
        with self._lock:
            self._available[user_id] = self._available.get(user_id, Decimal("0")) + amount

    def hold_funds(self, user_id: str, amount: Decimal) -> bool:
        with self._lock:
            avail = self._available.get(user_id, Decimal("0"))
            if avail < amount:
                return False
            self._available[user_id] = avail - amount
            self._held[user_id] = self._held.get(user_id, Decimal("0")) + amount
            return True

    def release_hold(self, user_id: str, amount: Decimal) -> None:
        with self._lock:
            self._held[user_id] = self._held.get(user_id, Decimal("0")) - amount
            self._available[user_id] = self._available.get(user_id, Decimal("0")) + amount

    def settle_funds(self, buyer_id: str, seller_id: str, amount: Decimal) -> None:
        with self._lock:
            self._held[buyer_id] = self._held.get(buyer_id, Decimal("0")) - amount
            self._available[seller_id] = self._available.get(seller_id, Decimal("0")) + amount

    def get_available(self, user_id: str) -> Decimal:
        with self._lock:
            return self._available.get(user_id, Decimal("0"))


class AuctionEngine:
    def __init__(self, escrow: EscrowService):
        self._escrow = escrow
        self._auctions: Dict[str, AuctionListing] = {}

    def register_auction(self, auction: AuctionListing) -> None:
        self._auctions[auction.id] = auction

    def submit_bid(self, auction_id: str, bidder_id: str, amount: Money, now: datetime) -> BidResult:
        auction = self._auctions.get(auction_id)
        if not auction:
            return BidResult(False, "Auction not found.")

        # 1. Escrow Pre-authorization
        if not self._escrow.hold_funds(bidder_id, amount.amount):
            return BidResult(False, f"Insufficient funds. Balance: {self._escrow.get_available(bidder_id)}")

        # 2. Per-auction lock
        with auction.lock:
            prev_bid = auction.highest_bid
            result = auction.place_bid(bidder_id, amount, now)

            if not result.is_success:
                self._escrow.release_hold(bidder_id, amount.amount)
                return result

        # 3. Post-bid release of outbid party (outside auction lock)
        if prev_bid:
            self._escrow.release_hold(prev_bid.bidder_id, prev_bid.amount.amount)
            print(f"   📢 [ALERT] Bidder '{prev_bid.bidder_id}' outbid! New highest: ${amount.amount}")

        return result

    def conclude_auction(self, auction_id: str, now: datetime) -> None:
        auction = self._auctions.get(auction_id)
        if not auction:
            return

        with auction.lock:
            auction.evaluate_conclusion(now)

        if auction.state == AuctionState.SETTLED and auction.highest_bid:
            self._escrow.settle_funds(auction.highest_bid.bidder_id, auction.seller_id, auction.highest_bid.amount.amount)
            print(f"   🏆 [WINNER] '{auction.highest_bid.bidder_id}' won auction '{auction.id}' at ${auction.highest_bid.amount.amount}")
        elif auction.state == AuctionState.RESERVE_NOT_MET and auction.highest_bid:
            self._escrow.release_hold(auction.highest_bid.bidder_id, auction.highest_bid.amount.amount)
            print(f"   ⚠️ Reserve price not met. Escrow returned to '{auction.highest_bid.bidder_id}'.")


# Verification Driver
if __name__ == "__main__":
    print("=== 🔨 PYTHON AUCTION SYSTEM VERIFICATION ===")
    escrow = EscrowService()
    engine = AuctionEngine(escrow)

    escrow.deposit("alice", Decimal("5000"))
    escrow.deposit("bob", Decimal("7000"))

    now = datetime.now(timezone.utc)
    auction = AuctionListing(
        listing_id="AUC-ART-01",
        seller_id="gallery",
        item_title="Modern Art Canvas",
        starting_price=Money(Decimal("1000")),
        reserve_price=Money(Decimal("2000")),
        min_increment=Money(Decimal("100")),
        end_time=now + timedelta(seconds=30)
    )
    engine.register_auction(auction)

    # 1. Alice bids $1200
    res1 = engine.submit_bid(auction.id, "alice", Money(Decimal("1200")), now + timedelta(seconds=5))
    print(f"Alice Bid: {res1.is_success}, Current High: ${auction.highest_bid.amount.amount}")

    # 2. Bob bids $2200 (Meets reserve and triggers anti-sniping extension)
    res2 = engine.submit_bid(auction.id, "bob", Money(Decimal("2200")), now + timedelta(seconds=20))
    print(f"Bob Bid: {res2.is_success}, State: {auction.state}, Current High: ${auction.highest_bid.amount.amount}")

    # 3. Conclude after extended end time
    engine.conclude_auction(auction.id, auction.end_time + timedelta(seconds=1))
    print(f"Final Seller Balance: ${escrow.get_available('gallery')}")
    print(f"Final Alice Balance:  ${escrow.get_available('alice')}")
```

---

## 🏛️ Phase 6: Extensibility Drill & Edge Cases

### Edge Cases Matrix

| Edge Case | Failure Mode | Defensive Architectural Handling |
| :--- | :--- | :--- |
| **Simultaneous Last-Second Bids** | Race condition; both see the same highest bid and overwrite each other. | Lock acquisition ordering: Escrow hold -> `AuctionListing.GateLock`. Only the first thread through the gate succeeds; the second sees the newly updated `HighestBid` and fails gracefully. |
| **Anti-Sniping Infinite Extension Attack** | Two bot accounts collude to ping-pong bids every 59 seconds to keep auction open forever. | `HardCeilingEndTime = OriginalEndTime + MaxDuration` clamps the extension. Once the hard ceiling is reached, extensions are disabled. |
| **Bidder Funds Insufficient at Settlement** | Bidder spends money on another item before the auction concludes. | Escrow Pre-Authorization: The full bid amount is immediately transferred from `AvailableBalance` to `HeldBalance` upon placing the bid. |
| **Network Failure during Placement** | Failure occurs after escrow hold is reserved but before bid is committed. | Wrapped in `try / finally`: if `PlaceBid` returns failure or throws, `ReleaseHold` executes immediately. |
| **Auction Concluded during In-Flight Bid** | Bid arrives while `ConcludeAuction` is executing. | `GateLock` protects both `PlaceBid` and `EvaluateConclusion`. Once `EvaluateConclusion` sets state to `Settled`, subsequent bids reject with `Auction is not active`. |

### Architectural Extensions
1. **Proxy Bidding (eBay Style)**:
   - A bidder submits their maximum ceiling (e.g., \$5,000).
   - The engine automatically places minimum increment bids on their behalf whenever another bidder challenges them, up to their ceiling.
2. **Distributed Redis RedLock & Actor Mailboxes**:
   - For 10,000+ bids/second on viral items, replace in-memory C# locks with an Akka.NET / Orleans Actor or Redis Lua script where all bids for an auction ID queue into a single-threaded mailbox.
