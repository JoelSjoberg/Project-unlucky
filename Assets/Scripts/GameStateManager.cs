using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {


    private PlayerControllerMapTut player;
    public static GameStateManager instance;

    [HideInInspector]
    public List<List<EnemyBehaviour>> enemiesInLevel = new List<List<EnemyBehaviour>>();

    public int health , scrap;

    public void savePlayer()
    {
        player = FindObjectOfType<PlayerControllerMapTut>();
        this.health = player.health;
        this.scrap = player.scrap;
    }
    public void loadPlayer(PlayerControllerMapTut p)
    {
        
        p.health = this.health;
        p.scrap = this.scrap;
    }

    public void DeleteEnemiesInLevel(int level) 
    {
        foreach(EnemyBehaviour ob in enemiesInLevel[level])
        {
            ob.setActiveTo(false);
        }
    }

    private void Awake()
    {
        // remove any other instance of this object when awoken
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
