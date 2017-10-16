using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotator : MonoBehaviour {


    public float offset = 0.0f;
    public float degrees;
    public Camera mainCamera;


    private float rotation;
    private Animator anim;
    private Vector3 difference;
    private Vector3 mousePos;
    private new SpriteRenderer renderer;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        // Take cursor position amd manipulate z value to avoid "location bug"
        mousePos = Input.mousePosition;
        mousePos.z = mainCamera.transform.position.y;

        // Vector from player to cursor
        difference = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
        difference.Normalize();

        // get angle of vector from player to cursor
        rotation = Mathf.Atan2(difference.z, difference.x) * Mathf.Rad2Deg + offset;
        if (rotation < 0) rotation += 360;  // Keep rotation in range 0 - 360

        degrees = rotation;
        print(degrees);

        if (degrees < 90 || degrees > 270) renderer.flipX = false;
        else renderer.flipX = true;

        // Play different anmations depending on cursor position
        if ((degrees < 45 || degrees >= 315) || (degrees >= 135 && degrees < 225))
        // Look to the side
        {
            anim.SetBool("Side", true);
            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
        }
        else if (degrees >= 45 && degrees < 135)
        // Look up
        {
            anim.SetBool("Side", false);
            anim.SetBool("Up", true);
            anim.SetBool("Down", false);
        }
        else if (degrees >= 225 && degrees < 315)
        // Look Down
        {
            anim.SetBool("Side", false);
            anim.SetBool("Up", false);
            anim.SetBool("Down", true);
        }
    }
}