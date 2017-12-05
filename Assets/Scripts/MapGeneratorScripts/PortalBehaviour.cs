using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PortalBehaviour : MonoBehaviour {

    public Transform plane;

    // Load a new map
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("makeDungeon");
            Level.level++;
            Debug.Log("level + 1");
            Instantiate(plane, FindObjectOfType<MapGenerator>().transform.position + new Vector3(0, 100, 0), Quaternion.identity); // MAKE NEW GROUND 
            FindObjectOfType<MapGenerator>().transform.position += new Vector3(0, 200, 0);
            FindObjectOfType<MapGenerator>().makeDungeon();
            FindObjectOfType<TimeController>().slowDown(2); // slow down time to emphasize transition
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
            // FindObjectOfType<GameStateManager>().savePlayer();
            // SceneManager.LoadScene("Level3");
        }
    }

}
