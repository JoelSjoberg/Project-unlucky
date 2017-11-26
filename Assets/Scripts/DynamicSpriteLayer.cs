using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSpriteLayer : MonoBehaviour {

    SpriteRenderer sr;
    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
        sr.sortingOrder = (int)transform.position.y;
	}

    private void Update()
    {
        sr.sortingOrder = (int)transform.position.y;
    }

}
