using UnityEngine;
using System.Collections;

public class Weapon {

    public PlayerController owner;
    public GameObject gunFXPrefab;
    public float cooldown = .5f;
    public float ammoPerClip = 10;
    public float ammoInClip = 10;
    public float ammo = 40;
    protected float cooldownTimer;
    public bool fullAuto = false;

    public Weapon(PlayerController owner) {
        this.owner = owner;
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
        if (cooldownTimer > 0) {
            cooldownTimer -= deltaTime;
        }
    }

}
