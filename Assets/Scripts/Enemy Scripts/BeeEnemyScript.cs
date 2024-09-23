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
     * Higher Scaling factor means slowing scaling in game.
     */
    public int scalingFactor;

    bool isMoving = false;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        _c = GetComponent<SpriteRenderer>().color;
        rb = GetComponent<Rigidbody2D>();
    }

    void scaleStats(float playerScore)
    {
        while (playerScore > 0)
        {
            float scaleFun = (playerScore * playerScore) / scalingFactor;
            health = scaleFun * health;
            beeDmg *= scaleFun;
            playerScore -= 10;
        }
    }

    // Update is called once per frame
    void Update()
    {
        scaleStats(player.GetComponent<PlayerMovement>().score);

        if (!roomVars.playerPresent)
        {
            transform.position = BeeHive.transform.position;
            isMoving = false;
        }
        else if(!isMoving)
        {
            StartCoroutine(buzzz());
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

    private Vector3 RandomVector(float min, float max)
    {
        var x = Random.Range(min, max);
        var y = Random.Range(min, max);
        var z = Random.Range(min, max);
        return new Vector3(x, y, z);
    }
}
