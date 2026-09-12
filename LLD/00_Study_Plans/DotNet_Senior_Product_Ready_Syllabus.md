# 📅 The 8-Layer C# Senior .NET Mastery Syllabus

This syllabus executes the **8-Layer Master Map** across a focused timeline.

**The Golden Rule:** Do not study this as `Topic → memorize definition → move on`. 
Study it as: `Topic → understand the design pressure → implement a small example → encounter it inside an LLD problem → handle interviewer follow-up`.

* 🏢 **For company-specific blueprints**, see **[Top 30 Product Companies LLD Interview Directory & Prep Guide](./Top_30_Product_Companies_LLD_Interview_Guide.md)**.
* ⚡ **For latency numbers, checklists, and templates**, see **[10. Interview Quick Reference, Decision Trees & Checklists](../00_Core_Concepts/10_Interview_Quick_Reference_and_Checklists.md)**.

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
* 💻 **[Library Management System](../02_Tier2_Classic_LLD/20_Library_Management_System.md)**
  * *Design Pressure:* "A book has multiple copies, members have borrow limits, overdue books incur fines."
  * *Action:* Aggregate root `Book` owning `BookCopy[]`, Strategy pattern for fines, Observer pattern for waitlist notifications.
* 💻 **[Snake and Ladder Game](../02_Tier2_Classic_LLD/23_Snake_and_Ladder.md)**
  * *Design Pressure:* "Dynamic board size, multi-player turns, crooked dice, cycle detection to prevent infinite loops."
  * *Action:* Clean OOP entity modeling (`Board`, `Cell`, `Player`), 3-color DFS cycle detector, Strategy pattern for dice.

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
* 💻 **[ATM System](../02_Tier2_Classic_LLD/21_ATM_System.md)**
  * *Design Pressure:* "Card insertion, PIN validation, cash dispensing with strict state validation."
  * *Action:* Implement State Pattern (`IdleState`, `HasCardState`, `AuthenticatedState`, `DispensingState`) with swappable `IBankService`.
* 💻 **[Task Management System (Jira Lite)](../02_Tier2_Classic_LLD/14_Task_Management_System.md)**
  * *Design Pressure:* "Hierarchical work items (Epics -> Stories -> Tasks) with undo/redo history."
  * *Action:* Apply Composite Pattern (`ITaskItem`), Command Pattern (`ITaskCommand` with Undo/Redo stack), and Observer for email alerts.
* 💻 **[In-Memory File System](../02_Tier2_Classic_LLD/17_In_Memory_File_System.md)**
  * *Design Pressure:* "Build hierarchical directories and files with `/a/b/c` path resolution."
  * *Action:* Apply the Composite Pattern (`INode`, `DirectoryNode`, `FileNode`) with `ReaderWriterLockSlim`.
* 💻 **[Coupon & Discount Rule Engine](../02_Tier2_Classic_LLD/25_Coupon_and_Discount_Engine.md)**
  * *Design Pressure:* "Percentage, flat, and BxGy discounts with minimum cart values and mutual exclusivity."
  * *Action:* Apply Composite Specification Pattern (`AndSpecification`, `OrSpecification`), Strategy Pattern for discounts, and execution pipeline.
* 💻 **[Cricbuzz / Live Cricket Scoreboard](../02_Tier2_Classic_LLD/24_Cricbuzz_Cricket_Scoreboard.md)**
  * *Design Pressure:* "Overs, balls, extras (wides/no-balls), strike rotation, live commentary broadcasts."
  * *Action:* Domain Service (`StrikeRotator`), strict legal ball accounting, and Observer pattern for live commentary subscribers.

---

## 🎯 Phase 3: Senior Backend Differentiators (Concurrency & Deadlocks)
*Focus: Making the code survive production multi-threading, avoiding deadlocks, and ensuring data integrity.*

**Layers Covered:** Layer 6 (Concurrency & Synchronization), Layer 7 (Backend Architecture).  
**Key Concepts:** `lock`, `SemaphoreSlim`, `ReaderWriterLockSlim`, `ConcurrentDictionary`, Deadlock Avoidance (Lock Ordering), Double-Entry Accounting, Interval Collision Detection.

**Core Concept References:**
* 📖 [07. Concurrency & Synchronization Patterns](../00_Core_Concepts/07_Concurrency_and_Synchronization_Patterns.md)
* 📖 [05. Backend Architecture](../00_Core_Concepts/05_Backend_Architecture.md)

**Hands-on Application (Tier 1, 2, 3 & 4 Problems):**
* 💻 **[Meeting Room Booking](../01_Tier1_Highest_Priority/01_Meeting_Room_Booking.md)**
  * *Design Pressure:* "Two users book the exact same timeslot simultaneously."
  * *Action:* Implement DB Concurrency Tokens (EF Core) + Application-level distributed locking. Merge Intervals.
* 💻 **[Hotel Management System](../02_Tier2_Classic_LLD/22_Hotel_Management_System.md)**
  * *Design Pressure:* "Reserve rooms across multi-day date ranges without double-booking under high concurrent traffic."
  * *Action:* Implement `DateRange` interval overlap checking with room-level synchronized locking and seasonal dynamic pricing strategy.
* 💻 **[Digital Wallet & Double-Entry Ledger](../04_Tier4_Senior_Backend/34_Digital_Wallet_Ledger.md)**
  * *Design Pressure:* "Transfer funds between Account A and Account B concurrently without deadlocks or double-spending."
  * *Action:* Implement deterministic lock ordering (by Account GUID), immutable double-entry ledger bookkeeping, and idempotency keys.
* 💻 **[Stock Exchange Matching Engine](../04_Tier4_Senior_Backend/35_Stock_Exchange_Matching_Engine.md)**
  * *Design Pressure:* "Match buy and sell orders with sub-millisecond latency under price-time priority."
  * *Action:* Implement custom `IComparer<Order>` with `SortedSet`, support partial order fills, and minimize critical section locking.
* 💻 **[Custom Cache TTL/LRU/Priority](../01_Tier1_Highest_Priority/07_Custom_Cache.md)**
  * *Design Pressure:* "Build an in-memory cache that expires items and supports high concurrent reads/writes."
  * *Action:* Use `ConcurrentDictionary`, `ReaderWriterLockSlim`, and background expiration threads.
* 💻 **[LRU & LFU Cache Mastery (LeetCode 146 & 460)](../03_Tier3_DSA_Screening/26_LRU_and_LFU_Cache.md)**
  * *Design Pressure:* "Strict $O(1)$ `Get` and `Put` with dynamic frequency buckets and LRU tie-breaking."
  * *Action:* Implement two HashMaps + frequency-ordered Doubly Linked Lists tracking `_minFrequency`.
* 💻 **[Search Autocomplete System](../03_Tier3_DSA_Screening/29_Search_Autocomplete_System.md)**
  * *Design Pressure:* "Real-time prefix typing with top-3 query completions and thread-safe dynamic frequency increments."
  * *Action:* Custom Trie with node-level Top-K PriorityQueue cache and `ReaderWriterLockSlim`.

---

## 🎯 Phase 4: Distributed Systems & Event-Driven Architecture (The Capstone)
*Focus: Reliable microservice communication, atomicity across boundaries, and resilience.*

**Layers Covered:** Layer 7 (Backend LLD continued), Layer 8 (Interview Execution & Quick Reference).  
**Key Concepts:** Idempotency, Outbox Pattern, Message Queues (Azure Service Bus), Dead Letter Queues, Circuit Breakers, Retry Policies, URL Shortening.

**Core Concept References:**
* 📖 [06. Interview Execution Framework](../00_Core_Concepts/06_Interview_Execution_Framework.md)
* 📖 [08. LLD Problem Families & Evolution](../00_Core_Concepts/08_LLD_Problem_Families_and_Evolution.md)
* 📖 [10. Quick Reference, Decision Trees & Checklists](../00_Core_Concepts/10_Interview_Quick_Reference_and_Checklists.md)

**Hands-on Application (Tier 1, 3 & 4 Problems):**
* 💻 **[DDD + CQRS + Azure Service Bus](../01_Tier1_Highest_Priority/04_DDD_CQRS_AzureServiceBus.md)**
  * *Design Pressure:* "Save to the DB and publish an event. What if the network fails in between?"
  * *Action:* Build the Transactional Outbox Pattern using an EF Core Interceptor and a Background Worker.
* 💻 **[Resilient API Aggregator](../01_Tier1_Highest_Priority/05_Resilient_API_Aggregator.md)**
  * *Design Pressure:* "Call 3 external APIs. One is slow and intermittently fails."
  * *Action:* Implement `HttpClientFactory` with `Polly` (Circuit Breaker, Retry, Timeout) and use `Task.WhenAll`.
* 💻 **[Online Auction System](../04_Tier4_Senior_Backend/36_Online_Auction_System.md)**
  * *Design Pressure:* "Real-time bids arrive in milliseconds; dynamic anti-sniping auction extension; financial escrow hold."
  * *Action:* Atomic bid evaluation, SignalR websocket broadcast, and escrow settlement transaction.
* 💻 **[Social Media News Feed / Twitter Lite](../03_Tier3_DSA_Screening/30_Social_Media_Feed_Twitter.md)**
  * *Design Pressure:* "Generate 10 most recent tweets across thousands of followees without table scans."
  * *Action:* K-way merge using `PriorityQueue<Tweet, long>`, singly-linked user tweet chains, and Push vs Pull hybrid architecture.
* 💻 **[High-Throughput URL Shortener (TinyURL)](../04_Tier4_Senior_Backend/37_URL_Shortener.md)**
  * *Design Pressure:* "Sub-5ms redirection, zero-collision ID generation, non-blocking click telemetry."
  * *Action:* Bijective Base62 encoding, distributed range token allocator, multi-tier cache-aside, and background `Channel<ClickEvent>` worker.

---

## 💡 The "Interview Follow-Up" Checklist
For every problem above, ensure you can verbally answer:
1. "Who creates this object?" (Testing your knowledge of DI/Factories)
2. "What happens if two users execute this simultaneously?" (Testing Concurrency & Deadlocks)
3. "What happens if the external dependency crashes?" (Testing Error Handling/Resilience/Outbox)
4. "How do you test this?" (Testing Abstraction/Mocking/xUnit Harnesses)
5. "Can you walk through your time allocation in a 45m vs 90m round?" (Reviewing [06_Interview_Execution_Framework](../00_Core_Concepts/06_Interview_Execution_Framework.md) and [10_Interview_Quick_Reference_and_Checklists](../00_Core_Concepts/10_Interview_Quick_Reference_and_Checklists.md))