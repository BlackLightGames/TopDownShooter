using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Weapon {

    public PlayerController owner;
    public GameObject gunFXPrefab;
    public float cooldown = .5f;
    public float ammoPerClip = 10;
    public float ammoInClip = 10;
    public float ammo = 40;
    protected float cooldownTimer;
    public bool fullAuto = false;
    Text ammoCounter;

    public Weapon(PlayerController owner, Text ammoCounter) {
        this.owner = owner;
        this.ammoCounter = ammoCounter;
        gunFXPrefab = owner.gunFX;
    }

    public virtual void OnFire() {

    }

    public virtual void OnReload() {

    }

    public virtual void update(float deltaTime) {
        if ((Input.GetButtonDown("Fire1")) || (fullAuto && Input.GetButton("Fire1"))) {
            OnFire();
        }
        if (Input.GetButtonDown("Reload")) {
            OnReload();
        }
        if (cooldownTimer > 0) {
            cooldownTimer -= deltaTime;
        }
        ammoCounter.text = ammoInClip + "/" + ammo;
    }

}
