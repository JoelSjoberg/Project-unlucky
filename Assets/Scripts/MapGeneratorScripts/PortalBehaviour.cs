using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PortalBehaviour : MonoBehaviour {

    // Load a new map
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            //FindObjectOfType<MapGenerator>().makeDungeon();
            SceneManager.LoadScene("Level3");
            Debug.Log("makeDungeon");
        }
    }

}
