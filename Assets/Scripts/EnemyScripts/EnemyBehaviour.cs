using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {


    private Room currentRoom;
    public int health = 4, speed = 100, offset = 5, damage = 1;
    public float staggerDuration = 0.05f, staggerTimer = 0;
    [HideInInspector]
    public bool collidingWithPlayer, staggered;
    [HideInInspector]
    float xAxis, zAxis;

    [HideInInspector]
    public PlayerControllerMapTut player;
    [HideInInspector]
    public Vector3 velocity;

    private bool up = false, down = false, left = false, right = false;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerControllerMapTut>();
    }

	// Update is called once per frame
	void Update ()
    {
        checkCollision();

        if (staggered && staggerTimer < staggerDuration)
        {
            staggerTimer += Time.deltaTime;
        }
        else
        {
            staggered = false;
            staggerTimer = 0;
        }
	}

    // return distance from player
    public float getDistanceFromPlayer()
    {
        return (player.transform.position - transform.position).magnitude;
    }
    // spawn enemy in room
    public void spawnInRoom(Room room, float y)
    {
        transform.position = room.getRandomRoomPosition(0, y);
        this.currentRoom= room;
    }
    // set current room(for collision and if enemy ever wants to leave room)
    public void setCurrentRoom (Room r)
    {
        currentRoom = r;
    }

    // collision similar to the one in playerControllerMapTut.cs
    Vector3 towardsPlayer;
    private void checkCollision()
    {
        if (player == null) return;
        towardsPlayer = player.transform.position - transform.position;
        xAxis = towardsPlayer.x;
        zAxis = towardsPlayer.z;

        
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
    }

    // returns velocity of movement vector(0 if not moving at all)
    public Vector3 getVelocity()
    {
        // limit players ability to move if colliding with walls, do it here to make inverseAxes work
        if (!up && zAxis > 0) zAxis = 0;
        if (!down && zAxis < 0) zAxis = 0;
        if (!left && xAxis < 0) xAxis = 0;
        if (!right && xAxis > 0) xAxis = 0;

        velocity = new Vector3(xAxis, 0, zAxis);
        return velocity.normalized * speed * Time.deltaTime;
    }

    // inverse the movement axes(x, z)
    public void inverseAxes()
    {
        xAxis *= -1;
        zAxis *= -1;
    }

    // Take damage
    public void takeDamage(int damage)
    {
        health -= damage;
        staggered = true;
    }

    // Returns true if it is in the same room as the player
    public bool inSameRoomAsPlayer()
    {
        return this.currentRoom == player.getRoom();
    }

    // get current room
    public Room getRoom()
    {
        return this.currentRoom;
    }

    // if player collides with enemy
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Player")
        {
            collidingWithPlayer = true;
        }
    }

    // collision signal if the player no longer touches the agent
    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.name == "Player")
        {
            collidingWithPlayer = false;
        }
    }

}
