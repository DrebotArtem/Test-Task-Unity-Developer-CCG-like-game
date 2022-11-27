<h1 align="center">Test Task Unity Developer</h1>
<p align="center">Version 2021.2.11f1</p>

## Task:

Use Unity version 2021.2.11f1

Create UI for an "in-hand card" object for CCG-like game. Card consist of:

- Art + UI overlay
- Title
- Description
- Attack icon + text value
- HP icon + text value
- Mana icon + text value

Load card art randomly from https://picsum.photos/ each time app starts.
Fill player’s hand with 4-6 cards in a visually pleasing way and use the arc pattern for displaying the cards (look at the pic below). The number of cards should be determined randomly at the start of the game.

Create an UI button at the center of the screen to randomly change one randomly selected value -2→9 (the range is from -2 to 9) of each one card sequentially, starting from the most left card in the player's hand moving right and repeating the sequence after reaching the most right card.

Bind Attack, Health and mana properties to UI. Changing those values from code must be reflected on the card's UI with counter animation. (counting from the initial to the new value) 

If some card’s HP drop below 1 - remove this card from player’s hand. (dont forget to     reposition other cards, use tweens to make it smooth)

**[Middle+]** Player can drag a card and drop it on middle section of the table (use drop panel of any size) Card moves back to player’s hand if it’s hasn't been dropped over the drop panel. Cards shines when its being dragged.

Reference: Hearthstone, MTG Arena, Gwent and other same games.

## Built With
- C#
- ECS
- DI Container
- Git

## Packages contained
### Unity packages
- Cinemachine
- High Definition RP
- Input System (new)
- Post Processing
- Test Framework
- TextMeshPro
- Timeline
- Unity UI
- Visual Studio Code Editor
- Visual Studio Editor
### Other packages
- [Entitas.](https://github.com/sschmid/Entitas-CSharp#download-entitas) The Entity Component System Framework for C# and Unity.
- [Extenject.](https://github.com/Mathijs-Bakker/Extenject) Extenject is a lightweight highly performant dependency injection framework.
- [Fluent Assertions.](https://github.com/BoundfoxStudios/fluentassertions-unity) A very extensive set of extension methods that allow you to more naturally specify the expected outcome unit tests.
- [NSubstitute.](https://github.com/Thundernerd/Unity3D-NSubstitute) NSubstitute is designed as a friendly substitute for .NET mocking libraries.
- [More Effective Coroutines.](http://trinary.tech/category/mec/) More Effective Coroutines (MEC) is an improved implementation of coroutines. It is a free asset on the Unity asset store.
- [DOTween.](http://dotween.demigiant.com/) DOTween is a fast, efficient, fully type-safe object-oriented animation engine for Unity, optimized for C# users, free and open-source, with tons of advanced features

## Author
**Artem Drebot**

- [Profile](https://github.com/DrebotArtem "Artem Drebot")
- [Email](mailto:drebotgs@gmail.com?subject=Hi% "Hi!")
- [LinkedIn](https://linkedin.com/in/drebot-artem "Hire me!")
