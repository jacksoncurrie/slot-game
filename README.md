# Slot Game

the game I made is a slot gambling game, where you choose a price to spin and depending on a random result, you either lose your bet, or win a multiple of the result. 

I chose this game as it is a good way to represent basic and advanced UI features, but is still easy enough to implement quickly. I utalised the grid control in UWP to allow for some resizing of the form. I used all native controls, to keep with a consistent look and feel for a better user experience.

In my solution you will see two projects, the main UI project SlottGame and the GameLogic project holding the entities used in the game. I broke out the entities as these are common to the logic of the game and don't rely on a UI. This makes for easy integration, and with a changing UI, the logic will still remain the same.

Explaining your implementation choices.
