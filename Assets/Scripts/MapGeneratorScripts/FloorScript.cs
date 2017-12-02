using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Scrap")
        {
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            Debug.Log("scrap");
        }
    }

}
