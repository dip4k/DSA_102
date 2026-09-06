# 17. Invalid Transactions (Validation + Modeling)

## 📌 Context
This problem bridges algorithmic strings/hashmaps with **Domain Validation**. It tests if you can parse raw data into an Object Model and enforce invariants across sliding time windows.

**Rules for invalidity:**
1. Transaction amount > $1000
2. Same user has another transaction in a different city within 60 minutes.

---

## 1. The Object Model

```csharp
public class Transaction
{
    public string Id { get; } // We use original string as ID here for simplicity
    public string Name { get; }
    public int Time { get; }
    public int Amount { get; }
    public string City { get; }
    public string RawCsv { get; }

    public Transaction(string csv)
    {
        RawCsv = csv;
        var parts = csv.Split(',');
        Name = parts[0];
        Time = int.Parse(parts[1]);
        Amount = int.Parse(parts[2]);
        City = parts[3];
    }
}
```

---

## 2. The Logic

```csharp
public class TransactionValidator
{
    public IList<string> InvalidTransactions(string[] transactions)
    {
        var parsedTxns = transactions.Select(t => new Transaction(t)).ToList();
        var invalidSet = new HashSet<string>();

        // Group by Name to reduce the search space
        var groupedByName = parsedTxns.GroupBy(t => t.Name).ToDictionary(g => g.Key, g => g.ToList());

        foreach (var txn in parsedTxns)
        {
            // Rule 1: Amount > 1000
            if (txn.Amount > 1000)
            {
                invalidSet.Add(txn.RawCsv);
            }

            // Rule 2: Different city within 60 minutes
            var sameUserTxns = groupedByName[txn.Name];
            foreach (var otherTxn in sameUserTxns)
            {
                if (txn.City != otherTxn.City && Math.Abs(txn.Time - otherTxn.Time) <= 60)
                {
                    invalidSet.Add(txn.RawCsv);
                    invalidSet.Add(otherTxn.RawCsv); // Both are considered invalid
                }
            }
        }

        return invalidSet.ToList();
    }
}
```

---

## 🗣️ Interviewer Discussion & Tradeoffs

**Interviewer:** *"Your solution has a nested loop `foreach (var otherTxn in sameUserTxns)`. Isn't that O(N²)? Can we make it faster?"*
**You:** "Yes, in the worst case (where every transaction is made by the exact same user), it degenerates to **O(N²)**. To optimize this, we could sort the user's transactions by time. Then, instead of scanning every transaction, we use a **Sliding Window** or binary search to only look at transactions that fall strictly within the `[Time - 60, Time + 60]` boundary."

**Interviewer:** *"If we were building this as a real-time Fraud Detection system, how would the architecture change?"*
**You:** "In real-time fraud detection, we don't have a static array. Transactions stream in via Kafka or Azure Service Bus. I would use a **Stream Processing Engine** (like Azure Stream Analytics or Apache Flink). We would define a **Tumbling Window** or **Hopping Window** of 60 minutes, partition the stream by `UserName`, and alert immediately if a different city appears in that window."