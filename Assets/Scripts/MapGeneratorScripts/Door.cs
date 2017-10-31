using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    [HideInInspector]
    public Door pair;
    [HideInInspector]
    public Room room; // room in which room resides
    private bool closed;
    
    public Door(float x, float z)
    {
        transform.position = new Vector3(x, 0, z);
    }

    public void connectToPair(Door p)
    {
        pair = p;
        p.pair = this;
    }

    public void placeOn(Vector3 pos)
    {
        transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            if (!pair.closed && !closed)
            {
                pair.closed = true;
                closed = true;
                other.GetComponent<PlayerControllerMapTut>().setRoom(pair.room);
                other.transform.position = pair.transform.position;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player") this.closed = false;
    }
}
