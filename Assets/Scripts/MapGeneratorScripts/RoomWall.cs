using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomWall : MonoBehaviour {

    private Vector3 positionOnTrigger;
    public void makeSize(float width, float height)
    {
        transform.localScale *= 0;
        transform.localScale = new Vector3(width, 10, height);
    }

    public void makeOn(float x, float z)
    {
        transform.position = new Vector3(x, 0, z);
    }

    PlayerControllerMapTut player;
    private void OnTriggerEnter(Collider other)
    {
        positionOnTrigger = other.transform.position;
        Vector3 reverseVelocity = Vector3.zero;
        if(other.name == "Player")
        {
            player = other.GetComponent<PlayerControllerMapTut>();
        }
    }
}
