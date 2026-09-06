# FAANG System Design Interview Preparation: Master Plan

## 📋 Overview

You have two comprehensive, interview-optimized syllabi ready:

1. **FAANG_LLD_Syllabus.md** - Low-Level Design / Machine Coding (6-8 weeks)
2. **FAANG_HLD_Syllabus.md** - High-Level Design / System Design (8-12 weeks)

This document provides:
- Quick comparison between LLD and HLD
- How they complement each other
- Recommended prep timeline
- Key focus areas by seniority level

---

## 🔍 Quick Comparison: LLD vs HLD

| Aspect | LLD | HLD |
|--------|-----|-----|
| **Focus** | Classes, OOP, design patterns | Services, databases, scalability |
| **Scope** | Single component/module | Entire system architecture |
| **Code** | Implement actual code | Sketch and discuss design |
| **Time** | 45-60 minutes | 45-60 minutes per design |
| **Tools** | OOP languages (Java, C#, Python) | Whiteboard, system thinking |
| **Interview** | "Design a Parking Lot" | "Design Twitter" |
| **Duration** | 6-8 weeks to master | 8-12 weeks to master |
| **Emphasis** | SOLID, patterns, thread safety | Trade-offs, scalability, CAP |

---

## 📚 What Each Syllabus Covers

### LLD Syllabus (6-8 weeks)

**Phase A: Runtime Mastery (Weeks 1-2)**
- Memory models, heap/stack, escape analysis
- Garbage collection deep dive (GC impact on design)
- CPU cache, virtual dispatch costs
- Threading primitives, synchronization, deadlock prevention
- Lock-free programming, atomics, false sharing

**Phase B: Asynchronous Patterns (Week 3)**
- Futures, promises, async/await mechanics
- Producer-consumer pattern, bounded queues
- Rate limiting algorithms (Token Bucket, Leaky Bucket)
- Event loops vs thread pools

**Phase C: OOD & Design Principles (Weeks 4-5)**
- SOLID principles (deep, not surface level)
- Composition vs inheritance, when to use each
- Gang of Four patterns (creational, structural, behavioral)
- Code smells and refactoring

**Phase D: Machine Coding Practice (Weeks 6-8)**
- Real-world systems: Parking Lot, Movie Booking, Food Ordering
- Technical systems: LRU Cache, Logging Framework, Task Scheduler
- API design, extensibility, production considerations

**Key Interview Questions:**
- "How does your design impact GC pressure?"
- "How would you make this thread-safe?"
- "Why inheritance here instead of composition?"
- "Walk me through the state transitions."

---

### HLD Syllabus (8-12 weeks)

**Phase A: Distributed Fundamentals (Weeks 1-3)**
- Scalability dimensions (vertical vs horizontal)
- CAP theorem and PACELC (when to choose consistency vs availability)
- Consistency models (strong, eventual, causal)
- Networking: TCP vs UDP, HTTP/1.1 vs HTTP/2 vs HTTP/3
- Load balancing and consistent hashing
- Consensus and coordination (Raft basics)

**Phase B: Data & Storage Mastery (Weeks 4-6)**
- SQL: B-trees, sharding, replication patterns
- NoSQL: Key-value, document, wide-column stores
- LSM trees and write-optimized databases
- Caching strategies (cache-aside, write-through, write-back)
- CDNs and global load balancing
- Message queues: Kafka vs RabbitMQ

**Phase C: System Design Case Studies (Weeks 7-11)**
- Read-heavy: URL Shortener, News Feed
- Write-heavy: Chat System, Rate Limiter (distributed)
- Geo-spatial: Uber / Ride-sharing
- Real-time: Leaderboard, Typeahead
- Complex: Video Streaming, Collaborative Editing

**Phase D: Advanced Topics (Week 12+)**
- Observability: Metrics, logs, traces
- SLOs, SLIs, SLAs and error budgets
- Deployment strategies (blue-green, canary, feature flags)
- Back-of-envelope estimation mastery
- Trade-off analysis and when to change your mind

**Key Interview Questions:**
- "Estimate QPS and storage for this system."
- "Why this database over that one? What are the trade-offs?"
- "How would you handle a data center failure?"
- "Walk me through the data flow for a request."

---

## 🎯 Recommended Timeline by Scenario

### Scenario 1: Preparing for FAANG Interview (3 Months)

**Month 1: LLD Foundation (4 weeks)**
- Weeks 1-2: Fundamentals (memory, concurrency, runtime)
- Weeks 3-4: OOD and design patterns
- Daily: 4-5 hours
- Output: Can design 5+ small systems cleanly

**Month 2: HLD Fundamentals + Practice (4 weeks)**
- Weeks 5-6: Distributed systems + databases
- Weeks 7-8: Case study designs (5+ systems)
- Daily: 5-6 hours
- Output: Can estimate and design large-scale systems

**Month 3: Refinement & Mocks (4 weeks)**
- Weeks 9-11: Deep dives on weak areas, advanced topics
- Week 12: Mock interviews, feedback, polish
- Daily: 4-5 hours
- Output: Confident, can handle follow-ups smoothly

**Total: ~80-100 hours (intense, focused)**

---

### Scenario 2: Already Strong on One Track

**If you're strong on LLD (OOP, patterns):**
- Skip Phase A & C of LLD
- Spend 4-6 weeks on HLD using the full syllabus
- Focus on: distributed systems fundamentals + case studies

**If you're strong on HLD (familiar with systems):**
- Skip Phase A of HLD
- Spend 4-6 weeks on LLD using the full syllabus
- Focus on: SOLID, patterns, thread safety, machine coding practice

---

### Scenario 3: Preparing for Staff-Level Interview

**Recommended: 12-16 weeks**
- Weeks 1-4: LLD (full depth, focus on thread safety and optimization)
- Weeks 5-8: HLD (full depth, include advanced topics)
- Weeks 9-12: Deep dives
  - Multi-region systems
  - Operational excellence (observability, SLOs)
  - Trade-off mastery and context-specific decisions
  - Real system design papers (Google Bigtable, Dynamo, Cassandra)
- Weeks 13-16: Refinement and leadership aspects
  - How would you lead this design discussion?
  - Team structure implications
  - Cost optimization

---

## 🔄 How They Complement Each Other

**LLD** teaches you the **building blocks**:
- Clean code patterns
- Thread-safe data structures
- Extensible components

**HLD** teaches you how to **assemble them at scale**:
- When to use which patterns
- Trade-offs between consistency and availability
- Handling distribution and failures

**Together**, they show you how to design systems that are:
- ✅ Clean and maintainable (LLD)
- ✅ Scalable and reliable (HLD)
- ✅ Operationally sound (both)

**Example: Designing a Chat System**

LLD perspective:
- Thread-safe message queue class
- Connection manager with proper cleanup
- State machine for message delivery

HLD perspective:
- Distribute across regions
- Choose database (Cassandra for time-series)
- Ensure messages ordered globally
- Handle millions of concurrent connections

---

## 📊 Interview Formats & Structure

### Format 1: LLD Only (45-60 minutes)
- Design a class or module
- Write pseudocode or actual code
- Discuss extensibility, thread safety
- Handle follow-ups

**Companies:** Some startups, specific teams

---

### Format 2: HLD Only (45-60 minutes)
- Design a large-scale system
- Discuss architecture and trade-offs
- Estimate capacity requirements
- Handle follow-ups and optimizations

**Companies:** Most FAANG for mid-level; all for senior

---

### Format 3: Both (90+ minutes total)
- HLD: Design the system (45 min)
- LLD: Deep dive on one component (45 min)
- Often combined as "deeper dives" mid-discussion

**Companies:** FAANG for senior/staff positions

---

### Format 4: Pair Programming / Machine Coding (2-4 hours)
- Complete LLD design in actual code
- More realistic but less common now

**Companies:** Some FAANG, many non-FAANG

---

## 🎓 By Seniority Level

### Entry / Mid-Level (SDE II)
**Both are important**
- LLD: 70% of prep (clean code, patterns, fundamentals)
- HLD: 30% of prep (basic system thinking, 5-6 case studies)

**Mock Interview Focus:**
- LLD: "Design a Parking Lot" (40 min, perfect implementation)
- HLD: "Design YouTube" (40 min, high-level, fewer deep dives)

---

### Senior (SDE III / L5)
**Both equally important**
- LLD: 40% (deep thread safety, optimization, extensibility)
- HLD: 60% (trade-offs, scaling, operational excellence)

**Mock Interview Focus:**
- LLD: "Design an LRU Cache" (30 min, perfect + follow-ups)
- HLD: "Design Uber" (50 min, scaled, operational concerns)

---

### Staff (L6+)
**HLD > LLD, but LLD depth matters**
- LLD: 25% (optimization, production patterns, deep knowledge)
- HLD: 75% (scaling challenges, trade-off mastery, architecture evolution)

**Mock Interview Focus:**
- LLD: "Design a Logging Framework" (30 min, production-grade)
- HLD: "Design a Multi-region Uber" (60+ min, deep discussion, leadership)

---

## 💡 Key Differences in Approach

### LLD Interview Tips

1. **Clarify deeply:** Ask about threads, scale, extensibility
2. **Design systematically:**
   - Entities → Relationships → APIs → Implementation
3. **Think out loud:** Explain your design choices
4. **Code quality:** Clean naming, proper encapsulation, SOLID
5. **Anticipate changes:** How would the design evolve?

### HLD Interview Tips

1. **Clarify requirements:** Functional + Non-functional (scale, latency, consistency)
2. **Back-of-envelope first:** Numbers drive architecture
3. **Discuss trade-offs:** Not just "what" but "why" and "when"
4. **Deep dive strategically:** 2-3 components deeply, others at high level
5. **Failure scenarios:** "What happens when a database dies?"

---

## 🚀 Quick Start: What to Do This Week

**Day 1-2: Read Overviews**
- Skim both syllabi
- Identify your weak areas
- Determine your timeline (3 weeks? 12 weeks?)

**Day 3-5: Start with YOUR Gap**
- Weak on OOP/patterns? Start with LLD Phase C
- Weak on distributed systems? Start with HLD Phase A
- No gaps? Start with LLD Phase A, then HLD Phase A

**Week 2: First Case Study**
- For LLD: Design a Parking Lot (from syllabus)
- For HLD: Estimate and design a URL Shortener
- Record yourself explaining it
- Iterate based on weaknesses

**Week 3+: Build Momentum**
- Complete one case study per 2-3 days
- Track progress
- Do mock interviews with peers

---

## 📝 Customization Guide

### If you have 3 weeks
- Skip Phase A of both
- Focus on LLD Phase C (patterns) + HLD case studies
- Prioritize: 3-4 LLD problems + 5-6 HLD problems

### If you have 8 weeks
- Go through both syllabi sequentially
- Complete all case studies
- Do mock interviews in week 7-8

### If you have 12+ weeks
- Follow syllabi as designed
- Add advanced topics (multi-region, scaling challenges)
- Study real system design papers
- Do 10+ mock interviews

---

## 📚 Additional Resources (Not in Syllabi)

**Books to supplement:**
- *Designing Data-Intensive Applications* (DDIA) - exceptional for HLD
- *Java Concurrency in Practice* - exceptional for LLD (Java-specific)
- *System Design Interview* series by Alex Xu - practical case studies

**Videos & Blogs:**
- ByteByteGo YouTube (system design deep dives)
- High Scalability blog (real system architectures)
- Engineering blogs: Google, Netflix, AWS, Uber

**Practice Platforms:**
- LeetCode (LLD section, machine coding)
- System Design Handbook (HLD case studies)
- DesignGurus.io (FAANG interview prep)

---

## ✅ Pre-Interview Checklist

### LLD
- [ ] Can implement any design pattern quickly
- [ ] Understand GC, memory, and thread safety deeply
- [ ] Designed 10+ systems with code
- [ ] Can discuss extensibility and trade-offs
- [ ] Comfortable with 45-minute design + code

### HLD
- [ ] Know CAP, PACELC, consistency models
- [ ] Can do back-of-envelope calculations rapidly
- [ ] Designed 10+ systems with full architecture
- [ ] Can justify database choices
- [ ] Comfortable with trade-off discussions

### Both
- [ ] Have a clear communication style
- [ ] Ask clarifying questions naturally
- [ ] Explain reasoning, not just answers
- [ ] Handle follow-ups confidently
- [ ] Can adapt design based on feedback

---

## 🎯 Your Next Step

1. **Choose your focus:** Which track needs more work?
   - LLD → Start with Phase A or C depending on your gaps
   - HLD → Start with Phase A or jump to case studies if strong on basics

2. **Set a timeline:** How much time do you have?
   - 3 weeks? Focus on case studies
   - 8 weeks? Follow the full syllabus
   - 12+ weeks? Include advanced topics

3. **Create a study schedule:** Break down into weekly milestones
   - Week 1: Concepts + one case study
   - Week 2: More case studies
   - Week 3+: Deep dives and mocks

4. **Track progress:**
   - Mark off topics as you learn them
   - Record mock interviews
   - Get feedback from peers

---

## 📞 Tips for Using These Syllabi

✅ **Do:**
- Use as a comprehensive checklist
- Dive deep into areas where you're weak
- Practice case studies multiple times
- Get feedback on your designs
- Adapt based on interview feedback

❌ **Don't:**
- Try to memorize everything
- Rush through without understanding
- Skip the "why" and focus only on "what"
- Practice only one type of problem
- Neglect operational concerns (observability, failures)

---

**Your success depends on consistent, focused practice over weeks. Use these syllabi as your roadmap, not a script. You've got this! 🚀**
