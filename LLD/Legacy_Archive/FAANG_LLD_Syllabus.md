# 🔥 FAANG-Level Low-Level Design (LLD) Mastery Syllabus

**Target:** Senior/Staff-level Machine Coding, OOD, and Technical Depth Rounds  
**Philosophy:** Production-grade code quality, thread safety, extensibility, and testability  
**Duration:** 6-8 weeks (intensive study, 30-40 hours/week)  
**MIT Alignment:** 6.031 (Software Construction) + 6.172 (Performance Engineering)

---

## 📋 Quick Overview

This syllabus is designed to bridge the gap between "code that works" and "code that scales safely in production." FAANG interviews focus on your ability to:

1. **Design for Extensibility:** Can this be easily modified for new requirements?
2. **Ensure Thread Safety:** Will this break under concurrent load?
3. **Optimize Resource Usage:** How does this impact memory, CPU, and GC?
4. **Apply Design Principles:** Do you understand *why* SOLID matters, not just *what* it is?
5. **Make Trade-offs:** When do you favor inheritance vs composition, interfaces vs abstract classes?

---

## PHASE A: Runtime Mastery & Concurrency Fundamentals (Weeks 1–2)

### Goal
Understand the machine your code runs on. FAANG interviewer question: *"How does your design impact GC pressure or cache locality?"*

### Week 1: Memory Models, Runtime, and Garbage Collection

#### Day 1-2: Memory Internals & Escape Analysis
**Topics:**
- Stack frame mechanics: allocation, unwinding, function calls
- Heap organization: segregation by generation (young/old space)
- **Escape Analysis:** When objects can stay on stack vs forced to heap
- Value Types vs Reference Types: memory layout and allocation costs
- Boxing/Unboxing: hidden allocations and performance pitfalls

**Key Insight for Interview:**
> *"If I use a `List<int>` versus `List<object>`, the second will box values, creating heap pressure and GC work. For a hot loop processing millions of items, this matters."*

**Exercises:**
- Analyze code snippets and predict GC impact
- Estimate heap pressure for different data structure choices
- Justify when you'd use struct vs class in a design

---

#### Day 3-4: Garbage Collection Deep Dive
**Topics:**
- Mark-and-Sweep vs Generational vs Concurrent vs G1 collectors
- Why generational GC works: most objects die young
- Stop-the-world pauses and their impact on latency
- GC tuning: heap sizing, pause time budgets
- Memory leaks: circular references, event handler leaks, resource leaks

**Interview Focus:**
> "If we're designing a 'fire-and-forget' event system, how do we prevent listeners from leaking memory?"

**Exercises:**
- Design a thread-safe event dispatcher that doesn't leak listeners
- Analyze a design for potential GC issues
- Write code that minimizes generational collection pressure

---

#### Day 5: The Cost of Abstraction
**Topics:**
- Virtual method dispatch (v-tables) overhead in OOP
- Cache locality: contiguous memory layouts vs pointer chasing
- Implications of inheritance depth on performance
- Profile-guided optimization: what modern JITs can inline

**Critical Exercise:**
Design an in-memory data structure for a game engine that minimizes cache misses. Consider:
- Should you use an object-oriented hierarchy or a data-oriented layout?
- What are the GC implications?

---

### Week 2: Advanced Concurrency & Thread Safety

#### Day 1-2: Threading Primitives & Visibility
**Topics:**
- User-space threads vs OS kernel threads
- Context switching costs: why thread pools exist
- Memory visibility: the `volatile` keyword and its guarantees
- Happens-before relationships in concurrency
- The Java Memory Model (or equivalent in your language)

**Interview Question:**
> "Can two threads see different values of an integer if it's not marked volatile? Under what conditions?"

**Exercise:**
Write code that demonstrates a visibility issue (if using Java/C#). Explain why it happens.

---

#### Day 3-4: Synchronization & Deadlock Prevention
**Topics:**
- Mutex vs Monitor vs Semaphores vs Condition Variables
- Reader-Writer Locks: when to use them
- **Coffman Conditions for Deadlock:** Mutual exclusion, hold and wait, no preemption, circular wait
- Deadlock prevention strategies (lock ordering, timeouts, banker's algorithm concept)
- Livelock and how to detect/prevent it

**Critical Exercise:**
Design a **Thread-Safe Bounded Blocking Queue** from scratch:
- Implement `put()` and `take()` with waiting
- Ensure no deadlock
- Use monitors/condition variables
- Handle spurious wakeups
- Discuss why `notify()` vs `notifyAll()`

---

#### Day 5: Lock-Free Programming & Atomics
**Topics:**
- Compare-and-Swap (CAS) operations: atomic primitives
- Atomic integers, references, and their use cases
- How `ConcurrentHashMap` uses segment-level locking (or CAS in newer versions)
- **False Sharing:** cache line contention when threads modify adjacent memory
- When lock-free is worth it (and when it's not)

**Exercise:**
Implement a lock-free counter class and explain its trade-offs vs a synchronized counter.

---

## PHASE B: Asynchronous & Event-Driven Patterns (Week 3)

### Goal
Modern systems are asynchronous. Master async patterns: futures, promises, event loops, and producer-consumer architectures.

#### Day 1-2: Futures, Promises & Async/Await
**Topics:**
- Futures vs Promises: semantics and use cases
- Chaining async operations: `thenApply`, `thenCompose`, error handling
- Async/Await under the hood: state machines and continuation passing
- Exception handling in async pipelines
- Backpressure and overload handling

**Exercise:**
Design an async HTTP client library that:
- Chains multiple requests
- Handles retries with exponential backoff
- Properly handles exceptions
- Implements backpressure (max inflight requests)

---

#### Day 3-4: Producer-Consumer & Rate Limiting
**Topics:**
- Bounded Blocking Queue design (deepen from Week 2)
- Backpressure strategies: blocking producer, dropping, or queuing
- Rate Limiting Algorithms (for a single-threaded library):
  - Token Bucket
  - Leaky Bucket
  - Sliding Window Counters
- Thundering Herd problem: how to prevent all threads from waking at once

**Design Exercise:**
Build a **Rate Limiter Library** that:
- Supports Token Bucket algorithm
- Works across multiple threads
- Provides methods: `allowRequest()` and `waitUntilAllowed()`
- Is efficient (minimal synchronization)

---

#### Day 5: Event Loops vs Thread Pools
**Topics:**
- Event loop architecture (Node.js style): single-threaded, non-blocking
- Thread pool architecture (C#/.NET style): worker threads handling tasks
- When to use which: I/O-bound vs CPU-bound workloads
- Comparing throughput and latency characteristics
- Work-stealing queues and task scheduling

---

## PHASE C: Object-Oriented Design & Design Principles (Weeks 4–5)

### Goal
Master SOLID principles and Gang-of-Four patterns *in context*. Know *why* and *when*, not just *what*.

### Week 4: SOLID & Architectural Principles

#### Day 1-2: Deep Dive into SOLID
**Topics:**

**S — Single Responsibility Principle**
- Definition: A class has one reason to change
- Anti-pattern: God Classes (classes doing too much)
- How to identify: Would you describe the class without using "and"?
- Example: A `User` class should NOT handle persistence AND validation AND logging

**O — Open/Closed Principle**
- Open for extension, closed for modification
- Strategy pattern: extend without changing existing code
- Example: Payment processors (Credit, PayPal, Crypto) without modifying `PaymentService`

**L — Liskov Substitution Principle**
- Subtypes must be substitutable for their base type
- When inheritance breaks: Square extending Rectangle (violates LSP)
- Why composition often wins over inheritance
- Covariance/Contravariance implications

**I — Interface Segregation Principle**
- Don't force clients to depend on interfaces they don't use
- Example: Printer interface split into `Printable`, `Scannable`, `Faxable`
- Dependency bloat: why it matters

**D — Dependency Inversion Principle**
- Depend on abstractions, not concretions
- Dependency Injection and Inversion of Control containers
- How to break tight coupling
- Example: Injection of `ILogger` vs hardcoding a `ConsoleLogger`

**Exercises:**
- Refactor a "God Class" into a clean hierarchy respecting SOLID
- Identify SOLID violations in code snippets
- Redesign a system to be more extensible

---

#### Day 3-4: Composition vs Inheritance
**Topics:**
- The "Diamond Problem" and why it matters
- Inheritance hierarchies: when they break down
- Composition: flexibility, easier testing, looser coupling
- Mixins, Traits, and interface-based design
- Real-world example: UI frameworks (inheritance hell) vs modern component models

**Critical Exercise:**
Design a **Payment Processing System** that supports:
- Multiple payment methods (Credit, PayPal, Crypto, Apple Pay)
- Multiple currencies and conversion
- Discount strategies (percentage, fixed, loyalty-based)

Use composition to avoid a brittle hierarchy of payment processors.

---

#### Day 5: Code Smells & Refactoring
**Topics:**
- Long Methods: break into smaller, testable units
- God Classes: split by responsibility
- Feature Envy: when a class uses another's internals too much
- Data Clumps: related data that should be grouped
- Refused Bequest: subclass doesn't use parent's methods (LSP violation)

**Exercise:**
Given a 200-line monolithic service class, refactor it using these techniques:
- Extract methods for distinct responsibilities
- Create value objects for data clumps
- Split into focused, composable classes

---

### Week 5: Design Patterns (Gang of Four)

The focus here is **when and why**, not just implementation.

#### Day 1-2: Creational Patterns
**Topics:**

**Singleton:**
- Use case: Configuration managers, connection pools, loggers
- Pitfall: hard to test (hides dependencies)
- Double-checked locking: why it's tricky in some languages
- Alternative: Dependency injection instead of singletons

**Factory & Abstract Factory:**
- Factory Method: decoupling object creation from usage
- Abstract Factory: families of related objects
- Example: Create different types of UI buttons (Windows vs Mac)

**Builder:**
- Use: complex immutable objects
- Example: Fluent API for configuring a cache
- Benefits: readability, immutability, validation

**Exercises:**
- Implement a Singleton with proper thread safety
- Refactor code that hardcodes class instantiation to use Factory
- Design a Builder for a complex config object (e.g., caching layer)

---

#### Day 3-4: Structural & Behavioral Patterns
**Topics:**

**Adapter:** Wrapping incompatible interfaces
- Example: Wrapping a legacy logging library to match your interface

**Decorator:** Adding behavior dynamically without inheritance
- Example: Caching decorator around a slow database reader
- Composition over inheritance

**Proxy:** Lazy loading, access control, smart references
- Example: A proxy that loads a heavy resource only on first access

**Observer:** Event handling systems
- Example: MVC model notifying views of changes
- Observer leaks: why weak references matter

**Strategy:** Swapping algorithms at runtime
- Example: Different sorting algorithms, compression methods
- How it enables composition

**Command:** Encapsulating requests as objects
- Use case: Undo/Redo, task scheduling, callback management

**Exercises:**
- Implement a **Caching Decorator** that wraps a slow service
- Design an **Observer-based Event System** that doesn't leak memory
- Build a **Strategy-based Discount Engine**

---

## PHASE D: Low-Level Design Practice (Weeks 6–8)

### Goal
Simulate the 45-60 minute FAANG LLD interview. Design and partially implement object-oriented systems.

### Week 6: Real-World Systems (Manageable Scope)

#### Problems & Focus Areas

**Problem 1: Parking Lot**
- **Focus:** Basic OOD, state management, extensibility
- **Key Questions:**
  - What vehicle types exist? How do you extend?
  - How to charge different rates? (Strategy pattern)
  - How to find an available spot? (Search algorithm)
  - Thread safety? (Multiple parking requests at once)
  - How would pricing change in the future?

**Solution Checklist:**
- Entity classes: `Vehicle`, `ParkingSpot`, `ParkingLot`, `Level`
- State transitions: `available` → `occupied` → `available`
- Extensibility: Payment strategies, Vehicle types
- O(1) spot lookup using a data structure

---

**Problem 2: Movie Ticket Booking System**
- **Focus:** Concurrency, state management, transactions
- **Key Questions:**
  - How to prevent double-booking? (Locking seats)
  - Handling concurrent reservations across multiple users
  - Payment processing: where does it fit?
  - Refunds and cancellations
  - How to scale to millions of bookings?

**Solution Checklist:**
- Entity classes: `User`, `Movie`, `ShowTime`, `Seat`
- State machine for seat: `available` → `reserved` → `paid`/`cancelled`
- Thread safety for concurrent bookings
- Rate limiting per user (prevent spam)
- Idempotent API (retry-safe)

---

**Problem 3: Restaurant Food Ordering System (Zomato/Swiggy-like)**
- **Focus:** Complex domain modeling, state transitions, extensibility
- **Key Questions:**
  - Menu items and customizations
  - Order state machine (placed → cooking → ready → delivered)
  - Delivery person assignment
  - Rating and review system
  - How to handle special requests?

**Solution Checklist:**
- Value objects: `FoodItem`, `Rating`, `DeliverySlot`
- Order state machine with transitions
- Observer pattern for order status updates
- Strategy for delivery assignment
- Notification system (SMS, push, email)

---

### Week 7: Technical Systems (High Concurrency)

#### Problem 1: Thread-Safe LRU Cache
- **Focus:** Data structures, concurrency, optimization
- **Constraints:**
  - O(1) get and put operations
  - Thread-safe for concurrent access
  - Proper eviction of least recently used items

**Implementation Details:**
- Data Structure: `HashMap<K, Node<V>>` + `DoublyLinkedList<Node<V>>`
- Synchronization: `ReadWriteLock` for better concurrent reads
- Eviction: O(1) removal from linked list

**Extensions:**
- LFU (Least Frequently Used) instead of LRU
- TTL support: entries expire after a time
- Size limits in bytes, not just count

---

#### Problem 2: Logging Framework
- **Focus:** Extensibility, async I/O, thread safety
- **Features:**
  - Multiple log levels: DEBUG, INFO, WARN, ERROR
  - Multiple appenders: Console, File, Network
  - Async logging to avoid blocking main thread
  - Thread-safe concurrent logging

**Design:**
- Abstract `Appender` interface for extensibility
- `LogEvent` value object
- Async processing: bounded queue + worker thread
- Graceful shutdown: drain queue before exit

**Extensions:**
- Log filtering
- Pattern-based formatting
- Metrics: how many logs per level?

---

#### Problem 3: Task Scheduler (Cron-like)
- **Focus:** Concurrency, scheduling, state management
- **Features:**
  - Schedule tasks at specific intervals
  - Execute tasks on a thread pool
  - Handle task failures and retries
  - Cancel scheduled tasks

**Design:**
- `Task` interface with execute method
- `ScheduleEntry` for delayed/periodic tasks
- Priority queue of scheduled tasks
- Worker threads picking up and executing

---

### Week 8: API Design & Production Considerations

#### Problem 1: Custom HashMap Implementation
- **Focus:** Hash function, collision handling, resizing
- **Requirements:**
  - Generic key-value storage
  - Handle collisions (chaining)
  - Dynamic resizing when load factor exceeded
  - Iterator support

**Implementation:**
- Array of buckets (each is a linked list)
- Hash function and mod operation
- Load factor threshold (0.75) triggering resize
- O(1) average case, O(n) worst case

**Extensions:**
- Open addressing instead of chaining
- Custom hash functions for specific types
- Thread-safe variant

---

#### Problem 2: API Best Practices & Defensive Design
**Topics:**
- Fluent interfaces: readable APIs (Builder pattern)
- Immutability: `final` keyword, defensive copying
- Method naming: intent-revealing names
- Idempotency: safe to call multiple times
- Null handling: Optional types vs nullability
- Error handling hierarchies: custom exceptions, meaningful messages

**Exercise:**
Design a cache client library API that is:
- Easy to use (fluent, readable)
- Hard to misuse (type-safe, defensive)
- Extensible (strategies for eviction, serialization)

---

## 📊 Interview Expectations by Level

### Mid-Level (SDE II)
- **Scope:** 45-60 minute design
- **Complexity:** Moderate (Parking Lot, Vending Machine)
- **Code:** Mostly pseudocode, key methods implemented
- **Focus:** OOP basics, SOLID, design patterns
- **Concurrency:** Basic understanding; synchronization if asked

### Senior (SDE III / L5)
- **Scope:** Same timeframe but more depth
- **Complexity:** Medium-hard (Booking System, Technical System)
- **Code:** Full implementation of critical components
- **Focus:** Extensibility, thread safety, production concerns
- **Concurrency:** Deep understanding; must discuss trade-offs
- **Follow-ups:** How would you scale? How would you test this?

### Staff (L6+)
- **Scope:** Deep dives and follow-ups push boundaries
- **Complexity:** Hard (LRU Cache, Scheduling System)
- **Code:** Production-quality code
- **Focus:** Performance optimization, observability, operational aspects
- **Concurrency:** Expert-level; can discuss lock-free alternatives
- **Follow-ups:** Load testing strategies, chaos engineering, monitoring, security

---

## 🎯 Preparation Strategy

### Study Phase (Weeks 1–5)
- **Week 1-2:** Watch videos + read materials on memory, concurrency
- **Week 3:** Study async patterns in your language
- **Week 4-5:** Deep-dive into each SOLID principle + patterns with real code

### Practice Phase (Weeks 6–8)
- **Week 6:** Design 2-3 real-world problems (Parking Lot, Movie Booking)
- **Week 7:** Design 2-3 technical problems (LRU Cache, Logging)
- **Week 8:** Refine, optimize, prepare API design aspects

### Mock Interviews
- Record yourself explaining your design (45 minutes)
- Have a peer review your code and design
- Iterate on feedback

---

## 📚 Key Resources by Topic

### Books
- *Effective Java* / *Effective C#* (language-specific best practices)
- *Design Patterns: Elements of Reusable OOP* (Gang of Four)
- *Clean Code* (Robert Martin)
- *Java Concurrency in Practice* / equivalent for your language

### Online Platforms
- LeetCode (Machine Coding section)
- Educative or System Design Handbook
- Real code from open-source projects (Spring Framework, React, etc.)

---

## ✅ Final Checklist Before Interview

- [ ] Can explain memory model and GC impact of your design
- [ ] Have implemented a thread-safe data structure from scratch
- [ ] Know SOLID principles deeply with code examples
- [ ] Have designed 5+ real-world LLD problems
- [ ] Comfortable with design pattern selection rationale
- [ ] Can discuss trade-offs: performance vs simplicity, thread-safety vs speed
- [ ] Have written pseudocode quickly and cleanly
- [ ] Can articulate follow-up questions and enhancements

---

**Good luck! Remember: LLD interviews test your ability to translate requirements into clean, scalable, testable code.**
