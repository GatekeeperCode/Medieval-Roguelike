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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _c = GetComponent<SpriteRenderer>().color;
    }

    void scaleStats(float playerScore)
    {
        while (playerScore > 0)
        {
            float scaleFun = (playerScore * playerScore) / scalingFactor;
            health = scaleFun * health;
            gobboDamage *= scaleFun;
            playerScore -= 10;
        }
    }

    // Update is called once per frame
    void Update()
    {
        scaleStats(player.GetComponent<PlayerMovement>().score);

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
