
using System;
using UnityEngine.SceneManagement;
using UnityEngine;

[Serializable]
public class AudioController : MonoBehaviour {

    AudioSource audio;

    public Sound[] sounds;

    public static AudioController instance;
    private Sound currentSong;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        fadeIn(SceneManager.GetActiveScene().name);
	}

	
	// Update is called once per frame
	void Update () {
    }

    private void OnLevelWasLoaded(int level)
    {
        String currentScene = SceneManager.GetActiveScene().name;
        fadeOut();
        fadeIn(currentScene);
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

    public void playTheme(string name)
    {
        // find sound from sounds array to play
        fadeOut();
        currentSong = Array.Find(sounds, sound => sound.name == name);
        currentSong.source.Play();
    }

    public void stop()
    {
        currentSong.source.Stop();
        currentSong = null;
    }

    // need debugging, does not work
    public void fadeOut()
    {
        while(currentSong.volume >= 0) currentSong.volume -= (Time.deltaTime * 0.0001f);
        stop();
    }

    // need debugging, does not work
    public void fadeIn(string name)
    {
        currentSong = Array.Find(sounds, sound => sound.name == name);
        currentSong.volume = 0f;
        currentSong.source.Play();
        while (currentSong.volume <= 0.7) currentSong.volume += (Time.deltaTime * 0.0001f);
    }
}
