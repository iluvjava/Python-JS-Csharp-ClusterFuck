

import time as time



def Find_All_4SumUpToZero(elements: list, target = 0):
    elements.sort()
    print("Sorted Array: " + str(elements))
    Res = []
    find_recursiveSolution(elements, 0, target, Res)
    return Res


def find_recursiveSolution(
        Elements:list, # The list of elements
        StartingIndex : int, # The portion of the list we are looking at, inclusive!
        Target:int, # The numbers we try to sum up to.
        SolutionBucket, # List of all the smaller subset with the same size.
        PartialSolution = None,
        RemainingChoices = 4  # How elements we left to choose, of the recusrive level.
):

    """
        Functions looks for all the subsorted list of the elements list, skipping repeated sebsetlist.
    :param Elements:
    :param StartingIndex:
    :param Target:
    :param SolutionBucket:
    :param PartialSolution:
    :param RemainingChoices:
    :return:
    """
    # print("Partial Solution: "+ str(PartialSolution))
    if(RemainingChoices == 0):
        if(Target == 0):
            SolutionBucket.append(PartialSolution)
        return

    PartialSolution = (PartialSolution, [])[PartialSolution is None]

    flag = False

    if (RemainingChoices == 2):
        TupleList = find_all_tuples_sumto(Elements[StartingIndex:], Target)
        if(len(TupleList) == 0):
            return
        for Tuple in TupleList:
            SolutionBucket.append(PartialSolution+ Tuple)
        return
    for i in range(StartingIndex, len(Elements) - RemainingChoices + 1):
        if(flag and Elements[i-1] == Elements[i]):
            continue
        flag = True
        n = Elements[i] # The number jth number we chose
        if(n > Target and n > 0):
            # All elements beyond it will be too large to sum up to
            break
        find_recursiveSolution(Elements,
                               i + 1,
                               Target - n,
                               SolutionBucket,
                               PartialSolution + [n],
                               RemainingChoices - 1)
    return

def find_all_tuples_sumto(arr: list, target:int):
    Solutions = []
    Index1 = 0
    Index2 = len(arr) - 1
    while(Index2 - Index1 > 0):
        if (Index1 >= 1 and arr[Index1] == arr[Index1 - 1]):
            Index1 += 1
            continue
        sum = arr[Index1] + arr[Index2]
        if(sum > target):
            Index2 -= 1
            continue
        if(sum < target):
            Index1 += 1
            continue
        Solutions.append([arr[Index1], arr[Index2]])
        Index1 += 1
        Index2 -= 1
    return Solutions

def main():
    print("Main...")
    testarray = [-3,-2,-1,0,0,1,2,3]
    print(Find_All_4SumUpToZero(testarray, 0))
    return

if __name__ == "__main__":
    main()