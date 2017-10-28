using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Vector3 offset;
    public float offsetYoff = 70, offsetYon = 400;
    public Transform focusTarget;
    public float delay = 0.3f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 target;
    private bool toggle = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        target = new Vector3(focusTarget.position.x + offset.x, focusTarget.position.y + offset.y, focusTarget.position.z + offset.z);
        //transform.position = new Vector3(focusTarget.position.x + offset.x, focusTarget.position.y + offset.y, focusTarget.position.z + offset.z);
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, delay);

        if(Input.GetKeyUp("m"))
        {
            toggle = !toggle;
            if (toggle)
            {
                offset.y = offsetYon;
            }
            if(!toggle)
            {
                offset.y = offsetYoff;
            }
        }
    }
}
