using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GobboScript : EnemyBase
{
    bool hitStun = false;
    bool pathStarted = false;
    public float gobboDamage;
    /*
     * Higher Scaling factor means Higher scaling in game. (A/B in the Desmos Graph)
     */
    [Tooltip("Higher Scaling factor means Higher scaling in game. (A/B in the Desmos Graph)")]
    public float scalingRise;
    /*
     * Higher Scaling Angle means longer power scale. (C in the Desmos Graph)
     */
    [Tooltip("Higher Scaling Angle means longer power scale. (C in the Desmos Graph)")]
    public float scalingLength;
    float lastPSCheck;

    //Pathfinding Variables
    public float NextWaypointDistance;
    Path path;
    int currentWaypoint = 0;
    Rigidbody2D rb;
    Seeker seeker;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _c = GetComponent<SpriteRenderer>().color;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        lastPSCheck = 0;
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
        {
            seeker.StartPath(transform.position, player.transform.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void scaleStats(float playerScore)
    {
        if (playerScore > 0)
        {
            //print(playerScore);
            lastPSCheck += playerScore;

            //Check How much to scale
            float cosAmt = Mathf.Cos(playerScore / scalingLength);
            float sinAmt = Mathf.Sin(playerScore / scalingLength);
            int floor = (int)(playerScore / (scalingLength * Mathf.PI));

            //Scaling Math, Thanks Jaxaar
            float scaleFun = scalingRise * (-(cosAmt * sinAmt) / Mathf.Abs(sinAmt) + (2 * floor)) + scalingRise;
            //float scaleFun = Mathf.Pow(2, playerScore) / scalingRise;

            //if(scaleFun > scalingRise) { scaleFun = 1.5f; }

            //Stats Scaling
            health = scaleFun + health;
            speed = scaleFun + speed;
            gobboDamage *= scaleFun;
        }
    }

    void FixedUpdate()
    {
        if (!roomVars.playerPresent)
        {
            transform.position = resetPoint.transform.position;
        }

        if(!hitStun && roomVars.playerPresent)
        {
            scaleStats(player.GetComponent<PlayerMovement>().score - lastPSCheck);

            if (!pathStarted)
            {
                InvokeRepeating("UpdatePath", 0f, .5f);
                pathStarted = true;
            }

            if (path == null)
                return;
            if (currentWaypoint >= path.vectorPath.Count)
                return;

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - (Vector2)transform.position).normalized;

            Vector2 force = direction * speed * 100 * Time.deltaTime;
            //print(force);

            rb.AddForce(force);

            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distance < NextWaypointDistance)
            {
                currentWaypoint++;
            }

            //transform.position = Vector3.MoveTowards(transform.position, (Vector2)path.vectorPath[currentWaypoint], speed * Time.deltaTime);
        }

        if (health<=0)
        {
            player.GetComponent<PlayerMovement>()._health += .5f;
            player.GetComponent<PlayerMovement>()._exp += .25f;
            Destroy(gameObject);
        }
    }

    public override IEnumerator hitReg()
    {
        hitStun = true;
        yield return new WaitForSeconds(.25f);
        GetComponent<SpriteRenderer>().color = _c;
        hitStun = false;
    }
}
