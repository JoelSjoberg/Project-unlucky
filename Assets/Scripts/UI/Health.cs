using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    // Take health from the player class instead, saves time and resources
    public static int health;
    public Text text;
    public Image[] healthImages;
    public Sprite[] healthSprites;

    public int heartAmount = 3;
    public int currentHealth;
    public int healthPerHeart = 2;


    void Awake()
    {
        text = GetComponent<Text>();
    }
    // Use this for initialization
    void Start()
    {
        // take player health here instead
        currentHealth = heartAmount * healthPerHeart;
        checkHeartAmount();

    }

    void checkHeartAmount()
    {
        for (int i = 0; i < heartAmount; i++)
        {
            healthImages[i].enabled = true;
        }
    }

    void UpdateHearts()
    {
        bool empty = false;
        int i = 0;

        foreach (Image image in healthImages)
        {
            if (empty)
            {
                image.sprite = healthSprites[0];
            }
            else
            {
                i++;
                if (currentHealth >= i * healthPerHeart)
                {
                    image.sprite = healthSprites[healthSprites.Length - 1];
                }
                else
                {
                    int currentHeartHealth = (int)(healthPerHeart - (healthPerHeart * i - currentHealth));
                    int healthPerImage = healthPerHeart / (healthSprites.Length - 1);
                    int imageIndex = currentHeartHealth / healthPerImage;
                    image.sprite = healthSprites[imageIndex];
                    empty = true;
                }
            }
        }

    }

    // Method already exists in player, also instead of clomping and dealing with damage-values like 0.5 dmg. Let's scale up health and deal 1 damage!
    public void DamageTaken(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, heartAmount * healthPerHeart);
        UpdateHearts();
    }

    

    // DO NOT do this here! The PlayerControllerMapTut already has its own takeDamage() method for health manipulation!
    // Doing this here will make it harder to write state machines
    // and the UI should not controll the player and/or level state! It should ONLY display it!
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            DamageTaken(1);

            if (currentHealth == 0)
            {
                SceneManager.LoadScene(2); 
            }
        }
    }
}
