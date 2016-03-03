using UnityEngine;
using System.Collections;

public class Pistol : Weapon {

    public Pistol(PlayerController owner) : base(owner) {

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
