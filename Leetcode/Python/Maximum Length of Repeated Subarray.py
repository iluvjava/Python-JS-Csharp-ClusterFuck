"""
This is a possible solution to leetcode problem Maximum Length of Reapted subarray.
A nested for loop solution might be better,
a recursive one will be cool for more than 2 array.

"""



def Solve(array1:list, array2:list)-> int:
    """
    """
    memo = [[None for i in range(len(array2)+1)] for j in range(len(array1) + 1)]
    print("Memo: ")
    res = recursive_find_max(array1, array2, 0, memo)
    print(memo)
    return res


def recursive_find_max(
        array1: list,
        array2: list,
        startingindex1: int, # the anchored index for array1.
        memo # 2d array, size len(array1) by len(array2).
        )-> int:
    """
    :param array1:
    :param array2:
    :param startingindex1:
    :param startingindex2:
    :param memo: let [i][j] be the maximum length of substing starting at i index of array1, j index
    of array2. 0<= i <= len(array1); 0<= j <=len(array2)
    :return:
    """
    # special edge case.
    if len(array1) == 1 or len(array2 = 1):
        for element in array1:
            if element in array2:
                return 1
        return 0

    i = startingindex1
    if i == len(array1):
        return max([max(r) for r in memo])

    for j in range(len(array2)):
        if array2[j] == array1[i]:
            memo[i+1][j+1] = memo[i][j] + 1
            continue
        memo[i+1][j+1] = 0

    return recursive_find_max(array1, array2, startingindex1 + 1, memo)


def main():
    print("Testing the trivial caes: ")
    testarray1 = [1,2,3,4]
    testarray2 = [1,2,3,4]
    print(Solve(testarray1, testarray2))
    print("Testing another case")
    testarray1 = [0, 3, 2, 1, 0]
    testarray2 = [3, 2, 1, 0, 0]
    print("Test array 1 = "+ str(testarray1))
    print("Test array2 " + str(testarray2))
    print(Solve(testarray1, testarray2))
    print("Testing example leet code cases: ")
    testarray1 = [1, 2, 3, 2, 1]
    testarray2 = [3, 2, 1, 4, 7]
    print("A: "+ str(testarray1) + " B: "+ str(testarray2))
    print(Solve(testarray1, testarray2))




if __name__ == "__main__":
    main()