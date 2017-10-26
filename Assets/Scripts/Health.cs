using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public static int health;
    Text text;
    Rigidbody rigidbody;
    Rigidbody pr = PlayerControllerMapTut.playerRigidbody;
    void Awake()
    {
        text = GetComponent<Text>();

        health = 3;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        

    }

    void OnCollisionEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            text.text = "Health: " + 1;
        }
    }
}
