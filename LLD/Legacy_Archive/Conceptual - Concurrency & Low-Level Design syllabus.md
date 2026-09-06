# 📘 TRACK 1: Conceptual, Concurrency & Low-Level Design (LLD)

**Target:** The "Machine Coding" or "Object-Oriented Design" Round.
**Philosophy:** "Code that works is not enough. It must be thread-safe, extensible, and testable."
**MIT Alignment:** 6.031 (Software Construction) + 6.172 (Performance Engineering).

### 🔷 Module A: The Runtime & The Machine (Weeks 1–2)

*Goal: Understand exactly what your code costs the hardware. Essential for justifying design choices.*

**Week 1: Memory Models & Garbage Collection**

* **The Heap vs. Stack (Advanced):** Escape analysis, value types vs. reference types, and stack allocation optimizations.
* **Garbage Collection Mechanics:**
* Mark-and-Sweep, Generational GC (Eden/Survivor/Tenured).
* Stop-the-world pauses vs. Concurrent marking.
* *Senior Q:* "How does your choice of `List` vs `LinkedList` impact GC pressure?"


* **Memory Consistency Model:** Visibility, Reordering, and Happens-Before relationships (volatile/atomic).

**Week 2: I/O Models & Runtime Internals**

* **Blocking vs. Non-Blocking I/O:** Synchronous, Asynchronous, Multiplexing (epoll/kqueue/IOCP).
* **The Cost of Context Switching:** User-space vs. Kernel-space threads (Green threads vs. OS threads).
* **Language Internals (C#/.NET/Java specific):**
* How `async/await` is implemented (State machines).
* JIT Compilation & Warm-up.



### 🔷 Module B: Concurrency & Multi-Threading (Weeks 3–4)

*Goal: The #1 filter for Senior candidates. Writing correct threaded code.*

**Week 3: Concurrency Primitives & Safety**

* **Locks & Synchronization:** Mutex, Semaphores, Monitors, Reader-Writer Locks.
* **Deadlock & Livelock:** The 4 conditions for deadlock; detection and prevention algorithms (Banker’s Algo).
* **Lock-Free Programming (Intro):** Compare-And-Swap (CAS), Atomic Integers, `ConcurrentHashMap` internals (striped locking vs. CAS).
* **Thread Pools:** Sizing strategies (CPU-bound vs. IO-bound), Work stealing queues.

**Week 4: Asynchronous Patterns & Producers/Consumers**

* **Producer-Consumer Pattern:** Bounded Blocking Queues implementation (using Condition Variables).
* **Futures & Promises:** Chaining async tasks, error handling in async pipelines.
* **Rate Limiting Algorithms (Local):** Implementing Token Bucket / Leaky Bucket in a multi-threaded class.
* **Thundering Herd Problem:** Mitigation strategies at the code level.

### 🔷 Module C: Applied OOD & Design Patterns (Weeks 5–6)

*Goal: moving beyond "What is a Singleton?" to "When does Singleton destroy testability?"*

**Week 5: SOLID & Gang of Four (The Useful Ones)**

* **SOLID in Production:**
* *Liskov Substitution:* Why inheritance often fails. Composition over Inheritance.
* *Dependency Inversion:* Dependency Injection containers and wiring.


* **Structural Patterns:** Adapter (wrapping legacy libs), Decorator (middleware streams), Composite (UI/File systems).
* **Behavioral Patterns:** Strategy (payment methods), Observer (event systems), Command (undo/redo).
* **Creational Patterns:** Factory (dependency hiding), Builder (immutable config objects).

**Week 6: Code Quality & Refactoring**

* **Design for Testability:** Mocking strategies, pure functions, dependency breaking.
* **Refactoring:** Identifying "Code Smells" (God classes, Feature envy) and applying specific refactoring moves.
* **API Design:** Idempotency, Fluent Interfaces, defensive copying.

### 🔷 Module D: Low-Level Design (LLD) / Machine Coding (Weeks 7–8)

*Goal: 45-minute "Design a Class" interviews.*

**Week 7: Modeling Real World Systems**

* **Problem Types:** Design a Parking Lot, Movie Ticket Booking, Elevator System.
* **The 4-Step Framework:**
1. **Clarify:** Scope and Constraints (Multi-threaded? In-memory?).
2. **Core Objects:** Identifying Entities and Value Objects.
3. **Relationships:** UML Class Diagrams (Inheritance vs. Association).
4. **API/Methods:** Defining the contract.



**Week 8: Modeling Technical Systems**

* **Problem Types:** Design an In-Memory Cache (LRU/LFU), Task Scheduler, Logging Framework.
* **Focus:** Thread safety becomes the primary constraint here.
* **Drill:** Implement a Thread-Safe LRU Cache with O(1) operations using `HashMap` + `DoublyLinkedList` + `ReadWriteLock`.

---