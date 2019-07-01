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

def prepare_list():
    '''
        This method will randomly choose 10 files from the 
        static/resource/bunchofpdfs directory. 

        Return: 
            A list of file names. 
    '''
    mydir = scriptrootdir + "/static/resource/bunchofpdfs"
    allfiles = [
        f for f in os.listdir(mydir)]
    res = []
    for f in range(10):
        randomnum = random.randint(0, len(allfiles)-1)
        res.append(allfiles.pop(randomnum))
    return res

if __name__ == "__main__":
    print(prepare_list())