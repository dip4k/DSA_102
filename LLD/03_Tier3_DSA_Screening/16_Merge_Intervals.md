# 16. Merge Intervals (DSA + Booking)

## 📌 Context
Merge Intervals is the algorithmic heart of the **Meeting Room Booking** and **Calendar** LLD problems. You must know how to sort and greedy-merge time blocks.

---

## 1. The Algorithm

```csharp
public class IntervalMerger
{
    public int[][] Merge(int[][] intervals) 
    {
        if (intervals.Length <= 1) return intervals;

        // 1. Sort intervals based on the starting time
        Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));

        var merged = new List<int[]>();
        var currentInterval = intervals[0];
        merged.Add(currentInterval);

        foreach (var interval in intervals)
        {
            int currentEnd = currentInterval[1];
            int nextStart = interval[0];
            int nextEnd = interval[1];

            if (currentEnd >= nextStart) 
            {
                // Overlapping intervals, merge them
                currentInterval[1] = Math.Max(currentEnd, nextEnd);
            } 
            else 
            {
                // Disjoint interval, add it to the list and move on
                currentInterval = interval;
                merged.Add(currentInterval);
            }
        }

        return merged.ToArray();
    }
}
```

---

## 🗣️ Interviewer Discussion & Tradeoffs

**Interviewer:** *"What is the time complexity of this solution?"*
**You:** "The sorting step dictates the time complexity, which is **O(N log N)**. The subsequent linear scan to merge the intervals takes **O(N)**. So the overall time complexity is **O(N log N)**. The space complexity is **O(N)** to store the merged output."

**Interviewer:** *"How does this map to the Meeting Room Booking system we designed earlier?"*
**You:** "In the real world, instead of merging integer arrays, we are merging `DateTimeOffset` ranges. If a user wants to find 'free slots' in a calendar, we query all existing bookings, run this exact `Merge` algorithm to combine overlapping meetings, and then invert the result to find the gaps (the free slots)."

**Interviewer:** *"What if the intervals are streaming in one by one over time, and we need to query if a new interval overlaps instantly?"*
**You:** "If the data is streaming, sorting the whole array every time becomes an O(N log N) bottleneck. Instead, I would use an **Interval Tree** or a **Segment Tree**. These data structures allow us to insert a new interval and check for overlaps in **O(log N)** time."