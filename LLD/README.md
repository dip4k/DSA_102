# 🚀 Senior .NET LLD & Architecture Master Curriculum

Welcome to the definitive **Low-Level Design (LLD)**, **Object-Oriented Design (OOD)**, and **Backend Architecture** curriculum for Senior/Lead Engineers preparing for top global and Indian product companies.

This repository pairs production-grade C# 12 / .NET 8, Domain-Driven Design (DDD), and thread-safe concurrency with strict 90-minute Machine Coding and 45-minute OOD interview playbooks.

---

## 🗺️ 1. Start Here: The Roadmaps & Syllabuses
Before writing code, understand the strategy. We use an **8-Layer Master Map**, the **6 Reusable Problem Archetypes**, and company-specific evaluation rubrics.

* [🏢 Top 30 Product Companies LLD Interview Directory & Prep Guide](./00_Study_Plans/Top_30_Product_Companies_LLD_Interview_Guide.md)
* [🏆 The Master 80/20 Roadmap (The 8-Layer Map & Problem Families)](./Master_80_20_Roadmap.md)
* [📅 The 4-Phase Senior .NET Syllabus](./00_Study_Plans/DotNet_Senior_Product_Ready_Syllabus.md)
* [🔥 Top LeetCode LLD & Product Company Questions](./00_Study_Plans/Top_LeetCode_LLD_Questions.md)

---

## 🧠 2. Core Concepts (The Theoretical Foundation & Playbooks)
Do not skip these. In a senior interview, defending your architecture is just as important as writing it.

* [01. OOP, SOLID Principles & Class Relationships (Association/Aggregation/Composition)](./00_Core_Concepts/01_OOP_and_SOLID.md)
* [02. Design Patterns (The 80/20 Cheat Sheet & GoF 23 Directory)](./00_Core_Concepts/02_Design_Patterns_Cheat_Sheet.md)
* [03. Domain Modeling & DDD](./00_Core_Concepts/03_Domain_Modeling_and_DDD.md)
* [04. C# DI & Concurrency](./00_Core_Concepts/04_CSharp_DI_and_Concurrency.md)
* [05. Backend Architecture (Outbox, CQRS, Idempotency)](./00_Core_Concepts/05_Backend_Architecture.md)
* [06. Interview Execution Framework (45m OOD vs 90m Machine Coding)](./00_Core_Concepts/06_Interview_Execution_Framework.md)
* [07. Concurrency & Synchronization Patterns](./00_Core_Concepts/07_Concurrency_and_Synchronization_Patterns.md)
* [08. LLD Problem Families & 4-Stage Evolution (V1 → V4)](./00_Core_Concepts/08_LLD_Problem_Families_and_Evolution.md)
* [09. UML Class Diagrams (Notation, Mermaid Cheat Sheet & Speed Template)](./00_Core_Concepts/09_UML_Class_Diagrams.md)
* [10. Interview Quick Reference, Decision Trees & Checklists](./00_Core_Concepts/10_Interview_Quick_Reference_and_Checklists.md)

---

## 💻 3. The 37-Problem Portfolio (Code Implementations)
These are production C# architectural templates. Each file contains domain modeling, thread-safe code, Mermaid architectural diagrams, and a dedicated **"🗣️ Interviewer Discussion & Tradeoffs"** section.

### 🔥 Tier 1: Highest Priority (The Golden 8)
1. [Meeting Room Booking (Concurrency & Intervals)](./01_Tier1_Highest_Priority/01_Meeting_Room_Booking.md)
2. [Generic Message Processor (Factory + DI)](./01_Tier1_Highest_Priority/02_Generic_Message_Processor.md)
3. [DDD Order Management (CQRS & Invariants)](./01_Tier1_Highest_Priority/03_DDD_Order_Management.md)
4. [DDD + CQRS + Azure Service Bus (Outbox Pattern)](./01_Tier1_Highest_Priority/04_DDD_CQRS_AzureServiceBus.md)
5. [Resilient API Aggregator (Polly + WhenAll)](./01_Tier1_Highest_Priority/05_Resilient_API_Aggregator.md)
6. [Parking Lot (Strategy + Composition)](./01_Tier1_Highest_Priority/06_Parking_Lot.md)
7. [Custom Cache (LRU + TTL + ReaderWriterLockSlim)](./01_Tier1_Highest_Priority/07_Custom_Cache.md)
8. [Notification System (Decorator + Strategy)](./01_Tier1_Highest_Priority/08_Notification_System.md)

### 🟡 Tier 2: Classic LLD & Machine Coding (State & Strategy Mastery)
9. [Movie Ticket Booking (Redis TTL vs SQL Lock)](./02_Tier2_Classic_LLD/09_Movie_Booking.md)
10. [Elevator System (SCAN Algorithm)](./02_Tier2_Classic_LLD/10_Elevator.md)
11. [Splitwise (Graphs + Decimals)](./02_Tier2_Classic_LLD/11_Splitwise.md)
12. [Vending Machine (State Pattern)](./02_Tier2_Classic_LLD/12_Vending_Machine.md)
13. [Logger (Chain of Responsibility + Async Logging)](./02_Tier2_Classic_LLD/13_Logger.md)
14. [Task Management System (Jira Lite — Composite + Command + Observer)](./02_Tier2_Classic_LLD/14_Task_Management_System.md)
15. [Ride-Hailing System (Uber / Ola)](./02_Tier2_Classic_LLD/15_Ride_Hailing_System.md)
16. [Food Delivery System (Swiggy / Zomato)](./02_Tier2_Classic_LLD/16_Food_Delivery_System.md)
17. [In-Memory File System (Composite Pattern + Trie)](./02_Tier2_Classic_LLD/17_In_Memory_File_System.md)
18. [Board Game Engine (Tic-Tac-Toe & Chess Lite)](./02_Tier2_Classic_LLD/18_Board_Game_Engine.md)
19. [Amazon Locker System (Best-Fit Strategy + OTP Verification)](./02_Tier2_Classic_LLD/19_Amazon_Locker_System.md)
20. [Library Management System (Catalog Search + Pluggable Fines + Waitlist)](./02_Tier2_Classic_LLD/20_Library_Management_System.md)
21. [ATM System (State Machine + Thread-Safe Cash Dispenser)](./02_Tier2_Classic_LLD/21_ATM_System.md)
22. [Hotel Management System (Interval Overlaps + Dynamic Pricing)](./02_Tier2_Classic_LLD/22_Hotel_Management_System.md)
23. [Snake and Ladder Game (Dice Strategy + Cycle Detection)](./02_Tier2_Classic_LLD/23_Snake_and_Ladder.md)
24. [Cricbuzz / Live Cricket Scoreboard (Strike Rotator + Match Observers)](./02_Tier2_Classic_LLD/24_Cricbuzz_Cricket_Scoreboard.md)
25. [Coupon & Discount Rule Engine (Composite Specification + Pipeline)](./02_Tier2_Classic_LLD/25_Coupon_and_Discount_Engine.md)

### 🟢 Tier 3: DSA + LLD Hybrids (Optimal Lookups & Top-K Engines)
26. [LRU & LFU Cache Mastery (LeetCode 146 & 460)](./03_Tier3_DSA_Screening/26_LRU_and_LFU_Cache.md)
27. [Merge Intervals (Calendar Algorithm)](./03_Tier3_DSA_Screening/27_Merge_Intervals.md)
28. [Invalid Transactions (Validation + Sliding Windows)](./03_Tier3_DSA_Screening/28_Invalid_Transactions.md)
29. [Search Autocomplete System (Trie + Top-K PriorityQueue)](./03_Tier3_DSA_Screening/29_Search_Autocomplete_System.md)
30. [Social Media News Feed / Twitter Lite (K-Way PriorityQueue Merge)](./03_Tier3_DSA_Screening/30_Social_Media_Feed_Twitter.md)

### 🟣 Tier 4: Senior Backend (Distributed & Financial Systems)
31. [Rate Limiter (Token Bucket + Interlocked)](./04_Tier4_Senior_Backend/31_Rate_Limiter.md)
32. [Pub/Sub Message Queue (Observer + Backpressure Channels)](./04_Tier4_Senior_Backend/32_Pub_Sub.md)
33. [In-Memory Transactional Key-Value Store (Redis-Lite)](./04_Tier4_Senior_Backend/33_Transactional_Key_Value_Store.md)
34. [Digital Wallet & Double-Entry Ledger (Fintech Deadlock-Free Transfer)](./04_Tier4_Senior_Backend/34_Digital_Wallet_Ledger.md)
35. [Stock Exchange Order Matching Engine (Price-Time Priority SortedSet)](./04_Tier4_Senior_Backend/35_Stock_Exchange_Matching_Engine.md)
36. [Online Auction & Real-Time Bidding Engine (Anti-Sniping + Escrow)](./04_Tier4_Senior_Backend/36_Online_Auction_System.md)
37. [URL Shortener (TinyURL — Base62 + Distributed Lease Allocation)](./04_Tier4_Senior_Backend/37_URL_Shortener.md)

---

## 🔗 4. Essential External Resources & Reading
To achieve true mastery, cross-reference the concepts in this repo with these industry-standard resources:

### LLD & Machine Coding Interview Repositories (Language-Agnostic)
* **[CrackingWalnuts LLD](https://crackingwalnuts.com/low-level-design):** Pioneer of the repeatable 6-step framework, V1 $\rightarrow$ V4 evolution, and 90+ multi-language interview problems.
* **[ashishps1/awesome-low-level-design](https://github.com/ashishps1/awesome-low-level-design):** The top community repository covering OOP fundamentals, 23 GoF design patterns, UML class diagrams, and multi-language LLD code.
* **[AlgoMaster LLD Course](https://algomaster.io/learn/lld/how-to-learn-lld):** Visual tutorials and interactive practice for OOP and Low-Level Design interviews.
* **[kumaransg/LLD](https://github.com/kumaransg/LLD):** Machine coding problems asked at Flipkart, Swiggy, Uber, and PhonePe.
* **[workat.tech Machine Coding Practice](https://workat.tech/machine-coding/practice):** Structured 90-minute practice problems for machine coding rounds.
* **[donnemartin/system-design-primer](https://github.com/donnemartin/system-design-primer):** The benchmark repo for system design and object-oriented design questions.

### Design Patterns & Refactoring
* **[Refactoring.guru](https://refactoring.guru/design-patterns/csharp):** The absolute best visual guide to the GoF design patterns in C#.
* **[Martin Fowler - Catalog of Patterns of Enterprise Application Architecture](https://martinfowler.com/eaaCatalog/):** The bible for Repository, Unit of Work, and Domain Model patterns.

### Domain-Driven Design (DDD) & CQRS
* **[Milan Jovanović (YouTube/Blog)](https://www.milanjovanovic.tech/):** Excellent modern .NET 8+ tutorials on Clean Architecture, DDD, and the Outbox Pattern.
* **[CodeOpinion / Derek Comartin (YouTube)](https://www.youtube.com/channel/UC3RKA4vunFAfrfxiJhPEplw):** The best resource for understanding Event Driven Architecture, messaging protocols, and avoiding distributed monoliths.
* **[Microsoft: DDD and CQRS](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/):** Official Microsoft guidance on implementing DDD in .NET microservices.

### Cloud Architecture & Distributed Systems
* **[Microsoft Azure Cloud Design Patterns](https://learn.microsoft.com/en-us/azure/architecture/patterns/):** Essential reading for Circuit Breaker, Retry, Strangler Fig, and CQRS patterns.
* **[ByteByteGo (Alex Xu)](https://bytebytego.com/):** For scaling these LLD implementations into High-Level Design (HLD).
* **[The Transactional Outbox Pattern](https://microservices.io/patterns/data/transactional-outbox.html):** Deep dive into atomicity in microservices by Chris Richardson.