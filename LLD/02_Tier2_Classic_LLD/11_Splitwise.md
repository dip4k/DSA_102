# 11. Splitwise (Strategy + Domain Modeling + Min-Cash-Flow Graph Algorithm)

## 📌 Context
Splitwise is one of the most frequently asked LLD and Machine Coding interview questions (**Uber, Flipkart, Swiggy, Amazon**). It tests:
1. **Monetary Precision:** Using `decimal` to avoid floating-point errors.
2. **The Strategy Pattern:** Supporting multiple expense split types (`Equal`, `Exact`, `Percent`).
3. **Graph Simplification (Min-Cash-Flow Algorithm):** Reducing a complex web of group debts into the absolute minimum number of settlement transactions.

---

## 1. Domain Modeling: Value Objects & Strategy Pattern

```csharp
public enum SplitType { Equal, Exact, Percentage }

// Value Object for monetary precision
public record Money(decimal Amount, string Currency = "USD")
{
    public static Money Zero(string currency = "USD") => new(0, currency);
}

public abstract class Split
{
    public string UserId { get; }
    public decimal Amount { get; set; }

    protected Split(string userId, decimal amount = 0)
    {
        UserId = userId;
        Amount = amount;
    }
}

public class ExactSplit : Split 
{ 
    public ExactSplit(string userId, decimal amount) : base(userId, amount) { } 
}

public class EqualSplit : Split 
{ 
    public EqualSplit(string userId) : base(userId) { } 
}

public class PercentageSplit : Split 
{ 
    public decimal Percent { get; }
    public PercentageSplit(string userId, decimal percent) : base(userId) 
    { 
        Percent = percent; 
    }
}

// Strategy Pattern for Split Calculations
public interface ISplitStrategy
{
    void CalculateAmounts(decimal totalAmount, List<Split> splits);
}

public class EqualSplitStrategy : ISplitStrategy
{
    public void CalculateAmounts(decimal totalAmount, List<Split> splits)
    {
        if (splits.Count == 0) return;

        var splitAmount = Math.Round(totalAmount / splits.Count, 2);
        splits.ForEach(s => s.Amount = splitAmount);

        // Handle remainders (e.g. 100 / 3 = 33.33 each, leftover 0.01 goes to first user)
        var sum = splitAmount * splits.Count;
        splits[0].Amount += (totalAmount - sum);
    }
}

public class PercentageSplitStrategy : ISplitStrategy
{
    public void CalculateAmounts(decimal totalAmount, List<Split> splits)
    {
        var percentageSplits = splits.OfType<PercentageSplit>().ToList();
        if (percentageSplits.Sum(s => s.Percent) != 100m)
            throw new InvalidOperationException("Total percentage split must equal 100%.");

        foreach (var s in percentageSplits)
        {
            s.Amount = Math.Round((totalAmount * s.Percent) / 100m, 2);
        }
    }
}
```

---

## 2. The Debt Ledger & Min-Cash-Flow Simplification Algorithm

When User A owes User B 10 USD, and User B owes User C 10 USD, Splitwise simplifies this so User A pays User C 10 USD directly (eliminating User B as an intermediary).

```csharp
public record SettlementTransaction(string FromUser, string ToUser, decimal Amount);

public class ExpenseManager
{
    // Net balance per user: Positive = Owed money (Creditor), Negative = Owes money (Debtor)
    private readonly Dictionary<string, decimal> _netBalances = new();

    public void AddExpense(string paidBy, decimal totalAmount, List<Split> splits, ISplitStrategy strategy)
    {
        strategy.CalculateAmounts(totalAmount, splits);

        // Credit the payer
        EnsureUser(paidBy);
        _netBalances[paidBy] += totalAmount;

        // Debit the participants
        foreach (var split in splits)
        {
            EnsureUser(split.UserId);
            _netBalances[split.UserId] -= split.Amount;
        }
    }

    private void EnsureUser(string userId)
    {
        if (!_netBalances.ContainsKey(userId))
            _netBalances[userId] = 0;
    }

    /// <summary>
    /// Min-Cash-Flow Greedy Algorithm:
    /// Repeatedly matches the largest net debtor with the largest net creditor.
    /// Reduces an N-node web of transactions down to at most (N - 1) transactions.
    /// </summary>
    public List<SettlementTransaction> SimplifyDebts()
    {
        var debtors = new PriorityQueue<string, decimal>();  // Min-Heap (most negative balance)
        var creditors = new PriorityQueue<string, decimal>(); // Min-Heap inverted with negative key (largest positive balance)

        foreach (var (user, balance) in _netBalances)
        {
            if (balance < -0.01m)
            {
                // Owes money (priority by amount owed ascending)
                debtors.Enqueue(user, balance);
            }
            else if (balance > 0.01m)
            {
                // Owed money (invert sign to get Max-Heap behavior)
                creditors.Enqueue(user, -balance);
            }
        }

        var settlements = new List<SettlementTransaction>();
        var balanceMap = new Dictionary<string, decimal>(_netBalances);

        while (debtors.Count > 0 && creditors.Count > 0)
        {
            var debtor = debtors.Dequeue();
            var creditor = creditors.Dequeue();

            var debtAmount = -balanceMap[debtor];
            var creditAmount = balanceMap[creditor];

            var settledAmount = Math.Min(debtAmount, creditAmount);
            settlements.Add(new SettlementTransaction(debtor, creditor, settledAmount));

            balanceMap[debtor] += settledAmount;
            balanceMap[creditor] -= settledAmount;

            // If debtor still owes, re-queue
            if (balanceMap[debtor] < -0.01m)
            {
                debtors.Enqueue(debtor, balanceMap[debtor]);
            }

            // If creditor is still owed, re-queue
            if (balanceMap[creditor] > 0.01m)
            {
                creditors.Enqueue(creditor, -balanceMap[creditor]);
            }
        }

        return settlements;
    }
}
```

---

## 3. End-to-End Driver Code

```csharp
var manager = new ExpenseManager();

// Alice pays 100 for Alice, Bob, Charlie (Equal split)
manager.AddExpense("Alice", 100m, new List<Split> 
{ 
    new EqualSplit("Alice"), 
    new EqualSplit("Bob"), 
    new EqualSplit("Charlie") 
}, new EqualSplitStrategy());

// Bob pays 60 for Bob and Charlie
manager.AddExpense("Bob", 60m, new List<Split> 
{ 
    new EqualSplit("Bob"), 
    new EqualSplit("Charlie") 
}, new EqualSplitStrategy());

var settlements = manager.SimplifyDebts();

foreach (var s in settlements)
{
    Console.WriteLine($"{s.FromUser} owes {s.ToUser}: ${s.Amount:F2}");
}
// Outputs simplified direct payments!
```

---

## 🗣️ Interviewer Discussion & Tradeoffs

**Interviewer:** *"Why did you use `decimal` instead of `double` or `float` for the amounts?"*
**You:** "Because `double` and `float` use base-2 floating-point math, which cannot accurately represent base-10 decimals like `0.1`. In financial applications, binary floating point generates rounding anomalies like `0.30000000000000004`, causing balance mismatches and accounting errors. `decimal` provides 128-bit high-precision base-10 arithmetic, making it the industry standard for financial systems."

**Interviewer:** *"How does your 'Simplify Debts' algorithm compare to an optimal NP-hard solution?"*
**You:** "Finding the absolute mathematical minimum number of transactions across arbitrary subsets is equivalent to the **Subset-Sum Problem** (NP-Hard). However, the **Greedy Min-Cash-Flow algorithm** (matching maximum debtor with maximum creditor) runs in O(N log N) time and produces at most N - 1 transactions, which is practically optimal and widely adopted in real-world systems like Splitwise."

**Interviewer:** *"How do you handle concurrent expense additions for a large group trip in production?"*
**You:** "If multiple users add expenses simultaneously, mutating in-memory balances directly causes race conditions. In production:
1. We persist expenses as an immutable append-only **Event Sourcing Log** in PostgreSQL/SQL Server.
2. We compute user balances asynchronously using **CQRS Read Projections**.
3. Group operations are serialized per `GroupId` using an optimistic concurrency check (`RowVersion`)."