using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//comment
public class PlayerControllerMapTut : MonoBehaviour {

    public float speed, evadeTime, evadeSpeed = 100;
    public bool evading = false;

    public GunController gun;
    public Room currentRoom;

    private float movementSpeed, timer;

    private bool up = true, down = true, left = true, right = true;
    private float xAxis, zAxis = 0;

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
        xAxis = Input.GetAxisRaw("Horizontal");
        zAxis = Input.GetAxisRaw("Vertical");

        if (transform.position.x < currentRoom.pos.x) left = false;
        else left = true;


        if (transform.position.x > currentRoom.pos.x + currentRoom.width) right = false;
        else right = true;


        if (transform.position.z < currentRoom.pos.z + 10) down = false;
        else down = true;


        if (transform.position.z > currentRoom.pos.z + currentRoom.height) up = false;
        else up = true;

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
	void FixedUpdate(){
        
	}


    void OnTriggerEnter(Collider other){
        if(other.name == "WallThing")
        {
            Debug.Log("PLAYER: "  + other.name);

        }
	}
    public void setUp(bool b)
    {
        up = b;
    }
    public void setDown(bool b)
    {
        down = b;
    }
    public void setLeft(bool b)
    {
        left = b;
    }
    public void setRight(bool b)
    {
        right = b;
    }
    public bool getUp()
    {
        return this.up;
    }
    public bool getDown()
    {
        return this.down;
    }
    public bool getLeft()
    {
        return this.left;
    }
    public bool getRight()
    {
        return this.right;
    }
    public float getXAxis()
    {
        return this.xAxis;
    }
    public float getZAxis()
    {
        return this.zAxis;
    }
}
