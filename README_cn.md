# 2DProceduralDungeon

[English](./README.md) | [中文](./README_cn.md) | [日本語](./README_jp.md)

这是一个轻量的简易地牢生成器项目
是我最开始学Unity时自己制作的第一个项目
我花了一些时间把它完全重构并开源，适合新手入门Unity时作为参考

<!--ts-->
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
        * [herbs](#herbs)
        * [Bottle](#bottle)
    * [Enemy](#enemy)
        * [bat](#bat)
        * [fire ball](#fire-ball)
        * [witcher](#witcher)
        * [Knight](#knight)
    * [UI](#ui)
        * [player UI](#player-ui)
            * [HP bar](#hp-bar)
            * [Experience bar](#experience-bar)
            * [Items Count](#items-count)
        * [Enemy UI](#enemy-ui)
            * [HP bar](#hp-bar2)
            * [Damage Number](#damage-number)
    * [Statement](#statement)
<!--te-->

## Getting Started
1. 该项目基于Unity 2022.3.36f1开发，请确保你的Unity版本为2022.3.36f1及以上
2. 从Github拷贝该项目至你指定的文件夹，用Unity打开
3. 点击Unity上面的开始按钮，即可运行该项目

## Control
该项目只支持键鼠控制，不支持手柄操作

| 按键      | 说明                     |
| ----------| ------------------------ |
| `WASD`       | 玩家移动                |
| `鼠标左键`       | 玩家普通攻击                |
| `鼠标右键`       | 玩家魔法攻击                |
| `B`       | 开启对话                |
| `Q`       | 使用回复药            |
| `M`       | 打开地图            |

---

## Attack
该项目中有两种攻击方式，普通攻击和魔法攻击

![Attack](./README_Images/Attack.gif)

### Basic Attack
普通攻击为近战攻击，范围为环绕人物一周，攻击角度依据鼠标位置来判断，没有冷却时间

### Magic Attack
魔法攻击为远程攻击，范围为整个屏幕，攻击地点依据鼠标位置来判断，有冷却时间

---

## Dialog
该项目有一个简易的对话系统，可根据txt文件的内容读取并播放对话内容，并切换对话人物的头像

![Dialog](./README_Images/Dialog.gif)

### Text
文本内容位于txt文件内，通过控制速度实现了类似打字机的效果

### Icon
图标内容位于txt文件内，通过控制人物的代码实现了切换头像的功能

---

## Dungeon Generator
该项目有一个简易的地牢生成器，一共十个房间
利用随机游走算法和领居检测来构建房间和墙壁，寻路和筛选算法确定终点房间

![Dungeon](./README_Images/Dungeon.png)

### Basic Room
普通房间为根据随机游走算法构成，每次生成的房间并不一样
房间有地面，墙体，可开关的门，以及少量普通敌人
敌人的类型和数量为随机生成，每个房间并不一致

### Door
进入房间时，该房间的所有门会自动关闭
当消灭了该房间的所有敌人后，该房间的门会自动打开

### Boss Room
Boss房间为终点房间，是距离起始房间(步长)最远的一个房间
Boss房间除了普通房间的配置外，还会额外生成一个boss

---

## Map
进入地牢后可随时打开简易地图
简易地图上可以观察当前已经走过的房间和目前玩家的位置
简易地图不会显示未走过的房间

![Map](./README_Images/Map.gif)

### Room position
通过白色的方块来表示房间的位置

### Player Position
通过红点来表示玩家的位置

---

## Camera
该项目是一个2D俯视角的游戏
当人物进入房间后，相机会固定在该房间上
人物在该房间内移动时，相机不会跟随人物移动
当人物移动到新房间后，相机会自动跟随至该房间
人物的攻击/受伤，均会有相机震动效果

![Camera](./README_Images/Camera.gif)

---

## Status
人物有四种属性，随着升级，属性值会逐渐增加

| 属性名     | 解释                     |
| ----------| ------------------------ |
| ATK       | 玩家攻击力                |
| DEF       | 玩家防御力                |
| ATK       | 玩家魔法攻击力            |
| ATK       | 玩家魔法防御力            |

---

## Level
该项目有一个简易的升级模块
击败的敌人会获得经验值，当经验值打到要求后会升级
每一级所需的经验值也不一样
每次升级，各属性新增的值不一样

---

## Item
该项目有三种物品，分别为金币，草药，回复药
当人物位置和物品位置重合时，会自动拾取活自动使用物品

![Item](./README_Images/Item.png)

### Golden Coin
金币为单纯的掉落物品，无法使用

### herbs
草药为回复生命的道具，拾取后会自动使用，能回复少量生命值

### Bottle
回复药为回复生命的道具，拾取后需要手动使用，能回复大量生命值

---

## Enemy
该项目有四种敌人，
包括三种普通敌人和一个boss
每种敌人的攻击方式都不一样

![Enemy](./README_Images/Enemy.gif)

### bat
当玩家进入蝙蝠的追踪范围中时，它会自动追踪玩家

### fire ball
当玩家进入火球怪的追踪范围中时，火球怪会自动追踪玩家，并每隔一段时间吐出一个会自动追踪玩家火球
火球会在碰到玩家或者墙壁时消亡，什么都每碰到的话几秒钟内会自动消亡

### witcher
不同于上面的两种敌人，巫师会随机在房间内移动
当玩家进入巫师的追踪范围中时，巫师会自动追踪玩家，并每隔一段时间吐出一个会自动追踪玩家火球

### Knight
存在于最后一个房间的boss
攻击方式为近战攻击

---

## UI
该项目有一些简易的UI配置，包括玩家UI和敌人UI

![UI](./README_Images/UI.png)

### player UI
玩家UI主要分为3部分，分别为左上角的血量UI，左侧的经验值和魔法能量UI，右上角的物品数量UI

#### HP bar
玩家血量UI会实时更新玩家血量，血量为0时即玩家死亡

#### Experience bar
经验条UI会实时更新玩家经验，当到达该等级需要的经验值后会进行升级，并重置经验条

#### Items Count
道具数量UI会实时显示玩家身上有多少道具
使用道具后会相应的减少数量

### Enemy UI
敌人UI主要分为2部分，分别为敌人的血条UI和受到的伤害数值UI

#### HP bar
敌人的血条UI会在敌人受伤时显示相应的血量

#### Damage Number
受到的伤害数值UI会在敌人受伤时显示相应的伤害值，该数字会向上飘动并一段距离并缓慢消失

---

## Statement
该项目整体能正常运行
但因为时间精力有限，也许会存在一些我没发现的bug，
如果遇到bug，请提交issues
我会在有空的时候处理
谢谢