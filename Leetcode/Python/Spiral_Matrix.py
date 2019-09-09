def main():
    print("Testing vertial sprialing and horizontal spiraling. ")
    print(make_spiral_index(1, 3))
    print(make_spiral_index(3, 1))
    print(make_spiral_index(1, 1))
    print("2x2 spiral")
    print(make_spiral_index(2, 2))
    print("2x3 spiral")
    print(make_spiral_index(2, 3))
    print("3x3 spiral")
    print(make_spiral_index(3, 3))
    print("4x4 Spiral")
    print(make_spiral_index(4, 4))
    print("Trying to tranverse a matrix: ")
    m = [
        [1, 2, 3],
        [4, 5, 6],
        [7, 8, 9]
    ]
    print(spiral_matrix(m))
    m =[[1,2,3,4],[5,6,7,8],[9,10,11,12]]
    print(spiral_matrix(m))
    return

def spiral_matrix(matrix:list) -> list:
    if len(matrix) == 0:
        return None
    Width = len(matrix[0])
    Height = len(matrix)
    traversalindices = make_spiral_index(Width, Height)
    res = []
    for t in traversalindices:
        res.append(matrix[t[0]][t[1]])
    return res


def make_spiral_index(width:int, height:int, indexlist:list = None, offset = (0, 0)) -> list:
    """
        Make a list of tuples that represents the spirals of that size, a single layer.

    :param width:
    :param height:
    :return:
    """
    Result = (indexlist,[])[indexlist is None]
    XoffSet, YoffSet = offset[0], offset[1]
    # make the top edge
    for i in range(width - 1):
        Result.append((XoffSet, YoffSet + i))
    # make Right edge
    for i in range(height - 1):
        Result.append((XoffSet + i, YoffSet + width -1))
    if height == 1 or width == 1:
        if height == 1:
            Result.append((XoffSet, YoffSet + width - 1))
        else:
            Result.append((XoffSet+ height -1,YoffSet))
        return Result
    # make botton edge
    for i in range(width - 1):
        Result.append((XoffSet + height -1, YoffSet+ width -1 - i))
    # left edge:
    for i in range(height - 1):
        Result.append((height -1 -i + XoffSet, YoffSet))
    # Are there more inner layer left?
    if height > 2 and width > 2:
        make_spiral_index(width - 2, height - 2, Result, (XoffSet + 1, YoffSet + 1))
    return Result


if __name__ == "__main__":
    main()