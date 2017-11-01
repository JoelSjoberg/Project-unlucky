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

	public float offset = 0.0f;
	public float degrees;
    public float followDistance;
    EnemyBehaviour basicBehaviour;

	float rotation;
	Vector3 difference;
	Vector3 enemyPos;

    SpriteRenderer renderer;
	Animator anim;
	// Use this for initialization
	void Start () {
        idle = true;
        basicBehaviour = GetComponent<EnemyBehaviour>();
        
		anim = GetComponent<Animator>();
		renderer = GetComponent<SpriteRenderer>();

		//Rotate the object 45 degrees around the x axis so the sprite is visible
		//transform.rotation = Quaternion.Euler(45, 0, 0);
		//enlarge enemy by 3 units
		transform.localScale = new Vector3(10,10,10);
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
			// Take enemy position
			enemyPos = Input.mousePosition;

			// Vector from player to enemy
			difference = Camera.main.ScreenToWorldPoint(enemyPos) - transform.position;
			difference.Normalize();

			// get angle of vector from player to cursor
			rotation = Mathf.Atan2(difference.z, difference.x) * Mathf.Rad2Deg + offset;
			if (rotation < 0) rotation += 360;  // Keep rotation in range 0 - 360

			degrees = rotation;

			if (degrees < 90 || degrees > 270) renderer.flipX = false;
			else renderer.flipX = true;

			// Play different anmations depending on player position
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

            if(basicBehaviour.getDistanceFromPlayer() <= followDistance)
            {
                setStateFalse();
                followPlayer = true;
				//Play walking animation
				anim.SetBool("Walking", true);
            }

            if (basicBehaviour.health == 1)
            {
                followPlayer = false;
                renderer.material.color = Color.red;
                if (basicBehaviour.getDistanceFromPlayer() <= followDistance) moveAwayFromPlayer();
                if (collidingWithPlayer) basicBehaviour.health = 0;
            }
        }
        else
        {
            setStateFalse();
            idle = true;
			//Play Idle animation
			anim.SetBool("Walking", false);
        }
        manageStateMachine();
	}

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

    public void moveTowardsPlayer()
    {
        transform.Translate(basicBehaviour.getVelocity());
    }
    public void moveAwayFromPlayer()
    {
        basicBehaviour.inverseAxes();
        transform.Translate(basicBehaviour.getVelocity());
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
