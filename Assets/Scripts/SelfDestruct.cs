using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {

    public float lifetime = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0) {
            GameObject.Destroy(this.gameObject);
        }
	}
}
