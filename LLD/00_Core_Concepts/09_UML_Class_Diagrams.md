# Layer 1: UML Class Diagrams — Interview Reference

UML class diagrams are the universal language of LLD interviews. Whether you're on a whiteboard in a 45-minute OOD round or writing Mermaid diagrams in a design doc, you need fluency in reading and drawing them quickly.

> **Interview Rule:** In a 45-minute round, spend the first 5 minutes drawing a UML class diagram BEFORE writing any code. Interviewers reward candidates who plan visually.

---

## 📚 AlgoMaster UML Quick-Reference
Before reading further, bookmark these:
- **[How to Learn LLD](https://algomaster.io/learn/lld/how-to-learn-lld)** — start here if you''re new
- **[LLD Course Roadmap](https://algomaster.io/learn/lld/course-roadmap)** — full structured learning path

---

## 1. Class Box Notation

```
┌───────────────────────────────┐
│          ClassName            │  ← Section 1: Name (bold = concrete, italic = abstract)
├───────────────────────────────┤
│ - _privateField: string       │  ← Section 2: Attributes (visibility + name: type)
│ # _protectedId: Guid          │
│ + PublicProperty: decimal     │
├───────────────────────────────┤
│ + PublicMethod(): void        │  ← Section 3: Methods (visibility + name(params): return)
│ - Validate(): bool            │
└───────────────────────────────┘
```

### Visibility Symbols
| Symbol | Meaning | C# Equivalent |
|---|---|---|
| `+` | Public | `public` |
| `-` | Private | `private` |
| `#` | Protected | `protected` |
| `~` | Package / Internal | `internal` |

### Stereotype Annotations
| Annotation | Meaning |
|---|---|
| `«interface»` | Interface (C# `interface`) |
| `«abstract»` | Abstract class |
| `«enum»` | Enumeration |

---

## 2. Relationship Arrows Cheat Sheet

```
──────────────────────────────────────────────────────────────────
 Relationship    Arrow Style        Meaning
──────────────────────────────────────────────────────────────────
 Inheritance     ──────▷            is-a: Car extends Vehicle
 Interface       - - - -▷          implements: PayPal implements IPayment
 Association     ─────────→        knows-a: Driver uses Car (parameter)
 Aggregation     ─────────◇        has-a (weak): Department has Employees
 Composition     ─────────◆        has-a (strong): Order has OrderItems
 Dependency      - - - - -→        uses transiently: OrderSvc uses IPayGateway
──────────────────────────────────────────────────────────────────
```

### Multiplicity (Cardinality) Labels

| Notation | Meaning |
|---|---|
| `1` | Exactly one |
| `0..1` | Zero or one (optional) |
| `*` or `0..*` | Zero or more |
| `1..*` | One or more |
| `2..5` | Between 2 and 5 |

Example: `ParkingLot 1 ◆── 1..* ParkingFloor` (a lot has one or more floors)

---

## 3. Mermaid Class Diagram Syntax (Quick Reference)

All problem files in this curriculum use Mermaid. Copy these patterns:

```mermaid
classDiagram
    %% Inheritance
    Vehicle <|-- Car
    Vehicle <|-- Truck

    %% Interface implementation
    IPaymentStrategy <|.. CreditCardStrategy
    IPaymentStrategy <|.. PayPalStrategy

    %% Composition (Order owns OrderItems)
    Order 1 *-- 1..* OrderItem

    %% Aggregation (Department has Employees, injected)
    Department 1 o-- 0..* Employee

    %% Association (Driver uses Car temporarily)
    Driver --> Car : drives

    %% Dependency (Service uses Interface)
    OrderService ..> IPaymentStrategy : uses

    class Vehicle {
        <<abstract>>
        #string Make
        #string Model
        +Start()* void
    }

    class IPaymentStrategy {
        <<interface>>
        +ProcessAsync(amount) Task~Result~
    }
```

### Mermaid Relationship Symbols

| Symbol | UML Relationship |
|---|---|
| `<\|--` | Inheritance |
| `<\|..` | Interface implementation (realization) |
| `*--` | Composition |
| `o--` | Aggregation |
| `-->` | Association |
| `..>` | Dependency |

---

## 4. Problem → UML Concept Mapping

Study these problems to see each UML concept in real production context:

| UML Concept | Best Problem to Study | Key Diagram Element |
|---|---|---|
| **Interface Realization** | [Parking Lot](../01_Tier1_Highest_Priority/06_Parking_Lot.md) | `IParkingSpot <|.. HandicappedSpot` |
| **Composition** | [Order Management](../01_Tier1_Highest_Priority/03_DDD_Order_Management.md) | `Order *-- OrderItem` |
| **Aggregation** | [Ride Hailing System](../02_Tier2_Classic_LLD/20_Ride_Hailing_System.md) | `Driver o-- Vehicle` |
| **State Machine** | [Vending Machine](../02_Tier2_Classic_LLD/12_Vending_Machine.md) | State objects within context |
| **Decorator Chain** | [Notification System](../01_Tier1_Highest_Priority/08_Notification_System.md) | Wrapped `INotificationSender` |
| **Composite** | [In-Memory File System](../02_Tier2_Classic_LLD/24_In_Memory_File_System.md) | `INode <|-- DirectoryNode, FileNode` |
| **Multiplicity Labels** | [Hotel Management](../02_Tier2_Classic_LLD/29_Hotel_Management_System.md) | `Hotel 1 *-- 1..* Room` |
| **ATM / State + Command** | [ATM System](../02_Tier2_Classic_LLD/28_ATM_System.md) | Full State Machine diagram |
| **Observer / Waitlist** | [Library Management](../02_Tier2_Classic_LLD/27_Library_Management_System.md) | `BookCopy o-- Member : waitlist` |
| **Composite Task Tree** | [Task Management](../02_Tier2_Classic_LLD/18_Task_Management_System.md) | `Epic *-- Story *-- Task` |

---

## 5. UML Whiteboard Speed Template (45-Minute Round)

Use this mental script when the interviewer asks you to design something:

```
1. ACTORS/NOUNS  → Identify entities → these become classes
2. VERBS/ACTIONS → Identify behaviours → these become methods
3. RELATIONSHIPS → Who creates whom? (Composition/Aggregation)
                   Who knows about whom? (Association/Dependency)
4. ABSTRACTIONS  → Where is polymorphism needed? (Interfaces)
5. CONSTRAINTS   → What are the invariants? (where do you validate?)
```

---

## 6. Common Mistakes to Avoid

| Mistake | Correct Approach |
|---|---|
| Drawing lines without labels | Always label multiplicity (`1`, `1..*`, `0..1`) |
| Using Composition everywhere | Distinguish: is lifetime shared? If not, use Aggregation |
| Forgetting interfaces | Every polymorphic point needs an interface |
| Too many classes upfront | Start with 3–5 core classes, expand on interviewer follow-up |
| Arrows pointing wrong way | Association arrow points FROM the class that HOLDS the reference |

---

*Cross-references: [01_OOP_and_SOLID.md](./01_OOP_and_SOLID.md) · [02_Design_Patterns_Cheat_Sheet.md](./02_Design_Patterns_Cheat_Sheet.md) · [awesome-lld GitHub](https://github.com/ashishps1/awesome-low-level-design)*
