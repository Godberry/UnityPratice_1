﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    public float FlyingSpeed;
    public float LifeTime;
    public GameObject explosion;
    public AudioSource bulletAudio;

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
        explosion.gameObject.transform.parent = null;
        explosion.gameObject.SetActive (true);
        bulletAudio.pitch = Random.Range(0.0f, 1);
        KillYourSelf();
    }
}
