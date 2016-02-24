using UnityEngine;
using System.Collections;

enum TileType { Empty, Dirt, Grass, Floor_Wood, Wall_Brick }

public class Tile {

    World world;
    int x;
    int y;
    TileType type;

    public Tile(int x, int y, World world) {
        this.world = world;
        this.x = x;
        this.y = y;
        this.type = TileType.Empty;
    }

}
