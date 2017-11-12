using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

    public PlayerControllerMapTut player;
    public static GameStateManager instance;

    public void savePLayer(PlayerControllerMapTut p)
    {
        this.player = p;
    }
    public void loadPlayer()
    {
        Instantiate(player, Vector3.zero, Quaternion.identity);
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
        //loadPlayer();
    }
}
