
def main():
    testlist =  [2,5,2,1,2]
    print(find_all_sum_up_to(testlist, 5))
    return


def find_all_sum_up_to(elements: list, target: int)-> list:
    elements.sort()
    res = []
    find_all_combinations_sum_up_to(elements, target, res)
    return res


def find_all_combinations_sum_up_to(
        elements: list, # a list of candidates elements, assume repetitions, assume list if sorted.
        targetsum: int,  # The target we want to sum up to.
        solutionsbucket,  # a list of combinations
        choices: list = None,  # A combination of the list of elements.
        startingindex: int = 0  # which part of the array we are looking at.
):

    if targetsum == 0 and choices is not None:
        solutionsbucket.append(choices)
        return

    # For each of the elements in the list beyond index i, we make choices on it.
    for i in range(startingindex, len(elements)):
        # Skip repeated elements:
        if i != startingindex and elements[i] == elements[i-1]:
            continue

        # Sorted array check if element too big for sum.
        n = elements[i]
        if n > targetsum:
            break;

        # make this choice and recur:
        choices = [] if choices is None else choices;
        find_all_combinations_sum_up_to(
            elements,
            targetsum - n,
            solutionsbucket,
            choices + [n],
            i + 1
        )
    return

if(__name__ == "__main__"):
    main()