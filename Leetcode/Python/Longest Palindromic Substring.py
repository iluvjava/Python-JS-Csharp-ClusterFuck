"""
Leetcode Link: https://leetcode.com/problems/longest-palindromic-substring/
------------------------------------------------------------------------------------------------------------------------
assume that there is a palindromic substring, s[i:j], then, we consider its relations with the
extended substring: s[i-1, j+1], assume i-1, j+1 is valid index.
    s[i-1] == s[j+1] ==> s[i-1, j+1] will be a palindrome.
        record the value and never check string inside the range again.
    else ==> s[i-1, j+1] is not a palindrome.
        start the search of palindrome outside the range.

aabbaa

dp[i][j] := whether s[i:j] is a palidrome, and the longest palindrome in the s[i:j]
    s[i:j] is a palindrome ==> we should not check any substring inside i, j

examples:

"ccc"
    s[0:0] is a palindrome, -1, 1, is not a outiside the array, no consideration.
    s[1:1] is a palindrome, using s[0] is a palindrome s[0] == s[2] ==> the whole string is a palindrome.

"babad"
    let's try to make the brute force better.
    let's define ! to indicate whatever proceeds the !, it's a palindrome.
    let's define !! to indicated whatever proceeds the !!, it's not a palindrome.
    s[0:0]!
    s[0:1]!!
        <== s[0] != s[1]
    s[0:2]!!
        query s[1], and s[1]! then s[0] != s[2]
    s[0:3]!!
        query s[1:2], and s[1] != s[2] break
    s[0:4]!
        query s[1:3], is not asked before.
        query s[2:2], and s[2:2]!
        s[1] == s[3] ==> s[1:3]!
        s[0] == s[4] ==> s[0:4]!

    improvements:
        s[i:j]! ==> s[i+1:j-1]! ==> s[i+2, j-2]! ...
------------------------------------------------------------------------------------------------------------------------
    dp[i][j] := if s[i:j] is palindrome;
    for i th index starting at 0 ending at len(s):
        for j th index starting at i ending at len(s):
            while i+m >= j-m && m<=0 where m starting at 1 and increments += 1:
                if dp[i+m][j-m] is solved:
                    dp[i+m-1][j-m+1] := dp[i+m][j-m]&&s[i+m]==s[j-m];
                    store the length in a running total;
                    change the rule of incrementing m into -=1;
------------------------------------------------------------------------------------------------------------------------
    It seems like we can grow the problem from buttom up.
------------------------------------------------------------------------------------------------------------------------

    define var "running max"
    for i th position in the string:
        s[i]! record length in to the running max.
        break if i is the end or the beginning of the array.
        for j starting at 1 ending when reaches the end of the array:
            if s[i-j] != s[i+j]
                record length into the running max
        if s[j]==s[j+1]:
            record the length into the running max
            for j starting at 1 ending when reaches the end of the array:
                if s[i-j] != s[i+j+1]
                    record length into the running max
------------------------------------------------------------------------------------------------------------------------
"""

def longest_palindrome(s)-> str:
    """
    :param s:
    :return:
    """
    """
    >>> longest_palindrome("aaasddsacc")
    'asddsa'
    """
    idx_l = (0, 0, 1) # indices, length
    for i in range(len(s)):
        if min(len(s) - i, i) < idx_l[2]//2 + 1:
            continue
        j = 1
        while i - j >= 0 and i + j < len(s):
            if s[i - j] == s[i + j]:
                if 2*j + 1 > idx_l[2]:
                    idx_l = (i - j, i + j, 2*j + 1)
                j += 1
            else:
                break
        if s[i - 1] == s[i]:
            idx_l = (i - 1, i, 2) if idx_l[2] < 2 else idx_l
            j = 1
            while i - 1 - j >= 0 and i + j < len(s):
                if s[i - 1 - j] == s[i + j]:
                    if 2*(j + 1) > idx_l[2]:
                        idx_l = (i - 1 - j, i + j, 2*(j + 1))
                    j += 1
                else:
                    break
    return s[idx_l[0]: idx_l[1] + 1]


"""
This is Nost's solutions: 
    memory: abosolutely demonlished all submissions
    speed: beat over 60% of submissions. 
"""
def longest_palindrome2(s: str) -> str:
    indexes = (0, 0)
    longest_length = 0
    slen = len(s)
    for ii in range(slen):
        for jj in [ii, ii + 1]:
            i = ii
            j = jj
            while i >= 0 and j <= slen:
                if j - i < 2:
                    pass
                elif s[i] == s[j - 1]:
                    pass
                else:
                    break
                i -= 1
                j += 1
            i += 1
            j -= 1
            if j - i > longest_length:
                longest_length = j - i
                indexes = (i, j)
    return s[indexes[0] : indexes[1]]
if __name__ == "__main__":
    print("this shit is running...")