using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningCollision : MonoBehaviour {

    public bool notspawn;

	void OnCollisionStay(Collision other)
    {
        if(other.gameObject.tag=="Object")
        {
          
            notspawn = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Object")
        {
            notspawn = false;
        }
    }
}
