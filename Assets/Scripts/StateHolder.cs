using UnityEditor;
using UnityEngine;

public class StateHolder : MonoBehaviour {

    private PlayerControllerMapTut player;
    public static StateHolder instance;
    public int health, scrap;

    // Save player Variables
    public void SavePlayerStats()
    {
        player = FindObjectOfType<PlayerControllerMapTut>();
        health = player.health;
        scrap = player.scrap;
    }

    // Load player variables
    public void LoadPlayseStats()
    {
        player = FindObjectOfType<PlayerControllerMapTut>();
        player.health = health;
        player.scrap = scrap;
    }

    private void Awake()
    {
        // delete new instances of this class created
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
}
