using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 10;
    Vector3 movement = Vector3.zero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        movement.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        movement.y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.rotation = Quaternion.identity;
    }

    void FixedUpdate() {
        Vector3 newPos = transform.position + movement;
        transform.position = newPos;
    }

}
