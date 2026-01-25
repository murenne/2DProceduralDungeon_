# 2DProceduralDungeon

[English](./README.md) | [中文](./README_cn.md) | [日本語](./README_jp.md)

This is a lightweight and simple dungeon generator project.
It was the very first project I created when I started learning Unity.
I spent some time completely refactoring and open-sourcing it, making it suitable as a reference for beginners getting started with Unity.

* [2D Simple Dungeon Generator](#2DProceduralDungeon)
    * [Getting Started](#getting-started)
    * [Control](#control)
    * [Attack](#attack)
        * [Basic Attack](#basic-attack)
        * [Magic Attack](#magic-attack)
    * [Dialog](#dialog)
        * [Text](#text)
        * [Icon](#icon)
    * [Dungeon Generator](#dungeon-generator)
        * [Basic Room](#basic-room)
        * [Door](#door)
        * [Boss Room](#boss-room)
    * [Map](#map)
        * [Room position](#room-position)
        * [Player Position](#player-position)
    * [Camera](#camera)
    * [Status](#status)
    * [Level](#level)
    * [Item](#item)
        * [Golden Coin](#golden-coin)
        * [Herbs](#herbs)
        * [Bottle](#bottle)
    * [Enemy](#enemy)
        * [Bat](#bat)
        * [Fire ball](#fire-ball)
        * [Wizard](#wizard)
        * [Knight](#knight)
    * [UI](#ui)
        * [Player UI](#player-ui)
            * [HP bar](#hp-bar)
            * [Experience bar](#experience-bar)
            * [Items Count](#items-count)
        * [Enemy UI](#enemy-ui)
            * [HP bar](#hp-bar-1)
            * [Damage Number](#damage-number)
    * [Statement](#statement)
## Getting Started
1. This project is developed based on **Unity 2022.3.36f1**. Please ensure your Unity version is **2022.3.36f1** or higher.
2. Clone this project from GitHub to your specified folder and open it with Unity.
3. Click the **Play** button at the top of the Unity editor to run the project.

## Control
This project only supports **Keyboard & Mouse** control. Gamepads are not supported.

| Key | Description |
| :--- | :--- |
| `WASD` | Player Movement |
| `Left Mouse Button` | Basic Attack |
| `Right Mouse Button` | Magic Attack |
| `B` | Start Dialog |
| `Q` | Use Potion |
| `M` | Open Map |

---

## Attack
There are two types of attacks in this project: Basic Attack and Magic Attack.

![Attack](./README_Images/Attack.gif)

### Basic Attack
A melee attack that hits in a circle around the character. The attack angle is determined by the mouse position. It has no cooldown.

### Magic Attack
A ranged attack that covers the entire screen. The target location is determined by the mouse position. It has a cooldown.

---

## Dialog
The project features a simple dialog system that reads and plays content from `.txt` files and switches character portraits.

![Dialog](./README_Images/Dialog.gif)

### Text
Text content is stored in `.txt` files. A typewriter effect is achieved by controlling the playback speed.

### Icon
Icon data is also defined in the `.txt` files. The system switches character avatars based on the code provided in the text.

---

## Dungeon Generator
The project includes a simple dungeon generator that creates a total of 10 rooms.
It uses a **Random Walk Algorithm** and neighbor detection to construct rooms and walls. Pathfinding and filtering algorithms determine the location of the Boss room.

![Dungeon](./README_Images/Dungeon.png)

### Basic Room
Standard rooms are constructed using the Random Walk algorithm, making every generation unique.
Rooms contain floors, walls, openable doors, and a small number of normal enemies.
The type and number of enemies are randomly generated and vary per room.

### Door
When entering a room, all doors close automatically.
Once all enemies in the room are defeated, the doors open automatically.

### Boss Room
The Boss Room is the destination room, determined as the one furthest (in step distance) from the starting room.
In addition to the standard configuration, the Boss Room spawns a Boss enemy.

---

## Map
A simple minimap can be opened at any time inside the dungeon.
It displays visited rooms and the current player position. Unvisited rooms are hidden.

![Map](./README_Images/Map.gif)

### Room position
White squares represent the position of rooms.

### Player Position
A red dot represents the player's position.

---

## Camera
This is a top-down 2D game.
When the character enters a room, the camera locks onto that room.
The camera does not follow the character while they move within the room.
When the character moves to a new room, the camera automatically pans to the new room.
Camera shake effects occur on attacks or when taking damage.

![Camera](./README_Images/Camera.gif)

---

## Status
The character has four attributes. As the character levels up, these values gradually increase.

| Attribute | Explanation |
| :--- | :--- |
| **ATK** | Player Physical Attack |
| **DEF** | Player Physical Defense |
| **MATK** | Player Magic Attack |
| **MDEF** | Player Magic Defense |

---

## Level
The project includes a simple leveling module.
Defeating enemies grants experience points (XP). When the required XP is reached, the player levels up.
The XP required for each level varies.
Stat increases vary with each level up.

---

## Item
There are three types of items: Gold Coins, Herbs, and Potions.
Items are automatically picked up or used when the character overlaps with them.

![Item](./README_Images/Item.png)

### Golden Coin
Gold coins are simple drop items and cannot be used currently.

### Herbs
Herbs are healing items. They are automatically used upon pickup and restore a small amount of HP.

### Bottle
Bottles (Potions) are healing items. They are added to the inventory upon pickup and must be used manually (Press `Q`). They restore a large amount of HP.

---

## Enemy
There are four types of enemies: three normal enemies and one boss.
Each enemy has a unique attack pattern.

![Enemy](./README_Images/Enemy.gif)

### Bat
Automatically chases the player when they enter its tracking range.

### Fire ball
Automatically chases the player when they enter its tracking range. Periodically spits out a fireball that tracks the player.
The fireball is destroyed upon hitting the player or a wall. If it hits nothing, it disappears after a few seconds.

### Wizard
Unlike the other enemies, the Wizard moves randomly within the room.
When the player enters its tracking range, it chases the player and periodically spits out a tracking fireball.

### Knight
The Boss located in the final room.
It uses melee attacks.

---

## UI
The project includes simple UI configurations for both the player and enemies.

![UI](./README_Images/UI.png)

### Player UI
The Player UI consists of three parts: HP bar (top-left), XP and Magic Energy (left), and Item Count (top-right).

#### HP bar
Updates in real-time. If HP reaches 0, the player dies.

#### Experience bar
Updates in real-time. When full, the player levels up, and the bar resets.

#### Items Count
Displays the number of items currently held. The count decreases when items are used.

### Enemy UI
The Enemy UI consists of two parts: HP bar and Damage Numbers.

#### HP bar (Enemy)
Displays the enemy's current health when they take damage.

#### Damage Number
Displays the damage value when an enemy is hit. The number floats upwards and slowly fades away.

---

## Statement
The project runs normally as a whole.
However, due to limited time and energy, there may be bugs that I haven't discovered yet.
If you encounter any bugs, please submit an issue.
I will address them when I have time.
Thank you!