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
     * Higher Scaling factor means slowing scaling in game.
     */
    public int scalingFactor;
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

            float scaleFun = Mathf.Pow(2, playerScore) / scalingFactor;

            if (scaleFun > scalingFactor) { scaleFun = 1.5f; }

            health = scaleFun * health;
            minoDamage *= scaleFun;
        }
    }

    // Update is called once per frame
    void Update()
    {
        scaleStats(player.GetComponent<PlayerMovement>().score - lastPSCheck);

        if (!roomVars.playerPresent)
        {
            transform.position = resetPoint.transform.position;
        }

        if (roomVars.playerPresent)
        {
            if(!isCharging)
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
        var chargeTarget1 = transform.position + transform.right * 10;
        var chargeTarget2 = transform.position + -transform.right * 10;

        if(Vector2.Distance(chargeTarget1, player.transform.position)< Vector2.Distance(chargeTarget2, player.transform.position))
        {
            chargeTarget = chargeTarget1;
        }
        else
        {
            chargeTarget = chargeTarget2;
        }
    }
}
