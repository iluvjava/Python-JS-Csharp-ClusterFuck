"""
    This python script prepares the content for the HTML template, 
    - specificwebsite.html 
"""
import os
import random
import sys

print("__name__ = " + __name__)
print("running under dir: " + sys.path[0])
scriptrootdir = sys.path[0]


class BookShelves:

    def __init__(self):
        mydir = scriptrootdir + "/static/resource/bunchofpdfs"
        self._AllBooks = [];
        if(not os.path.exists(mydir)):
            return
        self._AllBooks = [
            f for f in os.listdir(mydir)]
        self._AllBooks.sort()

    def get_books(self):
        '''
           Return all books in the folder in alphebetical order. 
        '''
        return self._AllBooks


Instance = BookShelves()

if __name__ == "__main__":
    print(Instance.get_books())
