# LLD & HLD Standards for Senior Engineers

## Context & Expectations
Senior/Lead interview candidates at FAANG and top-tier product companies must demonstrate end-to-end design fluency from in-memory thread safety (LLD) to horizontally scaled cloud architectures (HLD).

## Low-Level Design (LLD) Guidelines
1. **SOLID Principles & Clean Code:**
   - Single Responsibility, Interface Segregation, and Dependency Inversion.
   - Separation of Domain Models, Interfaces, Business Logic, and Persistence Adapters.
2. **Concurrency & Thread Safety:**
   - Always analyze race conditions in multi-threaded in-memory components (e.g., caches, rate limiters).
   - In C#, use `ReaderWriterLockSlim`, `ConcurrentDictionary<TKey, TValue>`, or `SemaphoreSlim` where appropriate.
   - In Python, explain GIL impacts and use threading locks or asyncio semantics.
3. **Extensibility & Patterns:**
   - State Pattern (Workflow engines, vending machines).
   - Strategy Pattern (Payment processing, routing algorithms).
   - Observer / Pub-Sub Pattern (Event dispatching, message brokers).
   - Factory / Builder (Complex entity creation).

## High-Level Design (HLD) Guidelines
1. **Requirements & Sizing (Capacity Estimation):**
   - DAU/MAU, Read/Write ratios, QPS (Average & Peak), Storage (Per Day & Over 5 Years), Bandwidth.
2. **API & Data Modeling:**
   - REST / gRPC API contract definitions.
   - Relational vs NoSQL trade-offs (PostgreSQL vs DynamoDB/CosmosDB vs Redis/Memcached).
3. **Core Architectural Blocks:**
   - Load Balancing, API Gateways, Reverse Proxies.
   - Caching strategies (Cache-aside, Write-through, Eviction policies).
   - Message Queues & Streaming (Kafka, RabbitMQ, SQS / Azure Service Bus).
   - Database Partitioning / Sharding strategies, replication, CAP theorem tradeoffs.
4. **Cloud Mapping (Azure & AWS):**
   - Leverage the user's background (.NET / Azure / AWS):
     - Compute: Azure Container Apps / AKS vs AWS ECS / EKS.
     - Storage & Cache: Azure Blob / S3, Azure Redis / ElastiCache, CosmosDB / DynamoDB.
     - Messaging: Azure Service Bus / Event Hubs vs AWS SQS / SNS / Kinesis.

