using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class WizardScript : EnemyBase
{
    public int scalingFactor;
    public float damage;
    public GameObject fireballObject;
    public GameObject fireballAim;
    public float attackDelay;

    bool playerInRange;
    bool canFire;
    float lastPSCheck;
    float lastAttack;

    //Pathfinding Variables
    public float NextWaypointDistance = 3f;
    Path path;
    int currentWaypoint = 0;
    Seeker seeker;
    bool pathStarted = false;


    // Start is called before the first frame update
    void Start()
    {
        canFire = true;
        player = GameObject.FindGameObjectWithTag("Player");
        _c = GetComponent<SpriteRenderer>().color;
        seeker = GetComponent<Seeker>();
        lastPSCheck = 0;
        lastAttack = 0;
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(transform.position, player.transform.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void scaleStats(float playerScore)
    {
        if (playerScore > 0)
        {
            lastPSCheck += playerScore;

            float scaleFun = Mathf.Pow(2, playerScore) / scalingFactor;

            if (scaleFun > scalingFactor) { scaleFun = 1.5f; }

            health = scaleFun * health;
            damage *= scaleFun;
        }
    }

    // Update is called once per frame
    void Update()
    {
        scaleStats(player.GetComponent<PlayerMovement>().score - lastPSCheck);

        //Aiming at Player
        Vector3 targ = player.transform.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        fireballAim.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (roomVars.playerPresent)
        {
            if(!pathStarted)
            {
                InvokeRepeating("UpdatePath", 0f, .5f);
                pathStarted = true;
            }

            if (Vector2.Distance(player.transform.position, transform.position)>=10 && canFire)
            {
                if (path == null)
                    return;
                if (currentWaypoint >= path.vectorPath.Count)
                    return;

                transform.position = Vector3.MoveTowards(transform.position, (Vector2)path.vectorPath[currentWaypoint], speed * Time.deltaTime);

                float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);
                if (distance < NextWaypointDistance)
                {
                    currentWaypoint++;
                }
            }
            else
            {
                if(canFire)
                {
                    GameObject fireball = Instantiate(fireballObject, fireballAim.transform.GetChild(0).transform.position, Quaternion.identity);
                    fireball.transform.rotation = fireballAim.transform.rotation * Quaternion.Euler(0, 0, 90);
                    fireball.GetComponent<Rigidbody2D>().AddForce(fireball.transform.up * -50);
                    fireball.GetComponent<FireballScript>().damage = damage;
                    lastAttack = 0;
                    canFire = false;
                }
            }
        }
        else
        {
            transform.position = resetPoint.transform.position;
        }

        if(lastAttack >= attackDelay)
        {
            canFire = true;
        }

        lastAttack += Time.deltaTime;

        if (health <= 0)
        {
            player.GetComponent<PlayerMovement>()._magicalStren += 1f;
            Destroy(gameObject);
        }
    }

    public override IEnumerator hitReg()
    {
        yield return new WaitForSeconds(.25f);
        GetComponent<SpriteRenderer>().color = _c;
    }
}
