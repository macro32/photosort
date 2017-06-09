# photosort
Walks a directory of jpg images and sorts them by date taken into a new time-based directory tree.

The problem was simple enough. Thousands of photographs, mainly in jpeg format (but that is another story) have been shunted around different machines and their file time-stamps got totally ruined as a valuable piece of data. They also seemed to magically move into directories which I am sure they were not really meant to be in, thanks to different photo and imaging editors, with their different import and management rules. The only constant and reliable source of information is the image metadata which includes the date that the photo was taken. This is usually correct (unless I forgot to reset the clock after changing the battery :)).

So, I want to wander through thousands of photos sitting in suspect directories and sort them into a new directory structure which maps each file to the day they were taken. 

Error handling is very basic. Mistakes are ignored. So any file which does not contain the appropriate metadata field, isn't a jpeg file, or decides to be otherwise uncooperative, is left behind to sulk. On my images this meant I had about 100 files to inspect by hand. This represented about 1% of the total. Not too bad for a first cut.

I can't take any credit for the code. Mr Google and stackoverflow contributors must be thanked for providing much of the useful bits. It would be deeply embarassing to have any sort of pretentious licence. The beerware license version is therefore most suitable.
