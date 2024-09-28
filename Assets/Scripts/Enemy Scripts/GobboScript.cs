using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GobboScript : EnemyBase
{
    bool hitStun = false;
    public float gobboDamage;
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
        lastPSCheck = 0;
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
    void Update()
    {
        scaleStats(player.GetComponent<PlayerMovement>().score - lastPSCheck);

        if (!roomVars.playerPresent)
        {
            transform.position = resetPoint.transform.position;
        }

        if(!hitStun && roomVars.playerPresent)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
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
