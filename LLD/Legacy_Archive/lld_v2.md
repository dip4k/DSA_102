# 📘 TRACK 1: Conceptual Core & Low-Level Design (LLD)

**Target:** The "Machine Coding" Round and the "Technical Depth" probes in behavioral interviews.
**Philosophy:** Production-grade code quality, thread safety, and rigorous object modeling.
**MIT Alignment:** *6.031 (Software Construction)* & *6.172 (Performance Engineering)*.

### 🔷 Phase A: Runtime Mastery & Concurrency (Weeks 1–2)

*Before designing classes, you must understand the machine they live on.*

#### 🧱 Week 1: Memory Models & Runtime Internals

**Goal:** deeply understand how your code utilizes RAM and the CPU.

* **Day 1: Stack vs. Heap & Value vs. Reference Types**
* Stack frame mechanics (review).
* Escape analysis (when does an object "escape" the stack?).
* Boxing/Unboxing costs.


* **Day 2: Garbage Collection (GC) Deep Dive**
* Mark-and-Sweep vs. Reference Counting.
* Generational GC (Gen 0, 1, 2) – why short-lived objects are cheap.
* Stop-the-world events and GC tuning.


* **Day 3: The Cost of Abstraction**
* Virtual tables (v-tables) and dynamic dispatch overhead.
* Cache locality: `List<Class>` vs `List<Struct>` (Pointer chasing vs contiguous).


* **Day 4: Exception Safety & Resource Management**
* RAII (Resource Acquisition Is Initialization).
* `try-catch-finally` internals.
* `IDisposable` / `AutoCloseable` patterns.



#### 🧱 Week 2: Advanced Concurrency & Thread Safety (Critical for Seniors)

**Goal:** Write thread-safe code without deadlocks.

* **Day 1: Threading Primitives**
* User-level threads vs. Kernel threads.
* Context switching costs.
* The `volatile` keyword and memory visibility.


* **Day 2: Synchronization Mechanisms**
* Mutex/Monitor vs. Semaphores.
* Reader-Writer Locks (when to use?).
* **Critical:** Deadlock conditions (Coffman conditions) & prevention.


* **Day 3: Lock-Free Programming & Atomics**
* Compare-and-Swap (CAS) operations.
* Atomic Integers/References.
* **MIT 6.172 Angle:** False sharing (cache line contention).


* **Day 4: Asynchronous Programming Models**
* Event Loops (Node.js style) vs. Thread Pools (C#/.NET style).
* Promises / Futures / Tasks / Async-Await mechanics.
* Producer-Consumer problem implementation.



### 🔷 Phase B: Object-Oriented Design & Patterns (Weeks 3–4)

*Structuring code for maintainability and scale.*

#### 📐 Week 3: SOLID & Architectural Principles

**Goal:** Internalize the rules of clean architecture.

* **Day 1: S.O.L.I.D Deep Dive**
* **S:** Single Responsibility (Cohesion).
* **O:** Open/Closed (Extension without modification).
* **L:** Liskov Substitution (Inheritance done right).
* **I:** Interface Segregation (Lean interfaces).
* **D:** Dependency Inversion (Decoupling modules).


* **Day 2: Composition vs. Inheritance**
* The "Diamond Problem."
* Mixins vs. Abstract Classes vs. Interfaces.
* Why "Favors Composition" is the modern standard.


* **Day 3: Code Smells & Refactoring**
* Long Method, God Class, Feature Envy.
* Refactoring techniques (Extract Method, Pull Up Member).



#### 📐 Week 4: The Design Patterns (Gang of Four)

**Goal:** Know *when* to use them, not just *how*.

* **Day 1: Creational Patterns**
* **Singleton:** (Double-checked locking implementation).
* **Factory vs. Abstract Factory:** Decoupling creation logic.
* **Builder:** Managing complex object construction.


* **Day 2: Structural Patterns**
* **Adapter:** Bridging incompatible interfaces.
* **Decorator:** Adding behavior dynamically (Wrapper pattern).
* **Proxy:** Lazy loading / Access control.


* **Day 3: Behavioral Patterns**
* **Observer:** Event handling systems.
* **Strategy:** Swapping algorithms at runtime (e.g., PaymentProcessors).
* **Command:** Encapsulating requests (Undo/Redo systems).



### 🔷 Phase C: Applied LLD / Machine Coding (Weeks 5–6)

*Simulating the "Pair Programming" interview.*

#### 🛠️ Week 5: Practical Object Modeling

**Goal:** Translate requirements into Class Diagrams and Code in 45 mins.

* **Day 1: Modeling Real World Systems**
* **Problem:** Design a Parking Lot.
* **Focus:** Extensibility (Different vehicle types, different pricing strategies).


* **Day 2: State Machines**
* **Problem:** Design a Vending Machine or Elevator System.
* **Focus:** State Pattern vs. If-Else spaghetti.


* **Day 3: Management Systems**
* **Problem:** Library Management or Movie Ticket Booking.
* **Focus:** Concurrency (Locking a seat/book).



#### 🛠️ Week 6: API Design & Extensibility

* **Day 1: Designing Libraries**
* **Problem:** Design a Logger Library (Levels, Appenders, Async writing).
* **Problem:** Design a Custom HashMap (Handling collisions, resizing).


* **Day 2: API Best Practices**
* Fluent Interfaces.
* Immutable objects (Value Objects).
* Error handling hierarchies.



---