# Weekly exercise 5

[Itch.io link](https://unijacob.itch.io/week-5)

## Synposis
A TileMap game where the goal is for the player - the knight - to reach the princess while avoiding enemies. 

If an enemy manages to touch the player - its game over.

The player can use various tools to reach his goal - thoese are found in certain tiles in the map.

## Chosen improvements
* 1.a - Added a boat, a pickaxe and a goat to the game as insructed
* 2.d - The player can only see tiles that are not blocked behind mountain-tiles and forest-tiles, the rest are blackend.

## Algorithm
Used [Bresenham's line algorithm](https://en.wikipedia.org/wiki/Bresenham%27s_line_algorithm) to efficiently find the "line of sight" of the character 
([code link](https://github.com/UniJacob/Week5/blob/a70f2fefedc6d1cf91e2406db4ce18142447dfe2/Assets/Scripts/LineOfSight.cs#L68)).
