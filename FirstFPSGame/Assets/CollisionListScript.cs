using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionListScript : MonoBehaviour {

    public List<Collider> CollisionObjs;

	public void OnTriggerEnter (Collider other)
    {
        CollisionObjs.Add (other);
    }

    public void OnTriggerExit (Collider other)
    {
        CollisionObjs.Remove (other);
    }
}
