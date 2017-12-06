using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

	public PlayerControllerMapTut playerRef;
	public GameObject[] merchantUI;

	void Start(){
		playerRef = FindObjectOfType<PlayerControllerMapTut> ();
		merchantUI = GameObject.FindGameObjectsWithTag ("MerchantUI");
	}

	public void StartGame(string firstLevel){
		SceneManager.LoadScene (firstLevel);
		Debug.Log ("Start game pressed");
	}

	public void ExitGame(){
		Application.Quit ();
	}

	public void Menu(string menuLevel){
		SceneManager.LoadScene (menuLevel);
		Debug.Log ("Menu button pressed");
	}

	public void Buy(){
		Debug.Log ("Buy pressed");
		if (playerRef.scrap >= 3 && playerRef.health < playerRef.maxHealth ) {
			playerRef.heal (1);
			playerRef.scrap-=3;
			Debug.Log ("Scrap | health: " + playerRef.scrap.ToString() + " " + playerRef.health.ToString());
		}
	}

	public void Leave(){
		Debug.Log ("Leave pressed");
		playerRef.transform.position = new Vector3 (50, (Level.level - 1) * 200, 50);
		foreach (GameObject obj in merchantUI) {
			obj.SetActive (false);
		}
		playerRef.inSafeHeaven = false;
		//FindObjectOfType<DialogueManager>().DisplayNextSentence();
	}
}


