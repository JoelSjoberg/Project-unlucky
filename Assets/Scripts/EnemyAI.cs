﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public float speed;

    private float movementSpeed;
    private Rigidbody rigidbody;
    private Vector3 velocity;
    private Vector3 direction;
    private int health;

    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
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

        rigidbody.velocity = velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (health > 0)
        {
            Vector3 currentPosition = transform.position;
            currentPosition.x = currentPosition.x - (direction.x * 2) ;
            currentPosition.z = currentPosition.z - (direction.z * 2) ;
            transform.position = currentPosition;


            health--;
        }
        else
        {

            Destroy(gameObject);
        }
    }
}
