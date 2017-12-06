using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBoss : MonoBehaviour {

	public Scene currentScene;
	public GameObject[] merchantUI;

	// Use this for initialization
	void Start () {
		merchantUI = GameObject.FindGameObjectsWithTag ("MerchantUI");
		currentScene = SceneManager.GetActiveScene ();
		Debug.Log (currentScene.name);
		if (currentScene.name.Equals ("BossTheme")) {
			foreach (GameObject obj in merchantUI) {
				obj.SetActive (false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
