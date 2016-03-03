using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

public class WorldController : MonoBehaviour {

    public static World world;
    public static WorldController Instance;
    Dictionary<Tile, GameObject> TileGameObjects;
    Dictionary<string,Sprite> sprites;
    static TileType[] nonWalkable = { TileType.Wall_Brick };

    // Use this for initialization
    void Start () {
        Instance = this;
        sprites = new Dictionary<string, Sprite>();
        Sprite[] spritesTemp = Resources.LoadAll<Sprite>("Textures/");
        foreach (Sprite s in spritesTemp) {
            sprites.Add(s.name, s);
        }
        TileGameObjects = new Dictionary<Tile, GameObject>();
        world = new World(25,25);
        for (int x = 0; x < world.width; x++){
            for (int y = 0; y < world.height; y++){
                GameObject tileGO = new GameObject();
                tileGO.name = "Tile_" + x + "_" + y;
                tileGO.transform.position = new Vector3(x, y, 0);
                tileGO.transform.SetParent(transform, true);
                SpriteRenderer sr = tileGO.AddComponent<SpriteRenderer>();
                sr.sprite = sprites["Grass"];
                Tile tile_data = world.getTileAt(x, y);
                TileGameObjects.Add(tile_data, tileGO);
                tile_data.RegisterTileChangedCallback(OnTileChanged);
            }
        }
        PathFinding.instance.InvalidateGrid();
	}

    void OnTileChanged(Tile tile_data) {
        GameObject tile_go = TileGameObjects[tile_data];
        SpriteRenderer tile_sr = tile_go.GetComponent<SpriteRenderer>();
        if (sprites.ContainsKey(tile_data.Type.ToString())){
            tile_sr.sprite = sprites[tile_data.Type.ToString()];
        }else{
            tile_sr.sprite = null;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public static bool isWalkable(Tile t)
    {
        if (t == null)
            return false;

        bool walkable = true;
        foreach (TileType type in nonWalkable)
        {
            if (t.Type == type)
            {
                walkable = false;
                break;
            }
        }
        return walkable;
    }

    public void load(TextAsset textAsset){
        TextReader sr = new StringReader(textAsset.text);
        string line = sr.ReadLine();
        while (line != null)
        {
            string[] args = line.Split(',');
            int x = int.Parse(args[0]);
            int y = int.Parse(args[1]);
            Tile tile = WorldController.world.getTileAt(x, y);
            if (tile != null)
                tile.Type = (TileType)Enum.Parse(typeof(TileType), args[2]);
            line = sr.ReadLine();
        }
    }
}
