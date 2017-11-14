using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {


    public PlayerControllerMapTut player;
    public static GameStateManager instance;

    public void savePLayer()
    {
        this.player = FindObjectOfType<PlayerControllerMapTut>();
    }
    public void loadPlayer()
    {
        Instantiate(player, Vector3.zero, Quaternion.identity);
    }

    public PlayerControllerMapTut getPlayer() { return this.player; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            player = (PlayerControllerMapTut)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Player.prefab", typeof(PlayerControllerMapTut));
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        Debug.Log("New GameManager");
        //loadPlayer();
    }
}
