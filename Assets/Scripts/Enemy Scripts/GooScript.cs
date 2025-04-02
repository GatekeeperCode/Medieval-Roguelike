using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooScript : EnemyBase
{
    public GameObject gooPrefab;

    bool hitStun = false;
    float startHealth;
    float statChanges;
    bool isMoving = false;
    public float gooDamage;
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
        startHealth = health;
        lastPSCheck = 0;
        statChanges = 0;
        //print("Health: " + health + ", Start Health: " + startHealth + ", Scale: " + transform.lossyScale.x);
    }

    void scaleStats(float playerScore)
    {
        if (playerScore > 0)
        {
            lastPSCheck += playerScore;

            float scaleFun = Mathf.Pow(2, playerScore) / scalingFactor;

            if (scaleFun > scalingFactor) { scaleFun = 1.5f; }

            statChanges += scaleFun;
            health = scaleFun * health;
            gooDamage *= scaleFun;
        }
    }

    void FixedUpdate()
    {
        if (!hitStun)
        {
            if(roomVars.playerPresent)
            {
                scaleStats(player.GetComponent<PlayerMovement>().score - lastPSCheck);

                if (!isMoving)
                {
                    StartCoroutine(creeping());
                }
            }
            else            
            {
                transform.position = resetPoint.transform.position;
            }
        }

        if (health <= 0)
        {
            if(transform.localScale.x > .25f)
            {
                //Spawning Mini Goo
                float r1 = Random.Range(-1f, 1f);
                float r2 = Random.Range(-1f, 1f);

                GameObject mini1 = Instantiate(gooPrefab, new Vector2(transform.position.x + r1, transform.position.y + r1), Quaternion.identity);
                GameObject mini2 = Instantiate(gooPrefab, new Vector2(transform.position.x + r2, transform.position.y + r2), Quaternion.identity);

                //Adjusting Mini Goo to correct size
                mini1.transform.localScale = new Vector2(mini1.transform.lossyScale.x - .25f, mini1.transform.lossyScale.y - .25f);
                mini2.transform.localScale = new Vector2(mini2.transform.localScale.x - .25f, mini2.transform.localScale.y - .25f);

                if (transform.localScale.x == 1f)
                {
                    mini1.GetComponent<GooScript>().health = startHealth * (mini1.transform.localScale.x);
                    mini1.GetComponent<GooScript>().gooDamage = startHealth * (mini1.transform.localScale.x);
                }
                else if(transform.localScale.x == .75f)
                {
                    mini1.GetComponent<GooScript>().health = startHealth * 3/4 + statChanges;
                    mini1.GetComponent<GooScript>().gooDamage = startHealth * 3/4 + statChanges;
                }
                else
                {
                    mini1.GetComponent<GooScript>().health = startHealth * 3 / 4 + statChanges;
                    mini1.GetComponent<GooScript>().gooDamage = startHealth * 3 / 4 + statChanges;
                }
                mini2.GetComponent<GooScript>().health = mini1.GetComponent<GooScript>().health;
                mini2.GetComponent<GooScript>().gooDamage = mini1.GetComponent<GooScript>().gooDamage;


                mini1.GetComponent<SpriteRenderer>().color = _c;
                mini2.GetComponent<SpriteRenderer>().color = _c;
            }

            player.GetComponent<PlayerMovement>()._magicalStren += .25f;
            Destroy(gameObject);
        }
    }

    public override IEnumerator hitReg()
    {
        yield return new WaitForSeconds(.25f);
        GetComponent<SpriteRenderer>().color = _c;
        hitStun = false;
    }

    private IEnumerator creeping()
    {
        isMoving = true;
        yield return new WaitForSeconds(1);

        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime *2);

        isMoving = false;
    }
}
