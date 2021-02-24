<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Spooked-logo-b.png" width="50%">

# Documentation
All the goings-ons for the wackiest CTD Capstone Game ever!

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Characters-Posed.JPG" width="75%">

- - -

## 1/26/2021 - Week 7
### Goals:
- C - Creating a version without medium ghosts to playtest

- D - Adding a menu scene and playtesting

- Z - Adjust ghost models and add inside vehicle model


### Weekly Updates:

- - -

## 1/26/2021 - Week 6 Levels and Effects!
### Goals:
- C - Local/World Effect Development

- D - Implementing Levels and Connecting all the Stuffs

- Z - UI and Menu Sketches


### Weekly Updates:
C - We were able to fix almost all the major bugs in our level prototype this week. Additionally, we fixed the movement in medium and small ghosts to prevent them from getting stuck on edges and to have more natural wander and flee movement. We also implemented a separate mechanic to prevent ghosts from recombining immediately. You can see this mechanic implemented in the first image. Finally, we built the framework for triggering random world events on a successful scare with a custom event system. The various events still need to be implemented but as a test we had the lights turn off in the scene on a successful scare which can be seen in the second image.

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/ghost-separate.gif" width="50%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/random-event.gif" width="50%">

D - This week was a successful week because we were able to fully finish our first version of the prototype. This includes all ghosts types working, player skins and maps implemented, and most of the bugs squashed. We were able to playtest with some of the new levels and we tried brainstorming ideas for new features and how to fix the parts of the game that weren't working. Check out some gameplay below. Our next steps will be to playtest, playtest, playtest!

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Prototype-Feb23-FailScare.gif" width="50%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Prototype-Feb23-BigScare.gif" width="50%">

Z - Our current idea for the UI and menu for Spooked is that the entire thing exists within the team's ghost van. The menu sketch pictured below shows a simple view of the inside wall of the van with a map of the island, pictures of the characters, a laptop and miscelaneous items from the levels completed. The wall map with act as the navigation menu for the team probably with small indicators or icons that the each player can move about to show where they want to go. When all players are hovered over the same level location, they will probably be promted to go to that location on the laptop screen along with their previous progress on that level and achievements or whatever. The players will be able to change and choose their characters when selected into the photo wall on the left.There, they can switch photos to select their character which will appear with their player number or name or whatever on the polaroid-like photo frame. The settings window will be able to pop out from the top right and overlay things like sound settings, controls, save and quit.  

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/UISketch-Menu.jpg" width="75%">

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/UISketch-Layout.jpg" width="35%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/UISketch-Settings.jpg" width="35%">

- - -

## 1/26/2021 - Week 5 Bags, Buttons, Bugs and Battlefields
### Goals:
- C - Implementing specific button scare to medium and small ghosts

- D - Implement player animations and visual feedback

- Z - Blocking out all levels


### Weekly Updates:
C - We have implemented the multi button scare mechanic to the medium and small ghosts. It is currently setup so that a random sequence of 4 buttons is generated for a large ghost, 2 for a medium ghost, and 1 for a small ghost each requiring the respective number of players to scare it at once. Additionally, we have added more visual feedback to the scaring mechanic including a timer, indicators for when a button has been pressed, and indicators for whether the scare was succesful or not.

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/medium-ghost-multi-button.png" width="50%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/small-ghost-multi-button.png" width="47%">

D - This week I was able to implement a lot of new features. One of which is the grab bag that allows you to grab small ghosts and collect them for future deposit. The bag has a limit and displays the amount of ghosts you are carrying in the bag at one time. This need a little bit of bugfixing. Additionally, I implemented player stunned animations so that the player can tell when they are out of the fight. The last feature was level dynamics, like the van moving back and forth, and a few small visual details that allow the player to be more immersed in the world.


<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Animations%20and%20visuals.gif" width="75%">

Z - The next steps within our level development were to create simplified 'block' levels with the general feel and layout that we want without too much detail or nice assets. We have moved forward with 9 levels including: the Car Ferry (first level), Boat Docks, Pier, Bar, Roller Rink, Library, Enchanted Forest, Cave, and Mushroom Village levels. Below are images of each in their first iteration (sans ferry level since that has been the playtested level in previous posts) 


<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/LevelBlock-BoatDocks.JPG" width="50%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/LevelBlock-Pier.JPG" width="50%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/LevelBlock-Bar.JPG" width="50%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/LevelBlock-RollerRink.JPG" width="50%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/LevelBlock-Library.JPG" width="50%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/LevelBlock-EnchantedForest.JPG" width="50%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/LevelBlock-Cave.JPG" width="50%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/LevelBlock-MushroomVillage.JPG" width="50%">



- - -

## 1/26/2021 - Week 4 All the BUTTONS!
### Goals:
- C - Implementing group button scare

- D - Adding ghost interactions with models and more playtesting

- Z - Adding player and ghost animations


### Weekly Updates:

C - We have implemented our multi-button scaring prototype into the Ferry level. When a player gets close to a big ghost a random button sequence is generated and displayed above the ghost. Players must collectively press all the buttons above the ghost within a certain time period (1-3 seconds) in order to split the ghost. For example, if the sequence generated is X, B, A, B then one player must press X while another presses A while another two players press B. If too many of a single button are pressed or the buttons aren't pressed in time, the scare fails and players are blown back. The next step for multi-button scaring will be to add visual feedback for seeing which buttons have been pressed and how much time is left to scare.

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/playtest.gif" width="75%">

We also playtested our prototype again with four players to get some feedback on the revised group of mechanics. The updates include ghosts spawning in with location effects, big ghosts having 4 buttons that all players need to press simultaneously to scare tham, and players getting exploded back when they unsuccesfully scare a ghost. Our notes from the session are as follows

- Lots of chaos initially again!
- Mentioned that the ghost splitting mechanic is a bit like the slimes from Minecraft
- Good communication about which buttons they each were pressing to scare the big ghosts
- Ghost stun needs to be less frequent
- Probably need to adjust the waves of ghosts per level
- Can successfully scare the big ghost but if it is surrounded, it immediately recombines, which is a problem.
- Some medium ghosts cannot be scared???
- It's a little bit unclear when you are trying to scare certain ghosts
- Also difficult to differentiate between which ghost you are all trying to scare when they look the same
- Spamming buttons when a big ghost splits to split the medium ones too (since they get trapped) -> might be good to implement a ghost push so they separate themselves from the players 
- Being able to push the medium and small ghosts around is funny

This was a pretty chaotic and fun playtest and we will probably use these players again to try out later levels since the group chemistry is good.

D - We added skins to all the ghosts along with their running and attack animations. Not pictured is the changes to the level which include a reskinned car and upcoming level dynamics. Additionally, we will be including the ghost BAG tool in the upcoming weeks and will implement that alongside the additional animations that the characters will have.

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/SkinnedGhosts.gif" width="75%">

Z - We created a cleaner ghost character with a movement and stun animation to add to the level as well as 6 new characters who all use the same player rig and animations. Below are the ghost animations and screenshots of the characters. So far we have 3 people player characters as well as 4 wacky friendos like: Buff Bird, Fish Head, Roboi and Sweets. We can potentially add as many new characters as we can create models for since the rig and animations are all tied together which is fun. 

**Ghosties:** 

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Animation-Ghost-Move.gif" width="50%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Animation-Ghost-Stun.gif" width="50%">

**Characters:**

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Character-Person1.png" width="20%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Character-Person2.png" width="20%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Character-Person3.png" width="20%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Character-BuffBird.png" width="20%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Character-FishHead.png" width="20%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Character-Roboi.png" width="20%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Character-Sweets.png" width="20%">


- - -

## 1/26/2021 - Week 3 Playtesting and Refining
### Goals:
- C - Beginning the multi button group scare prototype

- D - Playtesting and refining/fixing the first level

- Z - Sketching out the rest of the levels & put them infront of people


### Weekly Updates:
C - This week we made a number of general bug fixes regarding ghost mechanics as well as started prototyping different multiplayer scaring options. Currently, to split a ghost players must all press the same button simultaneously while near the ghost. We wanted to test splitting mechanics involving pressing a number of different buttons among players. To do this we made a new scene and setup a ScareManager which tracks different players scares and which button they pressed. The picture below shows the very basic setup for testing this mechanic which involves displaying a randomized set of 4 button directions for players to push within a certain period of time.
<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Multiplayer_Button.png" width="50%">

Upon a successful scare, the background turns to green. On a failed scare, which happens when either the players don't press all the buttons fast enough or when a player presses the wrong button, the background turns red.
<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Multiplayer_Button_Success.png" width="50%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Multiplayer_Button Fail.png" width="50%">
With the ScareManager set up it will be easy to tweak different variables and test out different methods for multi-button scaring including combo button presses.

D - This week we did more prototyping and bug fixing. Specifically, we worked on adjusting the way that the ghosts spawned in and added a new feature to the game: the B.A.G. (Bust-A-Ghost) or potentially S.A.C (Spirit-Aprehension-Contraption). Currently, the player is able to pick it up and put it down but there are issues that need adressing for more interactions to take place. There is also the upgrading of the van, an in-game object that players will need to interact with to deposite the ghosts caught in the B.A.G/S.A.C.

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Documentation2-2.gif" width="75%">

We also playtested our prototype (sans B.A.G.) with two players to get some feedback on the current group of mechanics. This includes the ghosts spawning in as they are scared away, banishing the small ghosts instead of catching and depositing them, both players needing to hit a single scare button simultaneously to scare the big ghosts, and the big ghosts being able to stun the players who become inactive for a few seconds. Our notes from the session were as follows:

- Lots of chaos initially!
- The big ghost stunning might be happening too frequently/quickly, a swing into the stun and pause might be helpful to make them feel slower and not soooo hard to beat
- Breaking the big ghost for the first time and it splitting into medium ghosts is **very** satisfying
- The subsequent scare spamming to break the medium ghosts and scare away the small ones is kinda fun if it is localized
- Currently, if both players get stunned together the level ends... do we want this fail condition?
- Eventually the players began communicating about when one got stunned which was cool to see
- Medium and small ghost recombining mught be happening too quickly after the larger size splits apart
- Need to add a stunned animation when the players get hit so that it is clear what is happening
- Might be good to adjust the scare animation so it doesn't look like the players are humping the ghosts lol
- Communication about scaring the big ghosts is pretty low at the moment -> different and specific buttons to press might help (see Colin's prototype above)
- Small ghosts are a little buggy when trying to scare/catch (pretty sure about a few of the fixes)
- Need to implement showing where the ghsots are spawning before they poof into existence
- Need to fix the failed group scare reaction so that players are blown back a bit (currently not exploding anyone anywhere)
- Ferry map is a bit crowded at the moment, might be good to shrink the players and ghsots a tiny bit on this map

Z - Our goal is to have ~8-10 playable levels in the game. This number can be adjusted depending on the time it takes to develop and playtest each of them but as far as our preparation and timeline go, we should be on track at least through this part. The levels can be broken up into 3 categories based on their location/theme: coast, town, and wild areas, below is a hand drawn and general digital version of the haunted island (name pending) created on the dope site https://inkarnate.com/ . 

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Map-Sketch.jpg" width="50%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Spooked-Map.jpg" width="50%">

These levels will include some dynamic features that turn the static area into a strategic environment for the players to conquer. The current ideas for the town area include: 

- Library or Grocery Store with shifting shelves
<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/LevelSketch-Library.jpg" width="75%">

- Roller Rink / Arcade with a spinning floor
<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/LevelSketch-RollerRink.jpg" width="75%">

- Bar or Coffee Shop with re-arranging tables
<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/LevelSketch-Bar.jpg" width="75%">

The current ideas for the coastal area include:

- Car ferry with tilting deck
<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/LevelSketch-Ferry.jpg" width="75%">

- Boat docks with floating or drifting boats
<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/LevelSketch-Docks1.jpg" width="75%">

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/LevelSketch-Docks2.jpg" width="40%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/LevelSketch-Docks3.jpg" width="40%">

- Pier with TBD dynamics
<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/LevelSketch-Pier.jpg" width="75%">

- Lighthouse with spinning illumination (Sketches coming soon)

The current ideas for the wild area include:

- Mushroom town with growing and changing mushrooms
<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/LevelSketch-MushroomTown.jpg" width="75%">

- Enchanted forest with passing animals
<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/LevelSketch-EnchantedForest.jpg" width="75%">

- Cave with TBD dynamics
<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/LevelSketch-Cave.jpg" width="75%">


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
