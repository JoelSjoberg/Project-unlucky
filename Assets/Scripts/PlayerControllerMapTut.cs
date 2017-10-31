using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//comment
public class PlayerControllerMapTut : MonoBehaviour {

    public float speed;
    public float evadeTime;
    public float evadeSpeed = 100;
    public bool evading = false;

    public GunController gun;

    private float movementSpeed;
    private float timer;

	private Rigidbody rigidbody;
	private Vector3 velocity;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody> ();
        movementSpeed = speed;
        timer = evadeTime;
	}
	
	// Update is called once per frame
	void Update () {
		velocity = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")).normalized * movementSpeed;

        // Input(Keyboard and Mouse)
        if (Input.GetMouseButtonDown(0)) gun.isFiring = true;
        if (Input.GetMouseButtonUp(0)) gun.isFiring = false;
        if (Input.GetKeyDown("left shift"))
        {
            movementSpeed = evadeSpeed;
            evading = true;
        }

        if (evading) timer -= Time.deltaTime;

        if (timer <= 0)
        {
            evading = false;
            movementSpeed = speed;
            timer = evadeTime;
        }
    }

	void FixedUpdate(){
        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);
	}

	void OnTriggerEnter(Collider other){		

		Vector3 currentPosition = transform.position;


		if (other.tag == "DoorRight") {
			Debug.Log ("Collision with door right");
			currentPosition.x = currentPosition.x + 20f;

		}

		if (other.tag == "DoorLeft") {
			Debug.Log ("Collision with door left");
			currentPosition.x = currentPosition.x - 20f;

		}

		if (other.tag == "DoorTop") {
			Debug.Log ("Collision with door top");
			currentPosition.z = currentPosition.z + 20f;

		}

		if (other.tag == "DoorDown") {
			Debug.Log ("Collision with door down");
			currentPosition.z = currentPosition.z - 20f;

		}

		if (other.tag == "Pickable") {
			Debug.Log ("Collision with pickable");
			Destroy (other.gameObject);

		}

		transform.position = currentPosition;
       
	}



}
