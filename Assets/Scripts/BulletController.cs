using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    [HideInInspector]
    public float speed;
    public float lifeTime;
    public float SpeedOverTime;
    public int damage;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        increaseSpeedOverTime(SpeedOverTime);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0) Destroy(gameObject);
    }
    private void increaseSpeedOverTime(float increaser)
    {
        this.speed += increaser;
    }
    private void OnTriggerExit(Collider other)
    { 
        if (other.name != "Player") Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "WallThingy") Destroy(gameObject);

        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyBehaviour>().takeDamage(damage);
            Destroy(gameObject);
        }
    }
}
