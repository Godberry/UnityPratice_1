using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MonsterScript : MonoBehaviour {

    private Animator animator;
    private float MinamunHitPeriod = 1f;
    private float HitCounter = 0; 
    public float CurrentHP = 100;

    public float MoveSpeed;
    public GameObject FollowObject;
    private Rigidbody rigidbody;
    public CollisionListScript PlayerSensor;
    public CollisionListScript AttackSensor;

	// Use this for initialization
	void Start () {
        animator = this.GetComponent<Animator>();
        rigidbody = this.GetComponent <Rigidbody> ();
	}
	
    public void Hit (float value)
    {
        if (HitCounter <= 0)
        {
            FollowObject = GameObject.FindGameObjectWithTag ("Player");
            HitCounter = MinamunHitPeriod;
            CurrentHP -= value;

            animator.SetFloat("HP", CurrentHP);
            animator.SetTrigger("Hit");

            if (CurrentHP <= 0)
            {
                BuryTheBody();
            }
        }
    }

    void BuryTheBody ()
    {
        this.GetComponent<Rigidbody>().useGravity = false;
        this.GetComponent<Collider>().enabled = false;
        this.transform.DOMoveY(-0.8f, 1f).SetRelative(true).SetDelay(1).OnComplete(() =>
        {
            this.transform.DOMoveY(-0.8f, 1f).SetRelative(true).SetDelay(3).OnComplete(() =>
           {
               GameObject.Destroy(this);
           });

        });
    }

	// Update is called once per frame
	void Update () {

        if (PlayerSensor.CollisionObjs.Count > 0)
        {
            FollowObject = PlayerSensor.CollisionObjs [0].gameObject;
        }

		if (CurrentHP > 0 && HitCounter > 0)
        {
            HitCounter -= Time.deltaTime;
        }
        else
        {
            if (CurrentHP > 0)
            {
                if (FollowObject != null)
                {
                    Vector3 lookAt = FollowObject.gameObject.transform.position;
                    lookAt.y = this.gameObject.transform.position.y;
                    this.transform.LookAt (lookAt);
                    animator.SetBool ("Run", true);

                    if (AttackSensor.CollisionObjs.Count > 0)
                    {
                        animator.SetBool ("Attack", true);
                        this.GetComponent <Rigidbody> ().velocity = Vector3.zero;
                    }
                    else
                    {
                        animator.SetBool ("Attack", false);
                        rigidbody.velocity = this.transform.forward * MoveSpeed;
                    }
                }
            }
            else
            {
                this.GetComponent <Rigidbody> ().velocity = Vector3.zero;
            }
        }

	}
}
