using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GobboScript : EnemyBase
{
    bool hitStun = false;
    public float gobboDamage;
    /*
     * Higher Scaling factor means slowing scaling in game.
     */
    public int scalingFactor;
    float lastPSCheck;

    //Pathfinding Variables
    public float NextWaypointDistance = 3f;
    Path path;
    int currentWaypoint = 0;
    Rigidbody2D rb;
    Seeker seeker;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        _c = GetComponent<SpriteRenderer>().color;
        seeker = GetComponent<Seeker>();
        lastPSCheck = 0;

        InvokeRepeating("UpdatePath", 0f, .5f);
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
            lastPSCheck += playerScore;

            float scaleFun = Mathf.Pow(2, playerScore) / scalingFactor;

            if(scaleFun > scalingFactor) { scaleFun = 1.5f; }

            health = scaleFun * health;
            gobboDamage *= scaleFun;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        scaleStats(player.GetComponent<PlayerMovement>().score - lastPSCheck);

        if (!roomVars.playerPresent)
        {
            transform.position = resetPoint.transform.position;
        }

        if(!hitStun && roomVars.playerPresent)
        {
            if (path == null)
                return;
            if (currentWaypoint >= path.vectorPath.Count)
                return;

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - (Vector2)transform.position).normalized;
            Vector2 force = direction * speed * 100 * Time.deltaTime;

            rb.AddForce(force);

            //transform.position = Vector3.MoveTowards(transform.position, (Vector2)path.vectorPath[currentWaypoint], speed * Time.deltaTime);

            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distance < NextWaypointDistance)
            {
                currentWaypoint++;
            }           
        }

        if (health<=0)
        {
            player.GetComponent<PlayerMovement>()._health += .5f;
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
