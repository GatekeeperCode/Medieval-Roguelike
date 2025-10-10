using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RangedMiniBossScript : EnemyBase
{
    public GameObject bowGO;
    public GameObject projectile;

    public float dmg;
    public float fireRate;
    public float range;
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

    GameObject bowObject;
    bool hitStun = false;

    //Pathfinding Variables
    public float NextWaypointDistance = 3f;
    Path path;
    int currentWaypoint = 0;
    Rigidbody2D rb;
    Seeker seeker;

    // Start is called before the first frame update
    void Start()
    {
        bowObject = bowGO.transform.GetChild(0).gameObject;
        bowObject.SetActive(true);

        player = GameObject.FindGameObjectWithTag("Player");
        _c = GetComponent<SpriteRenderer>().color;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        lastPSCheck = 0;

        StartCoroutine("fireArrow");
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
        if (playerScore > 0)
        {
            lastPSCheck += playerScore;

            //Check How much to scale
            float cosAmt = Mathf.Cos(playerScore / scalingLength);
            float sinAmt = Mathf.Sin(playerScore / scalingLength);
            int floor = (int)(playerScore / (scalingLength * Mathf.PI));

            //Scaling Math, Thanks Jaxaar
            float scaleFun = scalingRise * (-(cosAmt * sinAmt) / Mathf.Abs(sinAmt) + (2 * floor)) + scalingRise;


            health = scaleFun * health;
            bowGO.GetComponent<RangedDmgScript>().damage *= scaleFun;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (roomVars.playerPresent)
        {
            scaleStats(player.GetComponent<PlayerMovement>().score - lastPSCheck);

            Vector3 targ = player.transform.position;
            targ.z = 0f;

            Vector3 objectPos = transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            bowGO.transform.parent.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            if (Vector2.Distance(transform.position, player.transform.position) > range && !hitStun)
            {
                if (path == null)
                    return;
                if (currentWaypoint >= path.vectorPath.Count)
                    return;

                //Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - (Vector2)transform.position).normalized;
                //Vector2 force = direction * speed * 100 * Time.deltaTime;

                //rb.AddForce(force);

                transform.position = Vector3.MoveTowards(transform.position, path.vectorPath[currentWaypoint], speed * Time.deltaTime);

                float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);
                if (distance < NextWaypointDistance)
                {
                    currentWaypoint++;
                }

                //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
        }
        else
        {
            transform.position = resetPoint.transform.position;
        }

        if (health <= 0)
        {
            player.GetComponent<PlayerMovement>()._rangeStren += .5f;
            player.GetComponent<PlayerMovement>()._defense += .5f;
            Destroy(gameObject);
        }
    }

    IEnumerator fireArrow()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(fireRate - 2f, fireRate + 2f));

            if (roomVars.playerPresent && !hitStun && Vector2.Distance(transform.position, player.transform.position) < range)
            {
                GameObject arrow = Instantiate(projectile, bowObject.transform.position, Quaternion.identity);
                arrow.transform.rotation = bowObject.transform.rotation * Quaternion.Euler(0, 0, 90);
                arrow.GetComponent<Rigidbody2D>().AddForce(arrow.transform.up * -50);
                arrow.GetComponent<ArrowScript>().damage = dmg;
            }
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
