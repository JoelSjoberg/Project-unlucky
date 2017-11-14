using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public Ammo ammo;
    public bool shotFired;
    public int gunAmmo;
    public int gunMaxAmmo;

    [HideInInspector]
    public int ammoBuffer = 0;
    public int ammoBufferGoal = 3;
    public bool ammoAdded;


    public Camera mainCamera;
    public int damage;
    public bool isFiring;
    public float bulletSpeed;
    public float shotIntervall = 0.1f;

    private float timeSinceLastShot = 0;
    

    public Vector3 offset;
    public Transform firePoint;
    public BulletController bulletPrefab;

    private Ray cameraRay;
    private Plane groundPlane;
    private float rayLength;

	public AudioClip shootSound;


	// Use this for initialization
	void Start () {
        mainCamera = FindObjectOfType<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        // add ammo when ammoBuffer >= ammoBufferGoal
        if(ammoBuffer >= ammoBufferGoal)
        {
            Debug.Log("ammo added");
            ammoAdded = true;
            ammoBuffer = 0;
        }

        // rotate with mouse
        cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition); // Cast a "ray" from mainCamera to the cursor position
        groundPlane = new Plane(Vector3.up, Vector3.zero);            // A "mathematical" plane representing the ground

        if (groundPlane.Raycast(cameraRay, out rayLength))            // Assign value to rayLength whitch is the length of the ray from mainCamera to groundPlane
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);      // Get location of where the ray hits the ground
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);// Draw a line draw the line in blue color

            // rotate the gun towards ray point
            transform.LookAt(new Vector3(pointToLook.x + offset.x, transform.position.y + offset.y, pointToLook.z + offset.z));
        }
        if (isFiring && !ammo.ammoEmpty)
        {
            timeSinceLastShot += Time.deltaTime;
            if(shotIntervall <= timeSinceLastShot)
            {
                BulletController newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation) as BulletController;
                newBullet.speed = bulletSpeed;
                newBullet.damage = damage;

                FindObjectOfType<AudioController>().play("Shot");

                timeSinceLastShot = 0;

                shotFired = true;
            }
        }
        else
        {
            timeSinceLastShot = shotIntervall;
        }
    }
}
