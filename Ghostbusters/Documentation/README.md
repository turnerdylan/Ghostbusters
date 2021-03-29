<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Spooked-logo-b.png" width="50%">

# Documentation
All the goings-ons for the wackiest CTD Capstone Game ever!

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/GroupPhoto-Reduced.jpg" width="75%">

- - -

## 3/30/26/2021 - Week 10
### Goals:
- C - Ghost behavior & attack updates

- D - Ghost Ability implementation

- Z - Level content & lighting and post-processing


### Weekly Updates:

C - 

D - 

Z - This week turned into a huge work week for the development of both the ghost mechanics and the visual presentation of the game. We will probably end up with ~7-8 levels over all and each of those have been filled out with custom assets, nighttime lighting and some basic post-processing effects. Below are some images of the levels as they currently look. These levels may be adjusted slightly over the coming weeks to fix any bugs and allow the players to traverse the area without getting caught on things. 

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Ferry-NightMode.JPG" width="50%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Level2-NightMode.JPG" width="50%">
<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Level3-NightMode.JPG" width="50%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Level4-NightMode.JPG" width="50%">
<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Level5-NightMode.JPG" width="50%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Level6-NightMode.JPG" width="50%">
<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Level7-NightMode.JPG" width="50%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Level8-NightMode.JPG" width="50%">
<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Menu-NightMode.JPG" width="50%">

- - -

## 3/23/26/2021 - Week 10
### Goals:
- C - Ghost behavior updates

- D - Menu and tutorial development

- Z - Level development

- Group - Playtest, playtest, playtest!


### Weekly Updates:
Group - We have been playtesting to get feedback on different element of the game and boy do we have notes! From our first playtest this weeks we learned a lot of things and here are our key takeaways: 

- Players have a TON of fun chasing the little ghosts around the level, especially when there are LOTS of little ghosts around
- The big ghost 'event' (in this case super speed) worked GREAT right as the big ghost splits and definitely added chaos
- The peek-a-boo ghosts need some help being more engaging and clearer - visual feedback should help but mechanically stuff might need to change
- The button interaction especially for the 4 button big ghosts needs an overhaul UI-wise as it is NOT intuitive right now
- Related, players need feedback on WHO has scared a particular ghost already in the 4 button interactions
- Seeing players drop off ghosts at the van worked pretty well, escpecially lots of communication right at the end of the level
- We need to add a border or respawn so that when players walk off the screen they aren't gone for good

This was a very insightful playtest that has helped us figure out if certain things are working and how to rethink some of the moving parts in our game. More playtests and progress coming soon!

C - We took some of the feedback we received from our playtests this week to redesign some mechanics and also incorporate some more visual feedback. We've added an animation to the peekaboo ghost that has it move up and down from the ground as it moves to new locations to give it a feeling of poking its head out and peeking the player. We've also added a particle trail that indicates where the ghost is moving when it goes underground, this allows players to more easily cooperate to summon a ghost.

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/peekaboo-animation.gif" width="75%">

Another visual change that we implemented was to replace the big ghosts with ghosts of the same size as the medium but golden in color. The idea behind this was to avoid the confusion players were having between big and medium ghosts and to give the ghost a greater sense of importance or value so as to encourage the players to work together to scare it. We've also redone the button system to better show players which button they need to press and which buttons have already been pressed, regardless of the controller type being used. We now display a four button pattern above the ghost which imitates the four button layout on most controllers and display a silhouette icon to indicate that that button needs to be pressed. When a player succesfully presses one of the required buttons an icon of their character replaces the silhouette to indicate both that the button was pressed and who pressed it. We still have it set up so that a golden ghost(previously big ghost) requires four players and a regular ghost requires two players.

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/new-buttons.gif" width="75%">


D - This week I spend most of my time finishing the menu for the game. We now have an intro scene, a menu scene with character select, level select, and settings. There is also the start of some sort of tutorial development but that is on pause until we finish game mechanics.

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/MenuCapture.gif" width="75%">



Z - After changing up the mechanics and doing more playtesting, we realized that the level design needed to follow a more open layout concept. We dug into this idea and created a new set of level models that we have been using for our most recent playtests. Below are some images of what these more block based levels look like. Our goal is still to have a variety of levels (probably 8 or so) for the story and then one introduction/tutorial level (probably the ferry level that has been posted on here before). 

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/BlockLevel2.JPG" width="50%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/BlockLevel3.JPG" width="50%">
<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/BlockLevel4.JPG" width="50%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/BlockLevel5.JPG" width="50%">
<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/BlockLevel6.JPG" width="50%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/BlockLevel7.JPG" width="50%">
<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/BlockLevel8.JPG" width="50%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/BlockLevel9.JPG" width="50%">


- - -

## 3/16/26/2021 - Week 9 [Ch-ch-ch-ch-changes!](https://www.youtube.com/watch?v=pl3vxEudif8&ab_channel=ArthurQuintard)
### Goals:
- C - Tower mechanics updates and ghost button sequence adjustments

- D - Bag changes and Peek-a-Boo ghost updates

- Z - Menu scene development 


### Weekly Updates:
C - At the start of the week we decided to adjust the mechanics for the towers to better fit the interaction loop we were looking for. We changed it so there was a button to activate the towers and any number of towers could be activated at once with each tower freezing only the ghosts within its quadrant. 

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/tower-button.png" width="75%">

However after playtesting and receiving feedback we decided to move away from the tower idea as it ultimately did not achieve what we wanted to fix intitially. After discussing a few options we decided to strip our prototype of any unnecessary features in favor of a more simplified game loop. Our current idea is to have players able to pick up as many small ghosts as they want without needing to hold a bag. In order to catch small ghosts players must simply dive towards them. If players are hit by a big ghost than they drop all the ghosts they are holding which incentivizes dropping ghosts off at the van. 

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/drop-ghosts.gif" width="75%">

Another feature we added was a limited lifetime on the small ghosts so that after a certain period of time they flash red and disappear.

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/ghost-flashing.gif" width="75%">

D - This week we spent a lot of time redesigning our game. I updated the peekaboo ghosts so that they only run away when you get near them. We decided to get rid of the bag entirely and replace it with player-attached backpacks, because the bag was too slow. I also spent some time redesigning the main menu so that we could get a character select screen working. Check it out below!

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/MenuSceneUpdates.gif" width="75%">


Z - I mostly worked on getting some more modeling and menu things completed this week, starting with the low poly overview of the island. We wanted it to look like a simplified bird's eye view of the island including all of the areas that our levels are taking place in. A version of this map will exist in the menu for Spooked next to the character selection.

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/SpookedMap-LowPoly.JPG" width="75%">

For our character selection, we wanted players to easily be able to choose and swap their characters before entering a level. Our idea that the menu exists inside of the Spooked team van extends to the character selection as well. I took some renders of the characters and turned them into stylized polaroid like photos that will hand on the wall inside the van and swap to whichever character you want to play as. Below are the character portraits. 

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Polaroid-Yellow.png" width="25%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Polaroid-FishHead.png" width="25%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Polaroid-BuffBird.png" width="25%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Polaroid-Roboi.png" width="25%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Polaroid-Sweets.png" width="25%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Polaroid-Blondie.png" width="25%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/Polaroid-RedShirt.png" width="25%">


- - -

## 3/9/26/2021 - Week 7 & 8: Towers and 
### Goals:
- C - Updating ghost movement, adding player scare counter and implementing EMP tower prototype

- D - Adding a menu scene, implementing scare animations, creating a peak-a-boo ghost prototype & implementing player health

- Z - Modeling and rigging and animating more ghosts and modeling the menu inside of the van and EMP towers


### Weekly Updates:
Group - Our team was impacted by Covid-19 in week 7 so the updates for this section are a compilation of the progress and changes from week 7 and 8. After playtesting the game flow we had up to week 7 a bit, we realized that the game needed a few more elements to help the interactions have more dept and add to the cooperation and chaos we were looking for. 

One problem we saw was that the players have no incentive to worry about an errant medium or small ghost on the map and that the focus had become about trying to scare a big ghost in the level by breaking it down from there as a group. This is what we had wanted initially, but recently realized the gameplay felt too streamlined a process where players attention was *very* centralized. From here, we back up a bit and thought through the interactions we had created so far, and then new ideas we could add to it. After a bunch of ideation and talking through some of our new and old ideas, we decided to switch up the flow of scaring ghosts. 

It starts with a new way to begin the ghost interactions. Rather than entering into a level that has ghosts milling about and spawning in, we thought it might be fun to have some sort of summoning section before the ghosts are out in the open where the group needs to come together to scare them. Our first attempt at this summoning phase involves a peek-a-boo gopher style mechanic where ghosts poke their heads up from the ground and then down, only to pop back up in a different spot. When a player successfully spooks a gopher ghost, it pops into the level in a larger form where the players need to come together to split it. 

Our next big update involves the last stage of the ghost interaction. After a ghost (big or medium) is scared, it splits instead into a ton of super small speedy ghosts. Here players can now activate some ghost spooking technology around the map to blast and freeze the tiny ghosts. From there, it should be easy pickings to scare or grab all the small ghosts on the map. 

These updates will be implemented and ready to run for our largest playtesting session yet at the Whaaat?! Festival Naaaturecade event happening this weekend. (Playtest results to follow soon) 

C - This week we've added a number of new mechanics. One such change was the addition of scare towers. These are towers in four corners of the level that the players must "load" a scare into to activate. Once all four towers are activated with the four different scares, all the small ghosts in the scene get frozen in place. This allows players to grab them more easily. The speed of the ghosts has been set such that they are too fast for the player to catch normally. When each tower is activated it emits a particle effect that indicates which scare has been used. There is a timer on each tower before it gets deactivated. The idea is that if all four players contribute to activating the tower then all towers can be activated instantly while a single player could activate all the towers but it will take longer.

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/tower-scare-1.png" width="50%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/tower-scare-2.png" width="50%">

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/tower-scare-3.png" width="50%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/tower-scare-4.png" width="50%">

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/tower-scare.gif" width="75%">

D - This week was the start of a new round of mechanics and design updates. I was able to get a simple UI menu scene designed with some placeholder images, this will serve as our intro menu as players enter the game.

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/clipMenu.PNG" width="75%">

On the second round of changes we added the brand new 'peekaboo ghost' which players must locate and scare in order to summon new ghosts. I also implemented the new player health dynamic. This allows for better feedback to allow players to know what is going on, and also gives the players one more item to focus on as the chaos ensues inside the game.

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/clip3-8.PNG" width="75%">

Z -This week we developed a variety new ghost models all with movement animations (pictured below), stun animations and spooked animations. The goal was to create ~5 ghosts with distinct silhouettes to have spawn in within the levels, each having different versions of the basic system, a stun attack, movement and splitting into smaller versions of themselves. I think the variations in the enemies with give the levels in the game more flavor and also the ghosts are just so goofy it's wonderful.

<img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/GhostAnim-BedSheetBoi.gif" width="30%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/GhostAnim-Bun.gif" width="30%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/GhostAnim-GeneralGuy.gif" width="30%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/GhostAnim-Leggg.gif" width="30%"><img src="https://github.com/turnerdylan/Ghostbusters/blob/master/Ghostbusters/Documentation/Images/GhostAnim-SharkDodododododo.gif" width="30%">

- - -

## 2/23/2021 - Week 6 Levels and Effects!
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

## 2/16/2021 - Week 5 Bags, Buttons, Bugs and Battlefields
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

## 2/9/2021 - Week 4 All the BUTTONS!
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

## 2/2/2021 - Week 3 Playtesting and Refining
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
