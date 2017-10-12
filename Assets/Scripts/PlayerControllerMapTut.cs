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
		/*
		-I was thinking about spawning doors in pairs.
		-On collision compare the X-coordinates of the doors to determine whether
			the player should be moved in a positive or negative direction
		-translate to the position of the other door

		 */
		if (other.tag == "Door") {
			Debug.Log ("Collision with door");
		
		}
	}

}
