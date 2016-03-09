using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pistol : Weapon {

    int damage = 5;

    public Pistol(PlayerController owner, Text ammoCounter) : base(owner, ammoCounter) {

    }

    public override void OnReload()
    {
        base.OnReload();
        ammo += ammoInClip;
        if (ammo >= ammoPerClip)
        {
            ammo -= ammoPerClip;
            ammoInClip = ammoPerClip;
        }
        else {
            ammoInClip = ammo;
            ammo = 0;
        }
    }

    public override void OnFire()
    {
        base.OnFire();
        if (cooldownTimer <= 0 && ammoInClip > 0) {
            Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - owner.transform.position).normalized;
            RaycastHit2D[] hits = Physics2D.RaycastAll(owner.transform.position + new Vector3(.5f, .5f, 0), direction);
            bool hit = false;
            foreach (RaycastHit2D info in hits)
            {
                if (info.collider.gameObject != owner.gameObject) {
                    if (info.collider != null)
                    {
                        Debug.Log("hit");
                        GameObject gunFXInstance = (GameObject)GameObject.Instantiate(gunFXPrefab, Vector3.zero, Quaternion.identity);
                        LineRenderer lr = gunFXInstance.GetComponent<LineRenderer>();
                        lr.SetPosition(0, owner.transform.position + new Vector3(.5f, .5f, 0));
                        Vector3 otherPos = info.collider.gameObject.transform.position;
                        otherPos.z = 0;
                        lr.SetPosition(1, otherPos);
                        hit = true;
                        Transform trans = info.collider.transform;
                        Health health = null;
                        while (health == null && trans != null) {
                            health = trans.GetComponent<Health>();
                            trans = trans.parent;
                        }
                        if (health != null) {
                            health.DoDamage(damage);
                        }
                    }
                    break;
                }
            }
            if (!hit) {
                Debug.Log("miss");
                GameObject gunFXInstance = (GameObject)GameObject.Instantiate(gunFXPrefab, Vector3.zero, Quaternion.identity);
                LineRenderer lr = gunFXInstance.GetComponent<LineRenderer>();
                lr.SetPosition(0, owner.transform.position + new Vector3(.5f, .5f, 0));
                Vector3 otherPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                otherPos.z = 0;
                lr.SetPosition(1, otherPos);
                hit = true;
            }
            ammoInClip -= 1;
            cooldownTimer = cooldown;
        }
    }

}
