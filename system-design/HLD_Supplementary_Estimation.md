# 📊 HLD Supplementary: Back-of-the-Envelope Estimation Guide

## Table of Contents
1. Key Numbers to Memorize
2. Estimation Formulas
3. Real-World Estimation Examples
4. QPS Calculation Patterns
5. Storage Calculation Patterns
6. Bandwidth Calculation Patterns
7. Server Count Estimation
8. Quick Reference Tables

---

## 1️⃣ Key Numbers to Memorize

### Time/Latency Metrics

```
1 ns (nanosecond)       = 10^-9 seconds
1 µs (microsecond)      = 10^-6 seconds = 1,000 ns
1 ms (millisecond)      = 10^-3 seconds = 1,000 µs
1 second               = 1,000 ms
1 minute               = 60 seconds
1 hour                 = 3,600 seconds
1 day                  = 86,400 seconds
1 year                 = 31,536,000 seconds (≈ 3.15 × 10^7)
```

### Storage Metrics

```
1 KB (kilobyte)  = 10^3 bytes = 1,000 bytes
1 MB (megabyte)  = 10^6 bytes = 1,000 KB
1 GB (gigabyte)  = 10^9 bytes = 1,000 MB
1 TB (terabyte)  = 10^12 bytes = 1,000 GB
1 PB (petabyte)  = 10^15 bytes = 1,000 TB
```

### Component Latencies (Typical)

```
L1 cache hit               ≈ 1 ns          (on CPU)
L2 cache hit               ≈ 4 ns
L3 cache hit               ≈ 10 ns
Main memory access         ≈ 100 ns        (RAM)
SSD random read            ≈ 1-10 µs       (solid state disk)
HDD random read            ≈ 10 ms         (spinning disk)
Network roundtrip (local)  ≈ 1 ms
Network roundtrip (US)     ≈ 50 ms
Network roundtrip (Global) ≈ 100-150 ms
Database query (indexed)   ≈ 1-10 ms
Database query (full scan) ≈ 100+ ms
```

### User & Traffic Patterns

```
Daily Active Users (DAU) → Peak QPS ≈ DAU × avg_requests_per_user / 86,400

Peak multiplier over average: typically 3-5x
  - Morning/evening peaks
  - Weekend vs weekday variations
  - Event spikes (new content, viral moment)

Concurrent users ≈ DAU × percentage_online × avg_session_length_minutes / 60
  - E.g., 500M DAU × 1% online × 30 min sessions = 2.5M concurrent
```

---

## 2️⃣ Estimation Formulas

### QPS (Queries Per Second) Estimation

**Formula 1: From Daily Usage**
```
QPS_average = (Daily_Users × Requests_Per_User) / 86,400

QPS_peak = QPS_average × Peak_Multiplier (typically 3-5x)
```

**Example:**
```
- 500M DAU
- 10 requests per user per day (average)
- Average QPS = (500M × 10) / 86,400 ≈ 57,870 QPS
- Peak QPS = 57,870 × 4 ≈ 231,500 QPS (peak multiplier 4x)
```

**Formula 2: From Concurrent Users**
```
QPS = Concurrent_Users × Requests_Per_User_Per_Second
```

**Example:**
```
- 2.5M concurrent users
- 0.1 requests/sec per user (one request every 10 seconds)
- QPS = 2.5M × 0.1 ≈ 250,000 QPS
```

---

### Storage Estimation

**Formula: Total Storage = Daily New Data × Days Retained**

**Example: Social Media Posts**
```
- 1M users, each posts 1 post per day
- Post size: 1 KB average
- Daily new data: 1M × 1 KB = 1 GB
- Retention: 5 years = 1,825 days
- Total storage: 1 GB × 1,825 ≈ 1.8 TB

With 3x replication (for durability):
Total = 1.8 TB × 3 ≈ 5.4 TB
```

**Example: Video Streaming (YouTube-like)**
```
- 100M videos uploaded per year
- Average video size: 1 GB
- Yearly storage: 100M × 1 GB = 100 EB (exabytes)

That's massive! Solutions:
- Tiered storage: Recent videos (hot) in S3
- Old videos (cold) in Glacier
- Transcoding: Store multiple bitrates
- Compression: Reduce size per quality
```

---

### Bandwidth Estimation

**Formula: Bandwidth = Data Transfer Per Second = QPS × Avg Response Size**

**Example: Social Feed**
```
- 100k QPS (read-heavy)
- Average feed response: 1 MB (contains multiple posts)
- Required bandwidth: 100k × 1 MB = 100 GB/s = 800 Gbps

Problem: Single gigabit network can't handle!
Solution:
- Distribute across many servers
- Use CDN for edge caching
- Compress responses (JSON → gzip, -70% size)
- Pagination (return 10 posts, not 100)
```

**Data Transfer Direction:**
- Uplink: User → Server (typically smaller, just IDs, text)
- Downlink: Server → User (typically larger, media, responses)

---

### Server Count Estimation

**Formula: Server Count = QPS / Throughput Per Server**

**Assumptions:**
- Single server can handle 1,000-10,000 QPS (depends on query complexity)
- Simple read: 10,000 QPS per server
- Complex query: 1,000 QPS per server
- Write-heavy: 5,000 QPS per server

**Example:**
```
- Peak QPS: 100,000 (social feed read)
- Throughput per server: 10,000 QPS (simple query)
- Servers needed: 100,000 / 10,000 = 10 servers

Add redundancy:
- Need 2x for failover: 20 servers
- For different regions: 20 × 3 regions = 60 servers
```

---

## 3️⃣ Real-World Estimation Examples

### Example 1: Twitter Clone

**Requirements:**
- 500M DAU
- 10% active daily (50M)
- Read-to-write ratio: 100:1 (mostly reading feeds)
- Avg session: 30 minutes

**Estimation:**

```
CONCURRENT USERS:
- 50M × 1% online at peak = 500k concurrent
- Read requests: 500k × 0.1 req/sec = 50k QPS read
- Write requests: 50k QPS / 100 = 500 QPS write

DATABASE STORAGE:
- 500M users × 100 bytes/user = 50 GB
- 100M tweets/day × 1 KB/tweet = 100 GB/day
- 5 years: 100 GB × 1,825 days = 182.5 TB
- With 3x replication: ~550 TB

BANDWIDTH:
- Read: 50k QPS × 500 KB/response = 25 GB/s
- Write: 500 QPS × 100 KB/request = 50 MB/s
- Total: ≈ 25 GB/s (downlink)

SERVERS:
- Read servers: 50k / 10k per server = 5 servers
- With 2x redundancy: 10 read servers
- With 3 regions: 30 read servers

CACHE:
- Redis for hot tweets: 1 GB × 1000 = 1 TB
- Cache hit rate: 80% (saves DB hits)
```

---

### Example 2: Uber (Ride Sharing)

**Requirements:**
- 1M trips per day
- 500k+ concurrent drivers
- Real-time location updates every 30 seconds
- Peak: 5x average (surge pricing)

**Estimation:**

```
QPS CALCULATION:
- Trips: 1M/day ÷ 86,400 ≈ 12 QPS average
- Peak: 12 × 5 ≈ 60 QPS peak (trip creation)

LOCATION UPDATES:
- 500k drivers × 2 updates/min = 10k QPS
- Peak: 10k × 5 = 50k QPS

TOTAL: ≈ 50k QPS at peak

STORAGE:
- Drivers: 500k × 500 bytes = 250 MB
- Trip history: 1M trips/day × 1 KB = 1 GB/day
- 5 years: 1 GB × 1,825 = 1.8 TB
- With 3x replication: 5.4 TB

BANDWIDTH:
- Location updates: 50k QPS × 100 bytes = 5 MB/s
- Trip requests: 60 QPS × 1 KB = 60 KB/s
- Total: ≈ 5 MB/s

SERVERS:
- Location service: 50k / 10k = 5 servers
- Trip service: 60 / 1k = 1 server (minimal)
- With redundancy: 10-15 servers total
```

---

### Example 3: Netflix (Video Streaming)

**Requirements:**
- 200M subscribers
- 30% watching at any time
- Average bitrate: 5 Mbps (streaming quality)
- Peak: prime time (7-11 PM)

**Estimation:**

```
CONCURRENT VIEWERS:
- 200M × 30% = 60M concurrent at peak
- Not all watching simultaneously
- Peak concurrent: 60M × 20% = 12M viewers at 11 PM

BANDWIDTH:
- 12M viewers × 5 Mbps = 60 Petabits/sec = 7.5 PB/sec
- This is ENORMOUS! Network can't handle.

SOLUTION - CDN DISTRIBUTION:
- Netflix uses edge caches globally
- Videos cached in ISP data centers
- Reduces backbone bandwidth to 10% of above
- Still ≈ 750 TB/sec at peak (achievable with CDN)

STORAGE:
- 5,000 titles in library
- Average: 2 GB per title (multiple bitrates)
- Total: 5,000 × 2 GB = 10 TB raw
- Distributed to CDNs: 10 TB × 100 edge locations = 1 PB

SERVER ESTIMATION:
- Not traditional servers, but CDN nodes
- ~300-500 edge locations globally
- Each handles 20-50k concurrent users
- Total: 12M / 30k = 400 edge nodes
```

---

## 4️⃣ QPS Calculation Patterns

### Pattern 1: Peak vs Average

**Know These Ratios:**
```
Morning peak (8 AM):  1.5x average
Evening peak (8 PM):  3-5x average
Weekend:              1.2x weekday average
Holiday:              2-10x average
Event/Launch:         10-100x average
```

**Example:**
```
- Average QPS: 10k
- Evening peak: 10k × 5 = 50k
- Viral moment: 10k × 50 = 500k (need capacity for this!)
```

### Pattern 2: Read vs Write Ratio

**Typical Ratios:**
```
Social feed:        100:1 read-to-write (mostly reading)
Chat app:           10:1 read-to-write (messages + notifications)
Banking:            100:1 read-to-write (view balance more than transfer)
Collaborative app:  5:1 read-to-write (edits create events)
```

**Implication:**
- Read-heavy → optimize for reads (caching, read replicas)
- Write-heavy → optimize for writes (sharding, queue-based processing)

---

## 5️⃣ Storage Calculation Patterns

### Data Size Estimates

```
User Profile:          500 bytes - 2 KB
Social Media Post:     1-5 KB
Chat Message:          100 bytes - 1 KB
Image (compressed):    100 KB - 5 MB
Video (HD):            1-5 GB per hour
Index Entry:           50-200 bytes
```

### Growth Patterns

```
Linear growth:
- If 1M new users/month, storage grows 1M × 500B = 500 MB/month
- Per year: 6 GB

Exponential growth (viral):
- Can be 10-100x normal growth
- Plan for 10x peak load

Retention policies:
- Some data purged after time (old messages deleted)
- Some archived to cold storage (S3 Glacier)
```

---

## 6️⃣ Bandwidth Calculation Patterns

### Response Size Estimates

```
HTML page:          10-100 KB
JSON API response:  1-10 KB
Image thumbnail:    50-200 KB
Image full:         500 KB - 5 MB
Video frame:        depends on resolution
  - 480p: 100 KB per frame
  - 720p: 300 KB per frame
  - 1080p: 1 MB per frame
```

### Network Limits

```
1 Mbps   = 125 KB/s
10 Mbps  = 1.25 MB/s
100 Mbps = 12.5 MB/s
1 Gbps   = 125 MB/s
10 Gbps  = 1.25 GB/s
100 Gbps = 12.5 GB/s
1 Tbps   = 125 GB/s
```

---

## 7️⃣ Server Count Estimation

### By Use Case

```
API Server (stateless):
- Simple queries: 5,000-10,000 QPS per server
- Complex queries: 1,000-2,000 QPS per server
- Add 2x for redundancy/failover

Cache Server (Redis):
- Simple get: 100,000 QPS per server
- Complex operation: 10,000 QPS per server

Database Server:
- Read-optimized: 1,000-5,000 QPS per server
- Write-optimized: 500-2,000 QPS per server

Messaging (Kafka):
- 100,000+ messages/sec per broker
```

### Redundancy Factors

```
Development:     1x (single server is fine)
Production:      2x (primary + backup)
High-availability: 3x (primary + 2 replicas)
Multi-region:    3x per region
```

---

## 8️⃣ Quick Reference Tables

### QPS Capacity by Component

| Component | Simple Operation | Complex Operation | Scaling Strategy |
|-----------|-----------------|-------------------|------------------|
| Web Server | 5-10k QPS | 1-2k QPS | Horizontal (load balance) |
| Database | 1-5k QPS | 100-500 QPS | Vertical + Sharding |
| Cache (Redis) | 100k QPS | 10-50k QPS | Horizontal (consistent hash) |
| Message Queue | 100k+ | 50k+ | Horizontal (partitioning) |

### Storage Growth Over Time

| Time Period | 1 KB/sec | 1 MB/sec | 1 GB/sec |
|------------|----------|----------|----------|
| 1 Day | 86 MB | 86 GB | 86 TB |
| 1 Month | 2.6 GB | 2.6 TB | 2.6 PB |
| 1 Year | 31.5 GB | 31.5 TB | 31.5 PB |

### Latency Budget (For P99)

| Component | Budget | Remaining |
|-----------|--------|-----------|
| Total P99 | 1000 ms | - |
| API layer | 100 ms | 900 ms |
| Cache (hit) | 50 ms | 850 ms |
| Database | 200 ms | 650 ms |
| Network | 100 ms | 550 ms |
| Processing | 100 ms | 450 ms |
| **Buffer** | **450 ms** | - |

---

## 9️⃣ Common Mistakes

❌ **Forgetting redundancy:** Don't estimate for 1x capacity
❌ **Using average instead of peak:** Systems must handle peak!
❌ **Ignoring replication overhead:** Storage needs 3x for durability
❌ **Underestimating read/write ratio:** Read-heavy requires different strategy
❌ **Confusing decimal vs binary:** 1 GB = 10^9 bytes (decimal), not 2^30
❌ **Forgetting network latency:** Cross-DC calls add 100ms+
❌ **Not accounting for growth:** Plan for 5-10x growth
❌ **Overestimating single server capacity:** Many factors affect QPS

---

## 🔟 Estimation Cheat Sheet

```
Quick Rules of Thumb:

QPS Estimation:
- DAU / 1000 ≈ baseline QPS (rough)
- Peak = 3-5x average
- Add 10x buffer for unexpected spikes

Storage Estimation:
- Per user: 100 bytes - 10 KB
- Per event: 100 bytes - 1 MB
- Multiply by retention days
- Multiply by 3 for replication

Bandwidth Estimation:
- QPS × avg response size
- Divide by 1 billion for Gbps needed
- Add 3x for redundancy and growth

Server Estimation:
- Divide peak QPS by throughput per server
- Multiply by 2-3 for redundancy
- Add 20% buffer for maintenance

Database Estimation:
- Write-heavy? Plan for sharding
- Read-heavy? Plan for replicas + cache
- Calculate hotspot risk
```

---

**Use this guide during estimation. Practice these calculations until they're second nature!**
