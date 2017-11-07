using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public string[] audioName;
	public AudioSource[] audioSource;
	public bool clipFound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Play(string clipName){
	
		for (int i = 0; i < audioName.Length; i++) {

			if (clipName == audioName[i]) {
				audioSource [i].Play ();
			}

		}
	}
}
