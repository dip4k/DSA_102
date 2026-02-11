# Morris Traversal (Threaded Binary Tree Traversal)

Morris traversal is a technique to traverse a **binary tree** using O(1) auxiliary space by temporarily creating “threads” (temporary pointer links) and restoring the tree afterward.
It’s most commonly used for inorder traversal, with a closely related preorder variant, and a more advanced postorder variant. 

---

## Core idea and invariants

When the current node has a left subtree, Morris traversal finds the node’s inorder predecessor (the rightmost node in the left subtree) and temporarily points that predecessor’s `right` to the current node.
**Invariant:** every temporary thread you create must later be removed, so the original tree structure is restored.

---

## Morris inorder (most common)

If `curr.left == null`, you visit `curr` and move to `curr.right`; otherwise you find the inorder predecessor `pred`.
If `pred.right == null`, create the thread `pred.right = curr` and move left; if `pred.right == curr`, remove the thread, visit `curr`, and move right.

### Pseudocode (inorder)

```text
curr = root
while curr != null:
  if curr.left == null:
    visit(curr)
    curr = curr.right
  else:
    pred = curr.left
    while pred.right != null and pred.right != curr:
      pred = pred.right

    if pred.right == null:
      pred.right = curr      // create thread
      curr = curr.left
    else:
      pred.right = null      // remove thread
      visit(curr)
      curr = curr.right
```

Time is O(n) and auxiliary space is O(1), assuming threads are properly removed.

---

## Morris preorder (visit timing change)

Morris preorder uses the same threading mechanism, but you **visit `curr` when you first create the thread** (i.e., before moving into the left subtree).
If `curr.left == null`, visit and go right; otherwise find `pred` and either create a thread (visit now, go left) or remove the thread (go right).

### Pseudocode (preorder)

```text
curr = root
while curr != null:
  if curr.left == null:
    visit(curr)
    curr = curr.right
  else:
    pred = curr.left
    while pred.right != null and pred.right != curr:
      pred = pred.right

    if pred.right == null:
      visit(curr)            // PRE visit here
      pred.right = curr      // create thread
      curr = curr.left
    else:
      pred.right = null      // remove thread
      curr = curr.right
```

---

## Morris postorder (advanced / optional)

A Morris-style postorder traversal exists, but it is more complex than inorder/preorder and is typically treated as an advanced follow-up.
Most approaches rely on temporary structural changes and carefully processing reversed paths/edges so the output becomes left-right-root while still restoring the tree.

---

## Pitfalls and when to use

The most common bug is forgetting to remove a created thread (`pred.right = null`), which leaves the tree mutated after traversal.
Use Morris primarily when a problem explicitly requires O(1) auxiliary space traversal; otherwise, iterative stack traversal is usually simpler and safer.

---

## Practice checklist (no solutions)

- Implement Morris inorder and confirm it matches iterative inorder output on the same tree.
- Implement Morris preorder and confirm it matches recursive preorder output.
- Add a validation step to ensure the tree shape is unchanged after traversal (threads removed).
