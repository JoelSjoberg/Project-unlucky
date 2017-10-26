using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public static int health;
    Text text;

    public int heartAmount = 3;
    public int currentHealth;

    public Image[] healthImages;
    public Sprite[] healthSprites;


    void Awake()
    {
        text = GetComponent<Text>();
        health = 3;
    }
    // Use this for initialization
    void Start()
    {
        currentHealth = heartAmount;
        checkHeartAmount();

    }

    void checkHeartAmount()
    {
        for(int i = 0; i < heartAmount; i++)
        {
            healthImages[i].enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Health: " + health;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            health--;


            if (health == 0)
            {
                Application.LoadLevel("Level3");
            }
        }
    }
}
