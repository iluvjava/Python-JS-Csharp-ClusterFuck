"""
Leetcode link: https://leetcode.com/problems/maximum-length-of-repeated-subarray/

    Let dp[i][j] be the answer corresponds to input: A[i:], B[j:]
========================================================================================================================
Brain storm:
========================================================================================================================
    Assume the sub-array is continuous sub-array.

    then dp[i][j] := maximum length of the continuous sub-array presented in A[i:], B[j:]

    Dynamic Programming?
    consider dp[i][j], dp[i][j-1], dp[i-1][j], dp[i-1][j-1] in relations to dp[i][j].
========================================================================================================================
let's take a look at some of the possible relations:
========================================================================================================================
    Assume i, j >= 1:

        A[i] == B[j] ==> A[i], A[j] are in a shared continuous sub-array; there should be a integer storing that length
            of the common sub-array it's in, let it be denoted by "m"

        A[i-1] != B[j-1] ==> The continuous sub-array is broke, take dp[i+j][j+1] = max(dp[i][j], m)
            i := i - 1
            j := j

    I felt like this might not be the right approach, so I am thinking about using the hash table.
========================================================================================================================
    The most naive algorithm will take the complexity of O(MN) where M, N are the length of the 2 arrays.
        can we improve that with the use of data structure?
    It's tempted to think that:
        However, if there is a match found, then it can be removed from both of the array because any continuous
        sub-arrays in that array will match too, but their length will be less, hence doesn't affect the final outcome
        of the function.
    but observe:
        A = [1,2,3,7,2,3,5,7]
        B = [1,2,3,5,7]
        A[0:2] matches B[0:2] however B[1:] matches A[4:], hence removing or skipping the sub-array that that is already
        matched will remove the subsequent solution.

    Let's try to convert it to a hash table:
    A = {1=[0], 2=[1,4], 3=[2,5], 7=[3,7], 5=[6]}

    for i in B:
        find index of i in A named j
            compare starting at i in B and j in B
                record the maximum equaled sub-array.
                remove i from the hash-table.


    This algorithm is unlikely to pass the test because of the complexity depends on the length of the sub-arrat

"""


def findLength(A, B):
    memo = [[0] * (len(B) + 1) for _ in range(len(A) + 1)]
    for i in range(len(A) - 1, -1, -1):
        for j in range(len(B) - 1, -1, -1):
            if A[i] == B[j]:
                memo[i][j] = memo[i + 1][j + 1] + 1
    return max(max(row) for row in memo)


# def Solve(array1:list, array2:list)-> int:
#     """
#     """
#     memo = [[None for i in range(len(array2)+1)] for j in range(len(array1) + 1)]
#     print("Memo: ")
#     res = recursive_find_max(array1, array2, 0, memo)
#     print(memo)
#     return res


# def recursive_find_max(
#         array1: list,
#         array2: list,
#         startingindex1: int, # the anchored index for array1.
#         memo # 2d array, size len(array1) by len(array2).
#         )-> int:
#     """
#     :param array1:
#     :param array2:
#     :param startingindex1:
#     :param startingindex2:
#     :param memo: let [i][j] be the maximum length of sub-sting starting at i index of array1, j index
#     of array2. 0<= i <= len(array1); 0<= j <=len(array2)
#     :return:
#     """
#     # special edge case.
#     if len(array1) == 1 or len(array2 = 1):
#         for element in array1:
#             if element in array2:
#                 return 1
#         return 0
#
#     i = startingindex1
#     if i == len(array1):
#         return max([max(r) for r in memo])
#
#     for j in range(len(array2)):
#         if array2[j] == array1[i]:
#             memo[i+1][j+1] = memo[i][j] + 1
#             continue
#         memo[i+1][j+1] = 0
#
#     return recursive_find_max(array1, array2, startingindex1 + 1, memo)


def main():
    pass
    # print("Testing the trivial caes: ")
    # testarray1 = [1,2,3,4]
    # testarray2 = [1,2,3,4]
    # print(Solve(testarray1, testarray2))
    # print("Testing another case")
    # testarray1 = [0, 3, 2, 1, 0]
    # testarray2 = [3, 2, 1, 0, 0]
    # print("Test array 1 = "+ str(testarray1))
    # print("Test array2 " + str(testarray2))
    # print(Solve(testarray1, testarray2))
    # print("Testing example leet code cases: ")
    # testarray1 = [1, 2, 3, 2, 1]
    # testarray2 = [3, 2, 1, 4, 7]
    # print("A: "+ str(testarray1) + " B: "+ str(testarray2))
    # print(Solve(testarray1, testarray2))




if __name__ == "__main__":
    main()