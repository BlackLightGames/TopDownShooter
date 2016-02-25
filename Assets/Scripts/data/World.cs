using UnityEngine;
using System.Collections;

public class World {

    Tile[,] tiles;
    public int width { get; protected set; }
    public int height { get; protected set; }

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

    public void RandomizeTiles() {
        for (int x = 0; x < width; x++){
            for (int y = 0; y < height; y++){
                int num = Random.Range(1, 5);
                switch (num) {
                    case 1:
                        tiles[x, y].Type = TileType.Dirt;
                        break;
                    case 2:
                        tiles[x, y].Type = TileType.Grass;
                        break;
                    case 3:
                        tiles[x, y].Type = TileType.Floor_Wood;
                        break;
                    case 4:
                        tiles[x, y].Type = TileType.Wall_Brick;
                        break;
                    default:
                        tiles[x, y].Type = TileType.Grass;
                        break;
                }
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
