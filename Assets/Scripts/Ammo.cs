using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{
    public static int ammo;
    public static int maxAmmo;
    public static int remainderAmmo;

    public static int setAmmo = 30;
    public static int setMaxAmmo = 60;
    public static bool ammoEmpty = false;

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

        ammo = setAmmo;
        maxAmmo = setMaxAmmo;
        ammoEmpty = false;
    }

    public bool isAmmoEmpty
    {
        get
        {
            return ammoEmpty;
        }
       
    }


    // Update is called once per frame
    void Update()
    {

        //Uses the same as GunController, to be changed later
        if (Input.GetMouseButtonDown(0))
            isFiring = true;
        if (Input.GetMouseButtonUp(0))
            isFiring = false;

        if (isFiring && Time.time > nextFire && !ammoEmpty)
        {

            //Checks if ammo is less than 10 to use more spacing to make the counter look better
            if (ammo < 10)
            {

                ammo--;
                nextFire = Time.time + fireRate;
                text.text = "  " + ammo + " / " + maxAmmo;
            }
            else
            {
                ammo--;
                nextFire = Time.time + fireRate;
                text.text = ammo + " / " + maxAmmo;
            }
        }


        //If reloading when ammo is empty
        if (ammo <= 0 && maxAmmo > 0)
        {
            ammoEmpty = true;
            if (Input.GetKeyDown("r"))
            {
                ammoEmpty = false;
                ammo = setAmmo;
                maxAmmo -= setAmmo;
                text.text = ammo + " / " + maxAmmo;
               
            }
        }

        //If reloading with ammo left
        if (ammo > 0 && ammo != setAmmo && maxAmmo > 0)
        {

            if (Input.GetKeyDown("r"))
            {
                //Remainder ammo is the amount of ammo needed to be reloaded
                remainderAmmo = setAmmo - ammo;
                ammoEmpty = false;

                //Normal reload
                if (maxAmmo >= setAmmo)
                {
                    maxAmmo -= remainderAmmo;
                    ammo = setAmmo;
                    text.text = ammo + " / " + maxAmmo;
                }

                //If maxAmmo is less than the maximum amount in the magazine, but larger than the remainder
                else if (maxAmmo < setAmmo && remainderAmmo <= maxAmmo)
                {
                    ammo = setAmmo;
                    maxAmmo -= remainderAmmo;
                    text.text = ammo + " / " + maxAmmo;
                }

                //If remainder is larger than maxAmmo, the rest of the ammo is reloaded int the magazine
                else if (maxAmmo < setAmmo && remainderAmmo > maxAmmo)
                {
                    ammo += maxAmmo;
                    maxAmmo = 0;
                    text.text = ammo + " / " + maxAmmo;
                }
            }
        }

        if (maxAmmo <= 0 && ammo <= 0)
        {
            ammoEmpty = true;
        }

    }

}
