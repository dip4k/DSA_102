# 🏛️ TRACK 2: Scalable System Design (HLD)

**Target:** The "System Design" Round.
**Philosophy:** "It’s not about knowing the tools; it’s about knowing the *trade-offs* between them."
**MIT Alignment:** 6.824 (Distributed Systems) + 6.830 (Database Systems).

### 🔶 Phase A: Distributed Fundamentals (Weeks 1–3)

**Week 1: Scalability & The Theorem Space**

* **Scaling:** Vertical vs. Horizontal. Stateless vs. Stateful services.
* **CAP Theorem:** CP vs. AP systems. When to sacrifice consistency.
* **PACELC Theorem:** Latency vs. Consistency when partitions *aren't* happening.
* **Hashing:** Modulo hashing vs. Consistent Hashing (Virtual nodes, ring stability).

**Week 2: Communication & Coordination**

* **Protocols:** REST (JSON) vs. RPC (gRPC/Protobuf) vs. GraphQL. Performance implications.
* **Push vs. Pull:** Long Polling, WebSockets, Server-Sent Events (SSE).
* **Consensus (Conceptual):** Paxos/Raft basics. Leader election. Split-brain scenarios.
* **Failure Models:** Network partitions, Byzantine failures (briefly), Retry logic & Exponential Backoff.

**Week 3: Consistency & Transactions**

* **ACID vs. BASE:** Strong consistency vs. Eventual consistency.
* **Isolation Levels:** Read Uncommitted to Serializable. What is "Snapshot Isolation"?
* **Distributed Transactions:** 2-Phase Commit (2PC), Sagas Pattern (Choreography vs. Orchestration).

### 🔶 Phase B: The Building Blocks (Weeks 4–6)

**Week 4: Data Storage & Retrieval**

* **Relational (SQL):** Sharding strategies, Master-Slave Replication, Normalization.
* **NoSQL Engines:**
* *Key-Value (Dynamo/Redis):* Fast lookups.
* *Wide-Column (Cassandra/HBase):* Write-heavy workloads, LSM Trees vs. B-Trees.
* *Document (Mongo):* Flexible schema.


* **Blob Storage:** S3 internals (Immutable objects, CDN integration).

**Week 5: Caching & Performance**

* **Caching Strategies:** Read-Through, Write-Through, Write-Back, Look-Aside.
* **Eviction:** LRU, LFU, TTL.
* **Global Caching:** CDNs (Edge locations), Cache Stampede/Thundering Herd mitigation.
* **Load Balancing:** L4 (Transport) vs. L7 (Application). Algorithms (Round Robin, Least Connection, Weighted).

**Week 6: Async Processing & Queues**

* **Message Queues:** RabbitMQ (Push) vs. Kafka (Pull/Log-based).
* **Kafka Internals:** Topics, Partitions, Consumer Groups, Offset management, Log compaction.
* **Stream Processing:** Batch vs. Stream (Lambda vs. Kappa Architecture).

### 🔶 Phase C: The "Design A..." Case Studies (Weeks 7–10)

*Apply the blocks. Senior candidates drive the requirements.*

**Week 7: Read-Heavy Systems**

* **Design a URL Shortener (TinyURL):** ID generation (KGS vs. DB auto-increment), Redirection (301 vs. 302).
* **Design a Pastebin:** Object storage, cleanup policies.
* **Design a News Feed (Twitter/FB):** Fan-out-on-write vs. Fan-out-on-read. Timeline generation.

**Week 8: Write-Heavy & Consistency Systems**

* **Design a Chat System (WhatsApp/Discord):** Connection persistence, message ordering, "Seen" status.
* **Design a Rate Limiter:** Distributed counting, sliding window counters in Redis (Lua scripts).
* **Design a Web Crawler:** Frontier management, politeness, DNS resolution, deduping.

**Week 9: Geospatial & Real-Time Systems**

* **Design Uber/Grab:** Geospatial indexing (QuadTrees vs. Geohash), Driver matching state machines.
* **Design Yelp/Nearby Friends:** Radius search, static vs. dynamic location data.
* **Design Live Leaderboard:** Real-time updates, Redis Sorted Sets.

**Week 10: Specialized Systems**

* **Design Typeahead/Search Suggest:** Tries, Prefix matching, Caching top queries.
* **Design YouTube/Netflix:** Video chunking, Adaptive Bitrate Streaming (HLS/DASH), CDN optimization.
* **Design Google Drive/Dropbox:** Block-level deduplication, synchronization conflict resolution.

### 🔶 Phase D: Senior Engineering Excellence (Week 11)

**Week 11: Production Readiness (The "L6" Differentiator)**

* **Observability:** Metrics (Prometheus), Logging (ELK), Tracing (Jaeger/Zipkin).
* **Deployment:** Blue/Green, Canary releases, Feature Flags.
* **Capacity Estimation:** Back-of-the-envelope math. Calculating QPS, Storage, Bandwidth requirements rapidly.