using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour {

    public bool isActive = true;

    public void setRadius(float r)
    {
        // change size of attractor (or magnet if you will)
        GetComponent<SphereCollider>().radius = r;
    }
}
