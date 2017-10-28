using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collider other)
    {
        if(other.name == "Player")
        {
            Debug.Log("Enter");
            Destroy(this.gameObject);
        }
    }
}
