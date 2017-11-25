using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour {

    public void setRadius(float r)
    {
        // change size of attractor (or magnet if you will)
        GetComponent<SphereCollider>().radius = r;
    }

    private void OnTriggerStay(Collider other)
    {
        // make scrap float towards player
        if (other.tag == "Scrap") other.GetComponent<ScrapBehaviour>().moveToPlayer();
    }
}
