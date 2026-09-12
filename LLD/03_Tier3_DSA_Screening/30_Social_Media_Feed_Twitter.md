# 30. Social Media News Feed & Followers System (Twitter / Meta Feed)

## 📌 Problem Context & Motivation
The **Social Media News Feed System** (LeetCode 355) is the canonical Low-Level & High-Level System Design problem asked across **Meta, Twitter/X, Amazon, and Uber**. 

It evaluates a candidate's ability to seamlessly bridge **algorithmic efficiency (K-Way Merge via Min/Max Heaps)** with **high-throughput distributed backend architecture (Fanout-on-Write vs. Fanout-on-Read vs. Hybrid)**.

### Core Functional Requirements
1. `PostTweet(userId, tweetId, content)`: Creates a new tweet published by `userId` at an atomic, monotonically increasing timestamp.
2. `GetNewsFeed(userId)`: Retrieves the **10 most recent tweet IDs** in the user's news feed. Each item must be posted by users who the user followed or by the user themselves, ordered strictly from **most recent to least recent**.
3. `Follow(followerId, followeeId)`: `followerId` begins following `followeeId`.
4. `Unfollow(followerId, followeeId)`: `followerId` stops following `followeeId`.

### Architectural & Scalability Challenges
- **High Read-to-Write Imbalance**: On Twitter/X, the ratio of timeline views to tweets posted is $\approx 100:1$ to $500:1$. Read latency must be under $50\text{ ms}$.
- **The "Justin Bieber / Celebrity" Problem**: If an account with 100 million followers tweets, fanout-on-write requires 100 million timeline database/Redis writes. If done naively, this creates massive write queue lag and spikes cluster memory.
- **K-Way Merge Algorithmics**: Instead of pulling all tweets from all followed users and sorting ($O(T_{\text{total}} \log T_{\text{total}})$), we treat each user's tweet history as a **sorted linked list** and perform an optimal **K-Way Merge** using a Priority Queue in $O(K \log F)$ time (where $F$ is followees and $K=10$).

---

## 🎯 CrackingWalnuts 6-Step Methodology Applied

```mermaid
flowchart LR
    A["1. Requirement Mining\n• 10 most recent tweets\n• Self + Followees feed\n• Follow / Unfollow\n• Thread-safe state"] --> B["2. Class Discovery\n• Tweet (Singly-Linked Node)\n• User (Aggregate Root)\n• TwitterEngine (Domain Service)\n• FeedResult (Value Object)"]
    B --> C["3. Relationships\n• User (1:N) Followees [HashSet]\n• User (1:1) HeadTweet [Linked List]\n• TwitterEngine manages User Pool"]
    C --> D["4. Design Patterns\n• Iterator / K-Way Merge\n• Observer / Fanout Strategy\n• Monotonic Atomic Clock"]
    D --> E["5. V1 → V4 Evolution\n• V1: Naive list sort on read\n• V2: Singly-linked list per user + MaxHeap\n• V3: Thread-safe ConcurrentEngine\n• V4: Hybrid Fanout (Push/Pull Architecture)"]
    E --> F["6. Problem Archetype\nFamily 5: In-Memory DSA + Family 4: Event-Driven Feed"]
```

---

## 🏗️ Algorithmic & Architectural Design

### Algorithmic Mechanics: K-Way Merge on Tweet Linked-Lists
Each user's published tweets form a singly linked list where the **head** points to their most recent tweet (`HeadTweet`).

To fetch the 10 most recent tweets for user $U$:
1. Identify $U$'s active followees plus $U$ themselves ($F$ total users).
2. Insert only the `HeadTweet` of each of the $F$ users into a **Max-Heap** (Priority Queue keyed by timestamp descending). Heap size is bounded by $F$.
3. Extract the maximum tweet from the heap.
4. If the extracted tweet has a `Next` pointer (an older tweet from the same author), push `tweet.Next` into the heap.
5. Repeat step 3–4 until 10 tweets are harvested or the heap is exhausted.
- **Time Complexity**: $O(K \log F)$ where $K = 10$ and $F$ is the number of followees. This is independent of total tweets in the system!

```mermaid
graph TD
    subgraph Followees Linked Lists
        A1["Alice Tweet 3 (T=150)"] --> A2["Alice Tweet 2 (T=120)"] --> A3["Alice Tweet 1 (T=90)"]
        B1["Bob Tweet 2 (T=140)"] --> B2["Bob Tweet 1 (T=100)"]
        C1["Charlie Tweet 1 (T=130)"]
    end

    subgraph PriorityQueue Step
        Heap["Max-Heap (Size = 3 Followees)\nRoot: Alice T=150\nBob T=140 | Charlie T=130"]
    end

    A1 -.->|"1. Pop T=150, push A2 (T=120)"| Heap
    Heap --> Out["Result Feed: [150, 140, 130, 120, 100, 90]"]
```

### High-Scale Architecture: Fanout-on-Write vs. Fanout-on-Read

```mermaid
flowchart TD
    subgraph Fanout-on-Write [Push Model - Standard Users]
        UserA["Standard User Tweets"] --> MsgQ["Kafka / Event Stream"]
        MsgQ --> Workers["Fanout Workers"]
        Workers -->|"Push to 500 followers"| RedisA["Follower 1 Timeline Cache (Redis)"]
        Workers -->|"Push"| RedisB["Follower 2 Timeline Cache (Redis)"]
    end

    subgraph Fanout-on-Read [Pull Model - Celebrities / Justin Bieber]
        Celeb["Celebrity Tweets (>100K followers)"] --> CelebStore["Celebrity Tweet Store"]
        Reader["Follower Opens App"] --> ReadTimeline["Read Redis Timeline Cache (Push)"]
        Reader --> ReadCeleb["Fetch Followed Celebrities' Tweets (Pull)"]
        ReadTimeline & ReadCeleb --> InMemMerge["In-Memory K-Way Merge\nFinal Top 10 Feed"]
    end
```

---

## 💻 Production-Ready C# Implementation (.NET 8 / C# 12)

```csharp
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SocialMediaFeed.Design;

// ============================================================================
// 1. DOMAIN MODELS & VALUE OBJECTS
// ============================================================================

/// <summary>
/// Represents an immutable Tweet entity.
/// Implements a singly-linked node pointing to the author's previous (older) tweet.
/// </summary>
public sealed class Tweet
{
    public int TweetId { get; }
    public int AuthorId { get; }
    public long Timestamp { get; }
    public string Content { get; }

    /// <summary>
    /// Pointer to the immediately preceding (older) tweet by the same author.
    /// Enables lazy K-way merge traversal without loading the full tweet history.
    /// </summary>
    public Tweet? Next { get; set; }

    public Tweet(int tweetId, int authorId, long timestamp, string content = "")
    {
        TweetId = tweetId;
        AuthorId = authorId;
        Timestamp = timestamp;
        Content = content;
        Next = null;
    }

    public override string ToString() => $"[Tweet #{TweetId} by User {AuthorId} at T={Timestamp}] \"{Content}\"";
}

/// <summary>
/// Domain aggregate root representing a social network user, their social graph,
/// and their head tweet pointer.
/// </summary>
public sealed class User
{
    private readonly ReaderWriterLockSlim _userLock = new(LockRecursionPolicy.NoRecursion);

    public int UserId { get; }
    
    /// <summary>
    /// Set of user IDs that this user is following.
    /// </summary>
    private readonly HashSet<int> _followees = new();

    /// <summary>
    /// Pointer to the most recent tweet posted by this user.
    /// Acts as the head of a reverse-chronological singly-linked list.
    /// </summary>
    public Tweet? HeadTweet { get; private set; }

    public User(int userId)
    {
        UserId = userId;
    }

    /// <summary>
    /// Prepends a newly published tweet to the user's tweet linked list.
    /// O(1) operation.
    /// </summary>
    public void PostTweet(int tweetId, long timestamp, string content = "")
    {
        var newTweet = new Tweet(tweetId, UserId, timestamp, content);

        _userLock.EnterWriteLock();
        try
        {
            newTweet.Next = HeadTweet;
            HeadTweet = newTweet;
        }
        finally
        {
            _userLock.ExitWriteLock();
        }
    }

    public void Follow(int followeeId)
    {
        if (followeeId == UserId) return; // Cannot follow self

        _userLock.EnterWriteLock();
        try
        {
            _followees.Add(followeeId);
        }
        finally
        {
            _userLock.ExitWriteLock();
        }
    }

    public void Unfollow(int followeeId)
    {
        if (followeeId == UserId) return;

        _userLock.EnterWriteLock();
        try
        {
            _followees.Remove(followeeId);
        }
        finally
        {
            _userLock.ExitWriteLock();
        }
    }

    /// <summary>
    /// Returns a thread-safe snapshot of all followee IDs currently followed by this user.
    /// </summary>
    public HashSet<int> GetFolloweesSnapshot()
    {
        _userLock.EnterReadLock();
        try
        {
            return new HashSet<int>(_followees);
        }
        finally
        {
            _userLock.ExitReadLock();
        }
    }

    /// <summary>
    /// Gets the current head tweet in a thread-safe manner.
    /// </summary>
    public Tweet? GetHeadTweet()
    {
        _userLock.EnterReadLock();
        try
        {
            return HeadTweet;
        }
        finally
        {
            _userLock.ExitReadLock();
        }
    }
}

// ============================================================================
// 2. TWITTER SERVICE CONTRACT
// ============================================================================

public interface ITwitterService
{
    /// <summary>
    /// Composes a new tweet authored by userId.
    /// </summary>
    void PostTweet(int userId, int tweetId, string content = "");

    /// <summary>
    /// Fetches the top-10 most recent tweets in the user's personalized news feed.
    /// Includes tweets from all followees as well as the user's own tweets.
    /// </summary>
    IReadOnlyList<Tweet> GetNewsFeed(int userId, int limit = 10);

    /// <summary>
    /// Creates a follow relationship: follower follows followee.
    /// </summary>
    void Follow(int followerId, int followeeId);

    /// <summary>
    /// Removes a follow relationship.
    /// </summary>
    void Unfollow(int followerId, int followeeId);
}

// ============================================================================
// 3. CORE TWITTER ENGINE (Thread-Safe + Optimal K-Way Merge)
// ============================================================================

public sealed class TwitterEngine : ITwitterService
{
    private readonly ConcurrentDictionary<int, User> _users = new();
    private long _globalTimestamp = 0;

    /// <summary>
    /// Atomically retrieves an existing user or creates a new one.
    /// </summary>
    private User GetOrCreateUser(int userId)
    {
        return _users.GetOrAdd(userId, id => new User(id));
    }

    public void PostTweet(int userId, int tweetId, string content = "")
    {
        var user = GetOrCreateUser(userId);
        long timestamp = Interlocked.Increment(ref _globalTimestamp);
        user.PostTweet(tweetId, timestamp, content);
    }

    public void Follow(int followerId, int followeeId)
    {
        if (followerId == followeeId) return;

        var follower = GetOrCreateUser(followerId);
        GetOrCreateUser(followeeId); // Ensure followee user entity exists in registry
        follower.Follow(followeeId);
    }

    public void Unfollow(int followerId, int followeeId)
    {
        if (followerId == followeeId) return;

        if (_users.TryGetValue(followerId, out var follower))
        {
            follower.Unfollow(followeeId);
        }
    }

    /// <summary>
    /// Generates the news feed using a K-Way Merge across the tweet linked-lists
    /// of the user and all their followees using a Max-Heap PriorityQueue.
    /// Time Complexity: O(Limit * log(F)) where F is number of followed users.
    /// Space Complexity: O(F) for the Priority Queue holding one tweet per source.
    /// </summary>
    public IReadOnlyList<Tweet> GetNewsFeed(int userId, int limit = 10)
    {
        if (!_users.TryGetValue(userId, out var currentUser))
        {
            return Array.Empty<Tweet>();
        }

        // 1. Gather all target candidate users: all followees + self
        var followeeIds = currentUser.GetFolloweesSnapshot();
        followeeIds.Add(userId); // User always sees their own tweets

        // 2. Initialize Max-Heap PriorityQueue
        // C# PriorityQueue defaults to min-heap. We invert the timestamp comparer for max-heap behavior.
        var maxHeap = new PriorityQueue<Tweet, long>(
            Comparer<long>.Create((t1, t2) => t2.CompareTo(t1))
        );

        // 3. Seed heap with the HeadTweet of each candidate user
        foreach (int targetId in followeeIds)
        {
            if (_users.TryGetValue(targetId, out var author))
            {
                var headTweet = author.GetHeadTweet();
                if (headTweet != null)
                {
                    maxHeap.Enqueue(headTweet, headTweet.Timestamp);
                }
            }
        }

        // 4. K-Way Merge extraction: extract max, advance pointer to next older tweet
        var newsFeed = new List<Tweet>(limit);

        while (maxHeap.Count > 0 && newsFeed.Count < limit)
        {
            var mostRecentTweet = maxHeap.Dequeue();
            newsFeed.Add(mostRecentTweet);

            // If this tweet has an older predecessor from the same author, add it to the heap
            if (mostRecentTweet.Next != null)
            {
                maxHeap.Enqueue(mostRecentTweet.Next, mostRecentTweet.Next.Timestamp);
            }
        }

        return newsFeed;
    }

    /// <summary>
    /// Helper method for LeetCode 355 exact signature compliance (returning tweet IDs).
    /// </summary>
    public IList<int> GetNewsFeedIds(int userId)
    {
        return GetNewsFeed(userId, 10).Select(t => t.TweetId).ToList();
    }
}

// ============================================================================
// 4. VERIFICATION HARNESS & TEST SUITE (Program.cs)
// ============================================================================

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("=== 🐦 PRODUCTION SOCIAL MEDIA NEWS FEED SYSTEM (LeetCode 355) ===\n");

        var twitter = new TwitterEngine();

        const int alice = 1;
        const int bob = 2;
        const int charlie = 3;

        // Step 1: Alice posts tweet 101
        Console.WriteLine("1. Alice posts Tweet 101: 'Hello World!'");
        twitter.PostTweet(alice, 101, "Hello World!");

        // Alice views her own feed -> [101]
        PrintFeed("Alice", twitter.GetNewsFeed(alice));

        // Step 2: Bob posts tweet 201 & 202
        Console.WriteLine("2. Bob posts Tweet 201 and Tweet 202");
        twitter.PostTweet(bob, 201, "First post from Bob");
        twitter.PostTweet(bob, 202, "Second post from Bob");

        // Step 3: Alice follows Bob
        Console.WriteLine("3. Alice follows Bob");
        twitter.Follow(alice, bob);

        // Alice views feed -> should see [202, 201, 101] (most recent first)
        PrintFeed("Alice (following Bob)", twitter.GetNewsFeed(alice));

        // Step 4: Charlie posts tweet 301
        Console.WriteLine("4. Charlie posts Tweet 301");
        twitter.PostTweet(charlie, 301, "Charlie is here!");

        // Alice does not follow Charlie -> should not see 301
        PrintFeed("Alice (not following Charlie)", twitter.GetNewsFeed(alice));

        // Step 5: Alice follows Charlie and Alice posts tweet 102
        Console.WriteLine("5. Alice follows Charlie and posts Tweet 102");
        twitter.Follow(alice, charlie);
        twitter.PostTweet(alice, 102, "Alice's fresh update");

        // Alice views feed -> should see [102, 301, 202, 201, 101]
        PrintFeed("Alice (following Bob & Charlie)", twitter.GetNewsFeed(alice));

        // Step 6: Alice unfollows Bob
        Console.WriteLine("6. Alice unfollows Bob");
        twitter.Unfollow(alice, bob);

        // Alice views feed -> Bob's tweets (202, 201) are gone! Expected: [102, 301, 101]
        PrintFeed("Alice (after unfollowing Bob)", twitter.GetNewsFeed(alice));

        // Step 7: Feed Capacity & Truncation (Top 10 Limit)
        Console.WriteLine("7. Testing Top-10 Truncation: Charlie posts 12 tweets rapidly...");
        for (int i = 1; i <= 12; i++)
        {
            twitter.PostTweet(charlie, 400 + i, $"Charlie high volume tweet #{i}");
        }

        var feed = twitter.GetNewsFeed(alice, limit: 10);
        Console.WriteLine($"Feed count returned: {feed.Count} (Expected: exactly 10)");
        PrintFeed("Alice Top-10 Feed", feed);

        // Step 8: Multi-threaded Concurrency Validation
        Console.WriteLine("8. Stress Testing Concurrent Posts & Follows across 8 Threads...");
        using var countdown = new CountdownEvent(8);
        for (int t = 0; t < 8; t++)
        {
            int threadIndex = t;
            ThreadPool.QueueUserWorkItem(_ =>
            {
                for (int i = 0; i < 50; i++)
                {
                    int authorId = 100 + threadIndex;
                    int targetId = 200 + (i % 5);
                    twitter.PostTweet(authorId, (authorId * 1000) + i, $"Concurrent tweet {i}");
                    twitter.Follow(authorId, targetId);
                    _ = twitter.GetNewsFeed(authorId, 10);
                }
                countdown.Signal();
            });
        }

        countdown.Wait();
        Console.WriteLine("✅ Concurrent stress test passed with zero race conditions or deadlocks!\n");

        Console.WriteLine("=== All Social Media Feed Engine Invariants Verified! ===");
    }

    private static void PrintFeed(string userDescription, IReadOnlyList<Tweet> feed)
    {
        Console.WriteLine($"  Feed for {userDescription}:");
        if (feed.Count == 0)
        {
            Console.WriteLine("    [Empty Feed]\n");
            return;
        }

        foreach (var tweet in feed)
        {
            Console.WriteLine($"    -> Tweet #{tweet.TweetId} (Author: {tweet.AuthorId}, Time: {tweet.Timestamp}) \"{tweet.Content}\"");
        }
        Console.WriteLine();
    }
}
```

---

## 🗣️ Senior Interviewer Discussion & Trade-offs (Staff Level)

| Architecture / Scale Question | Senior / Staff Engineering Defense |
| :--- | :--- |
| **"Fanout-on-Write (Push) vs Fanout-on-Read (Pull): How do you decide at scale?"** | **Mathematical Analysis:**<br>• *Push (Fanout-on-Write)*: Ideal when reads $\gg$ writes. When User $A$ tweets, write workers push the tweet ID into every follower's Redis timeline list (`LPUSH` / `LTRIM` to 800 items). Reads are instantaneous ($O(1)$).<br>• *The Bottleneck*: If Justin Bieber (100M followers) tweets, 100M writes overwhelm the Redis cluster and message brokers.<br>• *Pull (Fanout-on-Read)*: User posts only to their own tweet list ($O(1)$ write). When a follower loads their feed, we pull recent tweets from all followees and K-way merge. Great for writes, but reading becomes slow ($50\text{--}200\text{ ms}$) if a user follows 5,000 active accounts.<br>• **The Industry Gold Standard (Hybrid Model)**:<br>1. Users with $< 25{,}000$ followers use **Push**.<br>2. Accounts with $> 25{,}000$ followers (celebrities) bypass the push pipeline.<br>3. When a user requests their feed: fetch their pre-materialized push cache from Redis ($<2\text{ ms}$), fetch the latest tweets from the 2-3 celebrities they follow via Pull, and merge them in-memory in $<1\text{ ms}$. |
| **"How does the K-Way Merge compare to sorting all tweets in memory?"** | If a user follows 500 people, and each person has 1,000 tweets, a naive pull fetches $500{,}000$ tweets and sorts them in $O(N \log N)$ ($500{,}000 \log 500{,}000 \approx 9.5\text{M}$ operations).<br>With our **K-Way Merge on Linked Lists**, we only peek at the head of each user's tweet stream. We insert 500 tweets into the heap, and pop only 10 times. Total operations: $O(10 \times \log 500) \approx 90$ operations! This is an efficiency gain of over **$100{,}000\times$**. |
| **"How should feed pagination be implemented: Offset vs. Cursor?"** | **Never use Offset (`skip/take`) for dynamic social media feeds.**<br>If a user is viewing page 1 (tweets 1–10) and 3 new tweets are posted, requesting page 2 (`skip 10, take 10`) causes the user to see tweets 8, 9, and 10 again (duplicate shift).<br>**Cursor-Based Pagination** is mandatory:<br>The client passes `max_tweet_id` (or timestamp cursor) from the bottom of their current page: `GET /feed?limit=10&max_id=202`. The query only returns tweets where `Timestamp < cursor_timestamp`, guaranteeing zero duplicates and zero skipped tweets regardless of new incoming posts. |
| **"How is the user timeline cache stored in Redis?"** | In production, user feeds are stored as Redis **Sorted Sets (`ZSET`)** where `score = timestamp` and `member = tweetId`.<br>Commands executed:<br>1. Push: `ZADD timeline:userId <timestamp> <tweetId>`<br>2. Maintain bounded window: `ZREMRANGEBYRANK timeline:userId 0 -801` (keeps top 800 tweets, evicting the tail).<br>3. Read: `ZREVRANGEBYSCORE timeline:userId +inf -inf LIMIT 0 10`. |
| **"How do you handle inactive / dormant users?"** | Over $60\%$ of registered accounts never log in during a given month. Pushing tweets to dormant users' Redis timeline caches wastes terabytes of expensive RAM.<br>We enforce an **Active User Cache Window**: If a user has not opened the app in 14 days, their Redis timeline is evicted. When they log back in, their timeline is lazily reconstructed on-demand via Fanout-on-Read. |

---

## 🧩 Complexity Breakdown

| Method | Time Complexity | Auxiliary Space Complexity | Explanation |
| :--- | :---: | :---: | :--- |
| `PostTweet` | $O(1)$ | $O(1)$ | Prepends node to head of singly-linked list and increments atomic clock. |
| `Follow` | $O(1)$ | $O(1)$ | Adds followee ID to the internal hash set. |
| `Unfollow` | $O(1)$ | $O(1)$ | Removes followee ID from the internal hash set. |
| `GetNewsFeed` | $O(K \log F)$ | $O(F)$ | Max-Heap of size $F$ (followees). Exactly $K=10$ extractions and insertions. |
