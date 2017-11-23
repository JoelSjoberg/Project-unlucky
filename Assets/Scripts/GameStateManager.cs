
using UnityEditor;
using UnityEngine;
public class GameStateManager : MonoBehaviour {


<<<<<<< HEAD
    private PlayerControllerMapTut player;
=======
    [HideInInspector]
    public PlayerControllerMapTut player;
>>>>>>> GameManagerTesting
    public static GameStateManager instance;
    private StateHolder stateHolder;

<<<<<<< HEAD
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
=======
    /*
    public void savePlayer()
    {
        AssetDatabase.CreateAsset(player, "Assets/Prefabs/Player.prefab");
    }
    public void loadPlayer()
    {
        this.player = (PlayerControllerMapTut)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Player.prefab", typeof(PlayerControllerMapTut));
        Instantiate(player, Vector3.zero, Quaternion.identity);
>>>>>>> GameManagerTesting
    }


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
<<<<<<< HEAD
        loadPlayer();
    }
=======
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
>>>>>>> GameManagerTesting

}
