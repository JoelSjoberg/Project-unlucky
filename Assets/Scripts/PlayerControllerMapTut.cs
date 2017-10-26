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
        velocity *= Time.deltaTime;
        //rigidbody.AddForce(velocity);
        transform.Translate(velocity.x, 0, velocity.z);

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
        
	}


    void OnTriggerEnter(Collider other){
        if(other.name == "WallThing")
        {
            Debug.Log("PLAYER: "  + other.name);

        }
	}

}
