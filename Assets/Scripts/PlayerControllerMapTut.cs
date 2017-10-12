using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerMapTut : MonoBehaviour {

	Rigidbody rigidbody;
	Vector3 velocity;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	void Update () {
		velocity = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")).normalized * 10;
	}

	void FixedUpdate(){
		rigidbody.MovePosition(rigidbody.position + velocity *  Time.fixedDeltaTime);
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Door") {
			Debug.Log ("Collision with door");
		
		}
	}

}
