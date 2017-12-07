using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerMapTut : MonoBehaviour {

    // Status variables
    public int health, scrap;
    public int maxHealth = 6;
    public bool evading = false;
    public float speed, slowDownSpeed, evadeTime, evadeSpeed = 100;
    private float movementSpeed, evadeTimer = 0, scrapAttractor;

    //invulnerability
    public float invulnerableTime = 0.5f;
    private float invulnerableTimer = 0;
    private bool invulnerable = false;

    public GunController gun;
    public Attractor attractor;

    // for collision detection
    public int offset = 5;
    private bool up = true, down = true, left = true, right = true;
    private float xAxis, zAxis = 0;
    public Room currentRoom;

	private Rigidbody rigidbody;
    private SpriteRenderer renderer;
	private Vector3 velocity;

	public bool inSafeHeaven = false;

    // place player on given cordinates
    public void spawn(Vector3 newPos)
    {
        transform.position = newPos;
        //FindObjectOfType<PlayerRotator>().GetComponent<DynamicSpriteLayer>().updateSortingOrder();
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
            renderer.color = new Color(255, 255, 255, 0.2f);

            if (health - d < 0) health = 0;
            else this.health -= d;

            FindObjectOfType<AudioController>().play("Hurt");
            stagger();
            // if you die
            if (health <= 0)
            {
                FindObjectOfType<AudioController>().playTheme("LevelEnd");
                gameObject.SetActive(false);
            }
        }
    }

    // TODO: make player stagger
    Vector3 staggerVector;
    public void stagger()
    {
        velocity = new Vector3((xAxis), 0, (zAxis)).normalized * -100;
        velocity *= Time.deltaTime;
        transform.Translate(velocity.x, 0, velocity.z); // move player in reverse direction of movement
        FindObjectOfType<FollowPlayer>().shake(0.1f); // shake the camera
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
        if (currentRoom == null) return; // avoid errors in case dungeon's not ready yet

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

    public void updateSpriteLayer()
    {
        GetComponentInChildren<PlayerSpriteDL>().updateLayer();
    }

    // Use this for initialization
    void Start () {

        rigidbody = GetComponent<Rigidbody>();
        renderer = transform.Find("PlayerSprite").GetComponent<SpriteRenderer>();
        movementSpeed = speed;
        gameObject.SetActive(true);
        FindObjectOfType<GameStateManager>().loadPlayer(); // loads status from 
	}

    
    // Update is called once per frame
    void Update () {

        // stop player if it is colliding with walls
        checkCollision();

        velocity = new Vector3 (xAxis, 0, zAxis).normalized * movementSpeed;
        velocity *= Time.deltaTime;

        // rigidbody.AddForce(velocity);
        // if (xAxis == 0 && zAxis == 0) rigidbody.velocity = Vector3.zero;
        // rigidbody.velocity = velocity;
		if (!inSafeHeaven) {
			transform.Translate(velocity.x, 0, velocity.z);
		}
        

        // Input(Keyboard and Mouse)
		//disable shooting while in safe heaven to make button presses work

		if (!inSafeHeaven) {
			if (Input.GetMouseButtonDown(0)) gun.isFiring = true;	
		}
			        
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
        if (invulnerable)
        {
            invulnerableTimer += Time.deltaTime;
        }
        if (invulnerableTimer >= invulnerableTime)
        {
            renderer.color = new Color(255, 255, 255, 255);
            invulnerable = false;
            invulnerableTimer = 0;
        }
    }
}
