
using UnityEditor;
using UnityEngine;
public class GameStateManager : MonoBehaviour {


    [HideInInspector]
    public PlayerControllerMapTut player;
    public static GameStateManager instance;
    private StateHolder stateHolder;

    /*
    public void savePlayer()
    {
        AssetDatabase.CreateAsset(player, "Assets/Prefabs/Player.prefab");
    }
    public void loadPlayer()
    {
        this.player = (PlayerControllerMapTut)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Player.prefab", typeof(PlayerControllerMapTut));
        Instantiate(player, Vector3.zero, Quaternion.identity);
    }

    public PlayerControllerMapTut getPlayer() { return this.player; }

    private void spawn() { Instantiate(player, Vector3.zero, Quaternion.identity); }

    private void Awake()
    {
        Debug.Log("GameManager Started");

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
        spawn();
        //loadPlayer();
    }

    private void OnLevelWasLoaded(int level)
    {
        if (player != null)
        {
            // destroy the current player to prevent several instances of player at once
        }
        else savePlayer();
        //loadPlayer();
        spawn();
    }
    */

}
