# APPLICATION NAME
Complete description and overview.  
This is where you describe in a brief, yet clear and concise, manner what your product should do and how you expect it should be used.  
Why did you write this application?  What purpose does it serve?  It's okay if the only purpose is to meet the homework requirement (but try to move bedyond just that - be creative!).

My Prodect should be able to take a picture and using the google api to guess what the picture is. At first my original choice for the application was to make a drawing and have the
google api guess what the drawing. However after countless hours I could not for the life of me get the drawing part to work nor the api. At the last min I suck in my pride and just 
chose to go with what everyone else was doing which was the taking a picture and having the api guess the picture. The purpose of the app is to recognize images the user takes.

## System Design 
This is where you specify all of the system's requirements.  This section should accurately portray the complete operation of your application.  
Provide scenarios, use cases, system requirements, and diagrams/screenshots of the system.

Needs a camera
Internet access for Google Vision API
Target: Android 7.1
Minimum: Android 5.0

## Usage
This is where you explain how to use your application

So at the moment, all my app does is that it just takes a picture and thats it. There a button titled "open camera". Once pressed, it will take the user into the camera, allowing them
to take a picture of whatever they want. Once the user taken the picture and selected "ok", the app then brings the user back to the main screen where the user will just see the button
"open camera" again. I'm not quite sure why the bitmap isn't showing up in the image box. I've trying searching it up but it seem everyone does it in a different way. I also tried just 
copying a new set of code from the example but it still wouldn't show the image. What is suppose to happen is that after the picture is selected and the app brings the user back to the 
main screen however this time the picture is suppose to display on the image box that was invisible but now isn't. Two new buttons, a yes and a no, becomes visible too allowing the user 
select either yes if the google api was correct or no the google api wasn't. After selecting either or buttons, a message of either "yes, we did it" or "damn....next time" will appear
causing the two buttons, yes and no, to once again to be invisible onces again.