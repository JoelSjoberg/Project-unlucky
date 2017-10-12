using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Vector3 offset;
    public Transform focusTarget;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(focusTarget.position.x + offset.x, focusTarget.position.y + offset.y, focusTarget.position.z + offset.z);
	}
}
