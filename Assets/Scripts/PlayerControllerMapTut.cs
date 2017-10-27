using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//comment
public class PlayerControllerMapTut : MonoBehaviour {

    public float speed, evadeTime, evadeSpeed = 100;
    public bool evading = false;

    public GunController gun;
    public int offset = 5;

    private float movementSpeed, timer;

    private bool up = true, down = true, left = true, right = true;
    private float xAxis, zAxis = 0;
    public Room currentRoom;

	private Rigidbody rigidbody;
	private Vector3 velocity;

    public void spawn(float x, float z)
    {
        transform.position = new Vector3(x, transform.position.y, z);
    }
    public void setRoom(Room r)
    {
        currentRoom = r;
    }
    // Use this for initialization
    void Start () {
		rigidbody = GetComponent<Rigidbody> ();
        movementSpeed = speed;
        timer = evadeTime;
	}
	
	// Update is called once per frame
	void Update () {
        xAxis = Input.GetAxisRaw("Horizontal");
        zAxis = Input.GetAxisRaw("Vertical");

        // Collision with walls in current room

        // collision with left wall
        if (transform.position.x < currentRoom.pos.x + offset) left = false;
        else left = true;

        // collision with right wall
        if (transform.position.x > currentRoom.pos.x + currentRoom.width - offset) right = false;
        else right = true;

        // colliion with lower wall
        if (transform.position.z < currentRoom.pos.z + offset) down = false;
        else down = true;

        // collision with upper wall
        if (transform.position.z > currentRoom.pos.z + currentRoom.height - offset) up = false;
        else up = true;

        // limit players ability to move if colliding with walls
        if (!up && zAxis > 0) zAxis = 0;
        if (!down && zAxis < 0) zAxis = 0;
        if (!left && xAxis < 0) xAxis = 0;
        if (!right && xAxis > 0) xAxis = 0;

        velocity = new Vector3 (xAxis, 0, zAxis).normalized * movementSpeed;
        velocity *= Time.deltaTime;

        // rigidbody.AddForce(velocity);
        // if (xAxis == 0 && zAxis == 0) rigidbody.velocity = Vector3.zero;
        transform.Translate(velocity.x, 0, velocity.z);
        //rigidbody.velocity = velocity;

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
    

    void OnTriggerEnter(Collider other){
        if(other.name == "WallThing")
        {
            Debug.Log("PLAYER: "  + other.name);
        }
	}
}
