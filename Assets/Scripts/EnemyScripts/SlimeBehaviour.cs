using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// without proper knowledge about state machines and game ai in general i defien the ai as follows:
/* 
    - it has a set of bools that define each state
    - for each state we create a function that in depth describes the behaviour for said state
    - each state is sorted in a hierarchy in a if - else if - else statement,
        where the most important state is on top and least important is at the bottom
    - after excecuting the death method, this enemy will be Destroyed and deleted from its room
    - this is the basic slime ai,
        it dies if health >= 0,
        it attacks if it collides with the player and deals damage,
        it follows the player if he/she enters within a given radius,
        if none of these states are true: it's idle
I also propose that we make it possible for the player to have a set of sub states outside of the hierarchy
 e.g. this enemy will begin running from the player if its health == 1 and it will get a red color.
    should the player collide with this slime the player will "eat" it up an recieve a slight boost in health or ammo
*/ 
public class SlimeBehaviour : MonoBehaviour {

    // primary states
    bool followPlayer = false, idle = false, die = false, attack = false;

    // secondary states
    bool collidingWithPlayer = false;

    public float followDistance;
    EnemyBehaviour basicBehaviour;
    Renderer renderer;
	// Use this for initialization
	void Start () {
        idle = true;
        basicBehaviour = GetComponent<EnemyBehaviour>();
        renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {

        if(basicBehaviour.health <= 0)
        {
            setStateFalse();
            die = true;
        }
        else if(collidingWithPlayer)
        {
            setStateFalse();
            attack = true;
            
        }
        else if (basicBehaviour.inSameRoomAsPlayer())
        {
            Debug.Log("true");
            if(basicBehaviour.getDistanceFromPlayer() <= followDistance)
            {
                setStateFalse();
                followPlayer = true;
            }

            if (basicBehaviour.health == 1)
            {
                followPlayer = false;
                moveAwayFromPlayer();
                renderer.material.color = Color.red;
            }
        }
        else
        {
            setStateFalse();
            idle = true;
        }


        manageStateMachine();

	}

    void setStateFalse()
    {
        followPlayer = false; idle = false; die = false; attack = false;
    }

    void manageStateMachine()
    {
        if (die) Destroy(gameObject);

        if(followPlayer)
        {
            moveTowardsPlayer();
        }
    }

    public void moveTowardsPlayer()
    {
        transform.Translate(basicBehaviour.getVelocity());
    }
    public void moveAwayFromPlayer()
    {
        transform.Translate(basicBehaviour.getVelocity() * -1);
    }

    private void OnCollisionEnter(Collision col)
    {
        if( col.gameObject.name == "Player")
        {
            collidingWithPlayer = true;
        }
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.name == "Player")
        {
            collidingWithPlayer = false;
        }
    }
}
