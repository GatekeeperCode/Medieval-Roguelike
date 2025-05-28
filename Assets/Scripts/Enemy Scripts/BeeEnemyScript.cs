using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeEnemyScript : EnemyBase
{
    public GameObject BeeHive;
    public float moveMin;
    public float moveMax;
    public float beeDmg;
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

    bool isMoving = false;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _c = GetComponent<SpriteRenderer>().color;
        rb = GetComponent<Rigidbody2D>();
        lastPSCheck = 0;
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
            beeDmg *= scaleFun;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(roomVars.playerPresent)
        {
            scaleStats(player.GetComponent<PlayerMovement>().score - lastPSCheck);

            if (!isMoving)
            {
                StartCoroutine(buzzz());
            }
        }
        else
        {
            transform.position = BeeHive.transform.position;
            isMoving = false;
        }

        if (health <= 0)
        {
            player.GetComponent<PlayerMovement>()._speed += .5f;
            Destroy(gameObject);
        }
    }

    public override IEnumerator hitReg()
    {
        yield return new WaitForSeconds(.25f);
        GetComponent<SpriteRenderer>().color = _c;
    }

    private IEnumerator buzzz()
    {
        isMoving = true;

        while(isMoving)
        {
            rb.velocity = RandomVector(moveMin, moveMax);
            yield return new WaitForSeconds(2);
        }
    }

    private Vector2 RandomVector(float min, float max)
    {
        var x = Random.Range(min, max);
        var y = Random.Range(min, max);
        return new Vector3(x, y);
    }
}
