using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapBehaviour : MonoBehaviour {

    AudioSource audio;
    public AudioClip clip;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            audio.PlayOneShot(clip, 1);
            Destroy(gameObject);
            other.GetComponent<PlayerControllerMapTut>().gun.ammoBuffer ++;

        }
    }
}
