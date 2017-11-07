using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImp : MonoBehaviour {

	PlayerControllerMapTut pCMT = new PlayerControllerMapTut();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other){

		if (other.tag == "Player") {
			
			if (gameObject.name == "moveFaster") {
				

				pCMT.Speed = 60;
				Destroy(gameObject);
				Debug.Log (pCMT.speed);
			}
			
			else if (gameObject.name == "rapidFire") {
				
				Destroy(gameObject);
			}
			
			if (false) {
				
			}
		}


	}

}
