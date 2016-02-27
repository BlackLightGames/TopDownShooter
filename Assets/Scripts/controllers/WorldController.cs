using UnityEngine;
using System.Collections.Generic;

public class WorldController : MonoBehaviour {

    public static World world;
    Dictionary<Tile, GameObject> TileGameObjects;
    Dictionary<string,Sprite> sprites;
    static TileType[] nonWalkable = { TileType.Wall_Brick };

    // Use this for initialization
    void Start () {
        sprites = new Dictionary<string, Sprite>();
        Sprite[] spritesTemp = Resources.LoadAll<Sprite>("Textures/");
        foreach (Sprite s in spritesTemp) {
            sprites.Add(s.name, s);
        }
        TileGameObjects = new Dictionary<Tile, GameObject>();
        world = new World();
        for (int x = 0; x < world.width; x++){
            for (int y = 0; y < world.height; y++){
                GameObject tileGO = new GameObject();
                tileGO.name = "Tile_" + x + "_" + y;
                tileGO.transform.position = new Vector3(x, y, 0);
                tileGO.transform.SetParent(transform, true);
                SpriteRenderer sr = tileGO.AddComponent<SpriteRenderer>();
                sr.sprite = sprites["Grass"];
                tileGO.AddComponent<BoxCollider2D>().enabled = false;
                Tile tile_data = world.getTileAt(x, y);
                TileGameObjects.Add(tile_data, tileGO);
                tile_data.RegisterTileChangedCallback(OnTileChanged);
            }
        }
	}

    void OnTileChanged(Tile tile_data) {
        GameObject tile_go = TileGameObjects[tile_data];
        SpriteRenderer tile_sr = tile_go.GetComponent<SpriteRenderer>();
        if (sprites.ContainsKey(tile_data.Type.ToString())){
            tile_sr.sprite = sprites[tile_data.Type.ToString()];
        }else{
            tile_sr.sprite = null;
        }
        if (!isWalkable(tile_data))
        {
            tile_go.GetComponent<BoxCollider2D>().enabled = true;
        }
        else {
            tile_go.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public static bool isWalkable(Tile t)
    {
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
}
