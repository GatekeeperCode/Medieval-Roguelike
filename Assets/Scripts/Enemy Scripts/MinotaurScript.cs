using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurScript : EnemyBase
{
    public float chargeBueildupTime;

    bool isCharging;
    Rigidbody2D _rbody;
    Vector3 chargeTarget;
    public float minoDamage;
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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _c = GetComponent<SpriteRenderer>().color;
        _rbody = GetComponent<Rigidbody2D>();
        lastPSCheck = 0;

        StartCoroutine("charge");
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
            minoDamage *= scaleFun;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!roomVars.playerPresent)
        {
            transform.position = resetPoint.transform.position;
        }
        else
        {
            scaleStats(player.GetComponent<PlayerMovement>().score - lastPSCheck);

            if (!isCharging)
            {
                //Stop Movement for prior charges
                _rbody.velocity = Vector2.zero;
            }
            if(isCharging)
            {
                transform.position = Vector3.MoveTowards(transform.position, chargeTarget, speed * Time.deltaTime);

                if(Vector3.Distance(transform.position, chargeTarget) < .5f)
                {
                    _rbody.velocity = Vector2.zero;
                    endCharge();
                }
            }
        }

        if (health <= 0)
        {
            player.GetComponent<PlayerMovement>()._physicalStren += .5f;
            player.GetComponent<PlayerMovement>()._defense += .5f;
            player.GetComponent<PlayerMovement>()._exp += .5f;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Floor")
        {
            endCharge();
        }
    }

    void endCharge()
    {
        isCharging = false;
        StartCoroutine("charge");
    }

    public override IEnumerator hitReg()
    {
        yield return new WaitForSeconds(.25f);
        GetComponent<SpriteRenderer>().color = _c;
    }

    private IEnumerator charge()
    {
        float chargeTime = Random.Range(chargeBueildupTime - .5f, chargeBueildupTime + .5f);

        yield return new WaitForSeconds(chargeTime);
        isCharging = true;
        //Setting end position
        var chargeTarget1 = player.transform.position;

        chargeTarget = chargeTarget1;
    }
}
