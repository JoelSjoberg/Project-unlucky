using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// without proper knowledge about state machines and game ai in general i defien the ai as follows:
/* 
    - it has a set of bools that define each state
    - for each state we create a function that in depth describes the behaviour for said state
    - each state is sorted in a hierarchy in a if - else if - else statement,
        where the most important state is on top and least important is at the bottom
    - after excecuting the die method, this enemy will be Destroyed and deleted from its room
    - this is the basic slime ai hierarchy,
        it dies if health >= 0,
        it attacks if it collides with the player and deals damage,
        it follows the player if he/she enters within a given radius,
        if none of these states are true: it's idle
I also propose that we make it possible for the enemy to have a set of sub states outside of the hierarchy
 e.g. this enemy will begin running away from the player if its health == 1 and the enemy will get a change in color.
    should the player collide with this slime the player will "eat" it up an recieve a slight boost in health or ammo
    if collision occurs within the bleemTime, or else the enemy will bleed out and die
*/ 
public class SlimeBehaviour : MonoBehaviour {

    // primary states
    bool followPlayer = false, idle = false, die = false, attack = false;

    // secondary states
    bool collidingWithPlayer = false;

    public float followDistance, bleedTimer = 1.5f;
    private float bleedBuffer = 0;

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
        // primary states in hierarchy(die > attack > follow > idle)
        if(basicBehaviour.health <= 0)
        {
            setStateFalse();
            die = true;
        }
        else if(collidingWithPlayer && basicBehaviour.health > 1)
        {
            setStateFalse();
            attack = true;
            
        }
        else if (basicBehaviour.inSameRoomAsPlayer())
        {
            if(basicBehaviour.getDistanceFromPlayer() <= followDistance)
            {
                setStateFalse();
                followPlayer = true;
            }

            if (basicBehaviour.health == 1)
            {
                followPlayer = false;
                // set sprite color to blue
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.blue;
                bleedBuffer += Time.deltaTime;
                if (bleedBuffer >= bleedTimer) basicBehaviour.health = 0;
                if (basicBehaviour.getDistanceFromPlayer() <= followDistance) moveAwayFromPlayer();
                if (collidingWithPlayer) basicBehaviour.health = 0;
            }
        }
        else
        {
            setStateFalse();
            idle = true;
        }
        manageStateMachine();
	}

    // Set every primary state to false
    void setStateFalse()
    {
        followPlayer = false; idle = false; die = false; attack = false;
    }

    // act according to the current state
    void manageStateMachine()
    {
        if (die) Destroy(gameObject);

        if (attack) basicBehaviour.player.takeDamage(basicBehaviour.damage);

        if(followPlayer)
        {
            moveTowardsPlayer();
        }
    }

    // In all farness these should probably not be here. These methods are a biproduct of the admittedly poor colision detection I wrote
    // in desperation when the physics-engine started letting the player leave the rooms if it went fast enough.
    public void moveTowardsPlayer()
    {
        transform.Translate(basicBehaviour.getVelocity());
    }
    public void moveAwayFromPlayer()
    {
        basicBehaviour.inverseAxes();
        transform.Translate(basicBehaviour.getVelocity());
    }


    // TODO: implement these in the enemyBehaviour instead so we don't have to define collision behavour for every enemy!
    // (collision should still be possible to implement if a certain enemy should react in a certain way that differst from enemyBehaviour)
    // This method is used to see if the object is coliding with the player.
    private void OnCollisionEnter(Collision col)
    {
        if( col.gameObject.name == "Player")
        {
            collidingWithPlayer = true;
        }
    }
    
    // collision signal if the player no longer touches the agent
    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.name == "Player")
        {
            collidingWithPlayer = false;
        }
    }
}
