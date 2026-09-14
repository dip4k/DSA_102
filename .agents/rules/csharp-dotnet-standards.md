# C# / .NET Standards for Senior DSA & System Design Practice

## Core Philosophy
- Write modern, production-ready C# (.NET 8/9).
- DSA in C# should be as performant as possible while remaining clean and readable for interviewers.
- Avoid unnecessary heap allocations where possible (`Span<T>`, `ReadOnlySpan<T>`, avoiding LINQ in hot loops).

## Language Idioms & Collections
1. **Priority Queue:**
   - Use `System.Collections.Generic.PriorityQueue<TElement, TPriority>` (.NET 6+).
   - Remember: `PriorityQueue` in .NET is a **Min-Heap by default** based on `TPriority`.
   - For Max-Heap, pass a custom comparer or negate numerical priorities:
     ```csharp
     var maxHeap = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b.CompareTo(a)));
     ```
2. **Associative Structures:**
   - Prefer `Dictionary<TKey, TValue>` and `HashSet<T>`.
   - Pre-allocate capacity when size is known: `new Dictionary<int, int>(capacity)`.
   - Use `TryGetValue` or `CollectionsMarshal.GetValueRefOrAddDefault` to avoid double lookups in hot loops.
3. **Double-Ended Queues & Stacks:**
   - Use `Stack<T>` and `Queue<T>` for standard LIFO/FIFO operations.
   - For monotonic deques (e.g., Sliding Window Maximum), use `LinkedList<T>` or a circular ring buffer array if `LinkedListNode` overhead is undesirable.
4. **Strings & Slicing:**
   - Avoid `string.Substring()` inside loops due to intermediate allocations. Use `ReadOnlySpan<char>` or index boundaries (`left`, `right`).
   - Use `StringBuilder` with initial capacity for string synthesis.
5. **Arithmetic & Overflow Safety:**
   - Always calculate midpoints safely: `int mid = left + (right - left) / 2;`
   - Cast to `long` before multiplication when calculating sums or products that may exceed `int.MaxValue`: `long product = (long)a * b;`
6. **Code Structure:**
   - Use clear guard clauses at method entry.
   - Use file-scoped namespaces and descriptive variable names.

