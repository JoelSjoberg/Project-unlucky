using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PortalBehaviour : MonoBehaviour {

    public Transform plane;
	public PlayerControllerMapTut playerRef;
	private Vector3 safeHeavenPosition = new Vector3 (-900, 0, 50); 
	public GameObject[] merchantUI;

	void Start(){
		playerRef = FindObjectOfType<PlayerControllerMapTut> ();
		merchantUI = GameObject.FindGameObjectsWithTag ("MerchantUI");
	}

    // Load a new map
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
			foreach (GameObject obj in merchantUI) {
				obj.SetActive (false);
			}
			
            // increment level
            Level.level++;

            Instantiate(plane, FindObjectOfType<MapGenerator>().transform.position + new Vector3(0, 100, 0), Quaternion.identity); // MAKE NEW GROUND 
            FindObjectOfType<MapGenerator>().transform.position += new Vector3(0, 200, 0);
            FindObjectOfType<MapGenerator>().makeDungeon();
            FindObjectOfType<TimeController>().slowDown(2); // slow down time to emphasize transition
            FindObjectOfType<PlayerControllerMapTut>().updateSpriteLayer();
            
			//FindObjectOfType<DialogueManager>().DisplayNextSentence();

            // SceneManager.LoadScene("Level3");

            if (Level.level == 3)
            {
                FindObjectOfType<GameStateManager>().savePlayer();
                SceneManager.LoadScene("BossTheme");
            }
			if (Level.level % 3 != 0) {
				FindObjectOfType<DialogueManager>().DisplayNextSentence();
			}

			//safe heaven
			if (Level.level % 3 == 0) {
				Debug.Log ("Go to safe heaven");
				playerRef.transform.position = safeHeavenPosition;
				playerRef.inSafeHeaven = true;

			}

        }
    }

}
