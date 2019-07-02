"""
    This python script prepares the content for the HTML template, 
    - specificwebsite.html 
"""
import os
import random
import sys


print("__name__ = " + __name__)
print("running under dir: "+ sys.path[0])
scriptrootdir = sys.path[0]

class BookShelves: 

    def __init__(self):
        mydir = scriptrootdir + "/static/resource/bunchofpdfs"
        self._AllBooks = [
            f for f in os.listdir(mydir)]
    
    def get_books(self):
        '''
            Return 10 random books from the list of books read from
            the folder. 
        '''
        res = []
        if (len(self._AllBooks) < 10):
            return None
        for f in range(10):
            randomnum = random.randint(0, len(self._AllBooks)-1)
            res.append(self._AllBooks.pop(randomnum))
        return res

Instance = BookShelves()


if __name__ == "__main__":
    print(Instance.get_books())