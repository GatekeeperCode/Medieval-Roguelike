using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearEnemyScript : EnemyBase
{
    bool attacking;
    GameObject spearObject;
    float lerpDuration = 0.5f;
    float stabSpeed = 2;

    public GameObject spearGO;
    /*
     * Higher Scaling factor means slowing scaling in game.
     */
    public int scalingFactor;

    bool hitStun = false;
    float lastPSCheck;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _c = GetComponent<SpriteRenderer>().color;
        spearObject = spearGO.transform.GetChild(0).gameObject;
        lastPSCheck = 0;
    }

    void scaleStats(float playerScore)
    {
        if (playerScore > 0)
        {
            lastPSCheck += playerScore;

            float scaleFun = Mathf.Pow(2, playerScore) / scalingFactor;

            if (scaleFun > scalingFactor) { scaleFun = 1.5f; }

            health = scaleFun * health;
            spearObject.transform.GetComponent<MeleeDmgScript>().damage = spearObject.GetComponent<MeleeDmgScript>().damage * scaleFun;
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
        else if (!hitStun)
        {
            if(Vector2.Distance(transform.position, player.transform.position) < 1)
            {
                if(!attacking)
                {
                    StartCoroutine(thrust());
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }

            Quaternion rotation = Quaternion.LookRotation(
                player.transform.position - transform.position,
                transform.TransformDirection(Vector3.up)
            );

            spearGO.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        }

        if (health <= 0)
        {
            player.GetComponent<PlayerMovement>()._defense += 1;
            Destroy(gameObject);
        }
    }

    IEnumerator thrust()
    {
        attacking = true;
        float timeElapsed = 0;
        Vector3 spearStartLoc = spearObject.transform.localPosition;

        while (timeElapsed < lerpDuration)
        {
            if (timeElapsed < lerpDuration / 2)
            {
                spearObject.transform.position = new Vector2(spearObject.transform.position.x + (spearObject.transform.up.x * stabSpeed * Time.deltaTime), spearObject.transform.position.y + (spearObject.transform.up.y * stabSpeed * Time.deltaTime));
            }
            else
            {
                spearObject.transform.position = new Vector2(spearObject.transform.position.x + (-spearObject.transform.up.x * stabSpeed * Time.deltaTime), spearObject.transform.position.y + (-spearObject.transform.up.y * stabSpeed * Time.deltaTime));
            }

            timeElapsed += Time.deltaTime;
            yield return null;
        }
        spearObject.transform.localPosition = spearStartLoc;
        yield return new WaitForSeconds(2);
        attacking = false;
    }

    public override IEnumerator hitReg()
    {
        yield return new WaitForSeconds(.25f);
        GetComponent<SpriteRenderer>().color = _c;
        hitStun = false;
    }
}
