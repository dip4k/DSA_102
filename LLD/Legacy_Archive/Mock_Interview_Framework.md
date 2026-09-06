# 🎤 Mock Interview Framework & Practice Guide

## Table of Contents
1. Interview Structure & Timing
2. Pre-Interview Preparation Checklist
3. During Interview: Step-by-Step Framework
4. Communication Techniques
5. Handling Difficult Situations
6. Mock Interview Rubric (How to Evaluate)
7. Practice Schedule
8. Recording & Self-Review

---

## 1️⃣ Interview Structure & Timing

### LLD Interview (45-60 minutes)

```
0-5 min:   Clarifications & Requirements
5-10 min:  High-level approach + Class diagram
10-35 min: Implementation (pseudocode or actual code)
35-40 min: Edge cases & error handling
40-45 min: Follow-ups (scalability, extensibility)
45-60 min: (Optional) Deep dives or additional problems
```

### HLD Interview (45-60 minutes)

```
0-5 min:   Clarifications & Requirements
5-10 min:  Estimation (back-of-envelope)
10-25 min: High-level architecture (components, data flow)
25-40 min: Deep dives (1-2 key components in detail)
40-45 min: Scaling & failure scenarios
45-60 min: Follow-ups & trade-offs
```

### Combined Interview (90 minutes)

```
0-45 min:  HLD (full system design)
45-75 min: LLD (deep dive on one component)
75-90 min: Follow-ups & Polish
```

---

## 2️⃣ Pre-Interview Preparation Checklist

### 1 Week Before

- [ ] Review syllabi relevant sections
- [ ] Do 2-3 similar case studies to the likely problem
- [ ] Record yourself explaining a design (45 min)
- [ ] Get feedback from peer
- [ ] Practice on whiteboard (if remote, use Zoom)

### 1 Day Before

- [ ] Good night's sleep (don't cram!)
- [ ] Light review of key concepts (30 min)
- [ ] Prepare physical setup (whiteboard, pen, paper)
- [ ] Have water nearby

### 1 Hour Before

- [ ] Use the bathroom
- [ ] Take 5 deep breaths
- [ ] Review interview tips
- [ ] Clear your mind

---

## 3️⃣ During Interview: Step-by-Step Framework

### Phase 1: Clarifications (5-10 minutes)

**Your Goal:** Understand requirements completely before designing

**Questions to Ask (LLD):**

```
Functional Requirements:
- "What are the main operations the system must support?"
- "Are there any specific constraints on input/output?"
- "Do we need to handle edge cases like [X]?"

Non-Functional Requirements:
- "How many users/requests do we need to support?"
- "Is thread-safety a concern?"
- "Are there performance requirements (latency, throughput)?"
- "Is extensibility important for future changes?"
```

**Questions to Ask (HLD):**

```
Scale:
- "How many users/requests per day?"
- "What's the peak QPS we need to handle?"
- "How much data needs to be stored?"

Consistency:
- "Do we need strong consistency or is eventual OK?"
- "What's the acceptable latency?"
- "How important is availability vs consistency?"

Operations:
- "Do we need multi-region support?"
- "What are SLA requirements?"
```

**Do's and Don'ts:**

✅ DO:
- Ask clarifying questions (shows you think systematically)
- Confirm your understanding ("So to clarify, you need...?")
- Listen carefully to answers

❌ DON'T:
- Make assumptions (ask instead)
- Be afraid to ask "dumb" questions
- Rush through clarifications

---

### Phase 2: Estimation (HLD Only)

**For HLD, always estimate before designing!**

**Framework:**

1. **State your assumptions** (out loud)
   - "I assume 500M DAU with 10% daily active"
   - "Read-write ratio is 100:1 based on typical social apps"

2. **Calculate QPS**
   ```
   QPS = (500M × 10% × 5 requests) / 86,400 ≈ 290k QPS
   Peak = 290k × 4 ≈ 1.2M QPS
   ```

3. **Calculate storage**
   ```
   Per-post: 1 KB
   Daily posts: 500M × 0.1 = 50M posts
   Yearly: 50M × 365 = 18.25B posts
   Storage: 18.25B × 1 KB ≈ 18 TB
   ```

4. **Explain trade-offs**
   - "At this scale, a single database can't handle writes"
   - "We need caching for read performance"
   - "Sharding becomes necessary"

**Say Out Loud (Thinking Process):**
> "So with 1.2M QPS peak, a single database can handle maybe 10k QPS. That means I need 120 database nodes if I shard. But wait, let me recalculate with caching. If cache hit rate is 80%, then actual DB QPS is 1.2M × 0.2 = 240k QPS. That's 24 nodes, which is more reasonable."

---

### Phase 3: High-Level Architecture

**LLD Approach:**

1. **Draw UML diagram** (classes, relationships)
   ```
   User ──┐
          └─→ Cache ──→ EvictionPolicy
   Order ──┐
          └─→ Database
   ```

2. **Explain key classes** and their responsibilities
3. **Show state transitions** (if applicable)
4. **Define API** (method signatures)

**HLD Approach:**

1. **Draw system architecture** (components, data flow)
   ```
   Client → Load Balancer → API Servers
                                 ↓
                            Cache (Redis)
                                 ↓
                           Database (Sharded)
                                 ↓
                          Message Queue
   ```

2. **Explain data flow** (step-by-step)
   - "User posts content → API server → validate → store in DB → cache → return"

3. **Justify each component**
   - "We use Redis for caching hot posts"
   - "We shard by user_id for horizontal scaling"

---

### Phase 4: Implementation / Deep Dive

**LLD:**

1. **Start with core logic** (essential methods)
2. **Add thread safety** (if needed)
3. **Handle edge cases**
4. **Explain as you code**

**Example:**
```java
// Thinking aloud:
"I'll start with the basic structure. We need a cache with 
a HashMap for storage and a LinkedList for LRU ordering. 
The HashMap maps key to node, and the LinkedList maintains 
order of access. Get operation moves node to front..."

public class LRUCache<K, V> {
    private Node<K,V> head, tail;
    private Map<K, Node<K,V>> cache;
    private int capacity;

    public V get(K key) {
        if(!cache.containsKey(key)) return null;
        Node<K,V> node = cache.get(key);
        moveToFront(node);  // Mark as recently used
        return node.value;
    }

    public void put(K key, V value) {
        if(cache.containsKey(key)) {
            // Update existing
            Node<K,V> node = cache.get(key);
            node.value = value;
            moveToFront(node);
        } else {
            // Add new
            if(cache.size() == capacity) {
                // Evict least recently used (tail)
                cache.remove(tail.key);
                removeNode(tail);
            }
            Node<K,V> newNode = new Node<>(key, value);
            addToFront(newNode);
            cache.put(key, newNode);
        }
    }
}
```

**HLD:**

1. **Deep dive into 1-2 key components**
   - Database sharding strategy
   - Cache architecture
   - Message queue design

2. **For each component:**
   - How data flows through it
   - Why you chose this approach
   - Trade-offs vs alternatives

---

### Phase 5: Edge Cases & Error Handling

**Mention these naturally:**

LLD:
- "What if the cache is full?"
- "What if we get concurrent requests for the same key?"
- "What if we insert null?"

HLD:
- "What if a database shard dies?"
- "What if cache goes down?"
- "What if we get a sudden traffic spike?"

---

### Phase 6: Follow-ups & Optimizations

**Be ready for:**

LLD:
- "How would you add LFU eviction?"
- "How would you make this distributed?"
- "How would you handle [new requirement]?"

HLD:
- "How would you reduce latency?"
- "How would you handle 10x traffic?"
- "How would you ensure data consistency?"

---

## 4️⃣ Communication Techniques

### Technique 1: Thinking Out Loud

**Do this:**
```
"Okay, so the requirement is to handle 1M QPS. 
A single database can handle maybe 10k QPS. 
That means I need at least 100 databases. 
But how do I partition the data? 
If I shard by user_id using hash-based sharding, 
then each database gets roughly 1/100 of the data.
When I query for a user, I can compute shard_id = hash(user_id) % 100.
This gives me O(1) lookup. But I need to handle replication 
for fault tolerance, so let's say 3x replication..."
```

**Benefits:**
- Shows your thought process
- Interviewer can help if you go wrong
- Demonstrates systematic thinking

### Technique 2: Explain as You Draw

```
"I'm drawing the API servers here because they're stateless 
and can scale horizontally. Behind them is a load balancer 
that distributes requests evenly. Then we have a cache layer 
because reads are 100x write, so we want to avoid hitting 
the database for every read..."
```

### Technique 3: Justify Each Decision

```
"I chose PostgreSQL over MongoDB for this because:
1. We have highly structured data (users, posts, likes)
2. We need ACID transactions (likes must be counted accurately)
3. We have complex queries with joins (user → posts → likes)

Trade-off: PostgreSQL doesn't scale horizontally as easily, 
but for this problem, sharding by user_id solves that."
```

### Technique 4: Confirm Understanding

```
"Just to make sure I understand correctly: 
- You want to design a system for 1M concurrent users
- It needs to be available 99.99% of the time
- Consistency is important but 1-2 second delay is OK
- Is that right?"
```

---

## 5️⃣ Handling Difficult Situations

### Situation 1: You Don't Know Something

**Bad:**
```
Interviewer: "How would you implement consistent hashing?"
You: "Umm... I'm not sure. Let me think..."
(Long silence)
```

**Good:**
```
Interviewer: "How would you implement consistent hashing?"
You: "I'm not deeply familiar with the implementation details,
but I know the key idea: instead of hash(key) % N, 
we use a ring of virtual nodes. This way, when we add a node,
only 1/N data needs to migrate. Can I explain the concept 
and then we can dive into implementation if needed?"
```

### Situation 2: Interviewer Challenges Your Design

**Bad:**
```
Interviewer: "Why use eventual consistency? Won't that cause problems?"
You: (defensive) "Well, it's fine because..."
```

**Good:**
```
Interviewer: "Why use eventual consistency? Won't that cause problems?"
You: "That's a great question. You're right that eventual 
consistency has trade-offs. For this use case (social feed),
users seeing a 1-2 second delay is acceptable, and it lets us
achieve better availability and scalability. But if this were 
a financial system, I'd use strong consistency instead, even 
though it's slower. Does that make sense?"
```

### Situation 3: You Realize Your Design is Wrong

**Bad:**
```
(Keep talking, hoping interviewer doesn't notice)
```

**Good:**
```
"Actually, wait. I realize there's an issue with sharding 
by user_id because that makes queries across users expensive.
Let me reconsider... Maybe I should shard by post_id instead. 
That way, querying all posts in a time range is efficient.
Thoughts?"
```

### Situation 4: You're Running Out of Time

**Bad:**
```
(Rush through last parts, incomplete)
```

**Good:**
```
"I see we're running short on time. Let me quickly sketch 
the remaining components: we'd use Redis for caching, Kafka 
for event streaming, and multiple databases sharded by user_id. 
Happy to go deeper into any of these if you'd like."
```

---

## 6️⃣ Mock Interview Rubric (Evaluation)

Use this to score yourself (10 = excellent, 1 = poor):

### Clarity & Communication
- [ ] Explained reasoning clearly
- [ ] Used diagrams effectively
- [ ] Spoke at right pace (not too fast)
- [ ] Avoided jargon without explanation
- **Score: __/10**

### Systematic Approach
- [ ] Asked clarifying questions first
- [ ] Made reasonable assumptions
- [ ] Broke problem into components
- [ ] Justified each decision
- **Score: __/10**

### Technical Depth
- [ ] Understood core concepts well
- [ ] Could explain trade-offs
- [ ] Handled edge cases
- [ ] Knew when to go deeper vs stay high-level
- **Score: __/10**

### Problem Solving
- [ ] Identified correct bottlenecks
- [ ] Proposed reasonable solutions
- [ ] Could handle follow-ups
- [ ] Adapted to feedback
- **Score: __/10**

### Time Management
- [ ] Used time efficiently
- [ ] Didn't get stuck on details
- [ ] Covered all important parts
- [ ] Had time for follow-ups
- **Score: __/10**

**Total Score: __/50**

### Scoring Guide:
- 45-50: Excellent (ready for FAANG interview)
- 40-44: Good (few gaps to fill)
- 35-39: Adequate (needs more practice)
- <35: Needs significant improvement

---

## 7️⃣ Practice Schedule

### Week 1: LLD Problems
```
Monday:   Parking Lot (1 hour)
Tuesday:  Vending Machine (1 hour)
Wednesday: Movie Booking (1.5 hours)
Thursday:  LRU Cache (1.5 hours)
Friday:    Review + weak areas (1 hour)
Weekend:   Rest or extra practice
```

### Week 2: HLD Case Studies
```
Monday:   URL Shortener (1.5 hours)
Tuesday:  News Feed (1.5 hours)
Wednesday: Chat System (2 hours)
Thursday:  Rate Limiter (1.5 hours)
Friday:    Uber/Geospatial (2 hours)
```

### Week 3: Mock Interviews
```
Monday:   LLD mock (1 hour) + review (30 min)
Tuesday:  HLD mock (1 hour) + review (30 min)
Wednesday: Combined mock (1.5 hours) + review (1 hour)
Thursday:  Weak area deep dive (1.5 hours)
Friday:    Final review + confidence building (1 hour)
```

---

## 8️⃣ Recording & Self-Review

### How to Record Yourself

1. **Setup:** Use Zoom or Google Meet (record locally)
2. **Whiteboard:** Physical or digital (Excalidraw)
3. **Talk:** Explain as if to interviewer
4. **Record:** Screen + audio
5. **Review:** Watch within 24 hours while fresh

### Self-Review Checklist

- [ ] Did I ask clarifying questions?
- [ ] Did I estimate before designing?
- [ ] Was my explanation clear and logical?
- [ ] Did I explain reasoning, not just conclusions?
- [ ] Did I handle edge cases?
- [ ] Did I respond well to feedback?
- [ ] What was my biggest weakness?
- [ ] What was my biggest strength?

### Common Issues to Watch For

❌ **Jumping to solution too fast**
- Fix: Ask questions first, estimate, then design

❌ **Not explaining your thinking**
- Fix: Talk more, explain every decision

❌ **Over-complicating early**
- Fix: Start simple, add complexity only if needed

❌ **Not adapting to feedback**
- Fix: Listen when interviewer questions a choice

❌ **Ignoring operational concerns**
- Fix: Mention monitoring, failover, SLOs

---

## 9️⃣ Interview Day Checklist

**Morning Of:**
- [ ] Good breakfast and water
- [ ] Use bathroom
- [ ] Warm up brain (review key concepts, 30 min)
- [ ] Test technical setup (camera, screen share, audio)
- [ ] Start 10 min early, calm yourself

**During Interview:**
- [ ] Smile (even on video, it comes through)
- [ ] Make eye contact (or look at camera)
- [ ] Sit up straight
- [ ] Speak clearly
- [ ] Breathe (don't rush)
- [ ] Think before answering
- [ ] Ask clarifying questions
- [ ] Explain your reasoning
- [ ] Embrace follow-ups (shows you're thinking)

**After Interview:**
- [ ] Thank interviewer
- [ ] Ask when you'll hear back
- [ ] Don't immediately critique yourself (takes time to sink in)

---

## 🔟 Final Tips

✅ **Do:**
- Practice multiple times
- Record and review
- Get peer feedback
- Ask for help on weak areas
- Believe in yourself

❌ **Don't:**
- Cram night before
- Try to memorize solutions
- Panic on follow-ups
- Give up on difficult questions
- Assume you know the answer without asking

---

**You're ready! Go crush this interview! 🚀**
