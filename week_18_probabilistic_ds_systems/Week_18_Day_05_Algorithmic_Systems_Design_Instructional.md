# 📘 Week 18, Day 5: Algorithmic Systems Design & Production Scaling

> 🧭 **Navigation:** [← Previous Day](Week_18_Day_04_Probabilistic_Data_Structures_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Week Playbook →](WEEK_18_FULL_PLAYBOOK.md)
> 
> 💡 **Instructor Note:** *Algorithmic systems design bridges pure computer science primitives with large-scale distributed architectures. Master Consistent Hashing with virtual nodes and precision Token Bucket rate limiting.*

---

## 🎯 Learning Objectives

*   **Core Mental Model:** Understand how Consistent Hashing and Token Bucket algorithms eliminate cascading rehash stampedes and burst-traffic service degradation.
*   **Mathematical Precision:**
    *   **Consistent Hashing:** Prove that adding/removing a server relocates only `1/N` of keys on average, and that allocating `V` virtual nodes reduces load variance by `O(1 / sqrt(V))`.
    *   **Token Bucket:** Formulate closed-form continuous refill equations `tokens = min(Capacity, tokens + delta_t * Rate)` in `O(1)` time without background ticker threads.
*   **Production Systems Anchors:** Ground these algorithms in Amazon DynamoDB/Apache Cassandra token rings, Envoy/Twemproxy ketama hashing, and Stripe API rate limiters.
*   **Algorithmic Protocol:** Implement complete, production-ready engines in modern C# (.NET 8/9) and Python (3.11+).

## ⚖️ FAANG Senior / Lead Interview Calibration

In System Design and Algorithmic Architecture interviews (L5/L6), candidates must defend partitioning and traffic shaping primitives with mathematical rigor:

| Mechanism | Technique | Routing / Limiting Time | Scaling Bottleneck | Concurrency Model | Architectural Anchor |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Modulo Sharding** | `Hash(key) % N` | `O(1)` | Full cluster rehash on `N +/- 1` | Stateless | Naive baseline; fatal in production clusters. |
| **Consistent Hashing** | Binary Search on Ring | `O(log(N * V))` | Hot partitions if `V` is small | Reader-Writer Lock / Copy-on-Write | **Amazon DynamoDB, Apache Cassandra, Discord** |
| **Multi-Probe Hashing**| Step probing | `O(k)` | Higher lookup latency | Cache-line friendly | Google Maglev, Envoy Proxy |
| **Token Bucket** | Continuous Refill | `O(1)` | Synchronized atomics on high concurrency | Mutex / Interlocked Compare-Exchange | **Stripe API, AWS API Gateway** |
| **Leaky Bucket** | FIFO Queue Drain | `O(1)` | Drops bursts exceeding queue size | Queue lock contention | Network egress traffic shaping |
| **Sliding Window Log** | Timestamp Sorted Set | `O(log W)` | Unbounded memory per active client | Redis ZSET memory spikes | Fine-grained fraud prevention |

### The Recognition Pattern: "When to Deploy These Primitives?"
1. **Dynamic Node Pools:** Whenever nodes join or depart dynamically (auto-scaling, spot instances, node failure), Consistent Hashing is mandatory to bound key movement to `1 / (N + 1)`.
2. **Burst Tolerant Rate Limiting:** When clients should be permitted to burst up to capacity `C` but sustained to an average rate `R`, Token Bucket is the industry standard.
3. **Zero-Thread Polling Invariant:** Refills must be computed on demand using elapsed wall-clock timestamps `delta_t * R` rather than background polling threads.

---

## 📖 Chapter 1: Context & Motivation

### 1. The Engineering Challenge: Distributed Cache Rehash Stampede

Suppose you manage a distributed caching tier (e.g., Memcached or Redis) with `N = 100` nodes.
A naive sharding function routes requests using modulo hashing:
```
ServerIndex = Hash(Key) % N
```
What happens when server #42 crashes, reducing the cluster from `N = 100` to `N = 99`?
- Key "user:101": `Hash % 100 = 21`, but `Hash % 99 = 48`.
- Key "order:55": `Hash % 100 = 74`, but `Hash % 99 = 12`.
**Catastrophic Consequence:**
Nearly `99%` of all cached keys map to a completely different node! The cache hit rate plummets to zero instantaneously, causing a **thundering herd / cache stampede** that overwhelms downstream primary relational databases and halts production services.

---

### 2. Physical Layout & Ring Topology

#### Consistent Hash Ring with Virtual Nodes
```
                 [0 / 2^32 - 1]
                      |
           Node_C#v1  *
                     / \
      Node_A#v2     *   *   Node_B#v1
                   /     \
    Key "user:9"  x       * Node_A#v1
                 |         |
      Node_B#v2   *       * Node_C#v2
                   \     /
                    *   *   Node_B#v3
                     \ /
                      * Node_A#v3

Routing Rule:
Hash(Key) places a key on the 32-bit ring.
Walk clockwise to the first virtual node encountered.
The physical server owning that virtual node handles the request!
```

#### Token Bucket Continuous Mechanics
```
Capacity = 10 tokens, Refill Rate = 2 tokens/sec
Time t = 0.0s:  Bucket full: [ T T T T T T T T T T ] (10 tokens)
Request arrives: Takes 4 tokens -> Remaining: [ T T T T T T ] (6 tokens)
Time t = 2.5s:  Elapsed delta_t = 2.5s -> Refill = 2.5 * 2 = 5 tokens.
New tokens = min(10, 6 + 5) = 10 tokens!
Calculated in O(1) on-demand on the next request—ZERO background polling threads!
```

---

## 🏛️ Chapter 2: Mathematical Formulation & Governing Invariants

### 1. Consistent Hashing Invariants
- **Ring Invariant:**
  The keyspace is mapped onto a cyclic integer interval `[0, 2^32 - 1]` (or `[0, 2^64 - 1]`).
  Both physical node replicas (virtual nodes) and data keys are mapped into this space using a uniform hash function (e.g., MD5 or Murmur3).
- **Relocation Bound (Monotonicity):**
  When node count changes from `N` to `N + 1`, the expected fraction of keys relocated is:
  ```
  Relocated Fraction = 1 / (N + 1)
  ```
  The remaining `N / (N + 1)` keys stay on their existing servers.
- **Virtual Node Variance Reduction:**
  In a ring with `N` physical nodes and `1` point per node, partition sizes follow an exponential distribution with high standard deviation: the most loaded node can carry `O(log N)` times the average load.
  By placing `V` virtual nodes per physical machine:
  ```
  Standard Deviation of Load = O(1 / sqrt(V))
  ```
  At `V = 100` to `200` virtual nodes per machine, load imbalance drops below `5%`.

### 2. Token Bucket Rate Limiter Formulation
- Let `C` be maximum burst capacity, `R` be refill rate (tokens/sec).
- Let `T_last` be the timestamp of the last request and `Tokens_last` be tokens remaining at `T_last`.
- On incoming request at time `T_now`:
  ```
  delta_t = max(0, T_now - T_last)
  Tokens_current = min(C, Tokens_last + delta_t * R)
  ```
- If `Tokens_current >= Cost`:
  - Allow request.
  - `Tokens_last = Tokens_current - Cost`.
  - `T_last = T_now`.
- Else: Reject request (HTTP 429 Too Many Requests).

---

## 💻 Chapter 3: Idiomatic Dual-Language Implementations

### C# Primary Implementation (.NET 8/9)

```csharp
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AdvancedAlgorithms;

// =============================================================================
// 1. Consistent Hash Ring with Virtual Nodes (O(log(N * V)) Lookup)
// =============================================================================
public sealed class ConsistentHashRing
{
    private readonly int _virtualReplicas;
    private readonly List<uint> _ringKeys = new();
    private readonly Dictionary<uint, string> _ringNodes = new();
    private readonly ReaderWriterLockSlim _rwLock = new();

    public ConsistentHashRing(int virtualReplicas = 150)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(virtualReplicas);
        _virtualReplicas = virtualReplicas;
    }

    private static uint HashKey(string key)
    {
        byte[] hash = MD5.HashData(Encoding.UTF8.GetBytes(key));
        return BitConverter.ToUInt32(hash, 0);
    }

    public void AddServer(string server)
    {
        ArgumentException.ThrowIfNullOrEmpty(server);
        _rwLock.EnterWriteLock();
        try
        {
            for (int i = 0; i < _virtualReplicas; i++)
            {
                uint hash = HashKey($"{server}#v{i}");
                _ringNodes[hash] = server;
                int idx = _ringKeys.BinarySearch(hash);
                if (idx < 0) _ringKeys.Insert(~idx, hash);
            }
        }
        finally
        {
            _rwLock.ExitWriteLock();
        }
    }

    public void RemoveServer(string server)
    {
        ArgumentException.ThrowIfNullOrEmpty(server);
        _rwLock.EnterWriteLock();
        try
        {
            for (int i = 0; i < _virtualReplicas; i++)
            {
                uint hash = HashKey($"{server}#v{i}");
                _ringNodes.Remove(hash);
                int idx = _ringKeys.BinarySearch(hash);
                if (idx >= 0) _ringKeys.RemoveAt(idx);
            }
        }
        finally
        {
            _rwLock.ExitWriteLock();
        }
    }

    public string GetPrimaryNode(string key)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);
        _rwLock.EnterReadLock();
        try
        {
            if (_ringKeys.Count == 0) throw new InvalidOperationException("Ring is empty.");

            uint hash = HashKey(key);
            int idx = _ringKeys.BinarySearch(hash);

            if (idx < 0)
            {
                idx = ~idx;
                // If hash is greater than all virtual nodes, wrap around clockwise to 0
                if (idx == _ringKeys.Count) idx = 0;
            }

            return _ringNodes[_ringKeys[idx]];
        }
        finally
        {
            _rwLock.ExitReadLock();
        }
    }
}

// =============================================================================
// 2. High-Precision Lock-Free Token Bucket Rate Limiter
// =============================================================================
public sealed class TokenBucketRateLimiter
{
    private readonly double _capacity;
    private readonly double _refillRatePerSecond;
    private readonly object _syncRoot = new();

    private double _currentTokens;
    private long _lastRefillTimestampTicks;

    public TokenBucketRateLimiter(double capacity, double refillRatePerSecond)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(refillRatePerSecond);

        _capacity = capacity;
        _refillRatePerSecond = refillRatePerSecond;
        _currentTokens = capacity;
        _lastRefillTimestampTicks = Environment.TickCount64;
    }

    public bool TryAcquire(double tokens = 1.0)
    {
        lock (_syncRoot)
        {
            long nowTicks = Environment.TickCount64;
            double elapsedSeconds = (nowTicks - _lastRefillTimestampTicks) / 1000.0;
            _lastRefillTimestampTicks = nowTicks;

            // Refill tokens based on elapsed wall time
            _currentTokens = Math.Min(_capacity, _currentTokens + elapsedSeconds * _refillRatePerSecond);

            if (_currentTokens >= tokens)
            {
                _currentTokens -= tokens;
                return true;
            }

            return false;
        }
    }
}
```

---

### Python Secondary Implementation (Python 3.11+)

```python
import bisect
import hashlib
import time
import threading
from typing import List, Dict

class ConsistentHashRing:
    """Production Consistent Hash Ring with virtual replicas and O(log(N*V)) routing."""
    def __init__(self, virtual_replicas: int = 150):
        self.virtual_replicas = virtual_replicas
        self.ring_keys: List[int] = []
        self.ring_nodes: Dict[int, str] = {}
        self.lock = threading.RLock()

    @staticmethod
    def _hash(key: str) -> int:
        digest = hashlib.md5(key.encode('utf-8')).digest()
        return int.from_bytes(digest[:4], byteorder='little')

    def add_server(self, server: str) -> None:
        with self.lock:
            for i in range(self.virtual_replicas):
                h = self._hash(f"{server}#v{i}")
                self.ring_nodes[h] = server
                bisect.insort(self.ring_keys, h)

    def remove_server(self, server: str) -> None:
        with self.lock:
            for i in range(self.virtual_replicas):
                h = self._hash(f"{server}#v{i}")
                if h in self.ring_nodes:
                    del self.ring_nodes[h]
                    idx = bisect.bisect_left(self.ring_keys, h)
                    if idx < len(self.ring_keys) and self.ring_keys[idx] == h:
                        self.ring_keys.pop(idx)

    def get_primary_node(self, key: str) -> str:
        with self.lock:
            if not self.ring_keys:
                raise ValueError("Ring is empty")

            h = self._hash(key)
            idx = bisect.bisect_right(self.ring_keys, h)

            # Circular wrap around clockwise
            if idx == len(self.ring_keys):
                idx = 0

            return self.ring_nodes[self.ring_keys[idx]]


class TokenBucketRateLimiter:
    """Continuous Token Bucket Rate Limiter with zero background thread overhead."""
    def __init__(self, capacity: float, refill_rate_per_sec: float):
        self.capacity = float(capacity)
        self.refill_rate = float(refill_rate_per_sec)
        self.tokens = float(capacity)
        self.last_refill = time.monotonic()
        self.lock = threading.Lock()

    def try_acquire(self, tokens: float = 1.0) -> bool:
        with self.lock:
            now = time.monotonic()
            elapsed = now - self.last_refill
            self.last_refill = now

            self.tokens = min(self.capacity, self.tokens + elapsed * self.refill_rate)

            if self.tokens >= tokens:
                self.tokens -= tokens
                return True

            return False
```

---

### 3. High-Concurrency Distributed Rate Limiting & Mechanical Sympathy

In Tier-1 distributed environments (e.g. Stripe, Cloudflare, AWS API Gateway), a basic mutex lock introduces unacceptable cross-thread contention, and local in-memory state cannot enforce limits across a multi-node API cluster. We deploy three architectural patterns:

#### Architecture A: Distributed Redis Lua Script (Atomic Zero-Race Rate Limiter)

In a distributed cluster of microservices, rate-limiting state must reside in a centralized cache (Redis). However, executing naive multi-step logic (`GET tokens -> Calculate -> SET tokens`) is fatal due to race conditions under concurrent requests. 

Instead of heavy distributed locks (Redlock), Redis executes Lua scripts **atomically in a single single-threaded event loop step**:

```lua
-- KEYS[1]: Rate limiter key (e.g., "ratelimit:user_123")
-- ARGV[1]: Max Capacity (tokens)
-- ARGV[2]: Refill Rate (tokens per second)
-- ARGV[3]: Current Timestamp (Unix epoch in seconds with decimals)
-- ARGV[4]: Requested Tokens (cost, typically 1)

local key = KEYS[1]
local capacity = tonumber(ARGV[1])
local refill_rate = tonumber(ARGV[2])
local now = tonumber(ARGV[3])
local requested = tonumber(ARGV[4])

-- Retrieve current state (tokens and last refill timestamp)
local data = redis.call("HMGET", key, "tokens", "last_updated")
local tokens = tonumber(data[1])
local last_updated = tonumber(data[2])

if tokens == nil then
    -- First request: initialize bucket to full capacity
    tokens = capacity
    last_updated = now
else
    -- Compute elapsed time and refill
    local elapsed = math.max(0, now - last_updated)
    tokens = math.min(capacity, tokens + elapsed * refill_rate)
    last_updated = now
end

-- Evaluate acquisition
if tokens >= requested then
    tokens = tokens - requested
    redis.call("HMSET", key, "tokens", tokens, "last_updated", last_updated)
    -- Expire bucket if inactive for the time it takes to fill from 0 to capacity
    local ttl = math.ceil(capacity / refill_rate) * 2
    redis.call("EXPIRE", key, ttl)
    return 1 -- Allowed
else
    -- Persist refilled token level and last updated time even on rejection
    redis.call("HMSET", key, "tokens", tokens, "last_updated", last_updated)
    return 0 -- Rejected (HTTP 429)
end
```

#### Architecture B: Atomic CAS Lock-Free Token Bucket (.NET 8/9 C#)

When building in-process reverse proxies or L7 gateways (e.g. Envoy, YARP) handling hundreds of thousands of requests per second per core, `Monitor` or `lock` creates thread serialization bottlenecks. We implement a **lock-free Token Bucket** using `Interlocked.CompareExchange` over an immutable state record:

```csharp
using System.Diagnostics;
using System.Threading;

public sealed class LockFreeTokenBucket
{
    private readonly double _capacity;
    private readonly double _refillRate;

    private sealed record BucketState(double Tokens, long TimestampTicks);
    private BucketState _state;

    public LockFreeTokenBucket(double capacity, double refillRatePerSecond)
    {
        _capacity = capacity;
        _refillRate = refillRatePerSecond;
        _state = new BucketState(capacity, Stopwatch.GetTimestamp());
    }

    public bool TryAcquire(double cost = 1.0)
    {
        var spinWait = new SpinWait();
        while (true)
        {
            var current = Volatile.Read(ref _state);
            long now = Stopwatch.GetTimestamp();
            double elapsedSeconds = (double)(now - current.TimestampTicks) / Stopwatch.Frequency;
            double refilledTokens = Math.Min(_capacity, current.Tokens + (elapsedSeconds * _refillRate));

            if (refilledTokens < cost)
            {
                return false; // Insufficient tokens; rejected without state mutation
            }

            var next = new BucketState(refilledTokens - cost, now);
            if (Interlocked.CompareExchange(ref _state, next, current) == current)
            {
                return true; // Atomically acquired without locks!
            }

            spinWait.SpinOnce(); // Contention detected; backoff and retry
        }
    }
}
```

#### Architecture C: Mechanical Sympathy & False Sharing Prevention

When running millions of requests per second on multi-core NUMA CPUs, adjacent rate-limiting buckets sharing the same 64-byte CPU cache line trigger **false sharing** (cache line bouncing across L1/L2 caches):

```text
CPU Core 0 (Thread 1)                CPU Core 1 (Thread 2)
Modifying Client A Bucket            Modifying Client B Bucket
         │                                    │
         ▼                                    ▼
┌─────────────────── 64-Byte CPU Cache Line ───────────────────┐
│ Client A State (32 bytes)   │ Client B State (32 bytes)      │
└──────────────────────────────────────────────────────────────┘
Result: Cores invalidate each other's L1 cache constantly, collapsing throughput!
```

**Production Fix:** Pad per-partition bucket structs to 64 bytes using `[StructLayout(LayoutKind.Explicit, Size = 64)]` or cache-line alignment attributes, guaranteeing each worker core's rate limiter lives on an isolated cache line.

---

## 🔬 Chapter 4: Explicit Complexity Deconstruction

| Metric | Modulo Hashing (`H % N`) | Consistent Hash Ring (`N*V`) | Token Bucket |
| :--- | :--- | :--- | :--- |
| **Lookup Time** | `O(1)` | `O(log(N * V))` via Binary Search | `O(1)` |
| **Add/Remove Node** | Rehashes `~100%` of keys | Relocates strictly `1 / (N + 1)` keys | N/A |
| **Memory Footprint** | `O(1)` | `O(N * V)` integers (~64 KB for 100 servers) | `O(1)` per user bucket |
| **Load Imbalance** | Degrades to hot spots | `< 5%` variance with `V = 150` | Exact smooth rate |
| **Concurrency Cost** | None | Read-Write lock / atomic array snapshot | Mutex / atomic CAS spin |

---

## 🎙️ Chapter 5: 45-Minute Verbal Script & Interview Playbook

```
[00:00 - 05:00] Framing the Distributed Challenge
"In large distributed caching and partitioning systems, naive modulo hashing fails because adding
or removing a single node forces a rehash of almost 100% of all keys.
This triggers a catastrophic cache stampede on backend primary databases.
We need Consistent Hashing to bound key relocation to strictly 1/N."

[05:00 - 15:00] Architectural Formulation & Math Defense
"We map both servers and keys onto a cyclic 32-bit hash ring [0, 2^32 - 1].
- A key routes clockwise to the first node encountered.
- Adding a server only takes keys from its immediate clockwise successor, leaving all other nodes untouched.
- However, assigning only 1 point per server causes non-uniform partitions with high variance.
- By introducing V virtual nodes (e.g., V = 150) per physical server, the Central Limit Theorem applies:
  the standard deviation of partition load scales down as O(1 / sqrt(V)), keeping load imbalance below 5%.
- Ring routing uses binary search in O(log(N * V)) time."

[15:00 - 32:00] Live Defensive Implementation
- Implement ConsistentHashRing with AddServer, RemoveServer, and GetPrimaryNode.
- Handle ring wrap-around: if binary search index equals ring size, route clockwise to index 0.
- Implement TokenBucketRateLimiter: calculate refill dynamically on-demand using elapsed wall time.
  Emphasize that this requires zero background timer threads!

[32:00 - 40:00] Edge Cases & Failure Recovery
- What if a server crashes? RemoveServer removes its virtual nodes in O(V log(N*V)).
- Ring Wrap: Verify that keys with hash greater than the last virtual node wrap to index 0.
- Concurrency: In C#, use ReaderWriterLockSlim or an immutable array snapshot for lock-free reads.

[40:00 - 45:00] Real-World Production Systems Anchors
- Amazon DynamoDB and Apache Cassandra use this ring for partition routing and multi-datacenter replica sets.
- Envoy Proxy and Twemproxy use ketama consistent hashing for memcached/redis connection pools.
- Stripe API rate limiter employs Token Bucket per client API key to throttle bursts without starvation."
```

---

## 🔍 Chapter 6: Edge-Case Verification

| Test Case | Scenario | Expected Outcome | Invariant Handling |
| :--- | :--- | :--- | :--- |
| **Empty Ring Query** | No servers registered | Throws `InvalidOperationException` | Guard clause prevents null-pointer dereference. |
| **Hash Greater Than Last Node** | `Hash(key) > max(ringKeys)` | Routes to `ringKeys[0]` | Handled by `if (idx == ringKeys.Count) idx = 0`. |
| **Single Physical Node** | `N = 1` server | All keys route to that node | Ring contains `V` points, all pointing to the single node. |
| **Bursty Traffic Rate Limiter** | 10 requests at `t = 0` | Allows burst up to `capacity` | Bucket drops to 0 tokens; 11th request rejected with `false`. |
| **Zero Elapsed Time Refill** | Successive queries in same ms | Zero spurious token generation | Refill calculation `elapsed * rate` handles microsecond intervals safely. |

---

> 🧭 **Navigation:** [← Previous Day](Week_18_Day_04_Probabilistic_Data_Structures_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Week Playbook →](WEEK_18_FULL_PLAYBOOK.md)
