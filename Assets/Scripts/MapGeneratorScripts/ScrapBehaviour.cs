using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapBehaviour : MonoBehaviour {

	public AudioClip sound;
	private AudioSource source;

	AudioController ac = new AudioController ();

	void Awake(){
		
		source = GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
		if(other.tag == "Player")
        {
			source.PlayOneShot (sound, 1f);



            Destroy(gameObject);
            other.GetComponent<PlayerControllerMapTut>().gun.ammoBuffer ++;
			//ac.PlayAudio (0);
        }
    }
}
