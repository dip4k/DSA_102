# 🎯 Quick Reference Card (Print & Bookmark!)

## Interview Timing Breakdown

### LLD Interview (45-60 minutes)
```
0-5 min:    Clarifications & Requirements
5-10 min:   High-level approach + Diagram
10-35 min:  Implementation (code/pseudocode)
35-40 min:  Edge cases & error handling
40-45 min:  Follow-ups (scalability, extensibility)
```

### HLD Interview (45-60 minutes)
```
0-5 min:    Clarifications & Requirements
5-10 min:   Estimation (QPS, storage, bandwidth)
10-25 min:  High-level architecture
25-40 min:  Deep dives (1-2 key components)
40-45 min:  Scaling & failure scenarios
45-60 min:  Follow-ups & trade-offs
```

---

## Key Numbers to Memorize

### Time Metrics
```
1 ns         L1 cache hit
100 ns       Memory access
1 µs         SSD read
10 ms        HDD seek / DB query
1 ms         Network roundtrip (local)
100 ms       Network roundtrip (US)
150 ms       Network roundtrip (global)
```

### Storage Metrics
```
1 KB         1,000 bytes
1 MB         1,000,000 bytes
1 GB         1 billion bytes
1 TB         1 trillion bytes
1 PB         1 quadrillion bytes
```

### User & Traffic Multipliers
```
Peak QPS    ≈ Average QPS × 3-5
Concurrent  ≈ (DAU × online%) / 60 × session_mins
Servers     ≈ Peak_QPS / QPS_per_server
```

---

## Database Selection Decision Tree

```
Structured data?
├─ YES → SQL (PostgreSQL, MySQL)
│        └─ Complex transactions → SQL ✓
│        └─ Massive scale → SQL + Sharding ✓
│
└─ NO → Continue...
   ├─ Time-series? → InfluxDB, TimescaleDB
   ├─ Write-heavy massive? → Cassandra, HBase
   ├─ Need flexibility? → MongoDB
   ├─ Fast key-value? → Redis, DynamoDB
   └─ Full-text search? → Elasticsearch
```

---

## Sharding Strategies

| Strategy | When | Trade-off |
|----------|------|-----------|
| **Range** | Time-based, geographic | Uneven distribution |
| **Hash** | Uniform distribution | Rehash on add server |
| **Consistent Hash** | Large scale, rebalancing | Complex |
| **Directory** | Flexible rebalancing | Lookup latency |

---

## Estimation Formula Cheat Sheet

### QPS
```
Average QPS = (DAU × requests/user) / 86,400
Peak QPS = Average QPS × 3-5
```

### Storage
```
Storage = (Daily_writes × bytes_per_write × days_retained) × 3_for_replication
```

### Bandwidth
```
Required_bandwidth = Peak_QPS × avg_response_size
```

### Servers
```
Servers_needed = Peak_QPS / throughput_per_server × redundancy_factor
```

---

## SOLID Principles Quick Guide

| Principle | In One Line |
|-----------|------------|
| **S**ingle Responsibility | One reason to change |
| **O**pen/Closed | Extend without modifying |
| **L**iskov Substitution | Subclass = usable as parent |
| **I**nterface Segregation | Lean interfaces |
| **D**ependency Inversion | Depend on abstractions |

---

## Design Patterns Cheat Sheet

### Creational (Object Creation)
```
Singleton    - One instance (config, logger)
Factory      - Hide creation logic
Builder      - Complex object construction
Prototype    - Deep copy objects
```

### Structural (Object Composition)
```
Adapter      - Make incompatible interfaces work
Decorator    - Add behavior dynamically
Proxy        - Control access/lazy load
Facade       - Simplify complex subsystem
```

### Behavioral (Object Interaction)
```
Observer     - Notify listeners of events
Strategy     - Swap algorithms at runtime
State        - State-based behavior change
Command      - Encapsulate requests
```

---

## Caching Strategies

| Strategy | Pros | Cons |
|----------|------|------|
| **Cache-Aside** | Simple, only cache used data | Slow on miss |
| **Write-Through** | Always consistent | Two writes |
| **Write-Behind** | Fast writes | Data loss risk |

---

## Common Interview Follow-ups

### Scaling
- "What happens with 10x traffic?"
- "How do you handle hot partitions?"
- "How would you reduce latency?"

### Databases
- "Why this database over that one?"
- "How would you shard this data?"
- "What about data consistency?"

### Reliability
- "What if a database dies?"
- "How do you prevent duplicate processing?"
- "How do you monitor this system?"

---

## Red Flags to Avoid

❌ Unclear API (ambiguous methods)
❌ No error handling
❌ Tight coupling (untestable)
❌ Race conditions (unprotected shared state)
❌ Memory leaks
❌ Cryptic naming
❌ God classes
❌ Hard-coded behaviors

---

## Pre-Interview Checklist

### 1 Hour Before
- [ ] Use bathroom
- [ ] Drink water
- [ ] 5 deep breaths
- [ ] Calm mindset

### Day Of (Morning)
- [ ] Good breakfast
- [ ] Review key concepts (30 min)
- [ ] Test tech setup
- [ ] Start 10 min early

### During Interview
- [ ] Ask clarifying questions
- [ ] Explain your reasoning
- [ ] Think before answering
- [ ] Embrace follow-ups
- [ ] Stay calm

---

## Self-Scoring Rubric (Quick Version)

Rate 1-10 on each:

```
1. Clarity & Communication    __ / 10
2. Systematic Approach        __ / 10
3. Technical Depth            __ / 10
4. Problem Solving            __ / 10
5. Time Management            __ / 10
   ────────────────────────────────
   Total:                      __ / 50
```

**Score Guide:**
- 45-50: Ready! 🚀
- 40-44: Good, minor gaps
- 35-39: Needs more practice
- <35: Significant work needed

---

## Common Estimation Examples

### Social Media (Twitter-like)
```
QPS: 300k peak (500M DAU, 5 req/user/day)
Storage: 550 TB (5 years, 3x replication)
Servers: 30 (with redundancy & regions)
Databases: Sharded by user_id
Cache: Redis for hot content
```

### Ride-Sharing (Uber)
```
QPS: 50k peak (1M trips/day)
Storage: 5.4 TB (5 years, replication)
Location updates: 10k QPS baseline
Servers: 10-15 (trip + location)
Database: SQL for trips, NoSQL for locations
```

### Video Streaming (Netflix)
```
Concurrent: 12M viewers at peak
Bandwidth: 7.5 PB/sec (with CDN reduction)
Storage: 1 PB (distributed to edges)
Servers: 400+ edge locations
Database: Distributed, eventual consistency
```

---

## Interview Communication Tips

### ✅ Do This

"Let me clarify the requirements first...
I'll estimate QPS as follows...
My approach is to...
The trade-off here is...
Let me explain my reasoning...
Does that make sense?"

### ❌ Don't Do This

[Immediately starts coding]
[Doesn't ask questions]
[Doesn't explain thinking]
[Rushes through clarifications]
[Defensive when questioned]

---

## Problem Type Quick Reference

### LLD Problem? Look for:
- "Design a class"
- "Implement a system"
- "45-minute problem"
→ Use: SOLID, patterns, code quality

### HLD Problem? Look for:
- "Design a service"
- "Hundreds of millions of users"
- "Trade-offs important"
→ Use: CAP, scalability, components

---

## File Quick Links

```
Need database decision?
→ HLD_Supplementary_Databases.md

Need to estimate?
→ HLD_Supplementary_Estimation.md

Need design pattern?
→ LLD_Supplementary_Templates.md

Need interview practice?
→ Mock_Interview_Framework.md

Need full structure?
→ Supplementary_Master_Index.md
```

---

## One-Minute Cheat Sheet

**Interview starts:**
1. ✓ Ask clarifying questions
2. ✓ Make assumptions explicit
3. ✓ Estimate (if HLD)
4. ✓ Draw architecture/class diagram
5. ✓ Explain data flow
6. ✓ Discuss trade-offs
7. ✓ Think about failures
8. ✓ Prepare for follow-ups

**Stay calm. Think systematically. Explain clearly. You've prepared well!**

---

## Emergency Tips (If You Get Stuck)

**Stuck on HLD?**
→ "Let me think about this. So the challenge is [issue]. I could solve it with [solution 1] or [solution 2]. Let me discuss the trade-offs..."

**Stuck on LLD?**
→ "Let me reconsider the design. I think the issue is [problem]. I'd refactor by [approach] to handle this better."

**Don't Know an Answer?**
→ "I'm not familiar with the exact implementation, but the concept is [explanation]. Can we explore that?"

**Interviewer Challenges You?**
→ "That's a good point. You're right that [trade-off]. Let me reconsider... Alternatively, we could..."

---

**Print this page. Reference before interview. Bookmark for life! 🚀**
