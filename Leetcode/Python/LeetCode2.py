class Solution:
    def combinationSum(self, candidates, target: int):
        candidates.sort()
        GrandList = []
        print("The Sorted List is: " + str(candidates))
        Solution.Try_To_Sum(candidates, 0, target, GrandList)
        return GrandList

    def Try_To_Sum(CandidatesList,
                   Index, Target, GrandSolutions, PartialSolution=None):
        """
            The array is sorted.
            For all elements in the array, element > 0
            We can use the elements repeatedly.

        """
        print("Index: "+ str(Index))
        print("Target: " + str(Target))
        print("GradSoltuion: "+ str(GrandSolutions))
        print("PartialSolution: "+ str(PartialSolution))
        if (Index >= len(CandidatesList)):
            return
        # Best base case:
        if (Target == 0):
            GrandSolutions.append(PartialSolution)
            return
        # Target Not reached:
        if (Target < 0):
            return
        # Element at current index is too large.

        if (CandidatesList[Index] > Target):
            return


        TheNumber = CandidatesList[Index]
        PartialSolution = (PartialSolution, [])[PartialSolution == None]
        Solution.Try_To_Sum(CandidatesList, Index + 1, Target, GrandSolutions, PartialSolution[:])
        PartialSolution.append(TheNumber)
        Solution.Try_To_Sum(CandidatesList, Index, Target - TheNumber, GrandSolutions, PartialSolution[:])
        Solution.Try_To_Sum(CandidatesList, Index + 1, Target - TheNumber, GrandSolutions, PartialSolution[:])

        return


def Recursive_Solution(SubArray, Target, GrandSolutions, PartialSolution= None):
    print("Subarray: "+ str(SubArray)+ " Target: "+ str(Target)+" PartialSol: "+ str(PartialSolution))

    TheNumber = SubArray[0];
    if(TheNumber > Target):
        return
    if(TheNumber == Target):
        PartialSolution.append(TheNumber)
        print("Sol: " + str(PartialSolution))
        return




    if(PartialSolution is None):
        PartialSolution = []

    # Choose this element but don't skip index
    PartialSolution.append(TheNumber)
    Recursive_Solution(SubArray, Target-TheNumber, GrandSolutions, PartialSolution[:])

    if(len(SubArray) > 1):
        # Choose this element and skip the index.
        Recursive_Solution(SubArray[1:], Target - TheNumber, GrandSolutions, PartialSolution[:])
        # Don't choose this element
        Recursive_Solution(SubArray[1:], Target, GrandSolutions, PartialSolution[:-1])
    return


def main():
    print("Main...")
    TestList = [2,3]
    TestList.sort()
    print("Sorted Testlist: "+ str(TestList))
    Recursive_Solution(TestList, 5, [])

if __name__ == "__main__":
    main()