using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

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
        currentHealth = heartAmount * healthPerHeart;
        checkHeartAmount();

    }

    void checkHeartAmount()
    {
        for(int i = 0; i < heartAmount; i++)
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

    public void DamageTaken(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, heartAmount * healthPerHeart);
        UpdateHearts();
    }
    
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            DamageTaken(1);

            if (currentHealth == 0)
            {
                //Application.LoadLevel("Level3");
                text.text = "You have died!";
            }
        }
    }
}
