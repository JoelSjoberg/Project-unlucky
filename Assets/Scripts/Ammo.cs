using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{
    public static int ammo;
    public static int maxAmmo;

    private float fireRate = 0.1f;
    private float nextFire = 0f;

    Text text;
    bool isFiring;
    // Use this for initialization
    void Start()
    {

    }



    void Awake()
    {
        text = GetComponent<Text>();

        ammo = 32;
        maxAmmo = 256;
    }


    // Update is called once per frame
    void Update()
    {

        //Uses the same as GunController, to be changed later
        if (Input.GetMouseButtonDown(0))
            isFiring = true;
        if (Input.GetMouseButtonUp(0))
            isFiring = false;

        if (isFiring && Time.time > nextFire)
        {

            //Checks if ammo is less than 10 to use more spacing to make the counter look better
            if (ammo < 10)
            {
                nextFire = Time.time + fireRate;
                text.text = ammo + "   / " + maxAmmo;
                ammo--;
            }
            else
            {
                nextFire = Time.time + fireRate;
                text.text = ammo + " / " + maxAmmo;
                ammo--;
            }
        }
        //If ammo is out, it automatically reloads
        if (ammo <= 0)
        {
            ammo = 32;
            maxAmmo -= 32;
        }
        if (maxAmmo < 0)
        {
            maxAmmo = 256;
        }

    }

}
