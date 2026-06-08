---
agent: agent
tools: [vscode, execute, read, agent, edit, search, web, browser, todo]
description: "Run daily practice review: log solved attempts, classify mistakes, and schedule reattempts."
---

Run a daily practice review for date ${input:date}.

Inputs
- Focus week: ${input:week}
- Problems attempted: ${input:problems}
- Total study time: ${input:minutes}

Steps
1. Add entries to learning_tracking/Practice_Log.md.
2. For partial/failed items, add root cause and fix strategy to learning_tracking/Question_Bank.md.
3. Propose reattempt dates and add them.
4. Summarize top 3 weak patterns.

Output
- Files updated in place.
- Short summary with next-day focus plan.
