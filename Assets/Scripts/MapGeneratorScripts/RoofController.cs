using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofController : MonoBehaviour {

    [Range (0.1f, 1.0f)]
    public float fadeSpeed = 1f;

    public Material fadeMaterial;
    private Renderer renderer;
    private Color color;
    private bool looping;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<Renderer>();
        color = renderer.material.color;

	}

    // Update is called once per frame
    float alpha = 1.0f;
	void Update () {
		
        // for fading
        if (looping)
        {
            alpha -= fadeSpeed * Time.deltaTime;
            renderer.material.color = new Color(color.r, color.g, color.b, alpha);

            if(alpha <= 0.1f)
            {
                Destroy(this.gameObject);
            }
        }
	}
    // delete roof if player collides with it
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            renderer.material = fadeMaterial;
            looping = true;
        }
    }
    // delete roof in spawn room
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
        {
            renderer.material = fadeMaterial;
            looping = true;
        }
    }
}
