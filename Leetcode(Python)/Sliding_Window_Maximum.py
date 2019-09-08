


def slide_window(array:list, windowsize: int) -> list:
    """
    Given an array, given a valid window size, for a non empty array, we sum
    the content in the window up and return the maximum of the window.
    :param array:
    :param windowsize:
    :return:
    """
    #Speical case if windows = len
    if len(array) <= 1 or windowsize == 1:
        return array
    if windowsize == len(array):
        return [max(array)]
    # Windows size strictly less than array length
    Result = []
    i = 0
    j = windowsize
    MaxSum = 0
    for ii in range(j):
        MaxSum= max(array[ii],MaxSum)
    Result.append(MaxSum)
    while j < len(array):
        MaxSum = max(array[j], MaxSum)
        j += 1
        i += 1
        Result.append(MaxSum)
    return Result

def add_to_sorted_list(array: list, element: int)-> list:
    pass


def main():
    testarray = [1,3,-1,-3,5,3,6,7]
    winsize = 3
    print(slide_window(testarray, winsize))

    return


if __name__ == "__main__":
    main()