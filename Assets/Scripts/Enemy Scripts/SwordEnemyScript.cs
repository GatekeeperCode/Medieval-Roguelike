using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SwordEnemyScript : EnemyBase
{
    bool rotating;
    float lerpDuration = 0.5f;
    Transform sword;
    bool hitStun = false;
    float timeSinceLastSwing;
    Quaternion rotReset;

    public GameObject swordGo;
    public float SwingTime;
    /*
     * Higher Scaling factor means slowing scaling in game.
     */
    public int scalingFactor;
    float lastPSCheck;

    //Pathfinding Variables
    public float NextWaypointDistance = 3f;
    Path path;
    int currentWaypoint = 0;
    Seeker seeker;

    // Start is called before the first frame update
    void Start()
    {
        rotating = false;
        player = GameObject.FindGameObjectWithTag("Player");
        _c = GetComponent<SpriteRenderer>().color;
        sword = swordGo.transform;
        rotReset = sword.rotation;
        timeSinceLastSwing = 10;
        seeker = GetComponent<Seeker>();
        lastPSCheck = 0;

        InvokeRepeating("UpdatePath", 0f, .5f);
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
        if(playerScore>0)
        {
            lastPSCheck += playerScore;

            float scaleFun = Mathf.Pow(2, playerScore) / scalingFactor;

            if (scaleFun > scalingFactor) { scaleFun = 1.5f; }

            health = scaleFun * health;
            swordGo.transform.GetChild(0).GetComponent<MeleeDmgScript>().damage = swordGo.transform.GetChild(0).GetComponent<MeleeDmgScript>().damage * scaleFun;
        }
    }

    // Update is called once per frame
    void Update()
    {
        scaleStats(player.GetComponent<PlayerMovement>().score - lastPSCheck);

        if (!roomVars.playerPresent)
        {
            transform.position = resetPoint.transform.position;
        }
        else if (!hitStun)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < 1)
            {
                if (!rotating && timeSinceLastSwing>=SwingTime)
                {
                    StartCoroutine(swing());
                    timeSinceLastSwing = 0;
                }
                else
                {
                    if(timeSinceLastSwing > SwingTime/3)
                    {
                        sword.rotation = rotReset;
                    }            
                    timeSinceLastSwing += Time.deltaTime;
                }
            }
            else
            {
                if (path == null)
                    return;
                if (currentWaypoint >= path.vectorPath.Count)
                    return;

                transform.position = Vector2.MoveTowards(transform.position, path.vectorPath[currentWaypoint], speed * Time.deltaTime);

                float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);
                if (distance < NextWaypointDistance)
                {
                    currentWaypoint++;
                }
            }

            Quaternion rotation = Quaternion.LookRotation(
                player.transform.position - transform.position,
                transform.TransformDirection(Vector3.up)
            );

            swordGo.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        }

        if (health <= 0)
        {
            player.GetComponent<PlayerMovement>()._physicalStren+=1;
            Destroy(gameObject);
        }
    }

    IEnumerator swing()
    {
        rotating = true;
        float timeElapsed = 0;
        Quaternion startRotation = swordGo.transform.rotation;

        Quaternion targetRotation;

        if ((player.transform.position.x - transform.position.x)>0)
        {
            targetRotation = swordGo.transform.rotation * Quaternion.Euler(0, 0, 125);
        }
        else
        {
            targetRotation = swordGo.transform.rotation * Quaternion.Euler(0, 0, -125);
        }

        rotReset = startRotation;

        while (timeElapsed < lerpDuration)
        {
            sword.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        sword.rotation = targetRotation;
        rotating = false;
    }

    public override IEnumerator hitReg()
    {
        yield return new WaitForSeconds(.25f);
        GetComponent<SpriteRenderer>().color = _c;
        hitStun = false;
    }
}
