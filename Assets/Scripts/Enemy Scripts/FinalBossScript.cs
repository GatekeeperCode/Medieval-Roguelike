using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FinalBossScript : EnemyBase
{
    bool hitStun = false;
    bool nextAttackStarted = false;
    bool canMove = true;
    bool pathStarted = false;
    public GameObject gameOverMenu;
    public float bossDmg;
    public GameObject[] retreatSpot;
    public GameObject chargingCircle;
    public GameObject attackingCircle;
    public GameObject attackBox;
    public GameObject rangeScope;

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
        //attackNum = 3;

        if (canMove && roomVars.playerPresent && !hitStun)
        {
            if(!pathStarted)
            {
                InvokeRepeating("UpdatePath", 0f, .5f);
                pathStarted = true;
            }

            if (!nextAttackStarted)
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

                    nextAttackStarted = true;
                    StartCoroutine("nextAttack");
                }
                else if (attackNum == 2) //Charges short AOE burst
                {
                    if (!nextAttackStarted)
                    {
                        nextAttackStarted = true;
                        StartCoroutine("chargeAttack");
                    }
                }
                else if (attackNum == 3)//Ranged Attack at player
                {
                    if (!nextAttackStarted)
                    {
                        nextAttackStarted = true;
                        StartCoroutine("rangeAttack");
                    }
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
        }
        else if(!canMove)
        {
            target = player;

            if (path == null)
                return;
            if (currentWaypoint >= path.vectorPath.Count)
                return;

            transform.position = Vector2.MoveTowards(transform.position, path.vectorPath[currentWaypoint], speed * .5f * Time.deltaTime);

            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distance < NextWaypointDistance)
            {
                currentWaypoint++;
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
        yield return new WaitForSeconds(Random.Range(0, 8));
        attackNum = Random.Range(1, 4);
        nextAttackStarted = false;
        canMove = true;
    }

    private void WaypointPicker()
    {
        nextCower = Random.Range(0, retreatSpot.Length);
    }

    public IEnumerator chargeAttack()
    {
        chargingCircle.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        chargingCircle.SetActive(false);
        attackingCircle.SetActive(true);
        StartCoroutine("endChargeAttack");
    }

    public IEnumerator endChargeAttack()
    {
        yield return new WaitForSeconds(.5f);
        attackingCircle.SetActive(false);
        canMove = false;
        StartCoroutine("nextAttack");
    }

    public IEnumerator rangeAttack()
    {
        Vector3 dir = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
        GameObject[] attackBoxes = new GameObject[5];

        for(int i=0; i<5; i++)
        {
            attackBoxes[i] = Instantiate(attackBox, transform.position + (dir*3.2f*i), rangeScope.transform.rotation);
        }

        yield return new WaitForSeconds(1f);
        
        for(int i=0; i<5; i++)
        {
            attackBoxes[i].GetComponent<BoxCollider2D>().enabled = true;
            Color tmp = attackBoxes[i].GetComponent<SpriteRenderer>().color;
            tmp.a = 226f;
            attackBoxes[i].GetComponent<SpriteRenderer>().color = tmp;
        }


        StartCoroutine("endRangeAttack", attackBoxes);
    }

    public IEnumerator endRangeAttack(GameObject[] attackBoxes)
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 5; i++)
        {
            Destroy(attackBoxes[i]);
        }
        canMove = false;
        StartCoroutine("nextAttack");
    }
}
