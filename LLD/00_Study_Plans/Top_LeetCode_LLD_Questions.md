# 🏆 Top Product Company & LeetCode LLD/OOD Master Sheet

This guide brings together the **most frequently asked Low-Level Design (LLD), Object-Oriented Design (OOD), and Machine Coding problems** across top global and product-driven tech companies (FAANG, Uber, Stripe, Atlassian, Bloomberg, Swiggy, Flipkart, PhonePe, Razorpay).

These questions are **strictly language-agnostic** (applicable to C#, Java, Python, C++, Go) and are designed to test your ability to balance **clean SOLID architectural abstractions** with **optimal data structures (O(1) / O(log N) operations)** under interview conditions.

---

## 🧭 Machine Coding & LLD Round Blueprint (90-Minute Strategy)

Top product firms evaluate machine coding using a strict rubric. For the step-by-step interview execution guide, see **[06. Interview Execution Framework](../00_Core_Concepts/06_Interview_Execution_Framework.md)** and **[07. Concurrency Patterns](../00_Core_Concepts/07_Concurrency_and_Synchronization_Patterns.md)**.

| Phase | Time | Deliverables & Focus |
| :--- | :--- | :--- |
| **1. Requirement Discovery** | 0 – 15 min | Ask clarifying questions, determine input/output, isolate core vs optional features, identify concurrency & data volume constraints. |
| **2. Domain Modeling** | 15 – 30 min | Define Entities, Value Objects, Enums, and Interfaces. Identify variation points (where Strategy/Factory/State will apply). |
| **3. Implementation** | 30 – 75 min | Write modular, functional code. Keep entities separate from services/repositories. Use Dependency Injection or Factory patterns. |
| **4. Driver & Edge Tests** | 75 – 90 min | Implement a clean `Main` / Driver loop. Verify edge cases (concurrency, nulls, out of stock, empty lists). Prepare to explain tradeoffs. |

---

## 🧩 Section 1: Top LeetCode Design & OOD Questions (By Category)

### 1.1 Data Structure Heavy (DSA + LLD Hybrids)
*Focus: Combining HashMaps, Trees, and Heaps to hit optimal time and memory limits while maintaining clean class design.*

| LC # | Problem Name | Difficulty | Core DSA / Patterns | Top Companies |
| :---: | :--- | :---: | :--- | :--- |
| **146** | [LRU Cache](https://leetcode.com/problems/lru-cache/) · [*(C# Solution)*](../03_Tier3_DSA_Screening/15_LRU_Cache.md) | Medium | HashMap + Doubly Linked List (O(1) Get/Put) | Universal (Google, Amazon, Meta, Microsoft) |
| **460** | [LFU Cache](https://leetcode.com/problems/lfu-cache/) | Hard | Two HashMaps + Freq-ordered Doubly Linked Lists | Amazon, Microsoft, Apple, Uber |
| **588** | [Design In-Memory File System](https://leetcode.com/problems/design-in-memory-file-system/) · [*(C# Solution)*](../02_Tier2_Classic_LLD/24_In_Memory_File_System.md) | Hard | Trie (Prefix Tree) / Composite Pattern + RWLock | Amazon, Uber, Airbnb, Google |
| **642** | [Design Search Autocomplete System](https://leetcode.com/problems/design-search-autocomplete-system/) | Hard | Trie with Top-3 Priority Queue / Min-Heap | Google, Amazon, Microsoft, Meta |
| **2034**| [Stock Price Fluctuation](https://leetcode.com/problems/stock-price-fluctuation/) | Medium | HashMap (timestamps) + Ordered Set / Two Heaps | Goldman Sachs, Google, Citadel |
| **2353**| [Design a Food Rating System](https://leetcode.com/problems/design-a-food-rating-system/) | Medium | Two HashMaps + Balanced BST / SortedSet | DoorDash, Uber, Amazon |
| **1396**| [Design Underground System](https://leetcode.com/problems/design-underground-system/) | Medium | Two HashMaps (Active trips & Route statistics) | Bloomberg, Amazon, Meta |
| **981** | [Time Based Key-Value Store](https://leetcode.com/problems/time-based-key-value-store/) | Medium | HashMap of Sorted Lists + Binary Search (`Upper_Bound`) | Google, Netflix, Amazon, Apple |
| **1146**| [Snapshot Array](https://leetcode.com/problems/snapshot-array/) | Medium | Array of Lists containing `(snap_id, val)` + Binary Search | Google, Amazon |
| **1244**| [Design A Leaderboard](https://leetcode.com/problems/design-a-leaderboard/) | Medium | HashMap (Scores) + TreeMap / Balanced BST | Bloomberg, Amazon |
| **432** | [All O`one Data Structure](https://leetcode.com/problems/all-oone-data-structure/) | Hard | HashMap + Doubly Linked List of Value-Buckets (O(1)) | LinkedIn, Uber, Amazon |
| **380** | [Insert Delete GetRandom O(1)](https://leetcode.com/problems/insert-delete-getrandom-o1/) | Medium | HashMap (val -> index) + Dynamic Array / Vector | Meta, Google, Amazon, Microsoft |
| **381** | [Insert Delete GetRandom O(1) - Dups](https://leetcode.com/problems/insert-delete-getrandom-o1-duplicates-allowed/) | Hard | HashMap (val -> HashSet of indices) + Dynamic Array | Amazon, LinkedIn |
| **2296**| [Design a Text Editor](https://leetcode.com/problems/design-a-text-editor/) | Hard | Doubly Linked List / Two Stacks (Cursor in between) | Microsoft, Uber, Google |
| **1166**| [Design File System](https://leetcode.com/problems/design-file-system/) · [*(C# Solution)*](../02_Tier2_Classic_LLD/24_In_Memory_File_System.md) | Medium | Trie / Prefix HashMap | Airbnb, Google |
| **1206**| [Design Skiplist](https://leetcode.com/problems/design-skiplist/) | Hard | Multi-Level Linked List with Probabilistic Heights | Microsoft, ByteDance |
| **1912**| [Design Movie Rental System](https://leetcode.com/problems/design-movie-rental-system/) | Hard | Multi-index SortedSets (Available vs Rented) | Amazon, Netflix |
| **211** | [Design Add and Search Words](https://leetcode.com/problems/design-add-and-search-words-data-structure/) | Medium | Trie + Backtracking / DFS for Wildcard `.` | Meta, Amazon, Microsoft |
| **155** | [Min Stack](https://leetcode.com/problems/min-stack/) | Medium | Two Stacks or Stack storing `(value, currentMin)` | Amazon, Bloomberg, Microsoft |
| **716** | [Max Stack](https://leetcode.com/problems/max-stack/) | Hard | Doubly Linked List + Balanced BST / TreeMap | LinkedIn, Amazon |
| **706** | [Design HashMap](https://leetcode.com/problems/design-hashmap/) | Easy | Array of Buckets with Chaining (LinkedList) | Amazon, Apple, Microsoft |

---

### 1.2 Pure Object-Oriented & Game Design
*Focus: State machines, 2D board representations, rules engines, and decoupled entity relationships.*

| LC # | Problem Name | Difficulty | Core Pattern & Technique | Top Companies |
| :---: | :--- | :---: | :--- | :--- |
| **348** | [Design Tic-Tac-Toe](https://leetcode.com/problems/design-tic-tac-toe/) · [*(C# Solution)*](../02_Tier2_Classic_LLD/25_Board_Game_Engine.md) | Medium | O(1) Row/Col/Diagonal Counters (No full board scan) | Amazon, Microsoft, Meta |
| **353** | [Design Snake Game](https://leetcode.com/problems/design-snake-game/) | Medium | Deque / Queue (Snake body) + HashSet (O(1) body collision) | Amazon, Google |
| **2241**| [Design an ATM Machine](https://leetcode.com/problems/design-an-atm-machine/) | Medium | Greedy banknote selection with backtracking validation | Microsoft, Atlassian |
| **1472**| [Design Browser History](https://leetcode.com/problems/design-browser-history/) | Medium | Doubly Linked List or Two Stacks (`back` & `forward`) | Bloomberg, Amazon |
| **2254**| [Design Video Sharing Platform](https://leetcode.com/problems/design-video-sharing-platform/) | Hard | PriorityQueue / Min-Heap for recycling IDs + Maps | Google (YouTube), Meta |
| **1603**| [Design Parking System](https://leetcode.com/problems/design-parking-system/) · [*(C# Solution)*](../01_Tier1_Highest_Priority/06_Parking_Lot.md) | Easy | Counter array / Strategy allocation basis | Amazon, Microsoft |
| **341** | [Flatten Nested List Iterator](https://leetcode.com/problems/flatten-nested-list-iterator/) | Medium | Stack / Iterator Pattern + Lazy evaluation | Meta, Google, Netflix |
| **284** | [Peeking Iterator](https://leetcode.com/problems/peeking-iterator/) | Medium | Decorator Pattern over `Iterator<T>` | Google, Apple |

---

### 1.3 System Design, Streaming & Concurrency Mini-Designs
*Focus: Timestamps, sliding windows, buffer queues, and multi-thread safety.*

| LC # | Problem Name | Difficulty | Core Strategy & DSA | Top Companies |
| :---: | :--- | :---: | :--- | :--- |
| **355** | [Design Twitter](https://leetcode.com/problems/design-twitter/) | Medium | User relations map + Min-Heap for K-way merge of feeds | Twitter, Amazon, Meta |
| **362** | [Design Hit Counter](https://leetcode.com/problems/design-hit-counter/) | Medium | Circular Buffer (Buckets of 300 sec) or Deque | Dropbox, Amazon, Google |
| **379** | [Design Phone Directory](https://leetcode.com/problems/design-phone-directory/) | Medium | Queue (available numbers) + HashSet / BitSet | Google, Amazon |
| **297** | [Serialize & Deserialize Binary Tree](https://leetcode.com/problems/serialize-and-deserialize-binary-tree/) | Hard | Preorder DFS or Level-Order BFS with delimiter | Amazon, Meta, Microsoft |
| N/A | [Design Rate Limiter](../04_Tier4_Senior_Backend/09_Rate_Limiter.md) | Medium/Hard | Token Bucket, Leaky Bucket, Sliding Window Log | Stripe, Uber, Atlassian |

---

## 🏢 Section 2: Top Machine Coding Problems (Indian & Global Product Firms)

These comprehensive 90-minute design challenges are standard at **Flipkart, Swiggy, Uber, PhonePe, Razorpay, Zepto, Cred, and Atlassian**.

### 2.1 Consumer & Platform LLD
1. **[Ride-Sharing / Cab Booking System (Uber / Ola / Grab)](../02_Tier2_Classic_LLD/20_Ride_Hailing_System.md)**
   * *Key Entities:* `Rider`, `Driver`, `Trip`, `CabType`, `Location (lat, long)`.
   * *Patterns:* **Strategy** (Driver matching: Nearest vs Highest-rated; Pricing: Normal vs Surge), **State** (Trip: `Requested` -> `Accepted` -> `Arrived` -> `Started` -> `Completed`).
   * *Interview Focus:* Race condition when two drivers accept the same trip simultaneously.
2. **[Food Delivery Service (Swiggy / Zomato / DoorDash)](../02_Tier2_Classic_LLD/21_Food_Delivery_System.md)**
   * *Key Entities:* `Restaurant`, `Menu`, `MenuItem`, `Order`, `Cart`, `DeliveryPartner`, `Coupon`.
   * *Patterns:* **Strategy** (Discount/Coupon engine: Flat vs Percentage; Delivery fee calculator), **Observer** (Order status updates to customer & delivery app).
   * *Interview Focus:* Managing item availability & cart price validation before checkout.
3. **[Movie Ticket Booking System (BookMyShow / Ticketmaster)](../02_Tier2_Classic_LLD/08_Movie_Booking.md)**
   * *Key Entities:* `Theater`, `Screen`, `Seat`, `Showtime`, `Booking`, `Payment`.
   * *Patterns:* **State** (Seat: `Available` -> `Locked/Reserved (10-min TTL)` -> `Booked`), **Optimistic / Pessimistic Locking**.
   * *Interview Focus:* Handling concurrent users booking the exact same seat at the same second.
4. **[Expense Sharing Application (Splitwise)](../02_Tier2_Classic_LLD/11_Splitwise.md)**
   * *Key Entities:* `User`, `Group`, `Expense`, `Split` (`EqualSplit`, `ExactSplit`, `PercentSplit`).
   * *Patterns:* **Strategy** (Expense split calculations), **Graph Algorithms** (Min-Cash-Flow / Debt simplification using Greedy Balance approach).
   * *Interview Focus:* Accurate monetary precision (`decimal`), rounding remainders.

### 2.2 Financial & Infrastructure LLD
5. **[Digital Wallet & Ledger System (PhonePe / Razorpay / Stripe)](../04_Tier4_Senior_Backend/22_Digital_Wallet_Ledger.md)**
   * *Key Entities:* `Wallet`, `Account`, `Transaction`, `LedgerEntry`.
   * *Patterns:* **Double-Entry Bookkeeping** (Every debit has an equal credit), **Idempotency** (Prevent duplicate charges on network retries), **Deadlock Prevention** (Deterministic Account ID ordering).
   * *Interview Focus:* Atomic money transfer between two wallets without deadlocks.
6. **[In-Memory Transactional Key-Value Store (Redis-Lite)](../04_Tier4_Senior_Backend/19_Transactional_Key_Value_Store.md)**
   * *Key Commands:* `SET(k, v)`, `GET(k)`, `UNSET(k)`, `BEGIN`, `COMMIT`, `ROLLBACK`.
   * *Patterns:* **Command Pattern** / **Stack of HashMaps** (Each `BEGIN` pushes a new scope; `ROLLBACK` pops; `COMMIT` merges down).
   * *Interview Focus:* Nested transactions support with rollback to clean state.
7. **Task Management & Sprint Planner (Jira / Trello / Asana)**
   * *Key Entities:* `User`, `Project`, `Sprint`, `Task`, `Story`, `Bug`, `BoardColumn`.
   * *Patterns:* **State** (Task status: `Backlog` -> `In Progress` -> `Review` -> `Done`), **Composite** (Epics containing Stories containing Subtasks), **Observer** (Notification on assignment).
8. **[Stock Exchange / Order Matching Engine (Zerodha / Nasdaq Lite)](../04_Tier4_Senior_Backend/23_Stock_Exchange_Matching_Engine.md)**
   * *Key Entities:* `Order (Buy/Sell, Price, Quantity, Timestamp)`, `OrderBook`, `Trade`.
   * *Patterns:* **Price-Time Priority Algorithm** (Bids descending SortedSet, Asks ascending SortedSet), partial order fills.
   * *Interview Focus:* Fast O(log N) matching and partial order fills.
9. **[Chess Game / Board Game Engine (Tic-Tac-Toe & Chess Lite)](../02_Tier2_Classic_LLD/25_Board_Game_Engine.md)**
   * *Key Entities:* `Board`, `Cell`, `Piece`, `Move`, `Player`, `RulesEngine`.
   * *Patterns:* **Strategy** (Move validation for Pawn, Rook, Knight; Special rules like Castling/En Passant).

---

## 📚 Section 3: Curated External Resources & Repositories

Use these trusted community repositories and courses to practice implementations across languages:

### ⭐ Must-Bookmark GitHub Repositories
* 🌟 **[ashishps1/awesome-low-level-design](https://github.com/ashishps1/awesome-low-level-design)** — Comprehensive repository with OOP fundamentals, design patterns, UML diagrams, and end-to-end problem solutions in Java/C++.
* 🌟 **[kumaransg/LLD](https://github.com/kumaransg/LLD)** — Curated machine coding problems and design templates asked in top product tech interviews.
* 🌟 **[kush1912/Machine-Coding](https://github.com/kush1912/Machine-Coding)** — High quality object-oriented implementations of Splitwise, Parking Lot, Snake & Ladder, and Booking systems.
* 🌟 **[donnemartin/system-design-primer](https://github.com/donnemartin/system-design-primer)** — The gold standard for system design; includes dedicated section on Object-Oriented Design problems.
* 🌟 **[workattech/machinecoding](https://github.com/workattech/machinecoding)** — Guidelines, problem statements, and best practices specifically for 90-minute machine coding rounds.

### 🎥 Top Video Playlists & Learning Platforms
* **[NeetCode Design Playlist](https://neetcode.io/)**: Step-by-step walkthroughs of LeetCode design problems (LRU, Twitter, File System).
* **[Gaurav Sen (System Design / LLD)](https://www.youtube.com/@gkcs)**: Exceptional breakdown of foundational LLD concepts, rate limiters, and game engines.
* **[Shreyansh Jain (LLD Playlist)](https://www.youtube.com/@shrayansh_jain)**: In-depth implementations of Parking Lot, Cricbuzz, Elevator, and design patterns in Java/C++.
* **[Refactoring.guru Design Patterns](https://refactoring.guru/design-patterns)**: The most intuitive visual guide for Creational, Structural, and Behavioral patterns with multi-language code snippets.
* **[Work@Tech LLD & Machine Coding Practice](https://workat.tech/machine-coding/practice)**: Practice questions with time constraints simulating real interviews.