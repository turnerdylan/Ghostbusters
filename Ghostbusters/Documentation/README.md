<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Spooked-logo-b.png" width="50%">

# Documentation
All the goings-ons for the wackiest CTD Capstone Game ever!

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/ghost-chase.gif" width="75%">

- - -
## 1/26/2021 - Week 3
### Goals:
- C - Beginning the multi button group scare prototype

- D - Playtesting and refining/fixing the first level

- Z - Sketching out the rest of the levels & put them infront of people


### Weekly Updates:
C - 

D - 

Z - Our goal is to have ~8-10 playable levels in the game. This number can be adjusted depending on the time it takes to develop and playtest each of them but as far as our preparation and timeline go, we should be on track at least through this part. The levels can be broken up into 3 categories based on their location/theme: coast, town, and wild areas, below is a hand drawn and general digital version of the haunted island (name pending) created on the dope site https://inkarnate.com/ . 

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Spooked-Map.jpg" width="50%"> <img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Map-Sketch.jpg" width="50%"> 

These levels will include some dynamic features that turn the static area into a strategic environment for the players to conquer. The current ideas for the town area include: 

- Library or Grocery Store with shifting shelves
<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/LevelSketch-Library.jpg" width="100%">
- Roller Rink / Arcade with a spinning floor
<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/LevelSketch-RollerRink.jpg" width="100%">
- Bar or Coffee Shop with re-arranging tables
<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/LevelSketch-Bar.jpg" width="100%">

The current ideas for the coastal area include:

- Car ferry with tilting deck
<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/LevelSketch-Ferry.jpg" width="50%">
- Boat docks with floating or drifting boats
<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/LevelSketch-Docks1.jpg" width="100%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/LevelSketch-Docks2.jpg" width="50%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/LevelSketch-Docks3.jpg" width="100%">
- Pier with TBD dynamics
<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/LevelSketch-Pier.jpg" width="100%">
- Lighthouse with spinning illumination (Sketches coming soon)

The current ideas for the wild area include:

- Mushroom town with growing and changing mushrooms
<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/LevelSketch-MushroomTown.jpg" width="100%">
- Enchanted forest with passing animals
<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/LevelSketch-EnchantedForest.jpg" width="100%">
- Cave with TBD dynamics
<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/LevelSketch-Cave.jpg" width="100%">


- - - 

## 1/26/2021 - Week 2 A Functional Prototype!
### Goals:
- C - Multiplayer scaring

- D - Ghost spawning throughout the level (time and space)

- Z - A Character Animation up and running in Unity


### Weekly Updates:
C - In line with our goal to make our game cooperative, we've added a multiplayer scaring functionality which makes it so players must work together to split apart ghosts. On a successful scare, meaning all players have activated their scare simultaneously while also being in range of the ghost, the ghost will split apart. On a scare failure, the ghost applies a force to all the players and sends them flying back. In the future we plan on creating a number of events that will be triggered on a scare success which we hope will add some more chaos to the game.

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/multiplayer-scare.gif" width="75%">

D - This is a look at a functional countdown, level timer, and ghost wave spawning. 
The timer and countdown can be stylized in the future to match the theme of the game/levels but is currently working and adjustable per level. 
The waves of ghosts are able to be customized per level across number, frequency, and difficulty of ghosts. This will allow us to control the variability and dificulty within each level we design and scale them according to the organization of our larger narrative.

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Waves Gif.gif" width="75%">

Z - Here are a few of the basic animations for our first rigged character. We might change to a different theme with our player characters, not necessarily sticking to just humans, but for the time being we wanted to develop them along the way and add player animations to the prototype. Below are our first passes at an idle, movement, and scare animation with this first character. Now that the process of creating the first characters, rigging them and adding animations is complete (at least initially), we can continue to add animations and new characters pretty easily. We have plans for a variety of other animations we might like to add including a stunned state, a scared state and maybe a jump. 

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Animation-Idle.gif" width="50%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Animation-Run.gif" width="50%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Animation-Scare.gif" width="50%">

- - -

## 1/19/2021 - Week 1 Prototype Preparation 

The first (mostly) full prototype for Spooked! involves a blocked out version of our first level, the car ferry to the haunted island! We have cylinder block characters who can spawn in and scare capsule ghosts who will split into smaller and smaller ghosts, eventually disappearing. 

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Week1.gif" width="75%">

- - -

## Project Timeline Goals

30% | 50% | 90% | 100%
------------ | ------------- | ------------- | ------------- 
If our game is a house, this is the “foundation” | The “structure” | The “walls, floors, and ceilings” | The "furniture and decoration"
Core game-loop completed | Mechanics polished and feeling good | Mechanics bug free within specific level dynamics | Final bug fixes
Sketching out levels | Levels blocked out | Filling out levels with content | Final little level details
Playtesting game-loop | Playtesting levels | Testing out game sound and UI | Publish Itch and Potentially Steam game pages


- - - 

## FA20 - Previous Testing

### Unity Prototypes

Our game mechanic test revolved around prototyping specific ghost catching player mechanics within Unity. This was further tested by running a few simple playtests to prototype certain mechanics and features of the game. The gif to the left demonstrates a scene in Unity that we have used for prototyping mechanics. This was a test to see if the base mechanic of chasing ghosts around was any fun and if it could be executed in a straightforward, scalable, and efficient manner.

We have multiple scenes in Unity that we have been using as prototyping spaces and where we can write various scripts for testing our mechanics. The gif to the right demonstrates a scene in Unity that we have used for prototyping mechanics. So far we have the ability to have multiple players at once and the ability to catch and trap enemies as well as various “weapons” for the players to use including an electric fence that both players must place together.

From these tests, we decided to pivot towards the players scaring the ghosts through different stages and catching them once they were vulnerable. Chasing them around and having many players in the scene at once proved to be quite fun and align with our player experience goals. 

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/ghost-catch-prototype.gif" width="48.5%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/ghost-movement-prototype.gif" width="51.5%">

Our next line of testing involved trying out a few new forms of ghost movement, and scaring the ghosts to defeat them. In the prototype below, the player are pushed backwards when only one of them scares a ghost. In a successful scare, both players contribute and the ghost turns red. We also wanted to try out invisibility with the ghosts phasing in and out as the players try to scare them.

<p align="center">
  <img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/scare-prototype.gif" width="75%">
</p>

### Style Research 

With the limited time of our team, we wanted to decide on a uniform design style for our 3D world so that we could all contribute assets to the development if need be. A sleek low poly style seemed feasible for our situation so we looked into a bunch of different games that include beautiful low poly design for inspiration. Below are a few with shots from each game.

_**Astroneer**_
<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Astroneer.png" width="100%">

_**For the King**_
<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/FortheKing.png" width="100%">

_**Lara Croft GO**_
<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/LCGo.png" width="100%">

_**Necropolis**_
<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Necropolis.png" width="100%">

_**Super Hot**_
<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/SuperHot.png" width="100%">

_**Grow Up**_
<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/GrowUp.png" width="100%">


### Low Poly Prototypes
After deciding to pursue a low poly style for our assets, we moved forward in designing a few different characters, buildings, and scenes to try out the entire design flow for making these models. We are using Blender 2.9 for creating our 3D models with the goal of only using our own assets within this project. 

Some initial character ideas, a bread guy and a rolling cactus plant.

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Character-Bread.png" width="50%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Character-Cactus.png" width="50%">

A few buildings that could exist in different places on our haunted island, a general building in town and a mushroom cottage for the enchanted forest.

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/House-Blue.png" width="50%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/House-Mush.png" width="50%">

One idea for the feel of a library level.

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Scene-Library.png" width="50%">

- - -

## FA20 - Precedents Work
### Overcooked! 2
Our main takeaway from the Overcooked games is their combination of chaos and player flow. A game with good flow constantly has the player at the brink of failure so that when they do succeed they experience a great sense of accomplishment. Overcooked does this very well and it is something we want to strive for in our own game. Our game obviously has a different theme and we would like to link the chaos in our levels to that theme by having the ghost scaring activate effects and introduce a whole new sandbox for player fun experiences.

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Overcooked2.gif" width="75%">


### Luigi's Mansion 3
The Luigi’s Mansion series is a wonderful example of a fun, spooky, non-horror themed ghost game. The ghost hunting mechanics in LM3 involve a whole series of contraptions including a vacuum (for ghosts and also non-ghost sucking), a punger, a flashlight for stunning, a wind gust whoosh jump and a Luigi Doppelganger made out of goo named Gooigi. This allows for a huge range of ghost catching combinations. We want to emulate some of this variability but in a much simpler structure. We also want to create more dynamic ghost interactions, involving multiple players and key cooperation.

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/luigisMansion3.gif" width="75%">
