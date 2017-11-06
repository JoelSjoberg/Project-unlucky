using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PortalBehaviour : MonoBehaviour {

    public Transform plane;

    // Load a new map
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            Debug.Log("makeDungeon");
            FindObjectOfType<MapGenerator>().transform.position += new Vector3(0, 400, 0);
            FindObjectOfType<MapGenerator>().makeDungeon();
            FindObjectOfType<TimeController>().slowDown(2); // slow down time to emphasize transition
            //Instantiate(plane, FindObjectOfType<MapGenerator>().transform.position, Quaternion.identity);
            //SceneManager.LoadScene("Level3");
        }
    }

}
