using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public int speed = 5;

    public Animator anim;
    public SpriteRenderer renderer;
    Vector3 inputMovement;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
	}
	

	// Update is called once per frame
	void Update () {

        inputMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Translate(inputMovement * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("up", true);
        }
        else
        {
            anim.SetBool("up", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("Right", true);
            renderer.flipX = true;
        }
        else
        {
            anim.SetBool("Right", false);
        }
        if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("Down", true);
        }
        else
        {
            anim.SetBool("Down", false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("Right", true);
            renderer.flipX = false;
        }
        else
        {
            anim.SetBool("Right", false);
        }
    }
}
