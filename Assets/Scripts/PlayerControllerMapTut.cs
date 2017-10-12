using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerMapTut : MonoBehaviour {

    public float speed = 150;
    public bool useController;
    public Camera mainCamera;
    public GunController gun;

	Rigidbody rigidbody;
	Vector3 velocity;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	void Update () {
        // Use 3D Vector for movement, normalize it to create equality on each axis and multiply it by speed to make the length = speed
		velocity = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")).normalized * speed * Time.deltaTime;
        if (!useController)
        {
            // rotate with mouse
            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;

            if (groundPlane.Raycast(cameraRay, out rayLength))
            // i.e. if the ray cast from the camera touches anything
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLength); // get the POINT where the ray touches the plane
                Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue); // Draw a debug-line from camera to plane

                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z)); // and rotate towards the POINT
            }

            // input for gunfire
            if (Input.GetMouseButtonDown(0)) gun.isFiring = true;
            if (Input.GetMouseButtonUp(0)) gun.isFiring = false;
        }

    }

	void FixedUpdate(){
        // move the rigidbody with velocity
        rigidbody.MovePosition(rigidbody.position + velocity *  Time.fixedDeltaTime);
        //rigidbody.velocity = velocity;
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
