using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSelf : MonoBehaviour {

    float aliveTime = 1;

	// Use this for initialization
	void Start () {
        Invoke("DestorySelf", aliveTime);
	}
	
	public void DestorySelf ()
    {
        GameObject.Destroy (this.gameObject);
    }
}
