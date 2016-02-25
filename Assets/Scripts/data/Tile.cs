using UnityEngine;
using System.Collections;
using System;

public enum TileType { Empty, Dirt, Grass, Floor_Wood, Wall_Brick }

public class Tile {

    Action<Tile> TileChangedCallback;

    World world;
    int x;
    int y;
    TileType type;

    public TileType Type {
        get {
            return type;
        }

        set {
            type = value;
            if (TileChangedCallback != null)
                TileChangedCallback(this);
        }
    }

    public Tile(int x, int y, World world) {
        this.world = world;
        this.x = x;
        this.y = y;
        this.Type = TileType.Empty;
    }

    public void RegisterTileChangedCallback(Action<Tile> cb) {
        TileChangedCallback += cb;
    }

    public void UnregisterTileChangedCallback(Action<Tile> cb)
    {
        TileChangedCallback -= cb;
    }

}
