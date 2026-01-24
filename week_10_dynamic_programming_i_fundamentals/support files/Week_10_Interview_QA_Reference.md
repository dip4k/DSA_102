# 🎙️ Week_10_Interview_QA_Reference.md

**Week:** 10 | **Topic:** Dynamic Programming I: Fundamentals  
**Status:** ✅ Comprehensive Interview Preparation  
**Format:** 50+ questions with multi-level follow-ups  
**Difficulty Progression:** Basic → Intermediate → Advanced

---

## 📖 HOW TO USE THIS DOCUMENT

**For Mock Interviews:**
1. Choose a difficulty level (Basic, Intermediate, Advanced)
2. Read the question aloud as if asked by an interviewer
3. **Speak your answer out loud** (don't write code unless asked)
4. Pause and think before answering
5. Anticipate follow-ups and think through them

**For Self-Study:**
1. Read a question and try to answer **without looking below**
2. Check your answer against the follow-ups
3. If you struggle, revisit the Guidelines or Summary documents
4. Mark questions you find tricky; return to them later

**For Recording Yourself:**
1. Set up a voice recorder or video camera
2. Work through 5-7 questions in a session
3. Replay and assess:
   - Did I explain clearly?
   - Did I miss edge cases?
   - How confident did I sound?

---

## 🟢 BASIC LEVEL (Foundation Understanding)

### **Q1: What is Dynamic Programming?**

**Expected Answer:**
"Dynamic Programming is a method to solve problems by breaking them into overlapping subproblems, storing their results to avoid redundant computation, and building up the solution from smaller subproblems to larger ones."

**Follow-up 1:** "How is it different from Divide and Conquer?"
- **Expected:** DC solves non-overlapping subproblems; DP solves overlapping ones. DC doesn't need memoization because each subproblem is unique.

**Follow-up 2:** "Give me an example where Divide and Conquer wouldn't work but DP would."
- **Expected:** Fibonacci—DC would still compute the same values exponentially many times. DP caches these.

**Follow-up 3:** "What are the two key ingredients of a DP problem?"
- **Expected:** (1) Overlapping subproblems, (2) Optimal substructure.

---

### **Q2: Explain the Fibonacci Problem and Why Naive Recursion is Slow**

**Expected Answer:**
"Fibonacci(n) = Fibonacci(n-1) + Fibonacci(n-2). Naive recursion recomputes the same values many times. For example, Fib(5) calls Fib(4) and Fib(3), but Fib(4) also calls Fib(3) again. This redundancy grows exponentially, making it O(2^n)."

**Follow-up 1:** "Draw the recursion tree for Fibonacci(5) and show where the redundancy happens."
- **Expected:** Tree should show Fib(3) being computed at least twice, Fib(2) three times, etc.

**Follow-up 2:** "How many unique subproblems are there for Fibonacci(n)?"
- **Expected:** n+1 unique subproblems (Fibonacci(0) through Fibonacci(n)).

**Follow-up 3:** "If there are only n+1 unique subproblems, why is naive recursion exponential?"
- **Expected:** Because the recursion tree is exponential in size, not the number of unique subproblems. With memoization, we solve each unique subproblem once, so time becomes O(n).

---

### **Q3: What is Memoization? How Does It Work?**

**Expected Answer:**
"Memoization is storing the results of subproblems in a cache (like a dictionary) so we don't recompute them. Before solving a subproblem, we check the cache. If it's there, we return it. Otherwise, we compute it, store it, and return it."

**Follow-up 1:** "Is memoization top-down or bottom-up?"
- **Expected:** Top-down. We start from the original problem and recurse down, caching along the way.

**Follow-up 2:** "What's the space complexity of memoization for Fibonacci?"
- **Expected:** O(n) for the cache plus O(n) for the recursion stack depth, so O(n) total.

**Follow-up 3:** "Can memoization be slower than tabulation even though they have the same time complexity?"
- **Expected:** Yes. Memoization has recursion overhead and cache lookups; tabulation uses simple loops and array access, which is faster in practice.

---

### **Q4: What is Tabulation? How Does It Differ from Memoization?**

**Expected Answer:**
"Tabulation is a bottom-up approach where we build a table starting from base cases and fill it iteratively up to the answer. No recursion is involved. We iterate through subproblems in the right order and use already-computed values."

**Follow-up 1:** "Why is the iteration order important in tabulation?"
- **Expected:** Because we can only compute dp[i] if all the subproblems it depends on have already been computed. Wrong order = using uninitialized values.

**Follow-up 2:** "For Fibonacci tabulation, what's the iteration order?"
- **Expected:** Left to right (i = 0, 1, 2, ..., n). We can only fill dp[i] after dp[i-1] and dp[i-2] are ready.

**Follow-up 3:** "Is tabulation always faster than memoization?"
- **Expected:** Usually yes, because it avoids recursion overhead and cache overhead. But for sparse subproblems, memoization might be faster (we skip unnecessary ones).

---

### **Q5: What is Optimal Substructure?**

**Expected Answer:**
"Optimal substructure means the optimal solution to a problem can be built from optimal solutions to its subproblems. If we have the best answer for a smaller problem, we can use it to construct the best answer for a larger one."

**Follow-up 1:** "Does every problem with overlapping subproblems have optimal substructure?"
- **Expected:** No. Overlapping subproblems alone isn't enough. You need optimal substructure too.

**Follow-up 2:** "Give an example of optimal substructure in a real problem."
- **Expected:** Shortest path: if the shortest path from A to C goes through B, then the A-to-B portion must also be the shortest path from A to B.

**Follow-up 3:** "How do you verify that a problem has optimal substructure?"
- **Expected:** Show that if you have the optimal solution for subproblems, combining them (with some operation) gives the optimal solution for the full problem.

---

### **Q6: Climbing Stairs — State Definition**

**Expected Answer:**
"Let dp[i] = number of ways to reach step i. To reach step i, you can either come from step i-1 (by taking 1 step) or step i-2 (by taking 2 steps). So dp[i] = dp[i-1] + dp[i-2]."

**Follow-up 1:** "What are the base cases for climbing stairs?"
- **Expected:** dp[0] = 1 (already at the start), dp[1] = 1 (one way: take 1 step).

**Follow-up 2:** "How would you optimize this to O(1) space?"
- **Expected:** Keep only two variables: prev2 (dp[i-2]) and prev1 (dp[i-1]). Update in place.

**Follow-up 3:** "If you can take 1, 2, or 3 steps, how does the recurrence change?"
- **Expected:** dp[i] = dp[i-1] + dp[i-2] + dp[i-3].

---

### **Q7: House Robber — Problem Understanding**

**Expected Answer:**
"You have houses in a row with money. You can rob any house, but not adjacent ones (alarm triggers). Maximize total money. dp[i] = max money robbing houses 0 to i. You either rob house i (gain nums[i] + dp[i-2]) or skip it (take dp[i-1])."

**Follow-up 1:** "Why can't you rob adjacent houses?"
- **Expected:** It's a constraint of the problem; the alarm system prevents it.

**Follow-up 2:** "Why do we need dp[i-2], not dp[i-1]?"
- **Expected:** If we rob house i, we can't have robbed house i-1, so the previous robbed house is at most at i-2. dp[i-2] is the best we can do up to that point.

**Follow-up 3:** "How would the problem change if you could rob at most 2 non-adjacent houses?"
- **Expected:** We'd need to track how many houses we've robbed. State becomes 3D: dp[i][j] = max money using first i houses and robbing exactly j houses.

---

### **Q8: Coin Change — Min Coins vs Counting Ways**

**Expected Answer:**
"For min coins: dp[i] = minimum coins to make amount i. We try each coin and take the minimum: dp[i] = 1 + min(dp[i-c] for all coins c ≤ i).

For counting ways: dp[i] = number of ways to make amount i. We sum: dp[i] += dp[i-c] for each coin c. The loop order matters: outer loop = coins, inner loop = amounts."

**Follow-up 1:** "Why do we initialize dp[i] = INF for coin change min?"
- **Expected:** To represent "impossible to make amount i." As we find ways, we update dp[i] to the minimum actual value.

**Follow-up 2:** "Why is loop order different for counting ways?"
- **Expected:** Inner coin loop would count combinations as permutations (order matters). Outer coin loop ensures each coin is considered once, avoiding duplicates.

**Follow-up 3:** "Can you optimize coin change min to O(1) space?"
- **Expected:** No, not generally. You need the full dp array because dp[i] depends on any dp[i-c], which could be anywhere. (Contrast with Fibonacci, where you only need the last two.)

---

### **Q9: 0/1 Knapsack — Why Backward Iteration?**

**Expected Answer:**
"In 0/1 knapsack, we use a 1D array dp[w] = max value with capacity w. To prevent using the same item twice, we iterate capacity backward (from capacity to weight). This ensures dp[w-weight] hasn't been updated with the current item."

**Follow-up 1:** "What happens if you iterate forward?"
- **Expected:** You'd use the same item multiple times. For example, if you update dp[w-weight] in this iteration, then later use it for dp[2w-2*weight], you've effectively used the item twice.

**Follow-up 2:** "Is unbounded knapsack (unlimited items) different?"
- **Expected:** Yes. You iterate forward. Now using an item multiple times is allowed.

**Follow-up 3:** "Can you use a 2D array instead of 1D?"
- **Expected:** Yes, and then forward iteration is fine. dp[i][w] = max value using first i items with capacity w. New item doesn't affect dp[i-1][...].

---

### **Q10: What is a State in DP?**

**Expected Answer:**
"A state is a unique subproblem characterized by parameters. For example, in coin change, the state is the amount (dp[i] for amount i). In edit distance, the state is a pair of indices (dp[i][j] for s1[0..i-1] and s2[0..j-1]). The state must have enough information to uniquely identify the subproblem."

**Follow-up 1:** "How do you know if your state definition is correct?"
- **Expected:** If you can write the recurrence clearly and unambiguously using the state, it's correct. If you feel like you're missing information, the state is incomplete.

**Follow-up 2:** "Can you have multiple valid state definitions for the same problem?"
- **Expected:** Yes. Different states lead to different DP formulations, though they may have similar time/space.

**Follow-up 3:** "Why does state definition matter?"
- **Expected:** It determines the problem's complexity, ease of implementation, and space requirements.

---

## 🟡 INTERMEDIATE LEVEL (Problem Solving)

### **Q11: Unique Paths in Grid**

**Expected Answer:**
"You're on an m×n grid, starting at (0,0), goal is (m-1,n-1). Move only right or down. Count paths. dp[i][j] = paths to reach (i,j). Base: dp[0][j] = 1, dp[i][0] = 1. Recurrence: dp[i][j] = dp[i-1][j] + dp[i][j-1]."

**Follow-up 1:** "How would you handle obstacles?"
- **Expected:** If grid[i][j] is an obstacle, set dp[i][j] = 0. Otherwise, apply the recurrence.

**Follow-up 2:** "Can you optimize space for this problem?"
- **Expected:** Yes. dp[i][j] only depends on dp[i-1][j] and dp[i][j-1]. Use a 1D array and update row by row. Iterate column-wise left to right to ensure dp[j-1] is from the current row.

**Follow-up 3:** "What if you can move in all four directions (up, down, left, right)?"
- **Expected:** This becomes a graph problem (BFS/DFS), not simple DP. DP assumes an ordering of subproblems; with cycles, there's no clear ordering.

---

### **Q12: Edit Distance**

**Expected Answer:**
"Transform s1 to s2 with min operations (insert, delete, replace). dp[i][j] = edit distance between s1[0..i-1] and s2[0..j-1]. 
- If s1[i-1] == s2[j-1]: dp[i][j] = dp[i-1][j-1].
- Else: dp[i][j] = 1 + min(dp[i-1][j], dp[i][j-1], dp[i-1][j-1]) for delete, insert, replace."

**Follow-up 1:** "What are the base cases?"
- **Expected:** dp[0][j] = j (insert j characters), dp[i][0] = i (delete i characters).

**Follow-up 2:** "Which operation is which?"
- **Expected:** dp[i-1][j] = delete from s1, dp[i][j-1] = insert, dp[i-1][j-1] = replace.

**Follow-up 3:** "Can you reconstruct the actual sequence of operations?"
- **Expected:** Yes. Work backward from dp[m][n]. If characters match, go diagonally. Otherwise, choose the direction that led to the minimum and backtrack.

---

### **Q13: Longest Common Subsequence**

**Expected Answer:**
"Find the longest sequence appearing in both s1 and s2 (not necessarily contiguous). dp[i][j] = LCS length of s1[0..i-1] and s2[0..j-1]. 
- If s1[i-1] == s2[j-1]: dp[i][j] = 1 + dp[i-1][j-1].
- Else: dp[i][j] = max(dp[i-1][j], dp[i][j-1])."

**Follow-up 1:** "How does LCS differ from edit distance?"
- **Expected:** LCS only allows matching characters; edit distance allows transformations (insert, delete, replace). Different problems, different recurrences.

**Follow-up 2:** "What are the base cases?"
- **Expected:** dp[0][j] = 0, dp[i][0] = 0 (empty string has LCS 0).

**Follow-up 3:** "Can you reconstruct the actual LCS string?"
- **Expected:** Yes. Backtrack from dp[m][n]. If characters match, include it and go diagonally. Otherwise, follow the maximum direction.

---

### **Q14: Longest Increasing Subsequence (LIS) — O(n²)**

**Expected Answer:**
"Find the longest strictly increasing subsequence. dp[i] = LIS length ending at index i. Recurrence: dp[i] = 1 + max(dp[j] for all j < i where arr[j] < arr[i]). Base: dp[i] = 1 (every element is LIS of length 1). Answer: max(dp[i])."

**Follow-up 1:** "What's the time complexity and why?"
- **Expected:** O(n²). For each i, we check all j < i.

**Follow-up 2:** "Can you reconstruct the actual LIS?"
- **Expected:** Yes. Track the parent pointer for each i (which j gave the maximum). Backtrack from the index with max dp value.

**Follow-up 3:** "How is LIS different from longest increasing subarray?"
- **Expected:** Subarray must be contiguous; subsequence doesn't. Subarray is easier (just track running max).

---

### **Q15: Longest Increasing Subsequence (LIS) — O(n log n) Optimization (Concept)**

**Expected Answer:**
"Maintain a 'tails' array where tails[len] = smallest ending value of any LIS of length len+1. For each element, binary search where it fits in tails. If it's larger than all, append (extend LIS). Otherwise, replace to keep tails optimal. Time: O(n log n)."

**Follow-up 1:** "Why is this optimization hard to understand but powerful?"
- **Expected:** The insight that we only care about the smallest tail for each length is non-obvious. But it works because a smaller tail is always better for extending in the future.

**Follow-up 2:** "Can you code this optimization?"
- **Expected:** Possibly tricky, but doable. Use binary search library (bisect in Python, lower_bound in C++). Be careful with the insertion/replacement logic.

**Follow-up 3:** "Why doesn't this approach give us the actual LIS, just the length?"
- **Expected:** The tails array stores values, not indices. To get the actual LIS, you'd need to track indices and reconstruct differently.

---

### **Q16: Coin Change Variants**

**Expected Answer:**
"Base: min coins for amount. Variant 1: min coins with unlimited items (forward iteration on amount). Variant 2: counting ways (sum instead of min, correct loop order). Variant 3: max coins (flip operations). Each requires understanding what the state represents and how to update it."

**Follow-up 1:** "For max coins (use maximum coins to make amount), how does the recurrence change?"
- **Expected:** dp[i] = max(dp[i], 1 + dp[i-coin]) instead of min. Same structure, different operator.

**Follow-up 2:** "What if coins have limited supply?"
- **Expected:** Bounded knapsack. Need to track item count. More complex state or limit the coin loop iterations.

**Follow-up 3:** "How do you handle the impossible case?"
- **Expected:** Initialize dp with a sentinel value (INF for min, -INF for max). If dp[amount] is still sentinel at the end, return -1 or error.

---

### **Q17: 2D DP Iteration Order**

**Expected Answer:**
"For 2D DP, you must fill in an order such that all dependencies are already computed. Typically, top-left to bottom-right (row by row, then left to right within row). Some problems allow backward or column-first order."

**Follow-up 1:** "For unique paths, can you iterate column by column instead of row by row?"
- **Expected:** Yes. dp[i][j] depends on dp[i-1][j] (above) and dp[i][j-1] (left). Column-first still works.

**Follow-up 2:** "For knapsack with 2D state dp[i][w], can you do backward iteration?"
- **Expected:** Yes. Backward on capacity (w) avoids using the same item twice. But don't need backward on i; forward is fine.

**Follow-up 3:** "What happens if you get the iteration order wrong?"
- **Expected:** You'll use uninitialized values or values from the wrong "version," giving incorrect answers.

---

### **Q18: State Space Explosion**

**Expected Answer:**
"As you add constraints or dimensions to a problem, the state space grows. E.g., dp[i] (1D) vs dp[i][j] (2D) vs dp[i][j][k] (3D). More dimensions = more memory and time. Sometimes you can compress states or realize constraints reduce the space."

**Follow-up 1:** "If a problem has state dp[i][j][k] with i, j, k all up to 100, is it feasible?"
- **Expected:** 100³ = 1 million states. If each update is O(1), and you can fit the table in memory, it's tight but possibly feasible.

**Follow-up 2:** "How do you know when a problem is too hard due to state explosion?"
- **Expected:** Estimate the state space. If it exceeds ~10^8 or your memory, look for optimizations or realize DP might not be the right approach.

**Follow-up 3:** "What techniques reduce state space?"
- **Expected:** Memoization (sparse states), state compression (encode multiple dimensions), rolling arrays, or mathematical insights that reduce dimensionality.

---

### **Q19: Base Cases and Edge Cases**

**Expected Answer:**
"Base cases are the smallest subproblems with known answers. Edge cases are corner scenarios (n=0, empty array, single element, all same value, etc.). Both are critical: wrong base cases corrupt the entire DP table; missed edge cases cause runtime errors."

**Follow-up 1:** "For DP on a grid, what's an edge case?"
- **Expected:** Empty grid (m=0 or n=0), grid of size 1×1, all cells are obstacles, etc.

**Follow-up 2:** "How do you systematically find base cases?"
- **Expected:** Think: "What's the smallest input? What's the answer for that?" Often n=0, n=1, or empty inputs.

**Follow-up 3:** "How do you verify base cases are correct?"
- **Expected:** Hand-trace or manually compute the answer. Don't guess; verify each base case.

---

### **Q20: Space Optimization in DP**

**Expected Answer:**
"Analyze which prior states dp[i] depends on. If only dp[i-1] and dp[i-2], use two variables instead of an array. If a full row/column is needed, keep two rows instead of all. This reduces space from O(n²) to O(n) or O(n) to O(1)."

**Follow-up 1:** "Can you always optimize to O(1) space?"
- **Expected:** No. It depends on dependencies. Knapsack can be O(1) only if items are one-dimensional. LCS requires access to arbitrary dp[i-1][j] values, so O(n) is minimal.

**Follow-up 2:** "What's the trade-off between space and readability?"
- **Expected:** Optimized code is less readable. For an interview, optimize if asked, but prioritize correctness first.

**Follow-up 3:** "How do you implement rolling arrays correctly?"
- **Expected:** Carefully manage indices. Often, use (i % 2) indexing or swap two arrays after each iteration.

---

## 🔴 ADVANCED LEVEL (Nuanced Understanding)

### **Q21: DP on DAGs (Directed Acyclic Graphs)**

**Expected Answer:**
"DP on DAGs processes vertices in topological order. For each vertex, compute its value using values of predecessors (which are already computed). Example: longest path in DAG. Topological sort ensures dependencies are satisfied."

**Follow-up 1:** "Why must the graph be acyclic?"
- **Expected:** Cycles create circular dependencies. Without an ordering, you can't fill values bottom-up.

**Follow-up 2:** "Can you solve longest path in a graph with cycles?"
- **Expected:** Not with standard DP. You'd need to detect the cycle and handle it differently (or use other methods).

**Follow-up 3:** "How does topological sort relate to DP iteration order?"
- **Expected:** DP on DAG == compute values in topological order. The topological order ensures all dependencies are ready.

---

### **Q22: DP on Trees**

**Expected Answer:**
"DP on trees processes nodes in a traversal order (DFS, post-order). For each node, combine results from children. Example: tree diameter = max path length. dp[node] = (max path down, second max path down) or similar."

**Follow-up 1:** "Why is post-order traversal natural for tree DP?"
- **Expected:** Post-order visits children before the parent. You compute children's values first, then use them for the parent.

**Follow-up 2:** "Can you do tree DP top-down (pre-order)?"
- **Expected:** Yes, but you'd pass information down from parent. More complex conceptually.

**Follow-up 3:** "What if the tree is unrooted (undirected graph without cycles)?"
- **Expected:** Pick any root, then do standard tree DP. Or handle both directions (edge relaxation style).

---

### **Q23: Bitmask DP**

**Expected Answer:**
"Bitmask DP encodes subsets as integers. dp[mask] = answer for the subset represented by mask. Iterate over all 2^n masks and transition by adding/removing elements. Example: TSP with DP. dp[mask][i] = min cost visiting cities in mask, ending at city i."

**Follow-up 1:** "How many states are there in bitmask DP?"
- **Expected:** O(2^n × n) for TSP (all subsets × ending position). Feasible for n ≤ 20.

**Follow-up 2:** "How do you iterate over submasks of a given mask?"
- **Expected:** `for submask in 0..mask: submask = (submask - 1) & mask` iterates all submasks efficiently.

**Follow-up 3:** "What problems are suitable for bitmask DP?"
- **Expected:** Small n (≤ 20), subset enumeration, traveling salesman, assignment problems, etc.

---

### **Q24: Convex Hull Trick (High-Level Concept)**

**Expected Answer:**
"Some DP problems have a special structure: dp[i] = min/max over all j < i of (a[j] × b[i] + c[j]). This can be optimized using convex hull trick from O(n²) to O(n log n) by exploiting geometric properties. Not commonly asked, but shows advanced DP optimization."

**Follow-up 1:** "When would you use convex hull trick?"
- **Expected:** In optimization problems where the recurrence has this specific form and n is large (10^5 or more).

**Follow-up 2:** "Is it important to memorize convex hull trick?"
- **Expected:** Not necessary for most interviews. It's an advanced technique. Understand that optimization tricks exist, but focus on problem-solving first.

**Follow-up 3:** "Are there other DP optimization techniques?"
- **Expected:** Yes: slope trick, matrix chain multiplication, broken profile DP, etc. These are very specialized.

---

### **Q25: Interpreting DP States Carefully**

**Expected Answer:**
"A state must precisely define what 'optimality' means. For example, 'max money robbing houses 0..i' vs 'max money robbing at most k houses from 0..i' are different. The state definition determines correctness."

**Follow-up 1:** "What's a common mistake in state definition?"
- **Expected:** Ambiguity: Does dp[i] mean 'up to i' or 'exactly i' or 'starting from i'? Be explicit.

**Follow-up 2:** "If your initial state definition doesn't work, how do you fix it?"
- **Expected:** Add constraints to the state. E.g., if dp[i] isn't enough, maybe you need dp[i][j] to track additional info.

**Follow-up 3:** "Can over-complicating the state be a problem?"
- **Expected:** Yes. It increases space/time. Start simple; add dimensions only if necessary.

---

### **Q26: Transition Design**

**Expected Answer:**
"Transitions are the edges in the subproblem dependency graph. Well-designed transitions make the recurrence natural and correct. Poor transitions are hard to code and error-prone."

**Follow-up 1:** "How do you ensure transitions are correct?"
- **Expected:** Trace through examples. Verify that each transition makes logical sense and is complete (all options covered).

**Follow-up 2:** "What if a problem has many possible transitions?"
- **Expected:** You may have too many states or a wrong state definition. Revisit the design.

**Follow-up 3:** "Can you have duplicate transitions?"
- **Expected:** Yes, accidentally (e.g., two branches that do the same thing). Not usually a problem; it's redundant but not incorrect.

---

### **Q27: Analyzing DP Time Complexity**

**Expected Answer:**
"Time = (# states) × (time per state). # states = product of all state dimensions. Time per state = # transitions. Example: coin change: O(n × m) states, O(1) per state = O(n × m)."

**Follow-up 1:** "Why is analyzing state count critical?"
- **Expected:** It determines feasibility. 10^8 states might be too slow. 10^6 might be acceptable.

**Follow-up 2:** "What if the recurrence has nested loops?"
- **Expected:** Time per state is not O(1). It's the inner loop count. Example: LIS O(n²) = O(n) states × O(n) per state.

**Follow-up 3:** "How do optimizations (like convex hull trick) change complexity?"
- **Expected:** They reduce the per-state time. CHT reduces LIS inner loop from O(n) to O(log n), giving O(n log n) total.

---

### **Q28: Common Implementation Bugs in DP**

**Expected Answer:**
"(1) Off-by-one indexing. (2) Uninitialized states. (3) Wrong iteration order. (4) Not handling base cases. (5) Integer overflow. (6) Modulo arithmetic mistakes. (7) Confusing >= vs > in comparisons."

**Follow-up 1:** "How do you catch off-by-one errors?"
- **Expected:** Hand-trace on small examples. Print intermediate dp values.

**Follow-up 2:** "When does integer overflow happen in DP?"
- **Expected:** Summing large numbers (coin change ways), or counting subsets (2^n). Use long integers or modulo arithmetic.

**Follow-up 3:** "If you're using modulo, where do you apply it?"
- **Expected:** After every addition/multiplication to keep numbers small. Be careful with division (multiplicative inverse).

---

### **Q29: Recognizing DP Disguised as Other Problems**

**Expected Answer:**
"Some problems don't explicitly ask for DP but have DP structure. Hints: 'minimize', 'maximize', 'count ways', 'find optimal'. If the problem has overlapping subproblems and optimal substructure, it's likely DP."

**Follow-up 1:** "Give an example of a problem that seems like greedy but is actually DP."
- **Expected:** Activity selection (greedy works), but some variants don't. Weighted interval scheduling requires DP.

**Follow-up 2:** "How do you distinguish between greedy and DP in an interview?"
- **Expected:** For greedy, you prove the greedy choice is always optimal (exchange argument). For DP, you show subproblem structure. If unsure, try greedy first; if counterexample, switch to DP.

**Follow-up 3:** "Are there problems where both greedy and DP work?"
- **Expected:** Yes. E.g., activity selection. Greedy is faster, but DP also works. Greedy is preferred if it works.

---

### **Q30: Memoization vs Tabulation in Practice**

**Expected Answer:**
"Memoization: easier to code (looks like recursion), handles sparse problems, but has overhead. Tabulation: faster, no recursion, but requires clear iteration order. In interviews, choose based on problem comfort. In production, usually tabulation wins."

**Follow-up 1:** "Can you mix memoization and tabulation?"
- **Expected:** Not typically, but you could use memoization on top of a partially filled table. Unusual.

**Follow-up 2:** "If you're unsure about iteration order, which approach is safer?"
- **Expected:** Memoization. Recursion naturally handles the order. Then optimize to tabulation if needed.

**Follow-up 3:** "Are there problems where only memoization works?"
- **Expected:** Rarely. Mostly, if tabulation seems hard, you need a better state design. But some state spaces are irregular (e.g., game trees); memoization is more natural.

---

## 🎯 SPECIAL FOCUS: TRANSITION LOGIC & CORRECTNESS

### **Q31: Recurrence Relation for Coin Change (Deep Dive)**

**Expected Answer:**
"For min coins: dp[i] is the minimum coins to make amount i. We try all coins c where c ≤ i. For each coin, we know dp[i-c] (min coins for remaining amount). So, dp[i] = min(dp[i], 1 + dp[i-c]) for all valid coins. This explores all possible 'first coins' we can use."

**Follow-up 1:** "Why do we compare with dp[i] in the min?"
- **Expected:** dp[i] might already have a value from a previous coin. We keep the minimum across all choices.

**Follow-up 2:** "What if no coin fits into amount i?"
- **Expected:** dp[i] stays as INF, indicating it's impossible.

**Follow-up 3:** "Does the order of coins in the loop matter for min coins?"
- **Expected:** No. Coin order doesn't affect the minimum result. (But it does for counting ways.)

---

### **Q32: Recurrence for Counting Ways (Deep Dive)**

**Expected Answer:**
"For counting ways to make amount with coins: dp[i] = number of ways. We iterate coins in outer loop and amounts in inner loop. For each coin c, we say 'if we use coin c at least once, the ways become dp[i - c].' We accumulate: dp[i] += dp[i - c]. This avoids counting the same combination multiple times."

**Follow-up 1:** "Why is the loop order (coins outer, amounts inner) crucial?"
- **Expected:** If amounts were outer loop, we'd count (1,2) and (2,1) as different ways, conflating combinations with permutations.

**Follow-up 2:** "What if we do (coins outer, amounts inner) for min coins?"
- **Expected:** Still correct for min coins, but the (min) operation doesn't benefit from loop order. Either order gives the same answer. But it's less efficient than (amounts outer, coins inner) because we update redundantly.

**Follow-up 3:** "How would you count ordered ways (permutations)?"
- **Expected:** Reverse the loop order: amounts outer, coins inner. Now (1,2) and (2,1) are counted separately.

---

### **Q33: Edit Distance — Understanding Operations**

**Expected Answer:**
"Three operations: insert, delete, replace. Insert means we insert a character into s1. Delete means we remove a character from s1. Replace means we change a character in s1. The recurrence dp[i][j] = 1 + min(dp[i-1][j], dp[i][j-1], dp[i-1][j-1]) represents these three options."

**Follow-up 1:** "Why does deleting from s1 correspond to dp[i-1][j]?"
- **Expected:** If we delete s1[i-1], we've matched s2[0..j-1] with s1[0..i-2]. The cost is dp[i-1][j] (transforming s1[0..i-2] to s2[0..j-1]) plus 1 (the delete).

**Follow-up 2:** "Can you derive the recurrence from first principles?"
- **Expected:** Consider: to transform s1[0..i-1] to s2[0..j-1], we can (a) delete s1[i-1] then transform s1[0..i-2] to s2[0..j-1], (b) insert s2[j-1] (transform s1[0..i-1] to s2[0..j-2] first), or (c) replace s1[i-1] with s2[j-1] (transform s1[0..i-2] to s2[0..j-2]).

**Follow-up 3:** "How would you handle costs where different operations have different costs?"
- **Expected:** Generalize: dp[i][j] = 1 + min(cost_delete + dp[i-1][j], cost_insert + dp[i][j-1], cost_replace + dp[i-1][j-1]).

---

### **Q34: LIS State Definition Clarification**

**Expected Answer:**
"dp[i] = length of longest increasing subsequence *ending at* index i. This is crucial. Not 'using elements up to i,' but 'with the last element being arr[i].' This state allows us to extend LIS to future elements."

**Follow-up 1:** "Why must the LIS *end* at index i?"
- **Expected:** Because in the recurrence, we extend dp[j] (j < i) by including arr[i]. This only makes sense if arr[i] is the last element.

**Follow-up 2:** "If we defined dp[i] as 'LIS using elements 0..i (not necessarily ending at i)', would the recurrence still work?"
- **Expected:** No. We'd lose track of what the last element is, making it hard to check the increasing condition.

**Follow-up 3:** "Once we have all dp[i] values, how do we get the final answer?"
- **Expected:** max(dp[i]) over all i. The maximum represents the longest LIS ending at any position.

---

### **Q35: 2D Grid DP — Setting Base Cases**

**Expected Answer:**
"For unique paths: dp[0][0] = 1 (one way to be at start). Entire first row = 1 (one way down to each cell). Entire first column = 1 (one way right to each cell). For edit distance: dp[0][j] = j (insert j chars), dp[i][0] = i (delete i chars). Both stem from the definition of each state."

**Follow-up 1:** "Why is the first row all 1s for unique paths?"
- **Expected:** To reach (0, j), you can only move right. There's exactly one way: go right j times.

**Follow-up 2:** "Why is the first column all 1s?"
- **Expected:** To reach (i, 0), you can only move down. There's exactly one way: go down i times.

**Follow-up 3:** "What if there's an obstacle at (0, 1)?"
- **Expected:** dp[0][1] = 0 (can't reach it), and dp[0][j] for j > 1 should also be 0 (can't bypass the obstacle moving only right).

---

## 📋 USAGE GUIDELINES FOR INTERVIEWS

### **Mock Interview Setup (60-90 minutes)**

1. **Question Selection (5 min)**: Choose 5-7 questions based on difficulty
2. **Reading (1-2 min per question)**: Interviewer reads the question
3. **Thinking (2-3 min)**: You think out loud, clarify the problem
4. **Explanation (3-5 min)**: Explain your approach, state, recurrence
5. **Coding (5-10 min)**: Code (or pseudocode) your solution
6. **Testing (2-3 min)**: Trace through an example
7. **Follow-ups (2-3 min)**: Address interviewer's follow-ups

---

### **Performance Self-Assessment**

After each question, rate yourself:
- **Green** ✅: Explained clearly, handled follow-ups, no major errors
- **Yellow** 🟡: Explained OK, stumbled on follow-ups, minor errors
- **Red** 🔴: Struggled with explanation, couldn't handle follow-ups, major errors

Track trends. By week's end, aim for mostly Green on basic/intermediate, mostly Yellow on advanced.

---

**Status:** ✅ Week 10 Interview QA Reference Complete — 35 questions with detailed follow-ups
