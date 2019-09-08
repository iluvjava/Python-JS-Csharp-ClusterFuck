def main():
    testarray = [1,2,3]
    GenerateSubsets(testarray)
    print("----------------------------")
    testarray = [2,2,2]
    GenerateSubsets(testarray)
    print("----------------------------")
    GenerateSubsets_NonUnique(testarray)
    print("----------------------------")
    testarray = [1, 2, 2, 2]
    GenerateSubsets_NonUnique(testarray)
    print("----------------------------")
    testarray = [1, 2, 2]
    GenerateSubsets_NonUnique(testarray)

    return


def GenerateSubsets(arr: list, idx: int = 0, stck: list = None):
    """
    This only works for array with all unique elements.
    :param arr: A sorted Array.
    :param idx:
    :param stck:
    :return:
    """
    if idx == len(arr):
        print(stck)
        return

    stck = [] if stck is None else stck
    stck.append(arr[idx])
    GenerateSubsets(arr, idx+1, stck)
    stck.pop()
    if arr[idx - 1] == arr[idx]:
        return
    GenerateSubsets(arr, idx+1, stck)
    return


def GenerateSubsets_NonUnique(arr: list, idx: int = 0, stck: list = None):
    if idx == len(arr):
        print(stck)
        return
    stck = [] if stck is None else stck
    LastAddedElement = None if len(stck) == 0 else stck[len(stck) - 1]
    stck.append(arr[idx])
    GenerateSubsets_NonUnique(arr, idx + 1, stck)
    if (LastAddedElement is not None) and (LastAddedElement == arr[idx]):
        stck.pop()
        return
    stck.pop()
    GenerateSubsets_NonUnique(arr, idx + 1, stck)
    return

if __name__ == "__main__":
    main()