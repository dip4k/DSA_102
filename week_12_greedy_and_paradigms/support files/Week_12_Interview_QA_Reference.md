# Week 12 Interview Q&A Reference — Greedy Algorithms and Proofs

This file is a question bank for Week 12: Greedy Algorithms and Proofs.

Use it to:
- Run mock interviews (you ask, candidate answers on whiteboard).
- Drill correctness proofs, not just coding.
- Train yourself to recognize when greedy is safe and when it fails.

No answers are provided by design. Treat every follow-up as something you must also be able to defend rigorously.


## 1. Greedy Fundamentals, Optimal Substructure, and Proof Techniques

**Q1.** What is a greedy algorithm?  
- Follow-up 1: How does a greedy algorithm differ conceptually from dynamic programming?  
- Follow-up 2: Give an example of a problem where greedy is optimal and one where greedy fails.  
- Follow-up 3: In an interview, how would you justify that your greedy choice is safe, beyond just “it works on examples”?

**Q2.** Define the “greedy choice property” and “optimal substructure.”  
- Follow-up 1: Why are both necessary (but not sufficient) conditions for a greedy algorithm to be correct?  
- Follow-up 2: Can you think of a problem that has optimal substructure but does not admit a correct greedy solution?  
- Follow-up 3: How would you test in an interview if a problem might have the greedy choice property?

**Q3.** Outline a generic template for designing a greedy algorithm for an optimization problem.  
- Follow-up 1: Where does “sorting by some criterion” typically appear in this template?  
- Follow-up 2: What are common ways to define the greedy priority or key?  
- Follow-up 3: Give an example of a problem where the right sort key is not obvious and must be carefully justified.

**Q4.** Explain the “exchange argument” in the context of greedy algorithms.  
- Follow-up 1: How would you structure an exchange argument proof step by step?  
- Follow-up 2: Why does the exchange argument often start from an optimal solution instead of from the greedy one?  
- Follow-up 3: Give a high-level sketch of an exchange argument for any greedy algorithm you know (without full details).

**Q5.** Compare and contrast “exchange argument” vs “greedy stays ahead” as proof strategies.  
- Follow-up 1: In what types of problems is “greedy stays ahead” more natural than an exchange argument?  
- Follow-up 2: How would you formalize “stays ahead” for a scheduling or selection problem?  
- Follow-up 3: Give an example where either proof style would work, but one is much simpler.

**Q6.** How would you prove termination and correctness for a greedy algorithm that repeatedly makes local modifications to a solution (e.g., repeatedly merging sets or intervals)?  
- Follow-up 1: What is the typical measure you track to argue that the algorithm will eventually terminate?  
- Follow-up 2: How do you ensure that no local modification ever makes the solution worse?  
- Follow-up 3: How would you argue that, at termination, no further local improvement is possible?

**Q7.** Describe a systematic approach to testing whether a greedy idea is likely to be wrong.  
- Follow-up 1: What kinds of counterexamples are you trying to construct?  
- Follow-up 2: Why are “ties,” “boundary cases,” and “overlapping structures” common failure modes for naive greedy approaches?  
- Follow-up 3: How can you use small brute-force search mentally (on tiny inputs) to stress-test a greedy proposal?

**Q8.** How do you integrate induction into a greedy correctness proof?  
- Follow-up 1: What is the typical induction hypothesis in such proofs?  
- Follow-up 2: At what point in the greedy process do you apply induction (after each step, or after building a prefix of the solution)?  
- Follow-up 3: How do you connect the inductive step to the greedy choice property?

**Q9.** Discuss the trade-offs between using greedy vs dynamic programming vs backtracking on the same optimization problem.  
- Follow-up 1: How do time and memory complexity typically compare between these paradigms?  
- Follow-up 2: Under what conditions would you choose a greedy approach even if DP is also possible?  
- Follow-up 3: Give an example of a problem where a greedy heuristic is fast but only approximate, while DP is exact but too slow for large inputs.

**Q10.** In interviews, what red flags suggest that a candidate’s greedy solution is probably incorrect even if it passes a few sample tests?  
- Follow-up 1: What kinds of missing arguments or hand-wavy claims do you look for?  
- Follow-up 2: Why is “it intuitively feels right” not enough for a correctness justification?  
- Follow-up 3: How would you gently push a candidate to strengthen their argument without giving away the full proof?


## 2. Activity Selection and Interval Scheduling Variants

**Q11.** Define the classic activity selection problem and its objective.  
- Follow-up 1: State the greedy algorithm for this problem in high-level pseudocode.  
- Follow-up 2: Why do we sort by finish times and not by start times or activity durations?  
- Follow-up 3: What is the time complexity of the standard implementation?

**Q12.** Explain the exchange argument that justifies sorting by earliest finishing time in activity selection.  
- Follow-up 1: How do you show that there exists an optimal solution that starts with the earliest-finishing activity?  
- Follow-up 2: What is the “swap” operation in this specific argument?  
- Follow-up 3: How does this argument extend from the first activity to the entire schedule?

**Q13.** Consider interval scheduling where each activity has a weight (or profit) and you want to maximize total weight of non-overlapping intervals.  
- Follow-up 1: Why does the simple greedy strategy by finish time fail for the weighted case?  
- Follow-up 2: What dynamic programming formulation solves the weighted interval scheduling problem?  
- Follow-up 3: How would you explain, at a high level, why the DP is necessary here?

**Q14.** Define the “minimum number of meeting rooms” (or classroom allocation) problem.  
- Follow-up 1: What is the typical algorithmic pattern (e.g., sweep line) used to solve it?  
- Follow-up 2: How does this differ from maximizing the number of non-overlapping intervals?  
- Follow-up 3: How can you interpret the maximum overlap of intervals in terms of required resources?

**Q15.** Describe the sweep line algorithm for computing the minimum number of resources (rooms) needed for a set of intervals.  
- Follow-up 1: How do you convert intervals into “events” for the sweep line?  
- Follow-up 2: Why is it important to decide whether “end events” are processed before “start events” when times are equal?  
- Follow-up 3: What is the complexity, and what data structures do you rely on?

**Q16.** Suppose you are given intervals and you want to remove as few as possible so that the remaining ones are non-overlapping.  
- Follow-up 1: How do you reduce this problem to an activity selection variant?  
- Follow-up 2: What is the greedy rule that solves it optimally?  
- Follow-up 3: How would you prove that this greedy rule yields the minimum number of removals?

**Q17.** Can you design a greedy solution for selecting intervals that cover a given time range with the minimum number of intervals?  
- Follow-up 1: What would you sort by, and what is your local choice at each step?  
- Follow-up 2: Where might a naive greedy rule (e.g., “shortest interval first”) fail?  
- Follow-up 3: Sketch an exchange argument or “greedy stays ahead” proof idea for your proposed rule.

**Q18.** Discuss common pitfalls when attempting to apply greedy strategies to interval problems.  
- Follow-up 1: Why is “shortest interval first” not a safe generic strategy?  
- Follow-up 2: How can overlapping structures and “gaps” break naive greedy rules?  
- Follow-up 3: In an interview, how would you systematically test your interval-based greedy idea on tricky edge cases?


## 3. Huffman Coding and Optimal Prefix Trees

**Q19.** Describe the Huffman coding problem and its objective.  
- Follow-up 1: Why do we restrict ourselves to prefix codes in this context?  
- Follow-up 2: How does prefix-freeness relate to decodability?  
- Follow-up 3: What metric are we minimizing when we build a Huffman tree?

**Q20.** Explain the Huffman algorithm step by step.  
- Follow-up 1: What data structure is used to repeatedly pick the two least frequent symbols?  
- Follow-up 2: How is the internal node frequency computed from its children?  
- Follow-up 3: What is the overall time complexity of building a Huffman tree?

**Q21.** Why is combining the two least frequent symbols at each step a safe greedy choice?  
- Follow-up 1: How would you set up an exchange argument to justify this choice?  
- Follow-up 2: Why must the two least frequent symbols appear at the maximum depth of some optimal prefix code tree?  
- Follow-up 3: How does repeatedly applying this idea lead to an optimal tree?

**Q22.** Sketch a correctness proof for Huffman coding using an exchange argument.  
- Follow-up 1: What is the initial “optimal solution” you start with in the argument?  
- Follow-up 2: What specific “swap” or restructuring do you perform on the tree?  
- Follow-up 3: How do you conclude that this process can transform any optimal tree into the Huffman-constructed tree without increasing cost?

**Q23.** Discuss the relationship between Huffman coding and other tree-based optimization problems.  
- Follow-up 1: How is it similar to building an optimal binary search tree, and how is it different?  
- Follow-up 2: Why does Huffman admit a greedy solution while optimal BSTs usually require dynamic programming?  
- Follow-up 3: What structural property makes the Huffman problem friendlier to greedy design?

**Q24.** How would you handle symbols that have equal frequencies in Huffman coding?  
- Follow-up 1: Does tying break optimality, or just lead to multiple optimal trees?  
- Follow-up 2: How might different tie-breaking choices affect the actual codewords generated?  
- Follow-up 3: In practice or in interviews, do you need to match a specific tree structure or just produce any optimal-length code?

**Q25.** Consider an extended version of Huffman coding where merging nodes has an additional fixed overhead cost.  
- Follow-up 1: How might this change the greedy rule or the cost function?  
- Follow-up 2: Do you think a simple “least frequency first” rule is still obviously optimal? Why or why not?  
- Follow-up 3: How would you start to either prove or disprove correctness of a modified greedy rule in this setting?


## 4. Fractional Knapsack, Job Sequencing, and Scheduling

**Q26.** State the fractional knapsack problem and how it differs from the 0/1 knapsack problem.  
- Follow-up 1: Why does the ability to take fractional amounts fundamentally change the algorithmic solution?  
- Follow-up 2: What is the standard greedy rule used to solve fractional knapsack optimally?  
- Follow-up 3: What is the time complexity, and where does sorting come in?

**Q27.** Prove or outline why sorting by value-to-weight ratio is optimal for fractional knapsack.  
- Follow-up 1: How would you set up an exchange argument between two items with different ratios?  
- Follow-up 2: What happens if your solution contains a lower-ratio item while some capacity is still taken by a higher-ratio item?  
- Follow-up 3: How do you handle the case where you partially include an item at the boundary?

**Q28.** Explain why the same greedy strategy by value-to-weight ratio fails for 0/1 knapsack.  
- Follow-up 1: Provide or describe a small counterexample that breaks this strategy.  
- Follow-up 2: What is the usual algorithmic approach for 0/1 knapsack instead of greedy?  
- Follow-up 3: In interviews, how would you articulate the fundamental reason greedy cannot handle the combinatorial interactions in 0/1 knapsack?

**Q29.** Describe the job sequencing with deadlines and profits problem.  
- Follow-up 1: What is the objective function in this problem?  
- Follow-up 2: Which greedy rule is commonly used (in terms of job order)?  
- Follow-up 3: What data structure or technique is used to assign jobs to time slots efficiently?

**Q30.** Outline an algorithm for job sequencing with deadlines that maximizes profit.  
- Follow-up 1: How do you decide which slot to place a job into, given its deadline?  
- Follow-up 2: What is the complexity of a naive implementation vs an optimized one?  
- Follow-up 3: How would you argue informally that placing each job as late as possible is a good idea?

**Q31.** Compare job sequencing with deadlines to interval scheduling and to knapsack.  
- Follow-up 1: In what way is job sequencing similar to interval scheduling?  
- Follow-up 2: How does the profit component make it more like knapsack?  
- Follow-up 3: Why does a greedy approach still work here, whereas it fails for 0/1 knapsack?

**Q32.** Give an example of a scheduling problem where a natural greedy rule fails.  
- Follow-up 1: Describe the naive greedy rule and why it seems reasonable at first glance.  
- Follow-up 2: Construct or describe a counterexample that shows the rule fails.  
- Follow-up 3: What alternative algorithmic paradigm would you consider for this problem?

**Q33.** How would you handle multiple machine scheduling with greedy ideas (for example, scheduling jobs on identical machines to minimize the makespan)?  
- Follow-up 1: What is the “Longest Processing Time first” (LPT) rule, and what does it try to achieve?  
- Follow-up 2: Is LPT always optimal? If not, what kind of guarantee (approximation) can it provide?  
- Follow-up 3: How would you explain the intuition behind LPT’s approximation behavior to an interviewer?


## 5. Greedy in Systems, MSTs, Matroids, and Approximation

**Q34.** Explain why minimum spanning tree algorithms like Kruskal’s and Prim’s can be viewed as greedy algorithms.  
- Follow-up 1: What is the local choice each algorithm makes at every step?  
- Follow-up 2: How does the cut property justify Kruskal’s edge selection rule?  
- Follow-up 3: How does Prim’s algorithm use a frontier or “growing tree” perspective?

**Q35.** State and explain the cut property for MSTs.  
- Follow-up 1: How does this property imply that the minimum-weight edge crossing any cut can always be part of some MST?  
- Follow-up 2: How is the cut property used in the correctness proof of Kruskal’s algorithm?  
- Follow-up 3: Can you think of a non-MST problem where a similar “cut argument” might be useful?

**Q36.** How are greedy algorithms used in routing or shortest path computations, such as Dijkstra’s algorithm?  
- Follow-up 1: What is the greedy choice at each iteration in Dijkstra’s algorithm?  
- Follow-up 2: Why does Dijkstra’s algorithm require non-negative edge weights to be correct?  
- Follow-up 3: What goes wrong if negative weight edges are present?

**Q37.** Describe how greedy ideas appear in caching strategies, such as LRU (Least Recently Used).  
- Follow-up 1: Why can LRU be considered a greedy heuristic in time?  
- Follow-up 2: In what sense is “Belady’s optimal algorithm” a theoretical greedy algorithm for caching?  
- Follow-up 3: Why is Belady’s algorithm not implementable in real systems, and how does that affect the design of practical caches?

**Q38.** What is a matroid, at a high level, and why is it relevant to greedy algorithms?  
- Follow-up 1: How do matroid properties generalize the correctness of greedy algorithms for certain problems?  
- Follow-up 2: Give a simple example of a matroid (e.g., graphic matroid or uniform matroid) and the associated greedy algorithm.  
- Follow-up 3: In an interview, how much matroid theory detail would you typically include vs just intuitive explanation?

**Q39.** Discuss a greedy approximation algorithm for the set cover problem.  
- Follow-up 1: What is the greedy rule used for set cover?  
- Follow-up 2: Why is this strategy not optimal, but still valuable?  
- Follow-up 3: What kind of approximation guarantee (in terms of log n) does the standard greedy set cover algorithm achieve?

**Q40.** How do you reason about the quality of a greedy approximation algorithm?  
- Follow-up 1: What are typical proof techniques used to derive approximation ratios?  
- Follow-up 2: How would you compare the greedy solution’s cost to the optimal cost in a proof?  
- Follow-up 3: Give an example of using a charging argument or potential function in the context of a greedy approximation.

**Q41.** In real systems, why might engineers use greedy heuristics even when exact algorithms exist?  
- Follow-up 1: How do constraints like latency, memory, and implementation complexity influence this choice?  
- Follow-up 2: Can you give an example from networking, operating systems, or databases where a simple greedy heuristic is widely used?  
- Follow-up 3: How would you discuss the trade-offs of such a heuristic in an interview?

**Q42.** Consider a problem that can be solved by both a greedy algorithm and a dynamic programming algorithm (e.g., some special cases of interval scheduling or shortest path on a DAG).  
- Follow-up 1: How would you decide which approach to present in an interview?  
- Follow-up 2: What does choosing the greedy solution demonstrate about your skills and understanding?  
- Follow-up 3: How would you defend your choice of paradigm to the interviewer?

**Q43.** How would you debug a greedy solution that seems correct but fails on certain test cases?  
- Follow-up 1: What systematic process would you follow to isolate the failure?  
- Follow-up 2: How would you use small counterexamples to refine or reject your greedy hypothesis?  
- Follow-up 3: How do you decide whether to fix the greedy idea or abandon it in favor of DP/backtracking?

**Q44.** What are some typical “signals” in an interview problem statement that hint a greedy solution might exist?  
- Follow-up 1: How do words like “earliest”, “latest”, “shortest”, “locally best”, or “always pick” guide your thinking?  
- Follow-up 2: How do constraints like “you can decide online as input arrives” influence your choice of a greedy model?  
- Follow-up 3: When you see an ordering or sorting constraint, how do you decide what key to sort on?

**Q45.** Summarize your personal checklist for validating a greedy idea before committing to implementation in an interview.  
- Follow-up 1: What specific questions do you ask yourself about optimal substructure and the greedy choice property?  
- Follow-up 2: How do you structure at least a sketch of a proof (exchange, stays-ahead, or induction) before coding?  
- Follow-up 3: How do you communicate this reasoning to the interviewer in a clear and time-efficient way?
