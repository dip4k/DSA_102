# 🌐 TRACK 2: System Design (HLD)

**Target:** The System Design Round.
**Philosophy:** Understanding the "Lifecycle of Data" from UI to Disk across a network.
**MIT Alignment:** *6.033 (Computer System Engineering)* & *6.824 (Distributed Systems)*.

### 🔷 Phase A: Distributed Fundamentals (Weeks 1–2)

*The theoretical constraints of the universe.*

#### 🏗️ Week 1: Scalability & Consistency Theorems

**Goal:** Speak the language of distributed systems.

* **Day 1: Scalability Dimensions**
* Vertical (Scale-up) vs. Horizontal (Scale-out).
* Stateless vs. Stateful architectures.


* **Day 2: The Theorems (MIT 6.824 Core)**
* **CAP Theorem:** Why you can't have it all.
* **PACELC:** The practical extension of CAP (Latency vs. Consistency).
* **Consistency Models:** Strong vs. Sequential vs. Eventual vs. Causal.


* **Day 3: Availability Patterns**
* Failover (Active-Passive vs. Active-Active).
* Replication lag issues.
* SLA/SLO/SLI definitions (99.9% vs 99.99%).



#### 🏗️ Week 2: Networking & Communication

**Goal:** How data moves.

* **Day 1: Protocols Deep Dive**
* TCP vs. UDP (Handshakes, congestion control).
* HTTP 1.1 (Keep-Alive) vs. HTTP/2 (Multiplexing) vs. HTTP/3 (QUIC).
* WebSockets vs. Long-Polling vs. SSE.


* **Day 2: RPC & IDL**
* REST vs. gRPC (Protobuf benefits).
* Schema evolution (Backward/Forward compatibility).


* **Day 3: Load Balancing**
* L4 (Transport) vs. L7 (Application) balancing.
* Algorithms: Round Robin, Least Connection, **Consistent Hashing** (Critical).



### 🔷 Phase B: Data & Storage Mastery (Weeks 3–4)

*Where data lives and how it is retrieved.*

#### 💾 Week 3: Databases (SQL vs. NoSQL Internals)

**Goal:** Picking the right tool for the job.

* **Day 1: Relational (ACID)**
* B-Tree Indexes vs. Hash Indexes.
* Isolation Levels (Read Committed -> Serializable).
* Sharding strategies (Key-based vs. Range-based) & Hotspots.


* **Day 2: NoSQL (BASE)**
* **LSM Trees:** How Write-Heavy DBs work (Cassandra/RocksDB).
* **Key-Value:** DynamoDB/Redis use cases.
* **Document:** MongoDB (Schemaless flexibility).
* **Wide-Column:** Cassandra (Time-series optimization).


* **Day 3: Replication Patterns**
* Single Leader vs. Multi-Leader vs. Leaderless (Quorum Reads/Writes).



#### 💾 Week 4: Caching & Asynchronous Processing

**Goal:** Speed and decoupling.

* **Day 1: Caching Strategies**
* Cache-Aside, Write-Through, Write-Back.
* Eviction Policies: LRU (Least Recently Used), LFU.
* The "Thundering Herd" problem.


* **Day 2: Message Queues & Event Streaming**
* **RabbitMQ (Queue):** Smart broker, dumb consumer.
* **Kafka (Log):** Dumb broker, smart consumer (Offset management).
* Pub-Sub patterns.
* Idempotency in consumers (Processing messages exactly once).



### 🔷 Phase C: Concrete System Design (Weeks 5–7)

*Putting it all together.*

#### 🏛️ Week 5: The "Easy" Problems (Building Blocks)

* **Day 1: Design a URL Shortener (TinyURL)**
* Focus: Unique ID generation (KGS vs. DB Auto-inc vs. Snowflake), 301 vs 302 redirect.


* **Day 2: Design a Rate Limiter**
* Focus: Algorithms (Token Bucket, Leaky Bucket), Distributed counters (Redis Lua).


* **Day 3: Design a Key-Value Store**
* Focus: Consistent Hashing, Gossip Protocol, Merkle Trees.



#### 🏛️ Week 6: The "Medium" Problems (User Applications)

* **Day 1: Design a Chat System (WhatsApp/Discord)**
* Focus: WebSockets, "Last Seen" presence, Message storage (HBase/Cassandra).


* **Day 2: Design a Social Feed (Instagram/Twitter)**
* Focus: Fan-out on Write (Push) vs. Fan-out on Read (Pull), Hybrid approach.


* **Day 3: Design Typeahead / Autocomplete**
* Focus: Tries, Prefix Hash, Updating frequently searched terms.



#### 🏛️ Week 7: The "Hard" Problems (Scale & Complexity)

* **Day 1: Design a Web Crawler**
* Focus: Politeness, DNS resolution, Distributed queue, Deduplication.


* **Day 2: Design Uber/Grab**
* Focus: Geo-hashing / QuadTrees, Matching algorithms, Locking rides.


* **Day 3: Design a Collaborative Editor (Google Docs)**
* Focus: Operational Transformation (OT) vs. CRDTs.



### 🔷 Phase D: The "Senior" Edge (Week 8)

* **Day 1: Observability**
* Metrics, Logging, Tracing (Distributed Tracing).


* **Day 2: Security**
* OAuth 2.0 / OIDC flows.
* Rate limiting for DDoS.


* **Day 3: Back-of-Envelope Math**
* Calculating QPS, Storage per year, Bandwidth requirements instantly.