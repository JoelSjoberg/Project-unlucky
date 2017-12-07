using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteDL : MonoBehaviour {

    SpriteRenderer sr;
	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        sr.sortingOrder = (int)transform.position.y + 10;
    }
	
	public void updateLayer()
    {
        sr.sortingOrder = (int)transform.position.y + 10;
    }
}
