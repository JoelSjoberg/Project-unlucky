using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {


    private PlayerControllerMapTut player;
    public static GameStateManager instance;

    public int health = 3, scrap = 36;

    public void savePlayer()
    {
        player = FindObjectOfType<PlayerControllerMapTut>();
        this.health = player.health;
        this.scrap = player.scrap;
    }
    public void loadPlayer()
    {
        player = FindObjectOfType<PlayerControllerMapTut>();
        player.health = this.health;
        player.scrap = this.scrap;
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
        loadPlayer();
    }

}
