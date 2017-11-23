using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public int scrapPerBullet = 3;
    public int bullets;
    public int remForBullet;

    public int damage;
    public bool isFiring;
    public float bulletSpeed;

    public float shotIntervall = 0.1f;
    private float timeSinceLastShot = 0;
    private int currentAmmo;

    public Vector3 offset;
    public Transform firePoint;
    public BulletController bulletPrefab;

    public Camera mainCamera;
    private Ray cameraRay;
    private Plane groundPlane;
    private float rayLength;
    PlayerControllerMapTut player;

	public AudioClip shootSound;


	// Use this for initialization
	void Start () {
        mainCamera = FindObjectOfType<Camera>();
        player = GetComponentInParent< PlayerControllerMapTut >();
    }
	
	// Update is called once per frame
	void Update () {

        // get scrap from player

        bullets = player.scrap / scrapPerBullet;
        remForBullet = player.scrap % scrapPerBullet;

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
        if (isFiring && bullets > 0)
        {
            timeSinceLastShot += Time.deltaTime;
            if(shotIntervall <= timeSinceLastShot)
            {
                BulletController newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation) as BulletController;
                newBullet.speed = bulletSpeed;
                newBullet.damage = damage;

                FindObjectOfType<AudioController>().play("Shot");

                timeSinceLastShot = 0;

                transform.parent.GetComponent<PlayerControllerMapTut>().scrap -= scrapPerBullet;
            }
        }
        else
        {
            timeSinceLastShot = shotIntervall;
        }
    }
}
