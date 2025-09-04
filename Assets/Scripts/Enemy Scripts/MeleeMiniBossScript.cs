using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MeleeMiniBossScript : EnemyBase
{
    bool rotating;
    float lerpDuration = 0.5f;
    Transform sword;
    bool hitStun = false;
    bool pathStarted = false;
    float timeSinceLastSwing;
    Quaternion rotReset;
    bool pickNextBehaivoir = true;

    public GameObject swordGo;
    public float SwingTime;
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
    public float NextWaypointDistance = 3f;
    Path path;
    int currentWaypoint = 0;
    Seeker seeker;

    public GameObject rangeProjecticle;

    //Sword Rotation Vars
    private Animation animator;

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
        animator = GetComponent<Animation>();
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

    public override IEnumerator hitReg()
    {
        yield return new WaitForSeconds(.25f);
        GetComponent<SpriteRenderer>().color = _c;
        hitStun = false;
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
            swordGo.transform.GetChild(0).GetComponent<MeleeDmgScript>().damage = swordGo.transform.GetChild(0).GetComponent<MeleeDmgScript>().damage * scaleFun;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Three actions that randomly happen:
        //1) Chase Down Player like normal SwordEnemy 45%;
        //2) Circle Sweep Attack (Can be done with animation?) 25%;
        //3) Ranged Attack, like Brall's charge ttack 30%;

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

            //Handles Behaivoir
            if(pickNextBehaivoir)
            {
                int rand = Random.Range(0, 10);
                pickNextBehaivoir = false;

                switch (rand)
                {
                    //Brall Attack
                    case 5:
                    case 8:
                    case 9:
                        StartCoroutine("rangedAttack");
                        break;

                    //Circle Sweep Attack
                    case 6:
                    case 7:
                        StartCoroutine("sweepclose");
                        break;

                    //rand = 1-4 : Sword Enemy Action
                    default:
                        if (!rotating && timeSinceLastSwing >= SwingTime)
                        {
                            StartCoroutine(swing());
                            timeSinceLastSwing = 0;
                        }
                        else
                        {
                            if (timeSinceLastSwing > SwingTime / 3)
                            {
                                sword.rotation = rotReset;
                            }
                            timeSinceLastSwing += Time.deltaTime;
                        }
                        break;
                }
            }


            //Always moves towards player. An unyielding boss
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

            //Resets orientation of Boss to look at player
            Quaternion rotation = Quaternion.LookRotation(
                player.transform.position - transform.position,
                transform.TransformDirection(Vector3.up)
            );

            swordGo.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        }
    }

    IEnumerator behaivoirWait()
    {
        yield return new WaitForSeconds(1f);
        pickNextBehaivoir = true;
    }

    IEnumerator rangedAttack()
    {
        GameObject attack = Instantiate(rangeProjecticle, swordGo.transform.GetChild(0).transform.GetChild(0).transform.position, Quaternion.identity);
        yield return new WaitForSeconds(.5f);
        attack.transform.position = Vector2.MoveTowards(attack.transform.position, player.transform.position, .5f);
        StartCoroutine(behaivoirWait());
    }

    IEnumerator sweepclose()
    {
        GameObject sr = GameObject.FindGameObjectWithTag("swingRadius");
        sr.SetActive(true);
        yield return new WaitForSeconds(.5f);
        sr.SetActive(false);
        animator.Play("MeleeMiniBossCircleSwing");
        StartCoroutine(behaivoirWait());
    }

    IEnumerator swing()
    {
        rotating = true;
        float timeElapsed = 0;
        Quaternion startRotation = swordGo.transform.rotation;

        Quaternion targetRotation;

        if ((player.transform.position.x - transform.position.x) > 0)
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
        }
        sword.rotation = targetRotation;
        rotating = false;

        yield return new WaitForSeconds(5f);
        StartCoroutine(behaivoirWait());
    }
}
