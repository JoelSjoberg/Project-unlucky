using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerMapTut : MonoBehaviour {

    // Status variables
    public int health = 3;
    public bool evading = false;
    public float speed, evadeTime, evadeSpeed = 100;
    private float movementSpeed, evadeTimer = 0;

    //invulnerability
    public float invulnerableTime = 0.5f;
    private float invulnerableTimer = 0;
    private bool invulnerable = false;


    public GunController gun;

    // for collision detection
    public int offset = 5;
    private bool up = true, down = true, left = true, right = true;
    private float xAxis, zAxis = 0;
    public Room currentRoom;

	private Rigidbody rigidbody;
    private SpriteRenderer renderer;
	private Vector3 velocity;

    // place player on given cordinates
    public void spawn(Vector3 newPos)
    {
        transform.position = newPos;
    }

    // Change the room thich the collsion works with
    public void setRoom(Room r)
    {
        currentRoom = r;
    }

    // get the room the player is assigned
    public Room getRoom()
    {
        return this.currentRoom;
    }

    // take damage equal to given amount and play hurt sound, if you die: load game over scene
    public void takeDamage(int d)
    {
        if(!invulnerable)
        {
            invulnerable = true;
            renderer.color = new Color(255, 255, 255, 0.5f);

            this.health -= d;
            FindObjectOfType<AudioController>().play("Hurt");

            // if you die
            if (health <= 0)
            {
                SceneManager.LoadScene(2);
            }
        }
    }
    

    // recieve health and TODO: play sound
    public void heal(int h)
    {
        health += h;
    }

    // keep player inside room
    void checkCollision()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        zAxis = Input.GetAxisRaw("Vertical");
        if (currentRoom == null) return;

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
    }

    // Use this for initialization
    void Start () {

        rigidbody = GetComponent<Rigidbody> ();
        renderer = transform.Find("PlayerSprite").GetComponent<SpriteRenderer>();
        movementSpeed = speed;

	}
	
	// Update is called once per frame
	void Update () {

        // stop player if it is colliding with walls
        checkCollision();

        velocity = new Vector3 (xAxis, 0, zAxis).normalized * movementSpeed;
        velocity *= Time.deltaTime;

        // rigidbody.AddForce(velocity);
        // if (xAxis == 0 && zAxis == 0) rigidbody.velocity = Vector3.zero;
        //rigidbody.velocity = velocity;
        transform.Translate(velocity.x, 0, velocity.z);

        // Input(Keyboard and Mouse)
        if (Input.GetMouseButtonDown(0)) gun.isFiring = true;
        if (Input.GetMouseButtonUp(0)) gun.isFiring = false;
        if (Input.GetKeyDown("left shift"))
        {
            movementSpeed = evadeSpeed;
            evading = true;
        }

        // evading
        if (evading) evadeTimer += Time.deltaTime;
        if (evadeTimer >= evadeTime)
        {
            evading = false;
            movementSpeed = speed;
            evadeTimer = 0;
        }

        // invulnerability
        if (invulnerable) invulnerableTimer += Time.deltaTime;
        if (invulnerableTimer >= invulnerableTime)
        {
            renderer.color = new Color(255, 255, 255, 255);
            invulnerable = false;
            invulnerableTimer = 0;
        }
    }
}
