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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            Debug.Log("hello");
            Destroy(other.gameObject);
        }

    }
}
