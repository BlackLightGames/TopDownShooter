using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed = 10;
    Vector3 movement = Vector3.zero;
    public int currentWeapon;
    public int weaponInventorySize = 5;
    public Weapon[] weapons;
    public GameObject gunFX;
    public Text ammoCounter;

    // Use this for initialization
    void Start () {
        weapons = new Weapon[weaponInventorySize];
        weapons[0] = new Pistol(this, ammoCounter);
    }
	
	// Update is called once per frame
	void Update () {
        if (weapons[currentWeapon] != null) {
            weapons[currentWeapon].update(Time.deltaTime);
        }
        movement.x =+ Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        movement.y =+ Input.GetAxis("Vertical") * speed * Time.deltaTime;
    }

    void FixedUpdate() {
		Vector3 newPos = transform.position + movement;
		if (GameObject.FindObjectOfType<MapEditorController> () == null) {
			Vector3 newMovement = transform.position + movement;
			newMovement.x = transform.position.x;
			if (!WorldController.isWalkable (WorldController.world.getTileAt (Mathf.FloorToInt (newMovement.x), Mathf.FloorToInt (newMovement.y))) || !WorldController.isWalkable (WorldController.world.getTileAt (Mathf.FloorToInt (newMovement.x) + 1, Mathf.FloorToInt (newMovement.y)))) {
				newPos.y = (Mathf.CeilToInt (newMovement.y)) + .01f;
			}
			if (!WorldController.isWalkable (WorldController.world.getTileAt (Mathf.FloorToInt (newMovement.x), Mathf.FloorToInt (newMovement.y) + 1)) || !WorldController.isWalkable (WorldController.world.getTileAt (Mathf.FloorToInt (newMovement.x) + 1, Mathf.FloorToInt (newMovement.y) + 1))) {
				newPos.y = (Mathf.FloorToInt (newMovement.y)) - .01f;
			}
			newMovement = transform.position + movement;
			newMovement.y = transform.position.y;
			if (!WorldController.isWalkable (WorldController.world.getTileAt (Mathf.FloorToInt (newMovement.x), Mathf.FloorToInt (newMovement.y))) || !WorldController.isWalkable (WorldController.world.getTileAt (Mathf.FloorToInt (newMovement.x), Mathf.FloorToInt (newMovement.y) + 1))) {
				newPos.x = (Mathf.CeilToInt (newMovement.x)) + .01f;
			}
			if (!WorldController.isWalkable (WorldController.world.getTileAt (Mathf.FloorToInt (newMovement.x + 1), Mathf.FloorToInt (newMovement.y))) || !WorldController.isWalkable (WorldController.world.getTileAt (Mathf.FloorToInt (newMovement.x) + 1, Mathf.FloorToInt (newMovement.y) + 1))) {
				newPos.x = (Mathf.FloorToInt (newMovement.x)) - .01f;
			}
		}
        transform.position = newPos;
    }

}
