using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 movement = new Vector3();
        movement.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        movement.y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(movement);
	}
}
