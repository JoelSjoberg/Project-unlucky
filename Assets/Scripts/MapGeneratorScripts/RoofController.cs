using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofController : MonoBehaviour {

    [Range (0.1f, 1.0f)]
    public float fadeSpeed = 1f;

    private Material mater;
    private Color color;
    private bool looping;
	// Use this for initialization
	void Start () {
        mater = GetComponent<Renderer>().material;
        color = mater.color;

	}

    // Update is called once per frame
    float alpha = 1.0f;
	void Update () {
		
        if (looping)
        {
            alpha -= fadeSpeed * Time.deltaTime;
            mater.color = new Color(color.r, color.g, color.b, alpha);

            if(alpha <= 0.1f)
            {
                Destroy(this.gameObject);
            }
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            looping = true;
        }
    }
}
