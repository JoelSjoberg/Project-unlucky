using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour {

    private EnemyBehaviour baseBehaviour;
    private bossState state = bossState.idle;

    private float timer = 0;
    private float durration = 0;
    [HideInInspector]
    public int scrap;

    public Attractor attractor;
    public Vector3 spawningPos;

    public void spawn(Vector3 pos)
    {
        transform.position = pos;
        spawningPos = pos;
    }

	// Use this for initialization
	void Start ()
    {
        baseBehaviour = GetComponent<EnemyBehaviour>();
        attractor.isActive = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (baseBehaviour.player.health <= 0 || baseBehaviour.health <= 0)
        {
            gameObject.SetActive(false);
            FindObjectOfType<AudioController>().playTheme("GameOver");
        }
       
		switch(state)
        {
            case bossState.idle:
                Idle();
                break;

            case bossState.raging:
                Raging();
                break;

            case bossState.attacking:
                Attacking();
                break;

            case bossState.stomping:
                Stomping();
                break;

            case bossState.rampaging:
                Rampaging();
                break;

            case bossState.specialAttacking:
                specialAttack();
                break;

            case bossState.stunned:
                Stunned();
                break;

            case bossState.draining:
                Draining();
                break;

            case bossState.returning:
                returning();
                break;
        }
	}

    private void Idle()
    {
        // first itteration
        if (timer == 0)
        {
            durration = 10;
            Debug.Log("idle");
            FindObjectOfType<AudioController>().playTheme("Idle");
        }
        if (timer <= durration)
        {
            timer += Time.deltaTime;
            if (baseBehaviour.staggered) timer = durration;
        }
        else
        {
            timer = 0;
            state = bossState.raging;
        }
    }

    private void Raging()
    {
        // first itteration
        if (timer == 0)
        {
            Debug.Log("ROOOAAAARRR");
            durration = 1;
            FindObjectOfType<FollowPlayer>().offset.y = 130;
            FindObjectOfType<FollowPlayer>().offsetYon = 130;
            FindObjectOfType<AudioController>().playTheme("Raging");
            FindObjectOfType<AudioController>().play("Roar");
            FindObjectOfType<FollowPlayer>().shake(durration);
            FindObjectOfType<BossRoomGenerator>().spawnScrap();
        }
        if (timer <= durration) timer += Time.deltaTime;
        else
        {
            timer = 0;
            state = bossState.attacking;
        }
    }

    Vector3 playerPos;
    private void Attacking()
    {

        if (timer == 0)
        {
            durration = 24;
            Debug.Log("attack");
            FindObjectOfType<AudioController>().playTheme("Attacking");
            playerPos = baseBehaviour.player.transform.position;
            baseBehaviour.speed = 150;
        }
        
        // Update state, this is the attack itself: charge, blast away and repeat
        if (timer <= durration)
        {
            timer += Time.deltaTime;
            if(((Vector3)(playerPos - transform.position)).magnitude > 5)moveTowardsPoint(playerPos);
            else
            {
                if (baseBehaviour.collidingWithPlayer) baseBehaviour.player.takeDamage(baseBehaviour.damage);
                if (!wait(1f))
                {
                    playerPos = baseBehaviour.player.transform.position;
                }
            }
        }
        else
        {
            timer = 0;
            state = bossState.stomping;
        }
    }

    // TODO: make debris fall when stomping
    private void Stomping()
    {
        if (timer == 0)
        {
            durration = 13;
            Debug.Log("STOMPING");
            FindObjectOfType<AudioController>().playTheme("Stomping");
            FindObjectOfType<FollowPlayer>().shake(durration);
            FindObjectOfType<BossRoomGenerator>().spawnScrap();
        }
        if (timer <= durration)
        {
            timer += Time.deltaTime;
            if (((Vector3)(baseBehaviour.player.transform.position - transform.position)).magnitude < 30) baseBehaviour.player.takeDamage(baseBehaviour.damage);
            FindObjectOfType<FollowPlayer>().shake(0.1f);
        }
        else
        {
            
            timer = 0;
            state = bossState.rampaging;
            if (baseBehaviour.health <= 15) state = bossState.stunned;
        }
    }

    // same ass attacking but faster and shorter
    private void Rampaging()
    {
        if (timer == 0)
        {
            durration = 12;
            Debug.Log("Rampaging");
            FindObjectOfType<AudioController>().playTheme("Rampaging");
            playerPos = baseBehaviour.player.transform.position;
            baseBehaviour.speed = 250;
        }
        if (timer <= durration)
        {
            timer += Time.deltaTime;
            if (((Vector3)(playerPos - transform.position)).magnitude > 12) moveTowardsPoint(playerPos);
            else
            {
                if (baseBehaviour.collidingWithPlayer) baseBehaviour.player.takeDamage(baseBehaviour.damage);
                if (!wait(0.75f))
                {
                    playerPos = baseBehaviour.player.transform.position;
                }
            }
        }
        else
        {
            timer = 0;
            if (baseBehaviour.health <= 15) state = bossState.stunned;
            else state = bossState.stomping;
        }
    }

    private void specialAttack()
    {
        if (timer == 0)
        {
            durration = 27;
            Debug.Log("SpecialAttack");
            FindObjectOfType<AudioController>().playTheme("SpecialAttacking");
            baseBehaviour.speed = Random.Range(100, 250);
        }
        if (timer <= durration)
        {
            timer += Time.deltaTime;
            if (((Vector3)(playerPos - transform.position)).magnitude > 12) moveTowardsPoint(playerPos);
            if (baseBehaviour.collidingWithPlayer) baseBehaviour.player.takeDamage(baseBehaviour.damage);
            else
            {
                if (!wait(0.8f, 0.01f))
                {
                    playerPos = baseBehaviour.getRoom().getRandomRoomPosition(10, 0);
                    baseBehaviour.speed = Random.Range(100, 250);
                }
            }
        }
        else
        {
            timer = 0;
            state = bossState.attacking;
            if (baseBehaviour.health <= 12) state = bossState.draining;
            baseBehaviour.speed = 150;
        }
    }
    private void Stunned()
    {
        if (timer == 0)
        {
            durration = 1;
            Debug.Log("stunned");
            FindObjectOfType<AudioController>().playTheme("Stunned");
            
        }
        if (timer <= durration) timer += Time.deltaTime;
        else
        {
            timer = 0;
            state = bossState.specialAttacking;

        }
    }
    private void Draining()
    {
        if (timer == 0)
        {
            durration = 17;
            Debug.Log("Draining");
            FindObjectOfType<AudioController>().playTheme("Draining");
            FindObjectOfType<AudioController>().play("ItSeems");
            attractor.isActive = true;
            baseBehaviour.speed = 15;
        }
        if (timer <= durration)
        {
            timer += Time.deltaTime;
            baseBehaviour.player.transform.Translate(((baseBehaviour.player.transform.position - transform.position) * -1).normalized * 1f);
            moveTowardsPlayer();
            if (baseBehaviour.collidingWithPlayer) baseBehaviour.player.takeDamage(baseBehaviour.damage);
        }
        else
        {
            baseBehaviour.speed = 150;
            attractor.isActive = false;
            timer = 0;
            state = bossState.returning;
        }
    }
    private void returning()
    {
        if (timer == 0)
        {
            durration = 10;
            Debug.Log("Returnin");
            FindObjectOfType<AudioController>().playTheme("Return");
            baseBehaviour.speed = 30;
        }
        if (timer <= durration)
        {
            timer += Time.deltaTime;
            if (((Vector3)(spawningPos - transform.position)).magnitude > 12) moveTowardsPoint(spawningPos);
        }
        else
        {
            timer = 0;
            state = bossState.raging;
            baseBehaviour.speed = 150;
        }
    }

    public void moveTowardsPlayer()
    {
        baseBehaviour.moveToDestination(baseBehaviour.player.transform.position);
        transform.Translate(baseBehaviour.getVelocity());
    }
    public void moveTowardsPoint(Vector3 v)
    {
        baseBehaviour.moveToDestination(v);
        transform.Translate(baseBehaviour.getVelocity());
    }

    // wait for t seconds, returns true if waiting, and false if not
    float WaitTimer = 0;
    private bool wait(float t)
    {
        if (WaitTimer == 0) FindObjectOfType<FollowPlayer>().shake(0.1f);
        WaitTimer += Time.deltaTime;
        if (WaitTimer >= t)
        {
            WaitTimer = 0;
            return false;
        }
        else
        {
            return true;
        }
    }
    private bool wait(float t, float magnitude)
    {
        if (WaitTimer == 0)
        {
            FindObjectOfType<FollowPlayer>().shake(magnitude);
            FindObjectOfType<BossRoomGenerator>().spawnScrap(0, 2);
        }
        WaitTimer += Time.deltaTime;
        if (WaitTimer >= t)
        {
            WaitTimer = 0;
            return false;
        }
        else
        {
            return true;
        }
    }
}

public enum bossState
{
    idle,
    raging,
    attacking,
    stomping,
    rampaging,
    specialAttacking,
    stunned,
    draining,
    returning
}