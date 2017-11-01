using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{
    public GunController gun;

    public int ammo;
    public int maxAmmo;
    public int remainderAmmo;
    public int setAmmo;
    public int setMaxAmmo;
    public bool ammoEmpty = false;
    
    Text ammoText;
    
    // Use this for initialization
    void Start()
    {

    }



    void Awake()
    {
        ammoText = GetComponent<Text>();
        setAmmo = gun.gunAmmo;
        setMaxAmmo = gun.gunMaxAmmo;
        ammo = setAmmo;
        maxAmmo = setMaxAmmo;
        ammoEmpty = false;
    }
    

    // Update is called once per frame
    void Update()
    {
        
        if (gun.shotFired && !ammoEmpty)
        {

            //Checks if ammo is less than 10 to use more spacing to make the counter look better
            if (ammo <= 10)
            {

                ammo--;
                ammoText.text = " " + ammo + "    " + maxAmmo;
            }
            else
            {
                ammo--;
                ammoText.text = ammo + "   " + maxAmmo;
            }
            gun.shotFired = false;
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
                ammoText.text = ammo + "   " + maxAmmo;
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
                    ammoText.text = ammo + "   " + maxAmmo;
                }

                //If maxAmmo is less than the maximum amount in the magazine, but larger than the remainder
                else if (maxAmmo < setAmmo && remainderAmmo <= maxAmmo)
                {
                    ammo = setAmmo;
                    maxAmmo -= remainderAmmo;
                    ammoText.text = ammo + "   " + maxAmmo;
                }

                //If remainder is larger than maxAmmo, the rest of the ammo is reloaded int the magazine
                else if (maxAmmo < setAmmo && remainderAmmo > maxAmmo)
                {
                    ammo += maxAmmo;
                    maxAmmo = 0;
                    ammoText.text = ammo + "   " + maxAmmo;
                }
            }
        }

        if (maxAmmo <= 0 && ammo <= 0)
        {
            ammoEmpty = true;
        }

    }

}
