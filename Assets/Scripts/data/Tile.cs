using UnityEngine;
using System.Collections;
using System;

public enum TileType { Dirt, Grass, Floor_Wood, Wall_Brick }

public class Tile {

    Action<Tile> TileChangedCallback;

    World world;
    public int x;
    public int y;
    TileType type;

    public TileType Type {
        get {
            return type;
        }

        set {
            type = value;
            PathFinding.instance.InvalidateGrid();
            if (TileChangedCallback != null)
                TileChangedCallback(this);
        }
    }

    public Tile(int x, int y, World world) {
        this.world = world;
        this.x = x;
        this.y = y;
        this.Type = TileType.Grass;
    }

    public void RegisterTileChangedCallback(Action<Tile> cb) {
        TileChangedCallback += cb;
    }

    public void UnregisterTileChangedCallback(Action<Tile> cb)
    {
        TileChangedCallback -= cb;
    }

    public Tile[] getNeighbors(bool diagOkay = false, bool squeezeOkay = false) {
        Tile[] neighbors;
        if (!diagOkay)
            neighbors = new Tile[4];
        else
            neighbors = new Tile[8];
        neighbors[0] = world.getTileAt(x + 1, y);
        neighbors[1] = world.getTileAt(x - 1, y);
        neighbors[2] = world.getTileAt(x, y + 1);
        neighbors[3] = world.getTileAt(x, y - 1);

        if (diagOkay) {
            if((WorldController.isWalkable(world.getTileAt(x + 1, y)) && WorldController.isWalkable(world.getTileAt(x, y+1))) || squeezeOkay)
                neighbors[4] = world.getTileAt(x + 1, y + 1);
            if ((WorldController.isWalkable(world.getTileAt(x - 1, y)) && WorldController.isWalkable(world.getTileAt(x, y - 1))) || squeezeOkay)
                neighbors[5] = world.getTileAt(x - 1, y - 1);
            if ((WorldController.isWalkable(world.getTileAt(x - 1, y)) && WorldController.isWalkable(world.getTileAt(x, y + 1))) || squeezeOkay)
                neighbors[6] = world.getTileAt(x - 1, y + 1);
            if ((WorldController.isWalkable(world.getTileAt(x + 1, y)) && WorldController.isWalkable(world.getTileAt(x, y - 1))) || squeezeOkay)
                neighbors[7] = world.getTileAt(x + 1, y - 1);
        }

        return neighbors;

    }

}
