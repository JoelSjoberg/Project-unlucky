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
        it bleeds to death if at 1 health
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
    State followPlayer = new State("FollowPlayer");
    State idle = new State("idle");
    State die = new State("die");
    State attack = new State("attack");
    State bleed = new State("bleed");
    State staggered = new State("staggered");
    State previousState;

    public float followDistance, bleedTimer = 1.5f;
    
    private float bleedBuffer = 0;

    EnemyBehaviour basicBehaviour;
    Renderer renderer;
    Vector3 roamingDestination;
    float roamTime = 6f, roamTimer = 0;

	// Use this for initialization
	void Start () {
        idle.active = true;
        previousState = idle;
        basicBehaviour = GetComponent<EnemyBehaviour>();
        renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        // primary states in hierarchy(die > bleed > attack > follow > idle)
        if (basicBehaviour.player == null)
        {
            setStateFalse();
            idle.active = true;
        }
        else
        {
            die.active = basicBehaviour.health <= 0;
            bleed.active = basicBehaviour.health == 1;
            attack.active = basicBehaviour.collidingWithPlayer && basicBehaviour.health > 1 && basicBehaviour.player.health > 0;
            followPlayer.active = basicBehaviour.inSameRoomAsPlayer() && basicBehaviour.getDistanceFromPlayer() <= followDistance && basicBehaviour.player.health > 0;
            staggered.active = basicBehaviour.staggered;
            idle.active = !die.active && !bleed.active && !attack.active && !followPlayer.active && !staggered.active;
        }
        manageStateMachine();
	}

    // Set every primary state to false
    void setStateFalse()
    {
        followPlayer.active = false; idle.active = false; die.active = false; attack.active = false; bleed.active = false;  staggered.active = false;
    }

    // act according to the current state
    void manageStateMachine()
    {
        if (die.active) Destroy(gameObject);

        else if (bleed.active)
        {
            // set sprite color to red
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
            bleedBuffer += Time.deltaTime;
            if (bleedBuffer >= bleedTimer) basicBehaviour.health = 0;
            if (basicBehaviour.getDistanceFromPlayer() <= followDistance) moveAwayFromPlayer();
            if (basicBehaviour.collidingWithPlayer)
            {
                basicBehaviour.health = 0;
                basicBehaviour.player.heal(1);

                // needs testing
                // FindObjectOfType<TimeController>().slowDown(0.3f); // slow down time to magnify impact
            }
            previousState = bleed;
        }

        else if(staggered.active)
        {
            //transform.position = new Vector3(transform.position.x + Mathf.Sin(Time.deltaTime),transform.position.y, transform.position.z);
            previousState = staggered;
        }

        else if (attack.active) basicBehaviour.player.takeDamage(basicBehaviour.damage);

        else if(followPlayer.active)
        {
            if (previousState == idle) FindObjectOfType<AudioController>().play("EnemyCry"); // notice player from idle state
            moveTowardsPlayer();
            previousState = followPlayer;
        }
        else if(idle.active)
        {
            previousState = idle;
            // Move to a random place when idle
            // get destination in room

            //...Nope
            if(roamTimer >= roamTime)
            {
                roamingDestination = basicBehaviour.getRoom().getRandomRoomPosition(basicBehaviour.offset, transform.position.y);
                roamTimer = 0;
            }
            roamTimer += Time.deltaTime;
            basicBehaviour.moveToDestination(roamingDestination);
            if(basicBehaviour)transform.Translate(basicBehaviour.getVelocity());

        }
    }

    // Define these methods here to create different movement patterns for each enemy
    public void moveTowardsPlayer()
    {
        basicBehaviour.moveToDestination(basicBehaviour.player.transform.position);
        transform.Translate(basicBehaviour.getVelocity());
    }
    public void moveAwayFromPlayer()
    {
        basicBehaviour.moveToDestination(basicBehaviour.player.transform.position);
        basicBehaviour.inverseAxes();
        transform.Translate(basicBehaviour.getVelocity());
    }
}
