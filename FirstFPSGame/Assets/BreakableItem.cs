using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BreakableItem : MonoBehaviour {

	[System.Serializable]
	public class BreakingEntry
	{
		public GameObject breakNode;
        public float breakingHP;
	}

    public float currentHP;
    public List<BreakingEntry> BreakSettings;
    public GameObject DestoryEffect;
    public void Hit(float _hitValue)
    {
        if (currentHP > 0)
        {
            currentHP -= _hitValue;
            if (currentHP <= 0)
            {
                DestoryEffect.gameObject.SetActive (true);
                this.transform.DOScale (new Vector3 (0.0f, 0.0f, 0.0f), 0.01f).SetDelay(0.1f).OnComplete(() =>
                {
                    Invoke("DisableParticleSystems", 10);
                });

            }
            else
            {
                foreach (BreakingEntry entry in BreakSettings)
                {
                    if (currentHP < entry.breakingHP)
                    {
                        entry.breakNode.gameObject.SetActive (true);
                    }
                }
            }
        }
    }

    public void DisableParticleSystems()
    {
        ParticleSystem[] particles = this.GetComponentsInChildren <ParticleSystem> ();
        foreach (ParticleSystem p in particles)
        {
            p.Stop ();
        }
        Invoke("KillYourSelf", 5);
    }

    public void KillYourSelf()
    {
       GameObject.Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
