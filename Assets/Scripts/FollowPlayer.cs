using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Transform focusTarget;
    public Vector3 offset;
    public float offsetYon = 400;
    public float delay = 0.3f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 target;
    private bool toggle = false;
    private float offsetYoff;

	// Use this for initialization
	void Start () {
        offsetYoff = offset.y;
	}
	
	// Update is called once per frame
	void Update () {
        if (focusTarget == null) return;
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
