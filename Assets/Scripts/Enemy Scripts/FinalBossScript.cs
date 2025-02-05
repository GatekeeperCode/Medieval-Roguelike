using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FinalBossScript : EnemyBase
{
    bool hitStun = false;
    bool nextAttackStarted = false;
    public GameObject gameOverMenu;
    public float bossDmg;
    public GameObject[] retreatSpot;

    int nextCower;
    GameObject target;

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
    int attackNum;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player;
        _c = GetComponent<SpriteRenderer>().color;
        seeker = GetComponent<Seeker>();
        lastPSCheck = 0;
        attackNum = Random.Range(1, 4);
        nextCower = 0;

        InvokeRepeating("UpdatePath", 0f, .5f);
        InvokeRepeating("WaypointPicker", 0f, 6);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(transform.position, target.transform.position, OnPathComplete);
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
            bossDmg *= scaleFun;
        }
    }

    // Update is called once per frame
    void Update()
    {
        scaleStats(player.GetComponent<PlayerMovement>().score - lastPSCheck);

        if(roomVars.playerPresent && !hitStun)
        {
            if (attackNum == 1) //Charges at player
            {
                target = player;

                if (path == null)
                    return;
                if (currentWaypoint >= path.vectorPath.Count)
                    return;

                transform.position = Vector2.MoveTowards(transform.position, path.vectorPath[currentWaypoint], speed * 2 * Time.deltaTime);

                float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);
                if (distance < NextWaypointDistance)
                {
                    currentWaypoint++;
                }

                if(!nextAttackStarted)
                {
                    StartCoroutine("nextAttack");
                }
            }
            else if (attackNum == 2) //Charges short AOE burst
            {
                nextAttackStarted = true;
                StartCoroutine("chargeAttack");
            }
            else if (attackNum == 3)//Ranged Attack at player
            {
                nextAttackStarted = true;
                StartCoroutine("rangeAttack");
            }
            else //Runs and hides from player to spots in throne room.
            {
                target = retreatSpot[nextCower];

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
        }

        




        if(health<=0)
        {
            Time.timeScale = 0;
            gameOverMenu.SetActive(true);
        }
    }

    public override IEnumerator hitReg()
    {
        hitStun = true;
        yield return new WaitForSeconds(.1f);
        GetComponent<SpriteRenderer>().color = _c;
        hitStun = false;
    }

    public IEnumerator nextAttack()
    {
        nextAttackStarted = true;
        yield return new WaitForSeconds(Random.Range(0, 8));
        attackNum = Random.Range(1, 4);
        nextAttackStarted = false;
    }

    private void WaypointPicker()
    {
        nextCower = Random.Range(0, retreatSpot.Length);
    }

    public IEnumerator chargeAttack()
    {
        yield return new WaitForSeconds(1.5f);
        print("Charged Attack Happens Now");
        attackNum = Random.Range(1, 4);
        nextAttackStarted = false;
    }

    public IEnumerator rangeAttack()
    {
        yield return new WaitForSeconds(1f);
        print("Ranged Attack Happens Now");
        attackNum = Random.Range(1, 4);
        nextAttackStarted = false;
    }
}
