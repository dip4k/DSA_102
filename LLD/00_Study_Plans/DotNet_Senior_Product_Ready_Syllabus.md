# 📅 The 8-Layer C# Senior .NET Mastery Syllabus

This syllabus executes the **8-Layer Master Map** across a focused timeline.

**The Golden Rule:** Do not study this as `Topic → memorize definition → move on`. 
Study it as: `Topic → understand the design pressure → implement a small example → encounter it inside an LLD problem → handle interviewer follow-up`.

---

## 🎯 Phase 1: Object & Domain Modeling (The Foundation)
*Focus: Stop writing "Anemic Domain Models". Learn to identify entities, value objects, invariants, class relationships, and draw clean UML diagrams.*

**Layers Covered:** Layer 1 (OOP & UML Class Diagrams), Layer 2 (SOLID & Design Principles), Layer 4 (Domain Modeling).
**Key Concepts:** Encapsulation, Composition over Inheritance, Class Relationships (Association, Aggregation, Composition, Dependency), Entity vs. Value Object, Aggregate Roots, Invariants, UML Notation & Speed Templates.

**Core Concept References:**
* 📖 [01. OOP, SOLID & Class Relationships](../00_Core_Concepts/01_OOP_and_SOLID.md)
* 📖 [09. UML Class Diagrams & Speed Template](../00_Core_Concepts/09_UML_Class_Diagrams.md)
* 📖 [03. Domain Modeling & DDD](../00_Core_Concepts/03_Domain_Modeling_and_DDD.md)

**Hands-on Application (Tier 1 & 2 Problems):**
* 💻 **[Parking Lot](../01_Tier1_Highest_Priority/06_Parking_Lot.md)**
  * *Design Pressure:* "A ParkingLot has ParkingFloors and ParkingSpots." 
  * *Action:* Use Composition (not Inheritance). Apply SRP/OCP.
* 💻 **[Order Management (DDD + CQRS)](../01_Tier1_Highest_Priority/03_DDD_Order_Management.md)**
  * *Design Pressure:* "An order cannot be shipped if it's not paid." 
  * *Action:* Protect invariants. Use Private Setters. Isolate state transitions inside the `Order` Aggregate Root.
* 💻 **[Library Management System](../02_Tier2_Classic_LLD/27_Library_Management_System.md)**
  * *Design Pressure:* "A book has multiple copies, members have borrow limits, overdue books incur fines."
  * *Action:* Aggregate root `Book` owning `BookCopy[]`, Strategy pattern for fines, Observer pattern for waitlist notifications.

---

## 🎯 Phase 2: Behaviors, Patterns, and Variations (The Abstraction)
*Focus: Replace massive `if/else` and `switch` blocks with polymorphism. Encapsulate what varies.*

**Layers Covered:** Layer 3 (Core Design Patterns & GoF Directory), Layer 5 (C# LLD & DI).
**Key Concepts:** Strategy, Factory, State, Observer, Composite, Command, Dependency Injection (Lifetimes), Interfaces.

**Core Concept References:**
* 📖 [02. Design Patterns Cheat Sheet](../00_Core_Concepts/02_Design_Patterns_Cheat_Sheet.md)
* 📖 [04. C# DI & Concurrency](../00_Core_Concepts/04_CSharp_DI_and_Concurrency.md)

**Hands-on Application (Tier 1 & 2 Problems):**
* 💻 **[Generic Message Processor](../01_Tier1_Highest_Priority/02_Generic_Message_Processor.md)**
  * *Design Pressure:* "We receive 5 different types of Azure Service Bus messages. Tomorrow we might receive 10." 
  * *Action:* Use Factory + DI + Generics (`IMessageHandler<T>`) to route messages without a single `if/switch` statement.
* 💻 **[Notification System](../01_Tier1_Highest_Priority/08_Notification_System.md)**
  * *Design Pressure:* "Send notifications via Email, SMS, or Push. Add rate limiting and retry."
  * *Action:* Use Strategy (Channel), Factory (Creator), and Decorator (Adding Retry/Rate Limit without modifying the core sender).
* 💻 **[ATM System](../02_Tier2_Classic_LLD/28_ATM_System.md)**
  * *Design Pressure:* "Card insertion, PIN validation, cash dispensing with strict state validation."
  * *Action:* Implement State Pattern (`IdleState`, `HasCardState`, `AuthenticatedState`, `DispensingState`) with swappable `IBankService`.
* 💻 **[Task Management System (Jira Lite)](../02_Tier2_Classic_LLD/18_Task_Management_System.md)**
  * *Design Pressure:* "Hierarchical work items (Epics -> Stories -> Tasks) with undo/redo history."
  * *Action:* Apply Composite Pattern (`ITaskItem`), Command Pattern (`ITaskCommand` with Undo/Redo stack), and Observer for email alerts.
* 💻 **[In-Memory File System](../02_Tier2_Classic_LLD/24_In_Memory_File_System.md)**
  * *Design Pressure:* "Build hierarchical directories and files with `/a/b/c` path resolution."
  * *Action:* Apply the Composite Pattern (`INode`, `DirectoryNode`, `FileNode`) with `ReaderWriterLockSlim`.

---

## 🎯 Phase 3: Senior Backend Differentiators (Concurrency & Deadlocks)
*Focus: Making the code survive production multi-threading, avoiding deadlocks, and ensuring data integrity.*

**Layers Covered:** Layer 6 (Concurrency & Synchronization), Layer 7 (Backend Architecture).
**Key Concepts:** `lock`, `SemaphoreSlim`, `ReaderWriterLockSlim`, `ConcurrentDictionary`, Deadlock Avoidance (Lock Ordering), Double-Entry Accounting, Interval Collision Detection.

**Core Concept References:**
* 📖 [07. Concurrency & Synchronization Patterns](../00_Core_Concepts/07_Concurrency_and_Synchronization_Patterns.md)
* 📖 [05. Backend Architecture](../00_Core_Concepts/05_Backend_Architecture.md)

**Hands-on Application (Tier 1, 2 & 4 Problems):**
* 💻 **[Meeting Room Booking](../01_Tier1_Highest_Priority/01_Meeting_Room_Booking.md)**
  * *Design Pressure:* "Two users book the exact same timeslot simultaneously."
  * *Action:* Implement DB Concurrency Tokens (EF Core) + Application-level distributed locking. Merge Intervals.
* 💻 **[Hotel Management System](../02_Tier2_Classic_LLD/29_Hotel_Management_System.md)**
  * *Design Pressure:* "Reserve rooms across multi-day date ranges without double-booking under high concurrent traffic."
  * *Action:* Implement `DateRange` interval overlap checking with room-level synchronized locking and seasonal dynamic pricing strategy.
* 💻 **[Digital Wallet & Double-Entry Ledger](../04_Tier4_Senior_Backend/22_Digital_Wallet_Ledger.md)**
  * *Design Pressure:* "Transfer funds between Account A and Account B concurrently without deadlocks or double-spending."
  * *Action:* Implement deterministic lock ordering (by Account GUID), immutable double-entry ledger bookkeeping, and idempotency keys.
* 💻 **[Stock Exchange Matching Engine](../04_Tier4_Senior_Backend/23_Stock_Exchange_Matching_Engine.md)**
  * *Design Pressure:* "Match buy and sell orders with sub-millisecond latency under price-time priority."
  * *Action:* Implement custom `IComparer<Order>` with `SortedSet`, support partial order fills, and minimize critical section locking.
* 💻 **[Custom Cache TTL/LRU/Priority](../01_Tier1_Highest_Priority/07_Custom_Cache.md)**
  * *Design Pressure:* "Build an in-memory cache that expires items and supports high concurrent reads/writes."
  * *Action:* Use `ConcurrentDictionary`, `ReaderWriterLockSlim`, and background expiration threads.

---

## 🎯 Phase 4: Distributed Systems & Event-Driven Architecture (The Capstone)
*Focus: Reliable microservice communication, atomicity across boundaries, and resilience.*

**Layers Covered:** Layer 7 (Backend LLD continued), Layer 8 (Interview Execution).
**Key Concepts:** Idempotency, Outbox Pattern, Message Queues (Azure Service Bus), Dead Letter Queues, Circuit Breakers, Retry Policies.

**Core Concept References:**
* 📖 [06. Interview Execution Framework](../00_Core_Concepts/06_Interview_Execution_Framework.md)
* 📖 [08. LLD Problem Families & Evolution](../00_Core_Concepts/08_LLD_Problem_Families_and_Evolution.md)

**Hands-on Application (Tier 1 & 4 Problems):**
* 💻 **[DDD + CQRS + Azure Service Bus](../01_Tier1_Highest_Priority/04_DDD_CQRS_AzureServiceBus.md)**
  * *Design Pressure:* "Save to the DB and publish an event. What if the network fails in between?"
  * *Action:* Build the Transactional Outbox Pattern using an EF Core Interceptor and a Background Worker.
* 💻 **[Resilient API Aggregator](../01_Tier1_Highest_Priority/05_Resilient_API_Aggregator.md)**
  * *Design Pressure:* "Call 3 external APIs. One is slow and intermittently fails."
  * *Action:* Implement `HttpClientFactory` with `Polly` (Circuit Breaker, Retry, Timeout) and use `Task.WhenAll`.
* 💻 **[Online Auction System](../04_Tier4_Senior_Backend/26_Online_Auction_System.md)**
  * *Design Pressure:* "Real-time bids arrive in milliseconds; dynamic anti-sniping auction extension; financial escrow hold."
  * *Action:* Atomic bid evaluation, SignalR websocket broadcast, and escrow settlement transaction.

---

## 💡 The "Interview Follow-Up" Checklist
For every problem above, ensure you can verbally answer:
1. "Who creates this object?" (Testing your knowledge of DI/Factories)
2. "What happens if two users execute this simultaneously?" (Testing Concurrency & Deadlocks)
3. "What happens if the external dependency crashes?" (Testing Error Handling/Resilience/Outbox)
4. "How do you test this?" (Testing Abstraction/Mocking/xUnit Harnesses)
5. "Can you walk through your time allocation in a 45m vs 90m round?" (Reviewing [06_Interview_Execution_Framework](../00_Core_Concepts/06_Interview_Execution_Framework.md))