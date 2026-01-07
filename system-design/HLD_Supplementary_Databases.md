# 💾 HLD Supplementary: Database & Storage Design Deep Dive

## Table of Contents
1. Database Selection Decision Tree
2. SQL vs NoSQL Quick Reference
3. Sharding Strategy Guide
4. Replication Patterns
5. Caching Strategies
6. Database Optimization Tips
7. Common Mistakes & Solutions

---

## 1️⃣ Database Selection Decision Tree

```
START: Need to store data?
│
├─ Is data HIGHLY STRUCTURED? (Clear schema, relationships)
│  │
│  ├─ YES → SQL (PostgreSQL, MySQL, Oracle)
│  │  │
│  │  └─ Questions:
│  │     ├─ Complex transactions? YES → SQL ✓
│  │     ├─ Frequent joins? YES → SQL ✓
│  │     ├─ Strict consistency? YES → SQL ✓
│  │     └─ Massive scale (billions)? YES → SQL with sharding
│  │
│  └─ NO → Continue...
│
├─ Is data FLEXIBLE/SEMI-STRUCTURED? (JSON, docs)
│  │
│  ├─ YES → Document DB (MongoDB)
│  │  │
│  │  └─ Use when:
│  │     ├─ Schema evolves frequently
│  │     ├─ Nested/hierarchical data
│  │     └─ Rapid development needed
│  │
│  └─ NO → Continue...
│
├─ Is this TIME-SERIES DATA? (Metrics, logs, events)
│  │
│  ├─ YES → Time-Series DB (InfluxDB, TimescaleDB)
│  │  │
│  │  └─ Use when:
│  │     ├─ Write-heavy with timestamp
│  │     ├─ Periodic compression needed
│  │     └─ Range queries on time
│  │
│  └─ NO → Continue...
│
├─ Is this WRITE-HEAVY AT MASSIVE SCALE?
│  │
│  ├─ YES → Wide-Column Store (Cassandra, HBase)
│  │  │
│  │  └─ Use when:
│  │     ├─ Billions of writes/day
│  │     ├─ Availability over consistency
│  │     └─ Geographic distribution needed
│  │
│  └─ NO → Continue...
│
├─ Do I just need FAST KEY-VALUE lookups?
│  │
│  ├─ YES → Key-Value Store (Redis, Memcached, DynamoDB)
│  │  │
│  │  └─ Use when:
│  │     ├─ Simple get/set operations
│  │     ├─ Low latency critical
│  │     ├─ Cache layer needed
│  │     └─ Leaderboards, sessions
│  │
│  └─ NO → Continue...
│
├─ Do I need FULL-TEXT SEARCH?
│  │
│  └─ YES → Search Engine (Elasticsearch, Solr)
│     │
│     └─ Use when:
│        ├─ Natural language search
│        ├─ Relevance ranking needed
│        └─ Faceted filtering
│
└─ DEFAULT → Start with SQL, optimize later
```

---

## 2️⃣ SQL vs NoSQL Quick Reference

### SQL (PostgreSQL, MySQL)

**Characteristics:**
- Structured schema (strict)
- ACID transactions (strong guarantees)
- Complex queries (joins, aggregations)
- Normalized (no data duplication)

**Strengths:**
✅ Data consistency and integrity  
✅ Complex queries with joins  
✅ ACID transactions (all-or-nothing)  
✅ Well-understood, mature  

**Weaknesses:**
❌ Scales vertically better than horizontally  
❌ Sharding is complex (loses some benefits)  
❌ Can't store semi-structured data well  
❌ ORM overhead

**Best For:**
- Financial systems (money can't be duplicated)
- ERP/CRM systems (complex data relationships)
- Accounting (strong consistency required)
- Most traditional business apps

**Example:**
```
User: id, name, email, created_at
Post: id, user_id, content, created_at
Like: id, post_id, user_id, created_at

Query: "Get all posts by user and their like count"
SELECT p.*, COUNT(l.id) as likes
FROM posts p
LEFT JOIN likes l ON p.id = l.post_id
WHERE p.user_id = ?
```

---

### NoSQL Key-Value (Redis, Memcached, DynamoDB)

**Characteristics:**
- No schema (flexible)
- Simple operations (get, set)
- Fast (in-memory or SSD)
- Eventually consistent (usually)

**Strengths:**
✅ Extremely fast (microsecond latencies)  
✅ Scales horizontally easily  
✅ Simple to use  
✅ Handles spikes well

**Weaknesses:**
❌ No complex queries  
❌ No transactions across keys  
❌ Eventually consistent (may return stale data)  
❌ No joins (data must be denormalized)

**Best For:**
- Caching hot data
- Session stores
- Leaderboards (sorted sets)
- Rate limiting
- Shopping carts
- Real-time counters

**Example:**
```
Redis: key-value pairs and data structures
SET user:123:session {"id": 123, "name": "Alice", "cart": [1,2,3]}
INCR counter:page_views
ZADD leaderboard 100 player1 50 player2 200 player3
LPUSH queue:tasks {task_data}
```

---

### NoSQL Document (MongoDB)

**Characteristics:**
- Semi-structured (JSON-like documents)
- Flexible schema
- Rich queries (on document fields)
- Embedded relationships

**Strengths:**
✅ Schema flexibility  
✅ Nested data structure support  
✅ Good query language  
✅ Horizontal scaling (sharding)  

**Weaknesses:**
❌ No ACID across documents (in older versions)  
❌ Data duplication (denormalized)  
❌ JOINs still difficult  
❌ Memory overhead (JSON parsing)

**Best For:**
- Rapidly evolving schemas
- Content management (blog posts, pages)
- User profiles (varied attributes)
- Mobile apps (offline sync)
- Product catalogs

**Example:**
```javascript
db.users.insertOne({
  _id: 123,
  name: "Alice",
  email: "alice@example.com",
  preferences: {
    theme: "dark",
    notifications: true
  },
  posts: [
    { id: 1, title: "First post", likes: 100 },
    { id: 2, title: "Second post", likes: 50 }
  ]
})

db.users.find({ 
  "preferences.notifications": true 
})
```

---

### NoSQL Wide-Column (Cassandra, HBase)

**Characteristics:**
- Distributed by design
- Write-optimized (LSM trees)
- Column-family storage
- Eventually consistent

**Strengths:**
✅ Massive scale (petabytes)  
✅ Write throughput (millions/sec)  
✅ High availability  
✅ Geographic distribution

**Weaknesses:**
❌ Complex to operate  
❌ Eventual consistency  
❌ Expensive memory (needs tuning)  
❌ Limited query capabilities

**Best For:**
- Time-series data (metrics, logs)
- Massive write-heavy applications
- Real-time analytics
- Historical data storage
- IoT sensor data

**Example:**
```
Cassandra: Wide-column format
Table: messages
Partition key: user_id (which shard)
Clustering key: timestamp (ordering within shard)

user_id | timestamp | sender_id | content
--------|-----------|-----------|----------
  123   | 2024-01-01 12:00:00 | 456 | "Hi"
  123   | 2024-01-01 12:00:05 | 789 | "Hello"

Perfect for: Get all messages for user 123 after timestamp X
```

---

## 3️⃣ Sharding Strategy Guide

### When to Shard?

✅ Shard when:
- Single database can't handle write throughput
- Data size exceeds single machine capacity
- Need geographic distribution

❌ Don't shard if:
- Can still scale vertically (add more RAM/CPU)
- Data is small enough for one machine
- Queries are complex (sharding breaks joins)

---

### Sharding Strategy 1: Range-Based

**How it works:**
```
User ID 0-1M → Shard 1
User ID 1M-2M → Shard 2
User ID 2M-3M → Shard 3
```

**Pros:**
✅ Simple to implement  
✅ Easy to add new shards (append new range)  
✅ Range queries efficient

**Cons:**
❌ Data distribution can be uneven (hotspots)  
❌ Growth planning needed (know future ranges)  
❌ Rebalancing is complex

**Use Case:**
- Time-based sharding (Jan-Mar → Shard 1, Apr-Jun → Shard 2)
- Geographic regions

---

### Sharding Strategy 2: Hash-Based

**How it works:**
```
hash(user_id) % number_of_shards = shard_id

hash("user_123") % 10 = 3 → Shard 3
```

**Pros:**
✅ Distributes data evenly  
✅ No hotspots  
✅ Simple implementation

**Cons:**
❌ Adding shards requires rehashing all data  
❌ Cross-shard queries difficult

**Use Case:**
- When data distribution is unknown
- When you don't need range queries

---

### Sharding Strategy 3: Consistent Hashing

**How it works:**
```
Ring of shards (virtual nodes)
hash(key) maps to a point on the ring
Clockwise: find first shard

Adding shard: only 1/N data migrates
```

**Pros:**
✅ Minimal rebalancing (1/N data moves)  
✅ Scales well  
✅ Fault tolerant

**Cons:**
❌ More complex to implement  
❌ Data distribution less predictable

**Use Case:**
- Large-scale distributed systems
- Caches, CDNs
- Databases needing frequent rebalancing

---

### Sharding Strategy 4: Directory-Based

**How it works:**
```
Lookup table: key → shard_id

user_123 → Shard 1
user_456 → Shard 2
```

**Pros:**
✅ Flexible rebalancing (just update directory)  
✅ Can add/remove shards easily  
✅ Data distribution control

**Cons:**
❌ Extra lookup latency  
❌ Directory is a single point of failure  
❌ Must be replicated/cached

**Use Case:**
- When frequent rebalancing needed
- When you want fine-grained control

---

## 4️⃣ Replication Patterns

### Pattern 1: Primary-Replica (Master-Slave)

```
Primary (Master)
  ↓ (replication)
Replica 1, Replica 2, Replica 3

Reads: From replicas (low latency)
Writes: To primary only (strong consistency)
```

**Pros:**
✅ Strong consistency for writes  
✅ Simple to understand  
✅ Good for read-heavy workloads

**Cons:**
❌ Primary is bottleneck for writes  
❌ Replica lag (brief inconsistency)  
❌ Single point of failure for writes

**Use Case:**
- Social media feeds (100:1 read-write ratio)
- News sites

---

### Pattern 2: Multi-Primary (Multi-Master)

```
Primary 1 ↔ Primary 2 ↔ Primary 3
  ↓ replicate   ↓ replicate   ↓ replicate
All can be written to

Writes: To any primary (high availability)
Reads: From any primary
```

**Pros:**
✅ No single point of failure  
✅ Writes accepted during partition  
✅ Geographic distribution

**Cons:**
❌ Complex conflict resolution  
❌ Slower consistency (eventual)  
❌ Write amplification (all must replicate)

**Use Case:**
- Global systems needing HA
- Collaborative editing
- Geo-distributed apps

---

### Pattern 3: Leaderless (Quorum)

```
All replicas equal
Quorum read: get latest from 3/5 replicas
Quorum write: write to 3/5 replicas

Vector clocks detect conflicts
```

**Pros:**
✅ No single point of failure  
✅ Fault tolerant  
✅ Good for eventually consistent systems

**Cons:**
❌ Complex (quorum coordination)  
❌ Slower than single replica  
❌ Conflict resolution needed

**Use Case:**
- Highly available systems (Dynamo)
- When any node can fail

---

## 5️⃣ Caching Strategies

### Cache-Aside (Lazy Loading)

```
1. Check cache
2. Cache miss → fetch from DB
3. Update cache
4. Return to client
```

**Pros:**
✅ Simple  
✅ Only cache accessed data  

**Cons:**
❌ Slow on first access  
❌ Cache staleness possible

**Use Case:**
- User profiles
- Product details

---

### Write-Through

```
1. Write to cache
2. Write to database
3. Return to client
```

**Pros:**
✅ Cache always up-to-date  
✅ No cache staleness

**Cons:**
❌ Two writes (slower)  
❌ Cache might have data DB doesn't (crash recovery)

**Use Case:**
- Critical data (don't want to lose)

---

### Write-Behind (Write-Back)

```
1. Write to cache
2. Async write to database
3. Return to client immediately
```

**Pros:**
✅ Fast writes  
✅ Good throughput

**Cons:**
❌ Risk of data loss (cache crash)  
❌ Eventual consistency

**Use Case:**
- High-traffic writes (session updates)
- Logs that can be lost

---

## 6️⃣ Database Optimization Tips

### Indexing

```sql
-- Without index: O(n) table scan
SELECT * FROM users WHERE email = 'alice@example.com';  -- slow

-- With index: O(log n) lookup
CREATE INDEX idx_users_email ON users(email);
SELECT * FROM users WHERE email = 'alice@example.com';  -- fast
```

**When to index:**
✅ Frequently searched columns  
✅ Foreign keys  
✅ Range queries (timestamp > X)  

**Don't index:**
❌ Rarely searched columns  
❌ Boolean columns (too many duplicates)  
❌ High cardinality on small tables

---

### Query Optimization

```sql
-- Bad: Full scan on large table
SELECT * FROM orders WHERE user_id = 123;

-- Good: Indexed column
CREATE INDEX idx_orders_user_id ON orders(user_id);
SELECT * FROM orders WHERE user_id = 123;

-- Bad: Expensive join on unindexed column
SELECT * FROM orders o
JOIN users u ON o.email = u.email

-- Good: Join on indexed ID
SELECT * FROM orders o
JOIN users u ON o.user_id = u.id
```

---

### Connection Pooling

```
Without pooling: Create connection per request (expensive)
With pooling: 
  - Maintain pool of connections
  - Reuse across requests
  - Reduces latency 10-100x
```

---

## 7️⃣ Common Mistakes & Solutions

### Mistake 1: Not Sharding Early Enough

**Problem:** Single database hits limit, migration is painful

**Solution:**
- Monitor database capacity
- Plan for sharding before urgency
- Use consistent hashing for smooth migration

---

### Mistake 2: Sharding by Wrong Key

**Problem:** One shard becomes hotspot (celebrity user)

**Solution:**
- Shard by frequently-joined column
- Monitor shard distribution
- Use directory-based sharding for hot data

---

### Mistake 3: Ignoring Replication Lag

**Problem:** User sees stale data after writing

**Solution:**
- Read-your-writes consistency (read from primary after write)
- Accept slight staleness (explicitly document)
- Use strong consistency where needed

---

### Mistake 4: Over-Indexing

**Problem:** Too many indexes slow down writes

**Solution:**
- Index only frequently searched columns
- Monitor slow queries
- Remove unused indexes

---

### Mistake 5: No Backup/Disaster Recovery

**Problem:** Database crash = data loss

**Solution:**
- 3x replication minimum
- Regular backups to separate location (S3)
- Test recovery regularly

---

## 🎯 Quick Decision Matrix

| Requirement | Best Choice | Runner-up |
|-------------|------------|-----------|
| Complex transactions | PostgreSQL | MySQL |
| Write-heavy, massive scale | Cassandra | HBase |
| Fast read cache | Redis | Memcached |
| Flexible schema | MongoDB | DynamoDB |
| Time-series data | TimescaleDB | InfluxDB |
| Full-text search | Elasticsearch | Solr |
| Simple key-value | DynamoDB | Redis |
| Geographic distribution | Cassandra | Multi-region SQL |

---

**Use this guide when making database decisions. Save it for reference!**
