using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    public float FlyingSpeed;
    public float LifeTime;

    public void InitAndShoot (Vector3 Direction)
    {
        Rigidbody rigidyBody = this.GetComponent<Rigidbody>();

        rigidyBody.velocity = Direction * FlyingSpeed;

        Invoke("KillYourSelf", LifeTime);
    }

    public void KillYourSelf ()
    {
        GameObject.Destroy(this.gameObject);
    }

    public float damage = 15;

    public void OnTriggerEnter (Collider other)
    {
        other.gameObject.SendMessage("Hit", damage);
        KillYourSelf();
    }
}
