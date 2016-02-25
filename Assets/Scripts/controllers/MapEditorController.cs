using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;
using UnityEditor;
using System.IO;

public class MapEditorController : MonoBehaviour {

    TileType type;

	// Use this for initialization
	void Start () {
        type = TileType.Grass;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject()) {
            Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousepos.x = Mathf.Floor(mousepos.x);
            mousepos.y = Mathf.Floor(mousepos.y);
            Tile tile = WorldController.world.getTileAt((int)mousepos.x, (int)mousepos.y);
            if (tile != null) {
                tile.Type = type;
            }
        }
	}

    public void setTileType(string name) {
        type = (TileType)Enum.Parse(typeof(TileType), name);
    }

    public void SetAllToSelected() {
        for (int x = 0; x < WorldController.world.width; x++){
            for (int y = 0; y < WorldController.world.height; y++){
                Tile tile = WorldController.world.getTileAt(x, y);
                if (tile != null)
                    tile.Type = type;
            }
        }
    }

    public void save() {
        string savePath = EditorUtility.SaveFilePanelInProject("Save Level", "Level", "lvl", "Save Level");
        StreamWriter sr = new StreamWriter(savePath);
        for (int x = 0; x < WorldController.world.width; x++){
            for (int y = 0; y < WorldController.world.height; y++){
                sr.WriteLine(x+","+y+","+WorldController.world.getTileAt(x, y).Type.ToString());
            }
        }
        sr.Flush();
        sr.Close();
    }

    public void load() {
        string openPath = EditorUtility.OpenFilePanel("Load Level", "C:/", "lvl");
        StreamReader sr = new StreamReader(openPath);
        string line = sr.ReadLine();
        while (line != null) {
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
