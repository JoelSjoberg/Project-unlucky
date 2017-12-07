using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicLayer : MonoBehaviour {

    Renderer r;

	// Use this for initialization
	void Start () {
        r = GetComponent<Renderer>();
        r.sortingOrder = (int)transform.position.y;
	}

    /*private void Update()
    {
        r.sortingOrder = (int)transform.position.y;
    }*/
}
