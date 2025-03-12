# 3rd game test
Unity version: 2021.3.44f1

Simple 3rd person game with object destruction

Used:
 - ScritableObjects
 - Object pooling
 - New input system
 - Sirenix
 - Threading
 - Cinemachine

# Control
The game supports both keyboard and gamepad controls.

Keyboard and mouse:
- W - move forward
- S - move backward
- A - move left
- D - move right
- ESC - pause/close current window or go back to previous one
- I - inventory
- Right mouse button - attack

Gamepad for example Xbox:
- Left analog - character movement
- Right analog - camera movement/looking around
- Y - inventory
- X - attack
- Start - pause/close current window or go back to previous

# Items
The player receives random items at the beginning of the game. Each of them affects the player's statistics. Items can be equipped to your character in the inventory window
![image](https://github.com/user-attachments/assets/e67791f1-f2c3-4b29-91d9-bbeb614719ec)
![image](https://github.com/user-attachments/assets/8723b9bc-bcb8-4bba-9f6c-7caa0db6dfc9)

# Objects on scene
Some items, as well as all characters, react to attacks by symbolizing how much life they have left and what damage they have taken.
![image](https://github.com/user-attachments/assets/735be4ef-57ce-41c5-b06e-d2ff56d05328)

Once an item destroyed, it can transform into other items to visualize its destruction.
![image](https://github.com/user-attachments/assets/d1717c76-ce31-46ec-8be4-60f95d425ffb)



