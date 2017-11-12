using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomArea : MonoBehaviour
{


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            other.GetComponent<PlayerControllerMapTut>().transform.position = new Vector3(0, 0, 0);

        }
    }
}
