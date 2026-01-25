# 🎯 Week 03 – Guidelines & Learning Strategy (Sorting & Hashing Foundations)

## 🎓 Weekly Theme & Purpose

Week 03 focuses on **fundamental sorting algorithms and hash tables**. It transitions you from simple quadratic-time sorts to efficient `O(n log n)` algorithms and then into hash table design with real-world collision-handling strategies.

By the end of this week, you should not just “know” bubble or quick sort; you should **visualize how elements move**, how pivots split arrays, and how hash tables probe or chain when collisions happen.

---

## 🎯 Weekly Learning Objectives

By the end of Week 03, you will be able to:

- ✅ Explain and mentally trace **bubble, selection, and insertion sort**, including when quadratic sorts are acceptable.
- ✅ Describe the full mechanics of **merge sort** and **quick sort**, including partition schemes and recursion structure.
- ✅ Understand **binary heaps** and implement the idea of **heap sort** and **priority queues** conceptually.
- ✅ Design and reason about **hash tables with separate chaining** (Week 3 Day 4).
- ✅ Design and reason about **hash tables with open addressing and advanced hashing** (Week 3 Day 5).
- ✅ Compare sorting algorithms and hashing strategies based on **time complexity, space usage, stability, and real-world behavior**.
- ✅ Identify which sort/hash strategy fits a given **interview problem or production system constraint**.

---

## 📚 Key Concepts Overview (By Day)

### 🟦 Day 1 – Elementary Sorts (Bubble, Selection, Insertion)

- **Element swapping vs selection vs insertion**: understand *how* each algorithm moves elements.
- **Quadratic-time behavior**: `O(n²)` patterns, when small `n` or nearly sorted input makes them acceptable.
- **Stability and in-place properties**: why insertion sort is favored for tiny segments in hybrid algorithms.

### 🟦 Day 2 – Merge Sort & Quick Sort

- **Merge sort**: divide array, recursively sort halves, merge in linear time.
- **Quick sort**: partition around a pivot; understand Lomuto vs Hoare schemes and their trade-offs.
- **Complexity trade-offs**: merge sort’s guaranteed `O(n log n)` vs quick sort’s average `O(n log n)` but worst-case `O(n²)`.

### 🟦 Day 3 – Heap Sort & Priority Queues

- **Binary heap** representation in an array: parent/child index relationships.
- **Heap operations**: heapify, push, pop, replace.
- **Heap sort**: build heap, repeatedly extract-min or extract-max.
- **Priority queues**: scheduling, Dijkstra’s algorithm, event simulation.

### 🟦 Day 4 – Hash Tables I (Separate Chaining)

- **Hash function intuition**: mapping keys to bucket indices.
- **Buckets** with linked lists or small dynamic arrays.
- **Load factor and resizing**: controlling average-case `O(1)` behavior.
- **Average vs worst-case**: why separate chaining degrades gracefully under high load factors.

### 🟦 Day 5 – Hash Tables II (Open Addressing & Advanced Hashing)

- **Open addressing**: linear probing, quadratic probing, double hashing.
- **Clustering**: primary/secondary clustering and performance impact.
- **Advanced hashing**: Robin Hood hashing, Cuckoo hashing, universal hashing, perfect hashing at a conceptual level.
- **Design intuition**: when to choose separate chaining vs open addressing vs advanced schemes.

---

## 🧭 Learning Approach & Methodology

Use a **mental-model-first loop** each day:

1. **Concept pass (30–40%)**
   - Read the instructional file once.
   - Focus on analogies (e.g., sorting as rearranging books, hashing as hotel rooms) and diagrams (array traces, trees, probe sequences).
   - Write *one paragraph* in your own words summarizing the core mental model.

2. **Visualization & tracing (30–40%)**
   - For each algorithm:
     - Trace at least **2–3 examples** on paper (simple, medium, edge case).
     - Draw arrays/trees at each step (no code).
   - For hashing:
     - Manually simulate **insertion, lookup, deletion** with collisions.
     - Draw how separate-chaining buckets grow and how open addressing probes.

3. **Problem exposure (20–30%)**
   - Pick **2–4 problems** per day:
     - At least one “straight” application (e.g., sort array, implement hash map).
     - At least one that **forces a design choice** (e.g., which sort or hash scheme to use).

4. **Reflection (10–15%)**
   - Write down:
     - Which algorithm you would pick for each sample scenario and *why*.
     - One insight about **time vs space vs implementation complexity** for that day.

---

## 🧱 Common Mistakes & Pitfalls (Week 03)

- ❌ **Memorizing sorting names without mechanics**  
  - Fix: Always draw 1–2 full passes of each sort. You should be able to narrate “what happens next” at each step.

- ❌ **Ignoring stability and space usage**  
  - Fix: For each sort, explicitly note: *stable or not? in-place or not? extra memory?*

- ❌ **Hand-waving over partitioning in quick sort**  
  - Fix: Commit to one scheme (Lomuto or Hoare) and be able to trace it step-by-step.

- ❌ **Thinking hash tables are “just O(1)”**  
  - Fix: Record conditions: load factor bounds, good hash function, and collision strategy (chaining vs open addressing).

- ❌ **Not relating heaps and priority queues to real systems**  
  - Fix: Tie heap use-cases to scheduling, Dijkstra’s algorithm, event simulation, and real-world runtimes.

- ❌ **Treating open addressing and advanced hashing as purely theoretical**  
  - Fix: Connect them to Python dict, C# Dictionary, Rust HashMap, and routing tables.

---

## 🕒 Time & Practice Strategy (Per Day)

Assuming ~2–3 hours per day:

- ⏱ **30–45 min** — Read the day’s instructional file (concept pass).  
- ⏱ **40–60 min** — Draw diagrams and run at least 2–3 manual traces.  
- ⏱ **40–60 min** — Solve or at least *set up* 2–4 problems:
  - Identify the algorithm/structure and justify your choice before coding.
- ⏱ **10–20 min** — Reflection:
  - Write what surprised you, what was hard, and what clicked.

**Weekly review (end of Day 5):**

- Revisit:
  - Sorting comparisons (quadratic vs `O(n log n)` vs heaps).
  - Hashing strategies (separate chaining vs open addressing vs advanced hashing).
- Draw a **concept map** linking all Week 03 algorithms.

---

## 📋 Weekly Checklist

Use this to confirm you are “Week 03 complete”:

- [ ] Can narrate bubble, selection, insertion sort mechanics without looking.  
- [ ] Can explain merge sort and quick sort, including recursion and partitioning.  
- [ ] Can draw a binary heap and trace heapify and extract operations.  
- [ ] Understand hash functions, load factor, and resizing for separate chaining.  
- [ ] Understand open addressing (linear, quadratic, double hashing) and the idea of clustering.  
- [ ] Know at least **3 real-world systems** that rely on sorting and **3 systems** that rely on hashing.  
- [ ] Have solved at least **8–12 problems** this week touching sorting and hashing.  
- [ ] Have written **at least one page of personal notes** summarizing Week 03 mental models and trade-offs.