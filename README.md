# PennSplat

## Penn Play Game Jam 2017
### by Connie Chang and Wanru Zhao

##### A game inspired by Splatoon.

Original idea:  
The idea is players form two teams and try to cover as much area with their team's color. Using mobile devices (only Android support so far), players can move around in the real world and leave behind a trail of color based on their GPS locations. The team with the most area covered wins.

Features Implemented:
- Networking. Android device can host a PC client.  
- Trail renders behind players  
- GPS location works without networking  
- Camera rotation based on device direction works without networking
- Button input for movement  
- Game timer of 20 seconds per game  
- Calculate percentage of image that is covered with player color  

Struggles:  
We had problems accessing the GPS coordinates of the device while also using Unity's Network Manager. The GPS location works when all the networking code is removed and a single player can move around with a colored trail. However, once the networking is added, the GPS location fails to update. Thus, we implemented buttons for movement.

We also had a hard time spawning players of different colors. The Network Manager only accepts one player character prefab which it spawns for all players. Online solutions say to create a custom Network Manager script that can accept multiple prefabs. We tried that, but it does not work. In fact, our character generation code is never executed for an unknown reason.

Resources:  
GPS Location Service: Google Go Maps - https://www.assetstore.unity3d.com/en/#!/content/78642  
