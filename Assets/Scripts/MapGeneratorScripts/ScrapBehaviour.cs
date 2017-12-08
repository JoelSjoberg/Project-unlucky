using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapBehaviour : MonoBehaviour {

    public float attractionSpeed;

    public void moveToPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, FindObjectOfType<PlayerControllerMapTut>().transform.position, attractionSpeed);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            // play the sound from the audio manager
            FindObjectOfType<AudioController>().play("Scrap");
            other.GetComponent<PlayerControllerMapTut>().scrap++;
            Destroy(gameObject);
        }
        if(other.tag == "PlayerAttractor")
        {
            moveToPlayer();
        }
    }
}
