# 🏆 The Unified 80/20 Senior .NET LLD Master Roadmap (8-Layer Map)

Welcome to the **Refactored Senior .NET LLD & Architecture** study path. This roadmap aligns strictly with the genuine 80/20 rule for Senior/Lead .NET interviews.

Instead of a giant "LLD topics + 30 random problems" list, this roadmap builds a unified map:
`Concept → Pattern → Problem Family → Representative example → Interview problem → Senior-level follow-ups`

---

## 🧠 The 8-Layer LLD Master Map

| Layer | Importance | What you must master | Core Concept Reference |
| :--- | :--- | :--- | :--- |
| **1. OOP & Object Modeling** | 🔴 Critical | Classes, objects, encapsulation, abstraction, polymorphism, composition | [01. OOP & SOLID](./00_Core_Concepts/01_OOP_and_SOLID.md) |
| **2. SOLID & Design Principles** | 🔴 Critical | SOLID, cohesion/coupling, DRY, KISS, composition over inheritance | [01. OOP & SOLID](./00_Core_Concepts/01_OOP_and_SOLID.md) |
| **3. Core Design Patterns** | 🔴 Critical | Strategy, Factory, State, Observer, Decorator, Adapter, Command, Composite | [02. Design Patterns Cheat Sheet](./00_Core_Concepts/02_Design_Patterns_Cheat_Sheet.md) |
| **4. Domain Modeling** | 🔴 Critical | Entity, Value Object, Aggregate, invariants, domain service | [03. Domain Modeling & DDD](./00_Core_Concepts/03_Domain_Modeling_and_DDD.md) |
| **5. C# LLD & DI** | 🔴 Critical | Interfaces, generics, DI, lifetimes, collections, immutability | [04. C# DI & Concurrency](./00_Core_Concepts/04_CSharp_DI_and_Concurrency.md) |
| **6. Concurrency & Synchronization** | 🔴 Critical for Senior | Locks, thread safety, race conditions, atomicity, concurrent collections | [07. Concurrency Patterns](./00_Core_Concepts/07_Concurrency_and_Synchronization_Patterns.md) |
| **7. Backend Architecture** | 🔴 Critical for Senior | Repository, transactions, CQRS, messaging, idempotency, Outbox | [05. Backend Architecture](./00_Core_Concepts/05_Backend_Architecture.md) |
| **8. Interview Execution & Taxonomy** | 🔴 Critical for Success | 45m OOD vs 90m Machine Coding, 6 Problem Families, V1 $\rightarrow$ V2 Evolution | [06. Interview Framework](./00_Core_Concepts/06_Interview_Execution_Framework.md) · [08. Problem Families](./00_Core_Concepts/08_LLD_Problem_Families_and_Evolution.md) |

---

## 🧭 The 6 Reusable Problem Archetypes (Mental Models)
When given any interview problem, immediately map it to one of these 6 families:

| Problem Family | Core Architectural Challenge | GoF Patterns Applied | Flagship Problems in Repo |
| :--- | :--- | :--- | :--- |
| **1. Allocation & Concurrency Conflicts** | Preventing double-allocation of shared finite resources under race conditions. | Strategy, Factory | [Parking Lot](./01_Tier1_Highest_Priority/06_Parking_Lot.md), [Meeting Room](./01_Tier1_Highest_Priority/01_Meeting_Room_Booking.md), [Movie Booking](./02_Tier2_Classic_LLD/08_Movie_Booking.md) |
| **2. Financial & Immutable Ledgers** | Double-entry accounting, auditability, precision (`decimal`), deadlock avoidance. | Command, Strategy | [Digital Wallet](./04_Tier4_Senior_Backend/22_Digital_Wallet_Ledger.md), [Splitwise](./02_Tier2_Classic_LLD/11_Splitwise.md), [Transactional KV](./04_Tier4_Senior_Backend/19_Transactional_Key_Value_Store.md) |
| **3. Stateful Workflow Engines** | Entity moves through rigid phases; invalid transitions strictly rejected. | State, Observer, Command | [Elevator](./02_Tier2_Classic_LLD/10_Elevator.md), [Vending Machine](./02_Tier2_Classic_LLD/12_Vending_Machine.md), [Ride Hailing](./02_Tier2_Classic_LLD/20_Ride_Hailing_System.md), [Food Delivery](./02_Tier2_Classic_LLD/21_Food_Delivery_System.md), [Board Game Engine](./02_Tier2_Classic_LLD/25_Board_Game_Engine.md) |
| **4. Event-Driven & Buffering** | Decoupled async producers/consumers, guaranteed delivery, backpressure. | Observer, Chain of Resp | [Azure Service Bus Outbox](./01_Tier1_Highest_Priority/04_DDD_CQRS_AzureServiceBus.md), [Pub/Sub](./04_Tier4_Senior_Backend/14_Pub_Sub.md), [Message Processor](./01_Tier1_Highest_Priority/02_Generic_Message_Processor.md), [Logger](./02_Tier2_Classic_LLD/13_Logger.md) |
| **5. In-Memory Data Structures** | Sub-millisecond latency, multi-key lookups, composite tree hierarchies. | Composite, Iterator | [LRU Cache](./03_Tier3_DSA_Screening/15_LRU_Cache.md), [File System](./02_Tier2_Classic_LLD/24_In_Memory_File_System.md), [Stock Matching Engine](./04_Tier4_Senior_Backend/23_Stock_Exchange_Matching_Engine.md), [Rate Limiter](./04_Tier4_Senior_Backend/09_Rate_Limiter.md) |
| **6. Gateway & Resilience** | Isolating flaky third-party APIs, circuit breaking, multi-channel dispatch. | Adapter, Decorator, Strategy | [Resilient API Aggregator](./01_Tier1_Highest_Priority/05_Resilient_API_Aggregator.md), [Notification System](./01_Tier1_Highest_Priority/08_Notification_System.md) |

---

## 🎨 Design Patterns — The Real 80/20
Do not attempt to memorize all 23 GoF patterns equally.
* **🔥 Tier 1 — Master (Implementation Practice):** Strategy, Factory, State, Observer, Decorator, Adapter, Command, Composite.
* **🟡 Tier 2 — Know Well:** Builder, Chain of Responsibility, Facade, Template Method, Proxy.
* **🟢 Tier 3 — Recognition Level:** Abstract Factory, Bridge, Prototype, Iterator, Mediator, Memento, Flyweight, Visitor.

---

## 🚀 The 25-Problem Interview Portfolio

Mastering these 25 problems exercises every core pattern across production scenarios.

### 🔥 Tier 1: Highest Priority (The Golden 8)
1. **[Meeting Room Booking](./01_Tier1_Highest_Priority/01_Meeting_Room_Booking.md)** (Domain, Concurrency, Intervals)
2. **[Generic Message Processor](./01_Tier1_Highest_Priority/02_Generic_Message_Processor.md)** (Factory, Polymorphism, DI, Messaging)
3. **[Order Management (DDD + CQRS)](./01_Tier1_Highest_Priority/03_DDD_Order_Management.md)** (DDD, CQRS, Aggregate Invariants)
4. **[DDD + CQRS + Azure Service Bus (Outbox)](./01_Tier1_Highest_Priority/04_DDD_CQRS_AzureServiceBus.md)** (Senior Backend Architecture)
5. **[Resilient API Aggregator](./01_Tier1_Highest_Priority/05_Resilient_API_Aggregator.md)** (Adapter + Decorator + Polly Resilience)
6. **[Parking Lot](./01_Tier1_Highest_Priority/06_Parking_Lot.md)** (OOP, SOLID, Composition, Strategy, Factory, DI, Concurrency)
7. **[Custom Cache TTL/LRU/Priority](./01_Tier1_Highest_Priority/07_Custom_Cache.md)** (DSA, Concurrency, Strategy)
8. **[Notification System](./01_Tier1_Highest_Priority/08_Notification_System.md)** (Strategy, Factory, Messaging)

### 🟡 Tier 2: Classic LLD (State & Strategy Mastery)
9. **[Movie Ticket Booking](./02_Tier2_Classic_LLD/08_Movie_Booking.md)** (Concurrency, State, Redis TTL vs SQL Lock)
10. **[Elevator System](./02_Tier2_Classic_LLD/10_Elevator.md)** (SCAN Algorithm, State, Strategy)
11. **[Splitwise](./02_Tier2_Classic_LLD/11_Splitwise.md)** (Strategy, Graph Simplification, Monetary Precision)
12. **[Vending Machine](./02_Tier2_Classic_LLD/12_Vending_Machine.md)** (State Pattern, Invariant Checking)
13. **[Logger](./02_Tier2_Classic_LLD/13_Logger.md)** (Chain of Responsibility, Async Buffering)
14. **[Ride-Hailing System (Uber / Ola)](./02_Tier2_Classic_LLD/20_Ride_Hailing_System.md)** (Strategy, State Machine, Driver Matching)
15. **[Food Delivery System (Swiggy / Zomato)](./02_Tier2_Classic_LLD/21_Food_Delivery_System.md)** (Strategy, Observer, Invariant Protection)
16. **[In-Memory File System](./02_Tier2_Classic_LLD/24_In_Memory_File_System.md)** (Composite Pattern, Trie, ReaderWriterLockSlim)
17. **[Board Game Engine (Tic-Tac-Toe & Chess Lite)](./02_Tier2_Classic_LLD/25_Board_Game_Engine.md)** (Command Pattern, Undo/Redo, Strategy, O(1) Win Checking)

### 🟢 Tier 3: DSA + LLD Hybrids
18. **[LRU Cache (Pure DSA)](./03_Tier3_DSA_Screening/15_LRU_Cache.md)** (HashMap + Doubly Linked List)
19. **[Merge Intervals](./03_Tier3_DSA_Screening/16_Merge_Intervals.md)** (Calendar Algorithm)
20. **[Invalid Transactions](./03_Tier3_DSA_Screening/17_Invalid_Transactions.md)** (Validation + Sliding Windows)

### 🟣 Tier 4: Senior Backend (Distributed & Financial Systems)
21. **[Rate Limiter](./04_Tier4_Senior_Backend/09_Rate_Limiter.md)** (Token Bucket, Leaky Bucket, Sliding Window Log)
22. **[Pub/Sub Message Queue](./04_Tier4_Senior_Backend/14_Pub_Sub.md)** (Observer, Async Channels, Backpressure)
23. **[In-Memory Transactional Key-Value Store](./04_Tier4_Senior_Backend/19_Transactional_Key_Value_Store.md)** (Stack of Scopes, ACID, Concurrency)
24. **[Digital Wallet & Double-Entry Ledger](./04_Tier4_Senior_Backend/22_Digital_Wallet_Ledger.md)** (Double-Entry Bookkeeping, Deadlock-Free Transfer, Idempotency)
25. **[Stock Exchange Matching Engine](./04_Tier4_Senior_Backend/23_Stock_Exchange_Matching_Engine.md)** (Price-Time Priority, Order Book, SortedSet, Partial Fills)

---

## 📊 Pattern → Problem Coverage Matrix (Your Study Tracker)

| Pattern / Concept | Parking | Booking | Cache | Notify | Order | Message | Wallet | Exchange | FileSystem | BoardGame |
| :--- | :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: |
| **Encapsulation** | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| **Composition**   | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| **SOLID**         | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| **Strategy**      | ✅ | ✅ | ✅ | ✅ |  |  |  |  |  | ✅ |
| **Factory**       | ✅ |  |  | ✅ |  | ✅ |  |  |  |  |
| **State**         |  | ✅ |  |  | ✅ |  | ✅ | ✅ |  | ✅ |
| **Observer**      |  |  |  | ✅ | ✅ | ✅ |  |  |  |  |
| **Decorator**     |  |  |  | ✅ |  |  |  |  |  |  |
| **Composite**     |  |  |  |  |  |  |  |  | ✅ |  |
| **Command**       |  |  |  |  | ✅ |  |  |  |  | ✅ |
| **DI**            | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |  |  |
| **Repository**    | ✅ | ✅ |  |  | ✅ |  | ✅ |  |  |  |
| **CQRS**          |  |  |  |  | ✅ | ✅ |  |  |  |  |
| **Messaging**     |  |  |  | ✅ | ✅ | ✅ |  |  |  |  |
| **Idempotency**   |  |  |  | ✅ | ✅ | ✅ | ✅ |  |  |  |
| **Concurrency**   | 🔴 | 🔴 | 🔴 | 🔴 | 🔴 | 🔴 | 🔴 | 🔴 | 🔴 |  |
| **Transactions**  | 🔴 | 🔴 |  |  | 🔴 |  | 🔴 |  |  |  |
| **Outbox**        |  |  |  |  | 🔴 | 🔴 |  |  |  |  |

---

## 💡 The Actual LLD Mastery Hierarchy
Study in this progression:
`OOP / Composition / SOLID` ➔ `Strategy / State / Factory / Observer / Composite / Command` ➔ `Domain Model (Entity/VO/Aggregate/Invariants)` ➔ `C# / DI / Testing` ➔ `Concurrency & Deadlock Prevention` ➔ `Persistence / CQRS` ➔ `Messaging / Events (Idempotency / Outbox)` ➔ `Production LLD`# 🏆 The Unified 80/20 Senior .NET LLD Master Roadmap (8-Layer Map)

Welcome to the **Refactored Senior .NET LLD & Architecture** study path. This roadmap aligns strictly with the genuine 80/20 rule for Senior/Lead .NET interviews.

Instead of a giant "LLD topics + 30 random problems" list, this roadmap builds a unified map:
`Concept → Pattern → Problem Family → Representative example → Interview problem → Senior-level follow-ups`

---

## 🧠 The 8-Layer LLD Master Map

| Layer | Importance | What you must master | Core Concept Reference |
| :--- | :--- | :--- | :--- |
| **1. OOP & Object Modeling** | 🔴 Critical | Classes, objects, encapsulation, abstraction, polymorphism, composition | [01. OOP & SOLID](./00_Core_Concepts/01_OOP_and_SOLID.md) |
| **2. SOLID & Design Principles** | 🔴 Critical | SOLID, cohesion/coupling, DRY, KISS, composition over inheritance | [01. OOP & SOLID](./00_Core_Concepts/01_OOP_and_SOLID.md) |
| **3. Core Design Patterns** | 🔴 Critical | Strategy, Factory, State, Observer, Decorator, Adapter, Command, Composite | [02. Design Patterns Cheat Sheet](./00_Core_Concepts/02_Design_Patterns_Cheat_Sheet.md) |
| **4. Domain Modeling** | 🔴 Critical | Entity, Value Object, Aggregate, invariants, domain service | [03. Domain Modeling & DDD](./00_Core_Concepts/03_Domain_Modeling_and_DDD.md) |
| **5. C# LLD & DI** | 🔴 Critical | Interfaces, generics, DI, lifetimes, collections, immutability | [04. C# DI & Concurrency](./00_Core_Concepts/04_CSharp_DI_and_Concurrency.md) |
| **6. Concurrency & Synchronization** | 🔴 Critical for Senior | Locks, thread safety, race conditions, atomicity, concurrent collections | [07. Concurrency Patterns](./00_Core_Concepts/07_Concurrency_and_Synchronization_Patterns.md) |
| **7. Backend Architecture** | 🔴 Critical for Senior | Repository, transactions, CQRS, messaging, idempotency, Outbox | [05. Backend Architecture](./00_Core_Concepts/05_Backend_Architecture.md) |
| **8. Interview Execution & Taxonomy** | 🔴 Critical for Success | CrackingWalnuts 6-Step Funnel, 45m OOD vs 90m Machine Coding, 6 Problem Families, V1 $\rightarrow$ V4 Evolution | [06. Interview Framework](./00_Core_Concepts/06_Interview_Execution_Framework.md) · [08. Problem Families](./00_Core_Concepts/08_LLD_Problem_Families_and_Evolution.md) |

---

## 🧭 The 6 Reusable Problem Archetypes (Mental Models)
When given any interview problem, immediately map it to one of these 6 families:

| Problem Family | Core Architectural Challenge | GoF Patterns Applied | Flagship Problems in Repo |
| :--- | :--- | :--- | :--- |
| **1. Allocation & Concurrency Conflicts** | Preventing double-allocation of shared finite resources under race conditions. | Strategy, Factory, State | [Parking Lot](./01_Tier1_Highest_Priority/06_Parking_Lot.md), [Meeting Room](./01_Tier1_Highest_Priority/01_Meeting_Room_Booking.md), [Movie Booking](./02_Tier2_Classic_LLD/08_Movie_Booking.md), [Amazon Locker](./02_Tier2_Classic_LLD/26_Amazon_Locker_System.md) |
| **2. Financial & Immutable Ledgers** | Double-entry accounting, auditability, precision (`decimal`), deadlock avoidance. | Command, Strategy, State | [Digital Wallet](./04_Tier4_Senior_Backend/22_Digital_Wallet_Ledger.md), [Online Auction](./04_Tier4_Senior_Backend/26_Online_Auction_System.md), [Splitwise](./02_Tier2_Classic_LLD/11_Splitwise.md), [Transactional KV](./04_Tier4_Senior_Backend/19_Transactional_Key_Value_Store.md) |
| **3. Stateful Workflow Engines** | Entity moves through rigid phases; invalid transitions strictly rejected. | State, Observer, Command | [Elevator](./02_Tier2_Classic_LLD/10_Elevator.md), [Vending Machine](./02_Tier2_Classic_LLD/12_Vending_Machine.md), [Ride Hailing](./02_Tier2_Classic_LLD/20_Ride_Hailing_System.md), [Food Delivery](./02_Tier2_Classic_LLD/21_Food_Delivery_System.md), [Board Game Engine](./02_Tier2_Classic_LLD/25_Board_Game_Engine.md) |
| **4. Event-Driven & Buffering** | Decoupled async producers/consumers, guaranteed delivery, backpressure. | Observer, Chain of Resp | [Azure Service Bus Outbox](./01_Tier1_Highest_Priority/04_DDD_CQRS_AzureServiceBus.md), [Pub/Sub](./04_Tier4_Senior_Backend/14_Pub_Sub.md), [Message Processor](./01_Tier1_Highest_Priority/02_Generic_Message_Processor.md), [Logger](./02_Tier2_Classic_LLD/13_Logger.md) |
| **5. In-Memory Data Structures** | Sub-millisecond latency, multi-key lookups, composite tree hierarchies. | Composite, Iterator | [LRU Cache](./03_Tier3_DSA_Screening/15_LRU_Cache.md), [File System](./02_Tier2_Classic_LLD/24_In_Memory_File_System.md), [Stock Matching Engine](./04_Tier4_Senior_Backend/23_Stock_Exchange_Matching_Engine.md), [Rate Limiter](./04_Tier4_Senior_Backend/09_Rate_Limiter.md) |
| **6. Gateway & Resilience** | Isolating flaky third-party APIs, circuit breaking, multi-channel dispatch. | Adapter, Decorator, Strategy | [Resilient API Aggregator](./01_Tier1_Highest_Priority/05_Resilient_API_Aggregator.md), [Notification System](./01_Tier1_Highest_Priority/08_Notification_System.md) |

---

## 🎨 Design Patterns — The Real 80/20
Do not attempt to memorize all 23 GoF patterns equally.
* **🔥 Tier 1 — Master (Implementation Practice):** Strategy, Factory, State, Observer, Decorator, Adapter, Command, Composite.
* **🟡 Tier 2 — Know Well:** Builder, Chain of Responsibility, Facade, Template Method, Proxy.
* **🟢 Tier 3 — Recognition Level:** Abstract Factory, Bridge, Prototype, Iterator, Mediator, Memento, Flyweight, Visitor.

---

## 🚀 The 27-Problem Interview Portfolio

Mastering these 27 problems exercises every core pattern across production scenarios.

### 🔥 Tier 1: Highest Priority (The Golden 8)
1. **[Meeting Room Booking](./01_Tier1_Highest_Priority/01_Meeting_Room_Booking.md)** (Domain, Concurrency, Intervals)
2. **[Generic Message Processor](./01_Tier1_Highest_Priority/02_Generic_Message_Processor.md)** (Factory, Polymorphism, DI, Messaging)
3. **[Order Management (DDD + CQRS)](./01_Tier1_Highest_Priority/03_DDD_Order_Management.md)** (DDD, CQRS, Aggregate Invariants)
4. **[DDD + CQRS + Azure Service Bus (Outbox)](./01_Tier1_Highest_Priority/04_DDD_CQRS_AzureServiceBus.md)** (Senior Backend Architecture)
5. **[Resilient API Aggregator](./01_Tier1_Highest_Priority/05_Resilient_API_Aggregator.md)** (Adapter + Decorator + Polly Resilience)
6. **[Parking Lot](./01_Tier1_Highest_Priority/06_Parking_Lot.md)** (OOP, SOLID, Composition, Strategy, Factory, DI, Concurrency)
7. **[Custom Cache TTL/LRU/Priority](./01_Tier1_Highest_Priority/07_Custom_Cache.md)** (DSA, Concurrency, Strategy)
8. **[Notification System](./01_Tier1_Highest_Priority/08_Notification_System.md)** (Strategy, Factory, Messaging)

### 🟡 Tier 2: Classic LLD (State & Strategy Mastery)
9. **[Movie Ticket Booking](./02_Tier2_Classic_LLD/08_Movie_Booking.md)** (Concurrency, State, Redis TTL vs SQL Lock)
10. **[Elevator System](./02_Tier2_Classic_LLD/10_Elevator.md)** (SCAN Algorithm, State, Strategy)
11. **[Splitwise](./02_Tier2_Classic_LLD/11_Splitwise.md)** (Strategy, Graph Simplification, Monetary Precision)
12. **[Vending Machine](./02_Tier2_Classic_LLD/12_Vending_Machine.md)** (State Pattern, Invariant Checking)
13. **[Logger](./02_Tier2_Classic_LLD/13_Logger.md)** (Chain of Responsibility, Async Buffering)
14. **[Ride-Hailing System (Uber / Ola)](./02_Tier2_Classic_LLD/20_Ride_Hailing_System.md)** (Strategy, State Machine, Driver Matching)
15. **[Food Delivery System (Swiggy / Zomato)](./02_Tier2_Classic_LLD/21_Food_Delivery_System.md)** (Strategy, Observer, Invariant Protection)
16. **[In-Memory File System](./02_Tier2_Classic_LLD/24_In_Memory_File_System.md)** (Composite Pattern, Trie, ReaderWriterLockSlim)
17. **[Board Game Engine (Tic-Tac-Toe & Chess Lite)](./02_Tier2_Classic_LLD/25_Board_Game_Engine.md)** (Command Pattern, Undo/Redo, Strategy, O(1) Win Checking)
18. **[Amazon Locker Delivery System](./02_Tier2_Classic_LLD/26_Amazon_Locker_System.md)** (Best-Fit Strategy, Compartment State Machine, OTP Verification, Concurrency)

### 🟢 Tier 3: DSA + LLD Hybrids
19. **[LRU Cache (Pure DSA)](./03_Tier3_DSA_Screening/15_LRU_Cache.md)** (HashMap + Doubly Linked List)
20. **[Merge Intervals](./03_Tier3_DSA_Screening/16_Merge_Intervals.md)** (Calendar Algorithm)
21. **[Invalid Transactions](./03_Tier3_DSA_Screening/17_Invalid_Transactions.md)** (Validation + Sliding Windows)

### 🟣 Tier 4: Senior Backend (Distributed & Financial Systems)
22. **[Rate Limiter](./04_Tier4_Senior_Backend/09_Rate_Limiter.md)** (Token Bucket, Leaky Bucket, Sliding Window Log)
23. **[Pub/Sub Message Queue](./04_Tier4_Senior_Backend/14_Pub_Sub.md)** (Observer, Async Channels, Backpressure)
24. **[In-Memory Transactional Key-Value Store](./04_Tier4_Senior_Backend/19_Transactional_Key_Value_Store.md)** (Stack of Scopes, ACID, Concurrency)
25. **[Digital Wallet & Double-Entry Ledger](./04_Tier4_Senior_Backend/22_Digital_Wallet_Ledger.md)** (Double-Entry Bookkeeping, Deadlock-Free Transfer, Idempotency)
26. **[Stock Exchange Matching Engine](./04_Tier4_Senior_Backend/23_Stock_Exchange_Matching_Engine.md)** (Price-Time Priority, Order Book, SortedSet, Partial Fills)
27. **[Online Auction & Real-Time Bidding System](./04_Tier4_Senior_Backend/26_Online_Auction_System.md)** (Anti-Sniping Dynamic Extension, Escrow Hold, Atomic Bid Ranking, Concurrency)

---

## 📊 Pattern → Problem Coverage Matrix (Your Study Tracker)

| Pattern / Concept | Parking | Booking | Cache | Notify | Order | Message | Wallet | Exchange | Locker | Auction |
| :--- | :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: |
| **Encapsulation** | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| **Composition**   | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| **SOLID**         | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| **Strategy**      | ✅ | ✅ | ✅ | ✅ |  |  |  |  | ✅ |  |
| **Factory**       | ✅ |  |  | ✅ |  | ✅ |  |  |  |  |
| **State**         |  | ✅ |  |  | ✅ |  | ✅ | ✅ | ✅ | ✅ |
| **Observer**      |  |  |  | ✅ | ✅ | ✅ |  |  | ✅ | ✅ |
| **Decorator**     |  |  |  | ✅ |  |  |  |  |  |  |
| **Composite**     |  |  |  |  |  |  |  |  |  |  |
| **Command**       |  |  |  |  | ✅ |  |  |  |  | ✅ |
| **DI**            | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| **Repository**    | ✅ | ✅ |  |  | ✅ |  | ✅ |  | ✅ | ✅ |
| **CQRS**          |  |  |  |  | ✅ | ✅ |  |  |  |  |
| **Messaging**     |  |  |  | ✅ | ✅ | ✅ |  |  | ✅ | ✅ |
| **Idempotency**   |  |  |  | ✅ | ✅ | ✅ | ✅ |  | ✅ | ✅ |
| **Concurrency**   | 🔴 | 🔴 | 🔴 | 🔴 | 🔴 | 🔴 | 🔴 | 🔴 | 🔴 | 🔴 |
| **Transactions**  | 🔴 | 🔴 |  |  | 🔴 |  | 🔴 |  |  | 🔴 |
| **Outbox**        |  |  |  |  | 🔴 | 🔴 |  |  |  |  |

---

## 💡 The Actual LLD Mastery Hierarchy
Study in this progression:
`OOP / Composition / SOLID` ➔ `Strategy / State / Factory / Observer / Composite / Command` ➔ `Domain Model (Entity/VO/Aggregate/Invariants)` ➔ `C# / DI / Testing` ➔ `Concurrency & Deadlock Prevention` ➔ `Persistence / CQRS` ➔ `Messaging / Events (Idempotency / Outbox)` ➔ `Production LLD`