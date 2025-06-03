using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SpearEnemyScript : EnemyBase
{
    bool attacking;
    GameObject spearObject;
    float lerpDuration = 0.5f;
    float stabSpeed = 2;
    bool pathStarted = false;

    //Pathfinding Variables
    public float NextWaypointDistance = 3f;
    Path path;
    int currentWaypoint = 0;
    Seeker seeker;

    public GameObject spearGO;
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

    bool hitStun = false;
    float lastPSCheck;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _c = GetComponent<SpriteRenderer>().color;
        spearObject = spearGO.transform.GetChild(0).gameObject;
        seeker = GetComponent<Seeker>();
        lastPSCheck = 0;
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
            spearObject.transform.GetComponent<MeleeDmgScript>().damage = spearObject.GetComponent<MeleeDmgScript>().damage * scaleFun;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!roomVars.playerPresent)
        {
            transform.position = resetPoint.transform.position;
        }
        else if (!hitStun)
        {
            scaleStats(player.GetComponent<PlayerMovement>().score - lastPSCheck);

            if (!pathStarted)
            {
                InvokeRepeating("UpdatePath", 0f, .5f);
                pathStarted = true;
            }

            if (Vector2.Distance(transform.position, player.transform.position) < 1)
            {
                if(!attacking)
                {
                    StartCoroutine(thrust());
                }
            }
            else
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

            Quaternion rotation = Quaternion.LookRotation(
                player.transform.position - transform.position,
                transform.TransformDirection(Vector3.up)
            );

            spearGO.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        }

        if (health <= 0)
        {
            player.GetComponent<PlayerMovement>()._defense += 1;
            Destroy(gameObject);
        }
    }

    IEnumerator thrust()
    {
        attacking = true;
        float timeElapsed = 0;
        Vector3 spearStartLoc = spearObject.transform.localPosition;

        while (timeElapsed < lerpDuration)
        {
            if ((player.transform.position.x - transform.position.x) > 0)
            {
                if (timeElapsed < lerpDuration / 2)
                {
                    spearObject.transform.position = new Vector2(spearObject.transform.position.x + (spearObject.transform.up.x * stabSpeed * Time.deltaTime), spearObject.transform.position.y + (spearObject.transform.up.y * stabSpeed * Time.deltaTime));
                }
                else
                {
                    spearObject.transform.position = new Vector2(spearObject.transform.position.x + (-spearObject.transform.up.x * stabSpeed * Time.deltaTime), spearObject.transform.position.y + (-spearObject.transform.up.y * stabSpeed * Time.deltaTime));
                }
            }
            else
            {
                if (timeElapsed < lerpDuration / 2)
                {
                    spearObject.transform.position = new Vector2(spearObject.transform.position.x + (-spearObject.transform.up.x * stabSpeed * Time.deltaTime), spearObject.transform.position.y + (-spearObject.transform.up.y * stabSpeed * Time.deltaTime));
                }
                else
                {
                    spearObject.transform.position = new Vector2(spearObject.transform.position.x + (spearObject.transform.up.x * stabSpeed * Time.deltaTime), spearObject.transform.position.y + (spearObject.transform.up.y * stabSpeed * Time.deltaTime));
                }
            }

            timeElapsed += Time.deltaTime;
            yield return null;
        }
        spearObject.transform.localPosition = spearStartLoc;
        yield return new WaitForSeconds(2);
        attacking = false;
    }

    public override IEnumerator hitReg()
    {
        yield return new WaitForSeconds(.25f);
        GetComponent<SpriteRenderer>().color = _c;
        hitStun = false;
    }
}
