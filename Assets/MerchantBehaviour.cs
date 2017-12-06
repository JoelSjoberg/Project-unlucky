using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantBehaviour : MonoBehaviour {

	public GameObject[] merchantUI;


	// Use this for initialization
	void Start () {
		merchantUI = GameObject.FindGameObjectsWithTag ("MerchantUI");
		foreach (GameObject obj in merchantUI) {
			obj.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		Debug.Log ("Merhcant clicked");
		foreach (GameObject obj in merchantUI) {
			obj.SetActive (true);
		}

	}
}
