
using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AudioController : MonoBehaviour {

    AudioSource audio;

    public Sound[] sounds;

    public static AudioController instance;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        play("Dungeon");
	}

    String currentScene = SceneManager.GetActiveScene().name;
	
	// Update is called once per frame
	void Update () {

        if (currentScene == "LevelEnd") play("GameOver");
        if (currentScene == "LevelStrar") play("MainMenu");

    }

    private void Awake()
    {

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.looping;
        }

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

    public void play(string name)
    {
        // find sound from sounds array to play
       Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

}
