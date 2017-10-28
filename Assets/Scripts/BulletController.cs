using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float speed;
    public float lifeTime;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0) Destroy(gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(other.name);
        
        if (other.name != "Player") Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);

        if (other.name == "WallThingy") Destroy(gameObject);
    }
}
