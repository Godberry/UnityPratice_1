using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GunManager : MonoBehaviour {

    public float MinimunShootPeriod;
    public float MuzzleShowPeriod;

    private float shootCounter = 0;
    private float muzzleCounter = 0;

    public GameObject muzzleFlash;
    public GameObject bulletCandidate;

	public void TryToTriggerGun ()
    {
        if (shootCounter <= 0)
        {
            this.transform.DOShakeRotation(MinimunShootPeriod * 0.8f, 3f);
            muzzleCounter = MuzzleShowPeriod;
            muzzleFlash.transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360));

            shootCounter = MinimunShootPeriod;
            GameObject newBullet = GameObject.Instantiate(bulletCandidate);
            BulletScript bullet = newBullet.GetComponent<BulletScript>();
            bullet.transform.position = muzzleFlash.transform.position;
            bullet.transform.rotation = muzzleFlash.transform.rotation;

            bullet.InitAndShoot(muzzleFlash.transform.forward);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (shootCounter >= 0)
            shootCounter -= Time.deltaTime;

        if (muzzleCounter > 0)
        {
            muzzleFlash.gameObject.SetActive(true);
            muzzleCounter -= Time.deltaTime;
        }
        else
        {
            muzzleFlash.gameObject.SetActive(false);
        }
	}
}
