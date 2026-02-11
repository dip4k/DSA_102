# string traversal drills

## Drill index (26)

**Input convention (keeps drivers simple):**
- All strings are provided as full lines (can include spaces).
- For “two strings”, you’ll get two lines.
- For “string + int”, you’ll get string line then int line.
- For list outputs, print indices space-separated on one line (empty list → print empty line).

| ID | Pattern | Drill | Input (after ID line) | Output |
|---|---|---|---|---|
| D01 | Two pointers (opposite) | Valid palindrome (ignore non-alnum, case-insensitive) | `s` | `true/false` |
| D02 | Two pointers (opposite) | Reverse vowels | `s` | transformed string |
| D03 | Two pointers (same dir) | Remove all occurrences of a character | `s` then `ch` | string |
| D04 | Two pointers (same dir) | Remove consecutive duplicates (keep one) | `s` | string |
| D05 | Frequency map | First unique character index | `s` | int |
| D06 | Frequency map | Can construct (ransom-like) | `need` then `have` | `true/false` |
| D07 | Fixed sliding window | Max vowels in any substring of length `k` | `s` then `k` | int |
| D08 | Fixed sliding window | Count windows length `k` with all unique chars | `s` then `k` | int |
| D09 | Variable window (at most) | Longest substring without repeating | `s` | int |
| D10 | Variable window (at most) | Longest substring with at most `k` distinct | `s` then `k` | int |
| D11 | Exactly K | Count substrings with exactly `k` distinct | `s` then `k` | int |
| D12 | Exactly K | Count substrings with exactly `k` vowels | `s` then `k` | int |
| D13 | Anagram window | Find all anagram start indices | `s` then `p` | indices |
| D14 | Anagram window | Permutation inclusion | `p` then `s` | `true/false` |
| D15 | String builder | Run-length encode (e.g., aaabb → a3b2) | `s` | string |
| D16 | Stack/String builder | Backspace compare (`#` deletes previous) | `s` then `t` | `true/false` |
| D17 | Rabin–Karp | All pattern matches | `text` then `pattern` | indices |
| D18 | Rabin–Karp | Repeated substrings of fixed length `L` | `s` then `L` | duplicates (space-separated) |
| D19 | KMP | First occurrence (strStr) | `text` then `pattern` | int |
| D20 | KMP/LPS | Repeated substring pattern | `s` | `true/false` |
| D21 | Manacher/Palindrome | Longest palindromic substring | `s` | string |
| D22 | Palindrome counting | Count palindromic substrings | `s` | int |
| D23 | Z algorithm | All pattern matches (via Z) | `text` then `pattern` | indices |
| D24 | Z algorithm | Longest happy prefix | `s` | prefix string |
| D25 | Trie | Trie ops (insert/search/startsWith) | `q`, then q command lines | outputs per query |
| D26 | Trie + wildcard | WordDictionary ops (add/search with '.') | `q`, then q command lines | outputs per query |

***

## Python (single driver + stubs)

```python
# string_drills.py
import sys
from collections import defaultdict, Counter

# -----------------------------
# Helper I/O
# -----------------------------
def read_all_lines():
    return sys.stdin.read().splitlines()

def print_bool(x: bool):
    sys.stdout.write("true\n" if x else "false\n")

def print_int_list(arr):
    if not arr:
        sys.stdout.write("\n")
    else:
        sys.stdout.write(" ".join(map(str, arr)) + "\n")

# -----------------------------
# Pattern implementations (TODO)
# -----------------------------

# D01
def valid_palindrome(s: str) -> bool:
    # TODO: Two pointers (opposite ends), skip non-alnum, compare lowercased.
    raise NotImplementedError

# D02
def reverse_vowels(s: str) -> str:
    # TODO: Two pointers (opposite ends), swap vowels.
    raise NotImplementedError

# D03
def remove_char(s: str, ch: str) -> str:
    # TODO: Two pointers (same direction), build output in list.
    raise NotImplementedError

# D04
def remove_consecutive_dups_keep_one(s: str) -> str:
    # TODO: Same-direction pointers; keep only if different from last kept.
    raise NotImplementedError

# D05
def first_unique_index(s: str) -> int:
    # TODO: Frequency map; then scan.
    raise NotImplementedError

# D06
def can_construct(need: str, have: str) -> bool:
    # TODO: Frequency map decrement.
    raise NotImplementedError

# D07
def max_vowels_fixed_k(s: str, k: int) -> int:
    # TODO: Fixed window; maintain count of vowels.
    raise NotImplementedError

# D08
def count_unique_windows(s: str, k: int) -> int:
    # TODO: Fixed window + freq; window is valid if all counts <= 1.
    raise NotImplementedError

# D09
def longest_no_repeat(s: str) -> int:
    # TODO: Variable window (at most), last seen index map.
    raise NotImplementedError

# D10
def longest_at_most_k_distinct(s: str, k: int) -> int:
    # TODO: Variable window (at most K distinct), freq map.
    raise NotImplementedError

# D11
def count_substrings_exactly_k_distinct(s: str, k: int) -> int:
    # TODO: exactlyK = atMostK(k) - atMostK(k-1)
    raise NotImplementedError

# D12
def count_substrings_exactly_k_vowels(s: str, k: int) -> int:
    # TODO: exactlyK = atMostK(k) - atMostK(k-1) where "vowel count" is the constraint.
    raise NotImplementedError

# D13
def find_anagram_starts(s: str, p: str):
    # TODO: Fixed window length len(p) + freq comparison (opt: match counter).
    raise NotImplementedError

# D14
def contains_permutation(p: str, s: str) -> bool:
    # TODO: Same as anagram window but return boolean.
    raise NotImplementedError

# D15
def run_length_encode(s: str) -> str:
    # TODO: String builder/list append; output char+count groups.
    raise NotImplementedError

# D16
def backspace_compare(s: str, t: str) -> bool:
    # TODO: Stack simulation OR reverse-scan skip counters.
    raise NotImplementedError

# D17
def rabin_karp_all(text: str, pattern: str):
    # TODO: Rolling hash + verify on hash hit; return all starts.
    raise NotImplementedError

# D18
def repeated_substrings_fixed_len(s: str, L: int):
    # TODO: Rolling hash; return all substrings length L seen >=2 (unique list).
    raise NotImplementedError

# D19
def kmp_first(text: str, pattern: str) -> int:
    # TODO: Build LPS, then KMP scan; return first index or -1.
    raise NotImplementedError

# D20
def repeated_substring_pattern(s: str) -> bool:
    # TODO: Use LPS on full string; classic condition using lps[-1].
    raise NotImplementedError

# D21
def longest_pal_substring(s: str) -> str:
    # TODO: Manacher (O(n)) OR expand-center (O(n^2)) for drill; prefer Manacher.
    raise NotImplementedError

# D22
def count_pal_substrings(s: str) -> int:
    # TODO: Expand-around-center O(n^2) OR Manacher-based counting.
    raise NotImplementedError

# D23
def z_all(text: str, pattern: str):
    # TODO: Z on pattern + '$' + text; collect matches where Z[i]==len(pattern).
    raise NotImplementedError

# D24
def longest_happy_prefix(s: str) -> str:
    # TODO: Z or LPS; return longest proper prefix also suffix.
    raise NotImplementedError

# D25
class Trie:
    # TODO: Implement insert/search/startsWith
    def __init__(self):
        raise NotImplementedError

# D26
class WordDictionary:
    # TODO: Trie + '.' wildcard DFS in search
    def __init__(self):
        raise NotImplementedError

# -----------------------------
# Driver
# -----------------------------
def main():
    lines = read_all_lines()
    if not lines:
        return
    code = lines[0].strip()
    rest = lines[1:]

    def line(i, default=""):
        return rest[i] if i < len(rest) else default

    if code == "D01":
        print_bool(valid_palindrome(line(0, "")))

    elif code == "D02":
        sys.stdout.write(reverse_vowels(line(0, "")) + "\n")

    elif code == "D03":
        s = line(0, "")
        ch = line(1, "")
        ch = ch[0] if ch else ""
        sys.stdout.write(remove_char(s, ch) + "\n")

    elif code == "D04":
        sys.stdout.write(remove_consecutive_dups_keep_one(line(0, "")) + "\n")

    elif code == "D05":
        sys.stdout.write(str(first_unique_index(line(0, ""))) + "\n")

    elif code == "D06":
        need = line(0, "")
        have = line(1, "")
        print_bool(can_construct(need, have))

    elif code == "D07":
        s = line(0, "")
        k = int(line(1, "0"))
        sys.stdout.write(str(max_vowels_fixed_k(s, k)) + "\n")

    elif code == "D08":
        s = line(0, "")
        k = int(line(1, "0"))
        sys.stdout.write(str(count_unique_windows(s, k)) + "\n")

    elif code == "D09":
        sys.stdout.write(str(longest_no_repeat(line(0, ""))) + "\n")

    elif code == "D10":
        s = line(0, "")
        k = int(line(1, "0"))
        sys.stdout.write(str(longest_at_most_k_distinct(s, k)) + "\n")

    elif code == "D11":
        s = line(0, "")
        k = int(line(1, "0"))
        sys.stdout.write(str(count_substrings_exactly_k_distinct(s, k)) + "\n")

    elif code == "D12":
        s = line(0, "")
        k = int(line(1, "0"))
        sys.stdout.write(str(count_substrings_exactly_k_vowels(s, k)) + "\n")

    elif code == "D13":
        s = line(0, "")
        p = line(1, "")
        print_int_list(find_anagram_starts(s, p))

    elif code == "D14":
        p = line(0, "")
        s = line(1, "")
        print_bool(contains_permutation(p, s))

    elif code == "D15":
        sys.stdout.write(run_length_encode(line(0, "")) + "\n")

    elif code == "D16":
        s = line(0, "")
        t = line(1, "")
        print_bool(backspace_compare(s, t))

    elif code == "D17":
        text = line(0, "")
        pattern = line(1, "")
        print_int_list(rabin_karp_all(text, pattern))

    elif code == "D18":
        s = line(0, "")
        L = int(line(1, "0"))
        arr = repeated_substrings_fixed_len(s, L)
        if not arr:
            sys.stdout.write("\n")
        else:
            sys.stdout.write(" ".join(arr) + "\n")

    elif code == "D19":
        text = line(0, "")
        pattern = line(1, "")
        sys.stdout.write(str(kmp_first(text, pattern)) + "\n")

    elif code == "D20":
        print_bool(repeated_substring_pattern(line(0, "")))

    elif code == "D21":
        sys.stdout.write(longest_pal_substring(line(0, "")) + "\n")

    elif code == "D22":
        sys.stdout.write(str(count_pal_substrings(line(0, ""))) + "\n")

    elif code == "D23":
        text = line(0, "")
        pattern = line(1, "")
        print_int_list(z_all(text, pattern))

    elif code == "D24":
        sys.stdout.write(longest_happy_prefix(line(0, "")) + "\n")

    elif code == "D25":
        q = int(line(0, "0"))
        trie = Trie()
        out = []
        for i in range(q):
            cmd = line(1 + i, "")
            parts = cmd.split(" ", 1)
            op = parts[0]
            arg = parts [algo](https://algo.monster/liteproblems/125) if len(parts) > 1 else ""
            if op == "insert":
                trie.insert(arg)
            elif op == "search":
                out.append("true" if trie.search(arg) else "false")
            elif op == "startsWith":
                out.append("true" if trie.starts_with(arg) else "false")
        sys.stdout.write("\n".join(out) + ("\n" if out else ""))

    elif code == "D26":
        q = int(line(0, "0"))
        wd = WordDictionary()
        out = []
        for i in range(q):
            cmd = line(1 + i, "")
            parts = cmd.split(" ", 1)
            op = parts[0]
            arg = parts [algo](https://algo.monster/liteproblems/125) if len(parts) > 1 else ""
            if op == "add":
                wd.add_word(arg)
            elif op == "search":
                out.append("true" if wd.search(arg) else "false")
        sys.stdout.write("\n".join(out) + ("\n" if out else ""))

    else:
        sys.stdout.write("Unknown drill id\n")

if __name__ == "__main__":
    main()
```

### Example inputs
```text
D01
A man, a plan, a canal: Panama
```

```text
D25
7
insert apple
search apple
search app
startsWith app
insert app
search app
startsWith ap
```

***

## C# (single driver + stubs)

```csharp
// Program.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Program
{
    // -----------------------------
    // Helpers
    // -----------------------------
    static void PrintBool(bool x) => Console.WriteLine(x ? "true" : "false");

    static void PrintIntList(List<int> arr)
    {
        if (arr == null || arr.Count == 0) { Console.WriteLine(); return; }
        Console.WriteLine(string.Join(" ", arr));
    }

    // -----------------------------
    // Pattern implementations (TODO)
    // -----------------------------

    // D01
    static bool ValidPalindrome(string s)
    {
        // TODO: Two pointers opposite ends; skip non-alnum; compare lowercase.
        throw new NotImplementedException();
    }

    // D02
    static string ReverseVowels(string s)
    {
        // TODO: Two pointers opposite ends; swap vowels.
        throw new NotImplementedException();
    }

    // D03
    static string RemoveChar(string s, char ch)
    {
        // TODO: Same-direction pointers; build with StringBuilder or char array.
        throw new NotImplementedException();
    }

    // D04
    static string RemoveConsecutiveDupsKeepOne(string s)
    {
        // TODO: Keep only if differs from last kept.
        throw new NotImplementedException();
    }

    // D05
    static int FirstUniqueIndex(string s)
    {
        // TODO: Frequency dictionary; then scan.
        throw new NotImplementedException();
    }

    // D06
    static bool CanConstruct(string need, string have)
    {
        // TODO: Count in have then decrement for need.
        throw new NotImplementedException();
    }

    // D07
    static int MaxVowelsFixedK(string s, int k)
    {
        // TODO: Fixed window vowel count.
        throw new NotImplementedException();
    }

    // D08
    static int CountUniqueWindows(string s, int k)
    {
        // TODO: Fixed window + freq; all unique if freq count==k.
        throw new NotImplementedException();
    }

    // D09
    static int LongestNoRepeat(string s)
    {
        // TODO: Variable window; last seen indices.
        throw new NotImplementedException();
    }

    // D10
    static int LongestAtMostKDistinct(string s, int k)
    {
        // TODO: Variable window at most K distinct.
        throw new NotImplementedException();
    }

    // D11
    static int CountSubstringsExactlyKDistinct(string s, int k)
    {
        // TODO: exactlyK = atMostK(k) - atMostK(k-1)
        throw new NotImplementedException();
    }

    // D12
    static int CountSubstringsExactlyKVowels(string s, int k)
    {
        // TODO: exactlyK = atMostK(k) - atMostK(k-1) on vowel-count constraint.
        throw new NotImplementedException();
    }

    // D13
    static List<int> FindAnagramStarts(string s, string p)
    {
        // TODO: Fixed window len(p) + frequency tracking (opt: match counter).
        throw new NotImplementedException();
    }

    // D14
    static bool ContainsPermutation(string p, string s)
    {
        // TODO: Return true if any anagram window matches.
        throw new NotImplementedException();
    }

    // D15
    static string RunLengthEncode(string s)
    {
        // TODO: Build output groups char+count.
        throw new NotImplementedException();
    }

    // D16
    static bool BackspaceCompare(string s, string t)
    {
        // TODO: Stack or reverse scan with skip counters.
        throw new NotImplementedException();
    }

    // D17
    static List<int> RabinKarpAll(string text, string pattern)
    {
        // TODO: Rolling hash + verify.
        throw new NotImplementedException();
    }

    // D18
    static List<string> RepeatedSubstringsFixedLen(string s, int L)
    {
        // TODO: Rolling hash; return unique duplicates.
        throw new NotImplementedException();
    }

    // D19
    static int KmpFirst(string text, string pattern)
    {
        // TODO: Build LPS; KMP scan; first index or -1.
        throw new NotImplementedException();
    }

    // D20
    static bool RepeatedSubstringPattern(string s)
    {
        // TODO: LPS-based check.
        throw new NotImplementedException();
    }

    // D21
    static string LongestPalSubstring(string s)
    {
        // TODO: Manacher preferred (or expand-center for first pass).
        throw new NotImplementedException();
    }

    // D22
    static int CountPalSubstrings(string s)
    {
        // TODO: Expand-center or Manacher count.
        throw new NotImplementedException();
    }

    // D23
    static List<int> ZAll(string text, string pattern)
    {
        // TODO: Z on pattern + '$' + text.
        throw new NotImplementedException();
    }

    // D24
    static string LongestHappyPrefix(string s)
    {
        // TODO: Z or LPS.
        throw new NotImplementedException();
    }

    // D25
    public class Trie
    {
        // TODO: Implement insert/search/startsWith
        public Trie() { throw new NotImplementedException(); }
        public void Insert(string word) { throw new NotImplementedException(); }
        public bool Search(string word) { throw new NotImplementedException(); }
        public bool StartsWith(string prefix) { throw new NotImplementedException(); }
    }

    // D26
    public class WordDictionary
    {
        // TODO: add + search with '.' wildcard
        public WordDictionary() { throw new NotImplementedException(); }
        public void Add(string word) { throw new NotImplementedException(); }
        public bool Search(string word) { throw new NotImplementedException(); }
    }

    // -----------------------------
    // Driver
    // -----------------------------
    public static void Main()
    {
        string? codeLine = Console.ReadLine();
        if (codeLine == null) return;
        string code = codeLine.Trim();

        string ReadLineSafe() => Console.ReadLine() ?? "";

        switch (code)
        {
            case "D01":
                PrintBool(ValidPalindrome(ReadLineSafe()));
                break;

            case "D02":
                Console.WriteLine(ReverseVowels(ReadLineSafe()));
                break;

            case "D03":
            {
                string s = ReadLineSafe();
                string chLine = ReadLineSafe();
                char ch = chLine.Length > 0 ? chLine[0] : '\0';
                Console.WriteLine(RemoveChar(s, ch));
                break;
            }

            case "D04":
                Console.WriteLine(RemoveConsecutiveDupsKeepOne(ReadLineSafe()));
                break;

            case "D05":
                Console.WriteLine(FirstUniqueIndex(ReadLineSafe()));
                break;

            case "D06":
            {
                string need = ReadLineSafe();
                string have = ReadLineSafe();
                PrintBool(CanConstruct(need, have));
                break;
            }

            case "D07":
            {
                string s = ReadLineSafe();
                int k = int.Parse(ReadLineSafe());
                Console.WriteLine(MaxVowelsFixedK(s, k));
                break;
            }

            case "D08":
            {
                string s = ReadLineSafe();
                int k = int.Parse(ReadLineSafe());
                Console.WriteLine(CountUniqueWindows(s, k));
                break;
            }

            case "D09":
                Console.WriteLine(LongestNoRepeat(ReadLineSafe()));
                break;

            case "D10":
            {
                string s = ReadLineSafe();
                int k = int.Parse(ReadLineSafe());
                Console.WriteLine(LongestAtMostKDistinct(s, k));
                break;
            }

            case "D11":
            {
                string s = ReadLineSafe();
                int k = int.Parse(ReadLineSafe());
                Console.WriteLine(CountSubstringsExactlyKDistinct(s, k));
                break;
            }

            case "D12":
            {
                string s = ReadLineSafe();
                int k = int.Parse(ReadLineSafe());
                Console.WriteLine(CountSubstringsExactlyKVowels(s, k));
                break;
            }

            case "D13":
            {
                string s = ReadLineSafe();
                string p = ReadLineSafe();
                PrintIntList(FindAnagramStarts(s, p));
                break;
            }

            case "D14":
            {
                string p = ReadLineSafe();
                string s = ReadLineSafe();
                PrintBool(ContainsPermutation(p, s));
                break;
            }

            case "D15":
                Console.WriteLine(RunLengthEncode(ReadLineSafe()));
                break;

            case "D16":
            {
                string s = ReadLineSafe();
                string t = ReadLineSafe();
                PrintBool(BackspaceCompare(s, t));
                break;
            }

            case "D17":
            {
                string text = ReadLineSafe();
                string pattern = ReadLineSafe();
                PrintIntList(RabinKarpAll(text, pattern));
                break;
            }

            case "D18":
            {
                string s = ReadLineSafe();
                int L = int.Parse(ReadLineSafe());
                var arr = RepeatedSubstringsFixedLen(s, L);
                Console.WriteLine(arr.Count == 0 ? "" : string.Join(" ", arr));
                break;
            }

            case "D19":
            {
                string text = ReadLineSafe();
                string pattern = ReadLineSafe();
                Console.WriteLine(KmpFirst(text, pattern));
                break;
            }

            case "D20":
                PrintBool(RepeatedSubstringPattern(ReadLineSafe()));
                break;

            case "D21":
                Console.WriteLine(LongestPalSubstring(ReadLineSafe()));
                break;

            case "D22":
                Console.WriteLine(CountPalSubstrings(ReadLineSafe()));
                break;

            case "D23":
            {
                string text = ReadLineSafe();
                string pattern = ReadLineSafe();
                PrintIntList(ZAll(text, pattern));
                break;
            }

            case "D24":
                Console.WriteLine(LongestHappyPrefix(ReadLineSafe()));
                break;

            case "D25":
            {
                int q = int.Parse(ReadLineSafe());
                var trie = new Trie();
                var outLines = new List<string>();
                for (int i = 0; i < q; i++)
                {
                    string cmd = ReadLineSafe();
                    var parts = cmd.Split(' ', 2);
                    string op = parts[0];
                    string arg = parts.Length > 1 ? parts [algo](https://algo.monster/liteproblems/125) : "";

                    if (op == "insert") trie.Insert(arg);
                    else if (op == "search") outLines.Add(trie.Search(arg) ? "true" : "false");
                    else if (op == "startsWith") outLines.Add(trie.StartsWith(arg) ? "true" : "false");
                }
                Console.WriteLine(string.Join("\n", outLines));
                break;
            }

            case "D26":
            {
                int q = int.Parse(ReadLineSafe());
                var wd = new WordDictionary();
                var outLines = new List<string>();
                for (int i = 0; i < q; i++)
                {
                    string cmd = ReadLineSafe();
                    var parts = cmd.Split(' ', 2);
                    string op = parts[0];
                    string arg = parts.Length > 1 ? parts [algo](https://algo.monster/liteproblems/125) : "";

                    if (op == "add") wd.Add(arg);
                    else if (op == "search") outLines.Add(wd.Search(arg) ? "true" : "false");
                }
                Console.WriteLine(string.Join("\n", outLines));
                break;
            }

            default:
                Console.WriteLine("Unknown drill id");
                break;
        }
    }
}
```