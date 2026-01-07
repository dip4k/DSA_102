# 🏛️ FAANG-Level High-Level Design (HLD) Mastery Syllabus

**Target:** Senior/Staff-level System Design, Architecture, and Scalability Rounds  
**Philosophy:** Design systems that scale to billions of requests, remain available under failures, and can be operated reliably  
**Duration:** 8-12 weeks (intensive study, 35-45 hours/week)  
**MIT Alignment:** 6.033 (Computer System Engineering) + 6.824 (Distributed Systems)

---

## 📋 Quick Overview

This syllabus prepares you for the **core FAANG system design round** where you are asked: "Design Twitter," "Design Uber," etc. Success requires:

1. **Clarify Requirements:** Ask smart questions about functional & non-functional requirements
2. **Back-of-Envelope Estimation:** Justify your design with numbers (QPS, storage, bandwidth)
3. **Component Selection:** Know *when* to use SQL vs NoSQL, caches, queues, CDNs
4. **Trade-off Analysis:** CAP theorem, consistency models, cost vs performance
5. **Scaling Strategy:** Identify bottlenecks and resolve them systematically
6. **Operational Concerns:** Observability, monitoring, failover, graceful degradation

---

## PHASE A: Distributed Systems Fundamentals (Weeks 1–3)

### Goal
Master the theoretical constraints and communication patterns of distributed systems.

### Week 1: Scalability & Consistency Theorems

#### Day 1: Scaling Dimensions
**Topics:**
- **Vertical Scaling (Scale-up):** Add more CPU, RAM, disk to a single server
  - Advantages: Simple, no distributed complexity
  - Limits: Hardware limits, cost grows non-linearly

- **Horizontal Scaling (Scale-out):** Add more servers
  - Advantages: Handles massive scale, fault-tolerant
  - Challenges: Network calls, data consistency, split-brain scenarios

- **Stateless vs Stateful Services:**
  - Stateless (recommended): Any server can handle any request
  - Stateful: Requires session affinity or distributed state storage
  - Example: Web API servers (stateless) vs. game servers (stateful)

**Interview Scenario:**
> *Interviewer: "Your design uses 10 servers. You want to handle 10x traffic. What do you do?"*
> 
> *Answer:* "For a stateless API layer, I add more servers behind a load balancer. For stateful components like caches, I use consistent hashing to redistribute data."

---

#### Day 2-3: CAP & PACELC Theorems
**Topics:**

**CAP Theorem:**
- **Consistency (C):** All nodes see the same data
- **Availability (A):** Every request gets a response
- **Partition Tolerance (P):** System works despite network partitions

Key insight: You can guarantee at most 2 of 3 in a distributed system.

**Trade-offs:**
- **CP Systems:** Prioritize consistency; sacrifice availability during partitions
  - Examples: Traditional SQL databases with strong consistency
  - Use case: Financial transactions

- **AP Systems:** Prioritize availability; accept temporary inconsistency
  - Examples: NoSQL databases with eventual consistency
  - Use case: Social media feeds, user timelines

**PACELC Theorem (The Practical Extension):**
Extends CAP to normal (non-partition) conditions:
- "If there is a network partition, the system must choose between consistency and availability"
- "Else (when the network is healthy), the system must choose between latency and consistency"

**Real-world example:**
- DynamoDB on a partition: chooses Availability (AP)
- DynamoDB in normal operation: chooses low Latency over strong Consistency

**Exercises:**
- For each system (Twitter, Uber, Banking): What should it choose? CP vs AP? Why?
- Trace through failure scenarios: What happens when a data center goes down?

---

#### Day 4: Consistency Models
**Topics:**

**Strong Consistency:**
- After a write, all subsequent reads see the latest value
- Easiest to reason about, hardest to scale
- Example: Single-leader database

**Eventual Consistency:**
- Reads may temporarily return stale data, but converge to the same value
- Scales better, requires client awareness
- Example: Distributed cache, social media feeds

**Causal Consistency:**
- Causally related events are seen in order
- Balances consistency and performance
- Example: Comment thread on a post (your comment appears after the post, always)

**Read-Your-Writes Consistency:**
- A client always sees their own writes immediately
- Common in session-based systems
- Example: After you update your profile, you immediately see the updated data

**Exercises:**
- For Twitter's feed: What consistency model? (Eventual - you may see slightly old posts)
- For a bank transfer: What consistency model? (Strong - money can't be duplicated)

---

#### Day 5: Availability Patterns
**Topics:**

**Failover Strategies:**
- **Active-Passive:** Primary server serves requests, replica stands by
  - Failover on detection: automatic or manual
  - Disadvantage: Standby capacity wasted

- **Active-Active:** Multiple servers handle requests
  - Requires coordination (distributed state)
  - Better resource utilization
  - Example: Multi-region setup with eventual consistency

**Replication Lag:**
- In eventually consistent systems, replicas lag behind primary
- Can cause inconsistencies if not handled
- Example: User updates profile, but old cache is served to friends temporarily

**SLA/SLO/SLI Definitions:**
- **SLA (Service Level Agreement):** Contractual promise (e.g., 99.9% uptime)
- **SLO (Service Level Objective):** Internal target (e.g., maintain 99.95% uptime)
- **SLI (Service Level Indicator):** Measurement (e.g., "We achieved 99.96% uptime")

**Mapping uptime to downtime:**
| Availability | Downtime per Year |
|---|---|
| 99% | 3.65 days |
| 99.9% | 8.76 hours |
| 99.99% | 52.6 minutes |
| 99.999% | 5.26 minutes |

---

### Week 2: Communication Patterns & Networking

#### Day 1: Protocol Deep Dive
**Topics:**

**TCP vs UDP:**
- **TCP:** Connection-oriented, reliable, ordered, slower
  - Use: HTTP, databases, anything requiring reliability

- **UDP:** Connectionless, unreliable, unordered, faster
  - Use: Video streaming, online games, DNS, where some loss is acceptable

**HTTP Evolution:**
- **HTTP/1.1:** One request-response per connection (or keep-alive), sequential
  - Limitation: Head-of-line blocking

- **HTTP/2:** Multiplexing over a single connection, binary framing
  - Improvement: Multiple streams, server push
  - Still half-duplex (request-response)

- **HTTP/3:** QUIC transport, faster connection establishment, better mobile experience
  - 0-RTT connection resumption

**WebSockets vs Long-Polling vs SSE:**
- **WebSockets:** Full-duplex, persistent connection, low latency
  - Use: Chat, live notifications, real-time updates

- **Long-Polling:** Client polls server repeatedly, server responds when data available
  - Use: Older browsers, simple notification systems
  - Drawback: Resource inefficient (many connections)

- **Server-Sent Events (SSE):** Server pushes to client over persistent HTTP connection
  - Use: Live updates, event streaming
  - Limitation: One-way (server → client)

**Interview Scenario:**
> *"Design a notification system for 100 million users. Should we use WebSockets or long-polling?"*
> 
> *Answer:* "WebSockets for individual notifications (one connection per user = 100M connections). Too many! Instead, we'd use long-polling with a batch service. Alternatively, we could use a pub-sub with fan-out to a user's notification queue."

---

#### Day 2: RPC & Service Communication
**Topics:**

**REST (HTTP + JSON):**
- Pros: Simple, human-readable, good for web APIs
- Cons: Verbose, not ideal for low-latency services

**gRPC (HTTP/2 + Protocol Buffers):**
- Pros: Fast, strongly typed, supports streaming
- Cons: Steeper learning curve, less browser-friendly

**GraphQL:**
- Pros: Flexible querying, only request what you need
- Cons: Complex caching, query DoS vulnerabilities

**Message Queues (async RPC):**
- Decouple services, handle traffic spikes
- Example: Kafka, RabbitMQ

**Schema Evolution:**
- Backward compatibility: old code can read new data
- Forward compatibility: new code can read old data
- Critical when services deploy independently

**Exercises:**
- Design APIs for a chat system using REST, gRPC, and WebSockets. Pros/cons?
- Handle schema evolution: Add a new field to User without breaking clients

---

#### Day 3-4: Load Balancing & Consistent Hashing
**Topics:**

**Load Balancing Algorithms:**
- **Round Robin:** Distribute evenly, ignoring server load
  - Pros: Simple
  - Cons: Doesn't account for uneven load

- **Least Connection:** Send request to server with fewest active connections
  - Pros: Better than round-robin for long-lived connections

- **Weighted Round Robin:** Allocate based on server capacity

- **Consistent Hashing:** Critical for distributed caches and databases
  - Hash-based: `hash(key) % number_of_servers` breaks when servers added/removed
  - Consistent Hashing: Virtual nodes on a ring, only 1/N data moves on rehashing

**L4 vs L7 Load Balancing:**
- **L4 (Transport):** Balances at TCP/UDP level (fast, limited intelligence)
- **L7 (Application):** Balances based on HTTP path, headers, cookies (flexible, slower)

**Interview Scenario:**
> *"Your system has 1000 cache servers. One dies. How much data is lost/rehashed?"*
> 
> *Answer with Consistent Hashing:* "With virtual nodes, roughly 1/1000 data goes to neighboring nodes. With modulo hashing, 100% of requests get wrong server. Consistent hashing scales much better."

**Exercises:**
- Implement consistent hashing with virtual nodes
- Trace through failure scenarios: Server dies, how is data redistributed?

---

#### Day 5: Consensus & Coordination
**Topics:**

**Consensus Problems:**
- Multiple servers must agree on a value despite failures
- Example: Choosing a new primary when the current one dies

**Paxos (Theoretical):**
- Complex, but guarantees safety
- Rarely used in practice

**Raft (Practical):**
- Leader-based consensus
- Much simpler than Paxos
- Used in etcd, Consul

**Key Concepts:**
- **Leader Election:** Servers vote for a new leader
- **Split-brain:** Network partition causes two leaders (dangerous!)
- **Quorum:** Majority of servers must agree (prevents split-brain)

**Exercises:**
- For a distributed lock service: Use Raft or ZooKeeper?
- Trace through a failure: Primary dies, new leader elected, minority partition recovers

---

### Week 3: Consistency Models & Transactions

#### Day 1-2: ACID vs BASE
**Topics:**

**ACID (Strong Consistency):**
- **Atomic:** All or nothing
- **Consistent:** Data integrity constraints maintained
- **Isolated:** Transactions don't interfere
- **Durable:** Committed data survives failures

**BASE (Eventual Consistency):**
- **Basically Available:** System responds even during failures
- **Soft state:** Data can change without external input
- **Eventually Consistent:** Data converges over time

**Trade-offs:**
- ACID: safer but slower, harder to scale
- BASE: faster, easier to scale, but more complex for client

---

#### Day 3-4: Isolation Levels & Transactions
**Topics:**

**SQL Isolation Levels (strictest to weakest):**
1. **Serializable:** Transactions behave as if run sequentially
   - Safest, slowest

2. **Repeatable Read:** Transaction sees consistent snapshot
   - Prevents non-repeatable reads and phantom reads (mostly)

3. **Read Committed:** Only see committed data
   - Faster, allows dirty reads in some DBs

4. **Read Uncommitted:** Can see uncommitted data
   - Fastest, least safe

**Distributed Transactions:**
- **2-Phase Commit (2PC):** Coordinator ensures all-or-nothing
  - Blocking, synchronous, not fault-tolerant

- **Sagas Pattern:** Choreography or Orchestration
  - Async, better fault tolerance, harder to reason about
  - Example: Order → Payment → Inventory update (each is a separate transaction)

**Exercises:**
- Design a payment system: How do you ensure money isn't duplicated or lost?
- Model a distributed order fulfillment: Order → Stock → Payment. What if payment fails?

---

#### Day 5: Practical Consistency in Scale
**Topics:**
- Eventual consistency strategies
- Conflict resolution: Last-write-wins, application-level
- Versioning and vector clocks (conceptual)
- Idempotency: the key to safe retries

---

## PHASE B: Data & Storage Mastery (Weeks 4–6)

### Goal
Choose the right database/storage for each use case. Understand internals to justify decisions.

### Week 4: Relational vs NoSQL Databases

#### Day 1-2: Relational Databases (SQL)
**Topics:**

**When to Use:**
- Structured data with clear schema
- Complex queries with joins
- Strong consistency requirements
- Data integrity important (constraints, foreign keys)

**Internals:**
- **B-Tree Indexes:** O(log N) lookups, efficient range scans
  - Used for primary keys and indexed columns

- **Query Planning:** Optimizer chooses execution path
  - Statistics: table size, index selectivity
  - Cost: estimated disk I/O

**Scaling SQL:**
- **Sharding:** Split data across servers
  - By user ID: All of user 123's data on server X
  - By hash: Consistent hashing of key
  - Disadvantage: Cross-shard queries are slow

- **Replication:** Primary-Replica setup
  - Reads from replicas (low consistency)
  - Writes to primary (strong consistency)

**Bottleneck in SQL at scale:**
- Joins across shards (expensive)
- Global transactions (slow)
- Solution: Denormalize, move logic to application

---

#### Day 2-3: NoSQL Databases
**Topics:**

**Key-Value Stores (DynamoDB, Redis):**
- **Access:** Only by key, no complex queries
- **Performance:** O(1) average case
- **Use Case:** Caches, sessions, leaderboards, shopping carts
- **Scaling:** Sharding by key

**Document Stores (MongoDB):**
- **Model:** JSON-like documents
- **Queries:** Some range queries, filtering
- **Flexibility:** Schema-less (can be a pro or con)
- **Use Case:** User profiles, blog posts, flexible data

**Wide-Column Stores (Cassandra, HBase):**
- **Model:** Rows with many columns, grouped into column families
- **Optimization:** Write-optimized (LSM trees)
- **Scaling:** Excellent horizontal scaling, high availability
- **Use Case:** Time-series data, analytics, massive scale

**Internals - LSM Trees (Log-Structured Merge):**
- Writes go to in-memory structure (MemTable)
- Flushed to disk as SSTable (Sorted String Table)
- Compaction: merge SSTables to optimize reads
- Advantage: Sequential disk I/O (fast), efficient for write-heavy workloads
- Disadvantage: Reads may hit multiple SSTables

**Search Engines (Elasticsearch):**
- **Index:** Inverted index for fast text search
- **Use Case:** Full-text search, log analysis
- **Scaling:** Sharding by document

---

#### Day 4: Replication Patterns
**Topics:**

**Single-Leader Replication:**
- Primary handles all writes, replicas handle reads
- Advantages: Strong consistency for writes, simple
- Disadvantages: Single point of failure for writes

**Multi-Leader Replication:**
- Multiple primaries accept writes
- Advantages: High availability, lower latency
- Disadvantages: Conflict resolution required, complex

**Leaderless Replication (Quorum Reads/Writes):**
- Read: Query multiple replicas, return most recent
- Write: Write to multiple replicas
- Advantages: Tolerates failures well
- Disadvantages: Eventual consistency, more complex

**Exercises:**
- For Twitter: Primary-replica? Multi-leader? Why?
- Design a geo-distributed system: primary in US, replicas in EU and Asia

---

### Week 5: Caching & Performance Optimization

#### Day 1-2: Caching Strategies
**Topics:**

**Cache-Aside (Lazy Loading):**
1. Check cache
2. If miss, fetch from database
3. Update cache
- Pros: Simple, only cache what's used
- Cons: Slow on miss, risk of stale data

**Write-Through:**
1. Write to cache
2. Write to database
- Pros: Cache always consistent with database
- Cons: Two writes (slower)

**Write-Behind (Write-Back):**
1. Write to cache
2. Async write to database
- Pros: Fast writes, good throughput
- Cons: Risk of data loss if cache fails

**Eviction Policies:**
- **LRU (Least Recently Used):** Evict least recently accessed
- **LFU (Least Frequently Used):** Evict least frequently used
- **TTL (Time To Live):** Entries expire after a time

**Cache Stampede / Thundering Herd:**
- Problem: When a cache entry expires, many requests hammer the database
- Solutions:
  - Probabilistic early expiration
  - Use of semaphores (only one request fetches, others wait)
  - Cache warming

**Exercises:**
- Design a cache for a social feed. What's the miss rate? What strategy?
- Handle cache stampede: 100k users accessing the same entry at once (10 second expiry)

---

#### Day 2-3: Content Delivery Networks (CDNs)
**Topics:**

**What is a CDN?**
- Geographically distributed caches of static content
- Serves content from cache closest to user
- Reduces latency and origin server load

**How it works:**
- Origin server: stores authoritative content
- Edge servers: CDN servers worldwide
- Routing: DNS returns closest edge server

**Cache Invalidation:**
- TTL-based: Content expires after time
- Purge-based: Manually invalidate on updates
- Version-based: URL includes version hash

**Use Cases:**
- Images, CSS, JS (static assets)
- Large video files (HLS/DASH streaming)
- API responses (if read-heavy)

---

#### Day 4: Global Load Balancing
**Topics:**
- Route users to nearest data center
- Achieve low latency globally
- Handle data center failures

---

#### Day 5: Database Optimization
**Topics:**
- Indexes: When to create, impact on writes
- Query optimization: EXPLAIN plans
- Connection pooling: Reuse connections
- Denormalization: Trading consistency for speed

**Exercises:**
- Design indexes for a Twitter-like query: "Get tweets from users I follow"
- Denormalize user follower count: How do you keep it fresh?

---

### Week 6: Queues, Streams & Async Processing

#### Day 1-2: Message Queues
**Topics:**

**RabbitMQ (Queue-based):**
- **Model:** Smart broker, dumb consumer
- **Semantics:** Explicit acks, dead-letter queues
- **Use Case:** Task distribution, microservices

**Kafka (Log-based):**
- **Model:** Dumb broker (append-only log), smart consumer
- **Semantics:** Consumer groups, offsets, replay
- **Use Case:** Event streaming, audit logs, real-time analytics

**Comparison:**
| Feature | RabbitMQ | Kafka |
|---|---|---|
| Throughput | Medium | High |
| Latency | Low | Medium |
| Retention | Short | Long (configurable) |
| Replay | No | Yes |
| Complexity | Lower | Higher |

**Processing Guarantees:**
- **At-most-once:** Message may be lost
- **At-least-once:** Message processed at least once (may duplicate)
- **Exactly-once:** Complex, but possible with idempotent operations

---

#### Day 3-4: Stream Processing
**Topics:**

**Batch Processing (Lambda Architecture):**
- Periodic jobs (e.g., hourly, daily)
- High throughput, higher latency

**Stream Processing (Kappa Architecture):**
- Real-time processing as data arrives
- Lower latency, better for alerts/notifications

**Orchestration vs Choreography:**
- **Orchestration:** Central coordinator manages workflow
  - Pros: Clear control flow
  - Cons: Single point of failure

- **Choreography:** Services react to events
  - Pros: Decoupled, scalable
  - Cons: Harder to reason about

**Exercises:**
- Design a real-time notification system: Use Kafka or RabbitMQ?
- Handle failures: What if a consumer dies mid-processing?

---

#### Day 5: Idempotency & Exactly-Once Semantics
**Topics:**
- Idempotent operations: Safe to retry without side effects
- Unique request IDs: Detect duplicates
- Distributed tracing: Track a request across services

---

## PHASE C: System Design Case Studies (Weeks 7–11)

### Goal
Integrate all components. Design real systems end-to-end with trade-off analysis.

**Each case study follows this structure:**
1. **Clarify Requirements:** Functional & non-functional
2. **Back-of-Envelope Estimation:** Calculate QPS, storage, bandwidth
3. **High-Level Design:** Components and architecture
4. **Deep Dives:** Key components and trade-offs
5. **Scaling & Optimization:** Handle growth, identify bottlenecks
6. **Failure Scenarios:** Handle outages gracefully
7. **Follow-ups:** Questions interviewer may ask

---

### Week 7: Read-Heavy Systems

#### Problem 1: URL Shortener (TinyURL)
**Requirements Clarification:**
- Generate short URLs for long URLs
- Redirect short URL to long URL (read-heavy: 100:1 ratio)
- Custom short URLs? (increases complexity)
- Analytics tracking? (clicks, geographic distribution)

**Estimation:**
- 1 billion URLs generated per month
- Peak QPS: ~2,000 write, ~200,000 read
- Storage: 1 billion URLs × 500 bytes ≈ 500 GB
- Need to handle 5 years of data

**Design:**
- **API:** 
  - POST `/shorten` → short code
  - GET `/{shortCode}` → 301/302 redirect

- **Unique ID Generation:**
  - Options: KGS (Key Generation Service), DB auto-increment, Snowflake
  - KGS: Dedicated service pre-generates keys
  - Advantage: Centralized, can reserve blocks

- **Database:**
  - SQL: `shortCode → longURL` mapping
  - Cache: Redis for hot URLs
  - Read-heavy: Use read replicas

- **Redirect Response:**
  - 301: Permanent (browser caches, less traffic to backend)
  - 302: Temporary (always hit backend, allows analytics)
  - For TinyURL, use 301 (reduce server load)

**Scaling:**
- Load balancer distributes requests
- Multiple API servers (stateless)
- Cache layer (Redis) for popular links
- Read replicas for the database
- CDN for redirect responses (if using 301)

**Failure Scenarios:**
- KGS dies: Pre-generate keys in multiple servers
- Cache dies: Revert to database
- Database replica dies: Automatic failover

---

#### Problem 2: News Feed / Social Timeline
**Requirements Clarification:**
- Get feed of posts from users you follow
- Real-time updates
- Personalized (relevant posts)
- Like, comment, share

**Estimation:**
- 500 million DAU
- Average user follows 1000 people
- Each user generates 1 post per week
- Peak feed reads: 1 million QPS
- Timeline cache size per user: ~1 KB × 500M = 500 GB

**Design:**
- **Fan-out-on-Write (Push):**
  - When user posts, update followers' feeds immediately
  - Pros: Fast reads
  - Cons: Heavy on write, hot users have many followers (100k+)

- **Fan-out-on-Read (Pull):**
  - When user requests feed, fetch from all followed users
  - Pros: Simple, handles hot users well
  - Cons: Slow reads (query 1000 users)

- **Hybrid (Common in Practice):**
  - Push for normal users (< 10k followers)
  - Pull for celebrity users (> 100k followers)

**Components:**
- **Post Service:** Handle post creation, storage
- **Timeline Service:** Generate feed
- **Feed Cache:** Redis stores user's feed
- **Notification Service:** Notify followers

**Deep Dive - Fan-out-on-Write:**
1. User publishes post
2. Service publishes event
3. Fan-out service gets followers
4. For each follower, append post to their feed (in Redis)
5. Follower reads from cache (O(1))

**Scaling:**
- Sharding by user ID: User 0-1M on server 1, etc.
- Cache pre-warming: Proactively load popular users' feeds
- Message queue for fan-out (decouple post from push)

---

### Week 8: Write-Heavy & Consistency Systems

#### Problem 1: Chat System (WhatsApp / Discord)
**Requirements:**
- Send and receive messages
- Group chat
- Message history
- Delivery acknowledgments
- Presence (online/offline status)

**Estimation:**
- 100 million DAU
- Peak QPS: 2 million (global messaging)
- Message retention: 1 year
- Storage: 100M × 50 msgs/day × 1KB ≈ 5 PB per year

**Design:**
- **WebSocket Connection:**
  - Persistent connection per user
  - Server maintains mapping: `userId → connection`
  - On message, route to recipient's connection

- **Message Ordering:**
  - Critical: Messages within a chat must be ordered
  - Solution: Assign monotonically increasing IDs
  - Database: HBase or Cassandra (wide-column, handles billions of messages)

- **Delivery Guarantees:**
  - At-least-once: Persist before deleting from queue
  - Idempotency: Detect duplicate messages by ID

- **Presence:**
  - Heartbeat: Periodic "I'm still online" signal
  - Timeout: Mark offline if no heartbeat for X seconds
  - Redis: Quick lookups

**Components:**
- **Gateway Server:** Maintains WebSocket connections
- **Message Service:** Stores and retrieves messages
- **Presence Service:** Tracks online status
- **Notification Service:** Push notifications for offline users

**Scaling:**
- Sharding by chat ID: Each chat on specific server(s)
- Multiple gateway servers behind load balancer
- Message database sharded by time or user
- Replicate for durability

**Failure Scenarios:**
- Gateway dies: User reconnects, fetches missed messages from DB
- Database dies: Replicas take over
- Message loss: Persist to queue before processing

---

#### Problem 2: Rate Limiter (Distributed)
**Requirements:**
- Limit requests per user (e.g., 100 req/min)
- Global rate limiter (across multiple servers)
- Different tiers (free, premium)

**Algorithms:**
- **Token Bucket:** Generate N tokens per window, consume 1 per request
  - Allows bursts

- **Leaky Bucket:** Requests leak out at constant rate
  - Smooths traffic

- **Sliding Window Counter:** Track exact timestamps of recent requests
  - Accurate but storage-heavy

**Design:**
- **Redis:** Fast, supports atomic operations
  - Key: `rate_limit:{userId}:{window}`
  - Value: tokens remaining
  - Increment window every minute

- **Implementation:**
  - Client sends request with API key
  - Rate limiter checks Redis
  - If over limit, return 429 Too Many Requests
  - Race condition: Use Redis Lua script for atomic check-and-decrement

**Scaling:**
- Multi-region: Local Redis in each region
- Sync replication: Risk of double-counting on replica fail
- Async replication: Risk of allowing over-limit requests

**Exercise:**
Design rate limiting for 1 million requests/sec. How many Redis nodes?

---

### Week 9: Geospatial & Real-Time Systems

#### Problem 1: Uber / Ride-Sharing
**Requirements:**
- Find nearby drivers
- Match driver to rider
- Track real-time location
- ETA calculation

**Estimation:**
- 10 million trips per day
- 500k+ concurrent active requests
- Updates: 30 million location updates per minute

**Design:**
- **Driver Location Index:**
  - Geohashing: Divide world into rectangles
  - Find nearby drivers: Check overlapping and adjacent cells
  - Alternatively: QuadTrees for spatial indexing

- **Matching Engine:**
  - Request comes in, find drivers in candidate cell
  - Rank by distance, ETA, rating
  - Offer to drivers (in order, top-down)
  - First to accept gets the ride

- **Location Tracking:**
  - Driver sends GPS every 30 seconds (or less)
  - Store in time-series database (Cassandra, InfluxDB)
  - For current location, use Redis (fast lookups)

**Components:**
- **Location Service:** Ingest and store driver locations
- **Matching Service:** Find and match drivers
- **Trip Service:** Manage trip lifecycle
- **Payment Service:** Handle payments

**Database:**
- **Current Locations:** Redis (hot data)
- **Trip History:** Cassandra (time-series optimized)
- **User Data:** PostgreSQL

**Scaling:**
- Sharding by location grid cell
- Event streaming (Kafka) for location updates
- Geohash enables geographic sharding

---

#### Problem 2: Live Leaderboard / Real-Time Rankings
**Requirements:**
- Rank players by score
- Updates in real-time
- Fetch top 100, user's rank and neighbors

**Estimation:**
- 10 million users playing
- 1 million score updates per minute
- 100k concurrent reads

**Design:**
- **Redis Sorted Sets:**
  - Key: `leaderboard`
  - Member: `userId`, Score: `score`
  - Operations: O(log N) update, O(1) rank lookup

- **Real-time Updates:**
  - Score update → Redis Sorted Set update
  - Broadcast to clients subscribed to leaderboard (WebSocket)

- **Efficiency:**
  - For top 100: `ZREVRANGE leaderboard 0 99` (O(log N + 100))
  - For user's rank: `ZREVRANK leaderboard {userId}` (O(log N))
  - For neighbors: Fetch rank, then users around it

**Components:**
- **Score Update Service:** Receive and store score updates
- **Ranking Service:** Query and return rankings
- **Notification Service:** Broadcast changes

**Scaling:**
- Multiple Redis instances by game/region
- Replication: Master-replica setup
- Pub-sub for broadcasting updates

---

### Week 10: Specialized & Complex Systems

#### Problem 1: Typeahead / Search Autocomplete
**Requirements:**
- As user types, suggest completions
- Low latency (< 100ms)
- Popular queries ranked higher

**Design:**
- **Trie Data Structure:**
  - Prefix tree: Efficient prefix matching
  - Store frequency at each node
  - Top 10 suggestions computed offline

- **Caching Popular Prefixes:**
  - "a" → thousands of prefixes
  - Cache top 10 results for "a", "ab", "abc", etc.
  - Redis: `prefix:abc → [result1, result2, ...]`

- **Offline Computation:**
  - Periodically (hourly) re-compute top results
  - Use historical search data (trending)
  - Updated to cache

**Database:**
- **Search Logs:** Store all searches (Kafka or similar)
- **Analytics:** Compute frequency (Spark job)
- **Trie:** Serialized in file or in-memory cache

**API:**
- GET `/autocomplete?prefix=abc&limit=10`

**Scaling:**
- Caching reduces need for Trie queries
- Prefix-based sharding (a-d on server 1, e-h on server 2)
- Global replicas for low latency

---

#### Problem 2: Video Streaming (YouTube / Netflix)
**Requirements:**
- Upload and stream videos
- Adaptive bitrate streaming (quality based on connection)
- Millions of concurrent viewers

**Estimation:**
- 1 billion videos stored
- 500 million viewers daily
- Peak concurrent: 50 million
- Video: 1 GB average

**Design:**
- **Video Upload:**
  - Storage: S3 or object store
  - Transcoding: Convert to multiple resolutions
  - Queue-based: Asynchronous processing

- **Streaming:**
  - HLS/DASH protocols: Split video into segments (m3u8 playlist)
  - Client: Adaptive bitrate selection
  - Segments: Cached on CDN globally

- **Metadata:**
  - Database: Video info, duration, thumbnails, comments
  - Cache: Hot metadata in Redis

**Components:**
- **Upload Service:** Receive and store videos
- **Transcoding Service:** Convert formats
- **Streaming Service:** Serve m3u8 playlists
- **CDN:** Distribute segments globally

**Database:**
- **Videos:** SQL (searchable, joinable)
- **Segments:** Object store (S3)
- **Viewing History:** NoSQL (scale easily)

**Scaling:**
- CDN critical: 80% of cost
- Transcoding: Distributed (many workers)
- Sharding by video ID

---

### Week 11: Production Excellence & Deep Dives

#### Advanced Scaling Topics
**Topics:**
- Multi-region deployment: Data consistency, failover
- Database sharding strategy: Range vs hash sharding, hotspots
- Cascade failures: How to prevent a small outage from cascading
- Graceful degradation: Serve stale data if live data unavailable

---

## PHASE D: Senior/Staff-Level Excellence (Weeks 12+)

### Goal
Differentiate yourself at senior levels. Focus on operational concerns, observability, and trade-offs.

### Week 12: Observability & Production Readiness

#### Topics:
**Observability (3 Pillars):**
1. **Metrics:** Quantitative measurements (QPS, latency, error rate)
2. **Logs:** Detailed records of events
3. **Traces:** Request flow across services

**SLOs, SLIs, SLAs:**
- Define objectives and measure adherence
- Error budget: allowed failures

**Deployment Strategies:**
- Blue-green: Two production environments
- Canary: Roll out to 1% of traffic first
- Feature flags: Control feature rollout independently of deployment

**Resilience Patterns:**
- Circuit breaker: Fail fast when downstream service is unhealthy
- Bulkhead: Isolate failures (thread pools per service)
- Retry with exponential backoff: Handle transient failures
- Timeout: Prevent hanging requests

---

### Week 13: Trade-off Mastery
**Topics:**
- Consistency vs Availability (CAP)
- Latency vs Consistency (PACELC)
- Cost vs Performance
- Simplicity vs Optimization

**Framework for discussing trade-offs:**
1. State the dilemma clearly
2. Explain each option's pros/cons
3. Justify your choice based on requirements
4. Mention what would change your decision

**Example:**
> *"For a social feed, we prioritize availability over consistency. Users might see slightly stale posts, but the system remains responsive. If this were a financial ledger, we'd flip the priority."*

---

### Week 14: Back-of-Envelope Estimation Mastery
**Topics:**
- Quickly estimate: QPS, storage, bandwidth, server count
- Make reasonable assumptions and state them
- Use powers of 10 to keep math simple

**Key Numbers to Know:**
| Operation | Latency |
|---|---|
| L1 cache hit | 1 ns |
| Memory access | 100 ns |
| SSD read | 1 µs |
| Network roundtrip (local) | 1 ms |
| Disk seek | 10 ms |
| Network roundtrip (cross-country) | 100 ms |

**Estimation Example: Twitter Clone**
- 500 million DAU
- 10% active daily
- Average 5 tweet reads per session
- Peak: 2x average
- QPS = (500M × 10% × 5) / 86,400 ≈ 300k QPS peak

---

## 📊 Interview Expectations by Level

### Mid-Level (SDE II)
- **Time:** 45-60 minutes
- **Complexity:** Medium (URL Shortener, Cache)
- **Depth:** High-level design + basic components
- **Trade-offs:** Mention 2-3 trade-offs
- **Estimation:** Basic QPS, storage calculation

### Senior (SDE III / L5)
- **Time:** Same, but includes follow-ups
- **Complexity:** Medium-hard (Chat System, Rate Limiter)
- **Depth:** Design + detailed component analysis
- **Trade-offs:** Deep analysis, context-dependent
- **Estimation:** Full calculation (QPS, storage, bandwidth, servers)
- **Follow-ups:** Handle scaling, failures, monitoring

### Staff (L6+)
- **Time:** 90+ minutes with deep dives
- **Complexity:** Hard (Distributed system, Real-time)
- **Depth:** End-to-end design with optimization
- **Trade-offs:** Nuanced, mention when you'd change your mind
- **Estimation:** Rapid, accurate numbers
- **Follow-ups:** Operational concerns, observability, cost, team structure

---

## 🎯 Preparation Strategy

### Month 1: Fundamentals
- Week 1: CAP, PACELC, consistency models
- Week 2: Scalability patterns, load balancing
- Week 3: SQL vs NoSQL deep dives
- Week 4: Caching strategies, queues

### Month 2: Case Studies (Simple to Complex)
- Week 5: URL Shortener, Rate Limiter
- Week 6: Chat System, News Feed
- Week 7: Uber, Leaderboard
- Week 8: Typeahead, Video Streaming

### Month 3: Advanced Topics
- Week 9: Multi-region, distributed transactions
- Week 10: Observability, production concerns
- Week 11: Trade-off mastery, back-of-envelope
- Week 12: Mock interviews + refinement

---

## 📚 Key Resources

### Books
- *Designing Data-Intensive Applications* (Martin Kleppmann)
- *System Design Interview* (Alex Xu)
- *The Art of Scalability* (Barker & Frey)

### Online Platforms
- System Design Handbook
- Educative HLD course
- ByteByteGo YouTube channel
- Real system design papers (AWS, Google, Netflix blogs)

---

## ✅ Final Checklist

- [ ] Can explain CAP & PACELC with real examples
- [ ] Know SQL and NoSQL trade-offs (and when to denormalize)
- [ ] Understand consistent hashing and virtual nodes
- [ ] Can design 10+ systems end-to-end
- [ ] Comfortable with back-of-envelope calculations
- [ ] Know Kafka vs RabbitMQ deeply
- [ ] Understand distributed transactions and sagas
- [ ] Can discuss observability, monitoring, alerting
- [ ] Know failure scenarios and mitigation strategies
- [ ] Can justify trade-offs with business context

---

**Good luck! Remember: HLD interviews test your ability to design scalable, reliable, maintainable systems. Clarity of thought and sound trade-off analysis matter more than a "perfect" design.**
