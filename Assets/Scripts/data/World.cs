using UnityEngine;
using System.Collections;

public class World {

    Tile[,] tiles;
    int width;
    int height;

    public World(int width = 100, int height = 100){
        this.width = width;
        this.height = height;
        tiles = new Tile[width, height];

        for (int x = 0; x < width; x++){
            for (int y = 0; y < height; y++){
                tiles[x, y] = new Tile(x, y, this);
            }
        }
    }

    public Tile getTileAt(int x, int y) {
        if (x < 0 || x >= width || y < 0 || y >= height) {
            return null;
        }
        return tiles[x, y];
    }

}
