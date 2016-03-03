using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    public TextAsset startingLevel;
    bool loadedStartingLevel = false;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (startingLevel != null && !loadedStartingLevel && WorldController.Instance != null){
            WorldController.Instance.load(startingLevel);
        }
    }
}
