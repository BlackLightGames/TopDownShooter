using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    public int MaxHealth = 10;
    int currentHealth;

	// Use this for initialization
	void Start () {
        currentHealth = MaxHealth;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void DoDamage(int amount) {
        currentHealth -= amount;
        if (currentHealth <= 0) {
            GameObject.Destroy(gameObject);
        }
    }
}
