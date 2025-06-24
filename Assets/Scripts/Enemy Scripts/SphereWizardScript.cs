using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SphereWizardScript : EnemyBase
{
    public float damage;
    public GameObject elementPrefab;
    public GameObject elementRotation;
    bool hitStun;
    bool pathStarted = false;
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
    public List<GameObject> elements = new List<GameObject>();

    //Pathfinding Variables
    public float NextWaypointDistance = 3f;
    Path path;
    int currentWaypoint = 0;
    Seeker seeker;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _c = GetComponent<SpriteRenderer>().color;
        hitStun = false;
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
            damage *= scaleFun;
        }
    }

    // Update is called once per frame
    void Update()
    {
        elementRotation.transform.Rotate(0, 0, 50 * Time.deltaTime);

        if (!roomVars.playerPresent)
        {
            transform.position = resetPoint.transform.position;
        }
        else if(!hitStun)
        {
            scaleStats(player.GetComponent<PlayerMovement>().score - lastPSCheck);

            if (!pathStarted)
            {
                InvokeRepeating("UpdatePath", 0f, .5f);
                StartCoroutine("elementSpawn");
                pathStarted = true;
            }

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

        if (health <= 0)
        {
            player.GetComponent<PlayerMovement>()._defense += .5f;
            Destroy(gameObject);
        }
    }

    private IEnumerator elementSpawn()
    {
        yield return new WaitForSeconds(3f);
        int numElements = elements.Count;

        if(numElements<3)
        {
            GameObject newElement = Instantiate(elementPrefab);
            newElement.GetComponent<ElementCircleScript>().damage = damage;
            newElement.transform.parent = elementRotation.transform;
            elements.Add(newElement);


            for (int i = 0; i < numElements+1; ++i)
            {
                float theta = (2 * Mathf.PI / (numElements+1)) * i;
                float x = elementRotation.transform.position.x + Mathf.Cos(theta)* 1.75f;
                float y = elementRotation.transform.position.y + Mathf.Sin(theta) * 1.75f;
                elements[i].transform.position = new Vector2(x, y);
            }
        }

        StartCoroutine("elementSpawn");
    }

    public override IEnumerator hitReg()
    {
        hitStun = true;
        yield return new WaitForSeconds(.25f);
        GetComponent<SpriteRenderer>().color = _c;
        hitStun = false;
    }
}
