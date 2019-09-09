

def permutation_search(arr: list):
    if arr is None:
        return []
    if len(arr) == 0:
        return [[]]
    if len(arr) == 1:
        return [[arr[0]]]
    arr.sort()
    indexchosen = [False] * len(arr)
    permutations = [] * len(arr)
    res = []
    permutation_search_helper(arr, indexchosen, permutations, res)
    return res

def permutation_search_helper(arr: list, indexchosen: list, permutations: list, result:list = None):
    """

    :param arr: the array with elements we want to permutate, there could be duplicates.
    :param indexchosen: an array of Booleans
    to keep track of the elements that has been chosen during the recursive process.
    :param permutations: A queue that is the current permutations we are focusing on .
    :return:
    """
    if len(permutations) == len(arr):
        print(permutations)
        if result is not None:
            result.append(permutations[:])
        return
    LastChosenElement = None
    for i in range(len(arr)):
        ElementLookingAt = arr[i]
        if indexchosen[i]:
            continue
        if (LastChosenElement is not None) and (ElementLookingAt == LastChosenElement):
            continue
        indexchosen[i] = True
        LastChosenElement = ElementLookingAt
        permutations.append(arr[i])
        permutation_search_helper(arr, indexchosen, permutations, result)
        indexchosen[i] = False
        permutations.pop()
    return


def main():
    testlist = [3, 3, 0, 3]
    permutation_search(testlist)
    return

if __name__ == "__main__":
    main()