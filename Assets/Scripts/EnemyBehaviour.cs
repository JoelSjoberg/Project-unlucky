using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {


    public Room currentRoom;
    public int health = 4, speed = 100, offset = 5;

    float xAxis, zAxis;

    [HideInInspector]
    public PlayerControllerMapTut player;
    [HideInInspector]
    public Vector3 velocity;

    private bool up = false, down = false, left = false, right = false;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerControllerMapTut>();
        this.currentRoom = new Room(0, 0, 100, 100);
    }

	// Update is called once per frame
	void Update ()
    {
        checkCollision();
	}

    // keep Enemy inside room
    public float getDistanceFromPlayer()
    {
        return (player.transform.position - transform.position).magnitude;
    }
    public void spawnInRoom(Room room)
    {
        transform.position = room.getRandomRoomPosition();
        this.currentRoom= room;
    }
    public void setCurrentRoom (Room r)
    {
        currentRoom = r;
    }

    Vector3 towardsPlayer;
    private void checkCollision()
    {
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
        
        // limit players ability to move if colliding with walls
        if (!up && zAxis > 0) zAxis = 0;
        if (!down && zAxis < 0) zAxis = 0;
        if (!left && xAxis < 0) xAxis = 0;
        if (!right && xAxis > 0) xAxis = 0;
    }
    public Vector3 getVelocity()
    {
        this.velocity = new Vector3(xAxis, 0, zAxis);
        return this.velocity.normalized * speed * Time.deltaTime;
    }
}
