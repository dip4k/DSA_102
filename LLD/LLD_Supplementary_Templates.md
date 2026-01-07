# 📌 LLD Supplementary: Code Templates & Quick References

## Table of Contents
1. Object-Oriented Design Checklist
2. Thread-Safe Data Structure Template
3. Design Pattern Quick Reference
4. Code Smell Detection Guide
5. API Design Best Practices
6. Testing Strategy Templates

---

## 1️⃣ Object-Oriented Design Checklist

Use this checklist when designing any LLD system:

### Functional Requirements
- [ ] What are the main entities/objects?
- [ ] What operations must the system support?
- [ ] What are the primary use cases?
- [ ] Are there any constraints on data types?

### Non-Functional Requirements
- [ ] Thread-safety required? (Single-threaded vs multi-threaded)
- [ ] Performance constraints? (Latency, throughput)
- [ ] Memory constraints? (Limited heap, mobile device?)
- [ ] Extensibility needed? (Will requirements change?)

### Design Phase
- [ ] Created UML class diagram?
- [ ] Identified all entities and relationships?
- [ ] Defined clear API/interface?
- [ ] Planned state transitions (if applicable)?
- [ ] Identified concurrency points?

### SOLID Compliance
- [ ] **S** - Each class has single responsibility?
- [ ] **O** - Open for extension, closed for modification?
- [ ] **L** - Can subclasses replace parent without breaking?
- [ ] **I** - Interfaces are lean (not forcing unused methods)?
- [ ] **D** - Depending on abstractions, not concretions?

### Thread Safety (if multi-threaded)
- [ ] All shared state protected by locks/synchronization?
- [ ] No deadlock potential (lock ordering documented)?
- [ ] No race conditions identified?
- [ ] Exception safety: resources properly released?

### Extensibility
- [ ] How would you add new entity types?
- [ ] How would you add new operations?
- [ ] How would you change algorithms/strategies?
- [ ] Documented extension points?

### Code Quality
- [ ] Clear, intent-revealing names?
- [ ] Methods are small and focused?
- [ ] Complex logic broken down and explained?
- [ ] Error handling comprehensive?
- [ ] Documented API with examples?

### Testing
- [ ] Can classes be tested in isolation?
- [ ] Dependencies are mockable?
- [ ] Edge cases handled?
- [ ] Thread safety testable?

---

## 2️⃣ Thread-Safe Data Structure Template

Use this template for designing thread-safe classes:

```java
// Template for thread-safe data structure
public class ThreadSafeXYZ<T> {

    // STATE: What data do we protect?
    private final Object lock = new Object();  // or use ReadWriteLock
    private SomeDataStructure<T> data;

    // CONSTRUCTOR
    public ThreadSafeXYZ(int capacity) {
        this.data = new SomeDataStructure<>(capacity);
    }

    // OPERATION 1: Reading
    public T get(Key key) {
        synchronized(lock) {  // Critical section
            return data.get(key);
        }
    }

    // OPERATION 2: Writing
    public void put(Key key, T value) {
        synchronized(lock) {
            data.put(key, value);
        }
    }

    // OPERATION 3: Waiting/Blocking
    public T take() throws InterruptedException {
        synchronized(lock) {
            while(data.isEmpty()) {
                lock.wait();  // Blocks until notified
            }
            T value = data.remove();
            return value;
        }
    }

    // OPERATION 4: Signaling
    public void put(T value) {
        synchronized(lock) {
            data.add(value);
            lock.notifyAll();  // Wake up waiting threads
        }
    }

    // EXCEPTION SAFETY: Always release locks
    public T safeOperation() {
        synchronized(lock) {
            try {
                return data.process();
            } finally {
                // Cleanup if needed
            }
        }
    }
}
```

**Key Points:**
- ✅ Identify what needs protection (shared state)
- ✅ Choose lock type (Mutex, ReadWriteLock, etc.)
- ✅ Minimize critical section (lock duration)
- ✅ Handle InterruptedException properly
- ✅ Use wait/notify for blocking operations
- ✅ Ensure exception safety (finally blocks)

---

## 3️⃣ Design Pattern Quick Reference

### Creational Patterns (Object Creation)

| Pattern | Use Case | Example |
|---------|----------|---------|
| **Singleton** | Single instance needed (config, logger) | Database connection pool |
| **Factory** | Hide creation logic | PaymentProcessorFactory.create("CREDIT") |
| **Builder** | Complex immutable objects | Cache configuration |
| **Prototype** | Deep copy expensive objects | Clone objects efficiently |

**When to use:**
- Singleton: Config, thread pools, loggers (but prefer DI)
- Factory: Multiple implementations, decoupling
- Builder: Objects with many optional fields
- Prototype: Object cloning is cheaper than recreation

### Structural Patterns (Object Composition)

| Pattern | Use Case | Example |
|---------|----------|---------|
| **Adapter** | Make incompatible interfaces compatible | Wrap legacy logging lib |
| **Decorator** | Add behavior dynamically | Caching decorator over DB |
| **Proxy** | Control access or lazy load | Proxy for heavy resource |
| **Facade** | Simplify complex subsystem | File system abstraction |

**When to use:**
- Adapter: Integrating third-party code
- Decorator: Features like caching, logging, transactions
- Proxy: Lazy loading, access control, smart references
- Facade: Hide complexity of subsystems

### Behavioral Patterns (Object Interaction)

| Pattern | Use Case | Example |
|---------|----------|---------|
| **Observer** | Notify multiple listeners | Event system |
| **Strategy** | Swap algorithms at runtime | Payment methods |
| **State** | State-based behavior change | Order status (new→paid→shipped) |
| **Command** | Encapsulate requests | Undo/redo system |
| **Template Method** | Define algorithm skeleton | Transaction processing |

**When to use:**
- Observer: Event systems, MVC models
- Strategy: Multiple algorithms for same problem
- State: Objects with distinct states and transitions
- Command: Queue operations, undo/redo, logging
- Template Method: Common algorithm with customizable steps

---

## 4️⃣ Code Smell Detection Guide

**Smell: Long Method**
- **Symptom:** Method > 20 lines
- **Fix:** Extract into smaller methods
- **Check:** Would you give this method a clear, short name?

**Smell: God Class**
- **Symptom:** Class responsible for many things
- **Fix:** Split by responsibility (SRP)
- **Check:** Can you describe the class in one sentence without "and"?

**Smell: Feature Envy**
- **Symptom:** Method uses another object's data too much
- **Fix:** Move method to the object that owns the data
- **Check:** Does this method belong on this class?

**Smell: Data Clumps**
- **Symptom:** Same data fields appear together
- **Fix:** Group into value object
- **Check:** Would these fields work together elsewhere?

**Smell: Primitive Obsession**
- **Symptom:** Using primitives instead of small objects
- **Fix:** Create value objects (Money, Distance, UserId)
- **Check:** Would a dedicated class make this clearer?

**Smell: Switch Statements**
- **Symptom:** Large switch/if-else blocks
- **Fix:** Use polymorphism or strategy pattern
- **Check:** Does this beg for a new type?

**Smell: Data Classes**
- **Symptom:** Class only holds data, no behavior
- **Fix:** Move related behavior to the class
- **Check:** Is this really a passive data holder?

**Smell: Refused Bequest**
- **Symptom:** Subclass doesn't use parent methods
- **Fix:** Violates LSP, use composition instead
- **Check:** Is this really a type of parent class?

---

## 5️⃣ API Design Best Practices

### Principle 1: Intent-Revealing Names
```java
// ❌ Bad
public void set(Object x) { ... }
public List get() { ... }

// ✅ Good
public void setCacheEvictionPolicy(EvictionPolicy policy) { ... }
public List<Message> getUndeliveredMessages() { ... }
```

### Principle 2: Minimal & Focused APIs
```java
// ❌ Bad - Does too much
public Result process(Object input, int param1, String param2, 
                      boolean flag1, boolean flag2) { ... }

// ✅ Good - Clear, minimal
public List<User> searchUsersByCountry(String countryCode) { ... }
```

### Principle 3: Fluent Interface (Builder-like)
```java
// ❌ Imperative
Cache cache = new Cache();
cache.setCapacity(1000);
cache.setEvictionPolicy(EvictionPolicy.LRU);
cache.setTTL(3600);

// ✅ Fluent
Cache cache = new CacheBuilder()
    .withCapacity(1000)
    .withEvictionPolicy(EvictionPolicy.LRU)
    .withTTL(3600)
    .build();
```

### Principle 4: Immutability for Safety
```java
// ✅ Immutable config
public class CacheConfig {
    private final int capacity;
    private final long ttlMs;
    private final EvictionPolicy policy;

    public CacheConfig(int capacity, long ttlMs, EvictionPolicy policy) {
        this.capacity = capacity;
        this.ttlMs = ttlMs;
        this.policy = policy;
    }

    // Only getters, no setters
    public int getCapacity() { return capacity; }
}
```

### Principle 5: Clear Error Handling
```java
// ❌ Ambiguous
public User getUser(String id) throws Exception { ... }

// ✅ Clear
public User getUser(String id) throws UserNotFoundException, 
                                       InvalidUserIdException { ... }
```

### Principle 6: Idempotency
```java
// ✅ Idempotent - safe to call multiple times
public void pay(Payment payment, String transactionId) {
    if(alreadyProcessed(transactionId)) {
        return getCachedResult(transactionId);
    }
    // Process payment
    cacheResult(transactionId, result);
}
```

---

## 6️⃣ Testing Strategy Templates

### Unit Testing Template
```java
public class CacheTest {

    private Cache<String, String> cache;

    @Before
    public void setUp() {
        cache = new Cache<>(100, EvictionPolicy.LRU);
    }

    // Happy Path
    @Test
    public void testPutAndGet() {
        cache.put("key1", "value1");
        assertEquals("value1", cache.get("key1"));
    }

    // Edge Cases
    @Test
    public void testGetNonExistentKey() {
        assertNull(cache.get("nonexistent"));
    }

    // Boundary Conditions
    @Test
    public void testCapacityLimit() {
        for(int i = 0; i < 100; i++) {
            cache.put("key" + i, "value" + i);
        }
        // Cache is full - add one more, should evict LRU
        cache.put("key100", "value100");
        assertNull(cache.get("key0"));  // First key should be evicted
    }

    // Exception Cases
    @Test(expected = IllegalArgumentException.class)
    public void testNullKeyRejected() {
        cache.put(null, "value");
    }
}
```

### Integration Testing Template
```java
public class CacheIntegrationTest {

    private Cache<String, String> cache;
    private DatabaseBackend database;

    @Before
    public void setUp() {
        cache = new Cache<>(100);
        database = new MockDatabase();
        cache.setBackend(database);
    }

    // Cache-Database Interaction
    @Test
    public void testCacheMissCallsDatabase() {
        cache.get("key1");
        verify(database).fetch("key1");
    }

    // Consistency
    @Test
    public void testCacheInvalidationSyncsWithDatabase() {
        cache.put("key1", "value1");
        cache.invalidate("key1");
        cache.get("key1");  // Should fetch from DB
        verify(database).fetch("key1");
    }
}
```

### Concurrency Testing Template
```java
public class ThreadSafeCacheTest {

    private Cache<String, String> cache;

    @Test
    public void testConcurrentPutAndGet() throws InterruptedException {
        int threads = 10;
        int operationsPerThread = 100;
        ExecutorService executor = Executors.newFixedThreadPool(threads);

        for(int t = 0; t < threads; t++) {
            executor.submit(() -> {
                for(int i = 0; i < operationsPerThread; i++) {
                    cache.put("key" + i, "value" + i);
                    cache.get("key" + i);
                }
            });
        }

        executor.shutdown();
        executor.awaitTermination(10, TimeUnit.SECONDS);

        // Verify no corruption
        assertEquals(100, cache.size());
    }

    @Test(timeout = 5000)
    public void testNoDeadlock() throws InterruptedException {
        // This test should complete without hanging
        for(int i = 0; i < 100; i++) {
            cache.put("key" + i, "value" + i);
        }
    }
}
```

---

## 7️⃣ Common LLD Problem Patterns

### Pattern 1: State Machine
**When to use:** Elevator, Vending Machine, Order Status

```
States: IDLE → MOVING → STOPPED
Transitions: 
  - Button pressed: IDLE → MOVING
  - Reached floor: MOVING → STOPPED
  - Wait timeout: STOPPED → IDLE
```

**Implementation:**
- Define enum for states
- Define transitions (valid state changes)
- Validate before transitioning
- Handle invalid transitions

### Pattern 2: Resource Pool
**When to use:** Connection Pool, Thread Pool, Cache

```
Acquire: 
  1. Check available resources
  2. If available, return
  3. If not, wait or reject
Release:
  1. Return resource to pool
  2. Notify waiters
```

### Pattern 3: Hierarchical System
**When to use:** Parking Lot, File System, Organization

```
Hierarchy: Root
  ├── Level 1
  │   ├── Level 2
  │   └── Level 2
  └── Level 1

Operations:
  - Add/remove at each level
  - Search recursively
  - Calculate aggregate properties
```

### Pattern 4: Event-Driven System
**When to use:** Notification system, Event listener

```
Publisher → Event → Subscribers
           (queue)

Operations:
  - Publish event
  - Subscribe/Unsubscribe
  - Handle event (callback)
  - Error handling if listener fails
```

---

## 8️⃣ Performance Optimization Checklist

### Memory Optimization
- [ ] Using right data structures? (Array vs LinkedList vs HashMap)
- [ ] Avoiding unnecessary object creation?
- [ ] Pooling expensive objects?
- [ ] Proper garbage collection friendly code?

### Time Optimization
- [ ] Operations are O(expected)? (Check Big O)
- [ ] Caching expensive computations?
- [ ] Avoiding nested loops where possible?
- [ ] Using appropriate index structures?

### Concurrency Optimization
- [ ] Minimizing lock contention (small critical sections)?
- [ ] ReadWriteLock when appropriate?
- [ ] Lock-free data structures where beneficial?
- [ ] No unnecessary blocking?

---

## 9️⃣ Interview Red Flags (Avoid These!)

❌ **Unclear API:** Methods that are ambiguous or do too much  
❌ **No error handling:** Ignoring failures and edge cases  
❌ **Tight coupling:** Classes that can't be tested independently  
❌ **Race conditions:** Unsynchronized shared state  
❌ **Memory leaks:** Holding onto resources unnecessarily  
❌ **Unclear naming:** Variables and methods named cryptically  
❌ **God classes:** Classes doing too much  
❌ **No extensibility:** Hard-coded behaviors  

---

## 🔟 Cheat Sheet: Problem → Solution

| Problem | Solution | Pattern |
|---------|----------|---------|
| Need multiple parking spot types | Use inheritance or composition | Strategy/Decorator |
| Hot users causing load spikes | Cache + async processing | Cache-Aside, Queue |
| Testing class that depends on DB | Inject mock DB | Dependency Injection |
| Want to add new payment method | Don't modify existing code | Strategy pattern |
| Messages must be ordered | Assign monotonic IDs | Command/Event |
| Thread-safe counter | Use AtomicInteger or synchronize | Concurrency |
| Complex object creation | Use Builder | Builder pattern |
| Notify many listeners of event | Use Observer | Observer pattern |

---

**This supplementary guide should be reviewed throughout your LLD prep. Bookmark it!**
