"""
    This python script prepares the content for the HTML template, 
    - specificwebsite.html 
"""
import os
import random
import sys

print("__name__ = " + __name__)
print("running under dir: " + sys.path[0])
ScriptRootDir = sys.path[0]


class BookShelves:
    DefaultDir = "/static/resource/bunchofpdfs"

    def __init__(self):
        mydir = ScriptRootDir + BookShelves.DefaultDir
        self._AllBooks = [];
        if (not os.path.exists(mydir)):
            return
        self._AllBooks = [
            f for f in os.listdir(mydir)]
        self._AllBooks.sort()
        print("BookShelves Ready: ")
        print(self._AllBooks)

    def get_books(self):
        '''
        :return
            A list containing of all the files names for books.
        '''
        return self._AllBooks

    def get_rootdir(self):
        """
        :return:
            A list containing the single absolute dir where all books are under,
            without "/" at the end of the path.
        """
        return ScriptRootDir + "/" + BookShelves.DefaultDir



class VideoShelves:
    """
    A class that prepares all the videos in the disk for post quick post response.
    - Scans the disk and make then into a dictionary.
    """
    VideoFileRootDir = "C:/Users/Administrator/Desktop/MLP Random Episodes"

    def __init__(self):
        self._AllVideos = []
        if (not os.path.exists(VideoShelves.VideoFileRootDir)):
            return
        self._AllVideos = [f for f in os.listdir(VideoShelves.VideoFileRootDir)]
        print("Videos List Is Ready")
        print(self._AllVideos)

    def get_filenames(self):
        """
        :return:
            A list of string represeting all the file names for videos. None is returned
            if the list is empty.
        """
        return self._AllVideos if len(self._AllVideos) != 0 else None

    def get_abs_rootdir(self):
        """
        :return:
            A root dir that represnting the dir for all the video files, without the "/" at the end.
        """
        return VideoShelves.VideoFileRootDir


# Field Constant
# --------------------------------------------------------------------------------------------------------------------

BooksShelveInstance = BookShelves()
VideoShelvesInstance = VideoShelves()
print("--------------------------ContentPreparation Finished----------------------------")

if __name__ == "__main__":
    pass
