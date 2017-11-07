using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

   public AudioSource audio;

    public static AudioController instance;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (audio.time > 106)
        {
			PlayAudio (-1);
			PlayAudio (1);

			//PlayAudio (0);
        }
	}

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

	public void PlayAudio (int play){
	
		if (play == 1) {
			
			audio.Stop();

		}

		else if (play == 0) {
			audio.Pause ();
		}
		else {
			audio.Play();
		}
	}

}

