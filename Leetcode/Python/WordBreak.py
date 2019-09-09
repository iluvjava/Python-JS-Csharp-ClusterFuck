
def Solve(s: str, l: list)-> bool:

    return Breakable(s, len(s), l)


def Breakable(
                s: str,
                m: int,
                dict:list,
                memo: list = None
             ) -> bool:
    """

    :param s:
    :param m:
    :param dict:
    :param memo:
    :return:
    True if s[0:m+1] is a Breakable String, else false.
    """
    memo = [None] * (len(s) + 1) if memo is None else memo
    memo[0] = True
    if memo[m] is not None:
        return memo[m]
    for i in range(m):
        if (s[i:m] in dict) and (Breakable(s, i, dict, memo) is True):
            memo[m] = True
            return True
    memo[m] = False
    return False

def main():
    print(Solve("leetcode",["leet", "code"]))
    print(Solve("thatisacat", ["that", "is","a", "cat"]))
    print(Solve("rainbowdash", ["rain", "bow", "dash", "rainbow","dash"]))
    print(Solve("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaabaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                ["a", "aa", "aaa", "aaaa", "aaaaa", "aaaaaa", "aaaaaaa", "aaaaaaaa", "aaaaaaaaa", "aaaaaaaaaa"]))
    return




if __name__ == "__main__":
    main()