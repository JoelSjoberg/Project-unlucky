using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//comment
public class PlayerControllerMapTut : MonoBehaviour {

    public float speed;
    public GunController gun;

	Rigidbody rigidbody;
	Vector3 velocity;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	void Update () {
		velocity = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")).normalized * speed;

        // Input(Keyboard and Mouse)
        if (Input.GetMouseButtonDown(0)) gun.isFiring = true;
        if (Input.GetMouseButtonUp(0)) gun.isFiring = false;

    }

	void FixedUpdate(){
        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);
	}

	void OnTriggerEnter(Collider other){
		/*

		~~~~~~Brainstorming regarding teh m0vem3nt between d0orz~~~~
		 
		-I was thinking about spawning doors in pairs.
		-On collision compare the X-coordinates of the doors to determine whether
		 the player should be moved in a positive or negative direction
		-translate to the position of the other door
		-every door needs a door script which holds their x,y coordinates
		-door scripts can be accessed from the PlayerController through gameObject 
		 getComponent<DoorScript> blablabla something 

		 */
		if (other.tag == "Door") {
			Debug.Log ("Collision with door");
		
		}
	}

}
