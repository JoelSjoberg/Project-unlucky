using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollideManager : MonoBehaviour {


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Player EXITED");
        other.GetComponent<PlayerControllerMapTut>().transform.position = new Vector3(0, 0, 0);
    }
}
