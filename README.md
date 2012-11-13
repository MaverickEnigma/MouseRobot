MouseRobot
==========
This specific MouseBot project utilizes the existing program MouseRobot to more efficently use the web-crawler Teleport Pro.  When Running MouseBot, select the MouseRobot Action you wish to use,
then every ten seconds the MouseBot will activate that program.  This will soon be hard coded to the simple Click Pause Program I have created, simply titled PP (Pause/Play).
If MouseBot detects that there is no more information coming into the file, typically Untitled in the users Documents Folder,(note that this is currently hard coded so it is necessary the name remain unchanged, options will be
added but the folder will need to be created by the user after they name it using Teleport Pro), then it will pause Teleport Pro, for three seconds, allowing en route data to hopefully finish and start again.  
If the folder remains unchanged, then the program pauses again, incrementing the wait time.  MouseBot also writes to a hard coded location, to be changed later, what the time interval was that the program was crawling at,
how long the program paused for, and how many bytes of data were recieved(bytes will be inaccurate as it notes all data recieved by all open network ports, since I am currently unable to track a specific process).
The program cannot currently tell the difference between being locked out and being bottle-necked (bottle-necked may be inaccurate as the only indication is that all crawl threads are active, but no data is being recieved).


MouseRobot