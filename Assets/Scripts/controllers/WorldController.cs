using UnityEngine;
using System.Collections.Generic;

public class WorldController : MonoBehaviour {

    World world;
    Dictionary<Tile, GameObject> TileGameObjects;
    Dictionary<string,Sprite> sprites;

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
                sr.sprite = null;
                Tile tile_data = world.getTileAt(x, y);
                TileGameObjects.Add(tile_data, tileGO);
                tile_data.RegisterTileChangedCallback(OnTileChanged);
            }
        }
        world.RandomizeTiles();
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
}
