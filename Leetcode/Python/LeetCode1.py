

def main():
    print("Testing looking for all tuple sums up to a certain number, without repeating elements. ")
    arr = [1,3,5,7,9, 2,4,6,8, 10, 8,10,14]
    print(find_all_tuplets_sumupto(arr, 15))
    return


def find_all_tuplets_sumupto(arr: list, target:int):
    Solutions = []
    arr.sort()
    Index1 = 0
    Index2 = len(arr) - 1
    AlreadyVisited = set()
    while(Index2 - Index1 > 0):
        if(arr[Index1] in AlreadyVisited):
            Index1 += 1
            continue
        sum = arr[Index1] + arr[Index2]
        if(sum > target):
            Index2 -= 1
            continue
        if(sum < target):
            Index1 += 1
            continue
        Solutions.append((arr[Index1], arr[Index2]))
        Index1 += 1
        Index2 -= 1
        AlreadyVisited.add(Index1)
    return Solutions









if __name__ == "__main__":
    main()