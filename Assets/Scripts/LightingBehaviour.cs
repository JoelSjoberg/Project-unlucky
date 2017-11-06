using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingBehaviour : MonoBehaviour {

	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(0, FindObjectOfType<FollowPlayer>().transform.position.y, 0);
	}
}
