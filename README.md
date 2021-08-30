<p align="center">
  <a href="https://github.com/tangivan/swordstrike-game">
    <img src="SwordStrike/Assets/Art/android-chrome-512x512.png" alt="Logo" width="80" height="80">
  </a>

  <h3 align="center">Swordstrike</h3>

  <p align="center">
    Fast-paced game to improve concentration
  </p>
</p>

<details open="open">
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#requirements">Requirements</a>
    </li>
    <li>
      <a href="#design">Design</a>
      <ul>
        <li><a href="#features-implemented">Features Implemented</a></li>
      </ul>
    </li>
    <li>
      <a href="#note">Note</a>
    </li>
  </ol>
</details>

# About The Project
![swordstrike-demo](https://github.com/tangivan/swordstrike-game/blob/master/SwordStrike/Assets/Art/swordstrike-gif.gif)  
[Check out the Live Demo!](https://ivantang.ca/swordstrike)
<br />
<br />
Swordstrike is a fast-paced single player game where the user must use the correct elemental sword to fight enemies. Currently, there are four types of enemies, one for each element and two different bosses that can be configured. The game is designed to slowly overwhelm the player with high numbers of enemies until they cannot keep up with the spawn rate.

### Built With
* [Unity](https://unity.com/)
* [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
* [Photoshop](https://www.adobe.com/ca/products/photoshop.html)

# Requirements
[Unity version](https://unity3d.com/get-unity/download/archive) 2020.3.3f1 or later

# Design
The core mechanic Swordstrike utilizes in the game is the ability for the player to throw their sword at where they click and instantly retrieving the weapon. This mechanic which is paired with the enemy disintegrating on impact makes the gameplay experience both visually and audibly satisfying. There are four different elemental swords which correspond to four types of enemies. When attacking an elemental enemy with a sword, you will either have an advantage, disadvantage, or neutral damage based on the element you use. 

### Features Implemented
* Elemental mechanics where using the correct element can 1-hit enemies, neutral element 2-hit enemies, disadvantaged element 4-hit enemies
  * Fire > Earth
  * Earth > Air
  * Air > Water
  * Water > Fire
  * Fire > Earth > Air > Water > Fire
* Enemies drop health items on death (% based drop chance)
* Auto-patrolling enemies
* Ranged attacking enemies
* Melee attacking enemies
* Mouse and Keyboard controls
* Player dashes with invincibility frames
* Background music, sound effects, and visual cues
* Combo hit-counter tracking
* Two types of bosses
* Three difficulties

### Note
This is not a full feature-complete game and is just a personal project that I work on to test mechanic ideas I have in my mind. 
