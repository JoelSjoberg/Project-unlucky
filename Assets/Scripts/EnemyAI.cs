using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public float speed;
    public static Rigidbody enemyRigidbody;

    private float movementSpeed;
    private Vector3 velocity;
    public static Vector3 direction;
    private int health;

    // Use this for initialization
    void Start () {
        enemyRigidbody = GetComponent<Rigidbody>();
        movementSpeed = speed;
        health = 50;
    }
	
	// Update is called once per frame
	void Update () {
        
        velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * movementSpeed;
        direction = PlayerControllerMapTut.playerRigidbody.transform.position - transform.position;
        direction.Normalize();
        direction.y = 0;
        //transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

        velocity = direction * speed;
        velocity.y = 0;

        enemyRigidbody.velocity = velocity;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Vector3 currentPosition = transform.position;
            currentPosition.x = currentPosition.x - (direction.x * 10);
            currentPosition.z = currentPosition.z - (direction.z * 10);
            transform.position = currentPosition;
        }
        if(other.tag == "Bullet")
        {
            if (health > 0)
            {
                Vector3 currentPosition = transform.position;
                currentPosition.x = currentPosition.x - (direction.x * 2);
                currentPosition.z = currentPosition.z - (direction.z * 2);
                transform.position = currentPosition;
                health--;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        if(other.tag == "Walls")
        {
            Vector3 currentPosition = transform.position;
            currentPosition.x = currentPosition.x - direction.x * 3;
            currentPosition.z = currentPosition.z - direction.z * 3;
            transform.position = currentPosition;
        }


    }
}
