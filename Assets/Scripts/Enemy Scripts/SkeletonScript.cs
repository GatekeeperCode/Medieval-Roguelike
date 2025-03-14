using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonScript : EnemyBase
{
    public GameObject arrowPrefab;
    public float fireRate;
    public float skeleDmg;
    public float range;
    /*
     * Higher Scaling factor means slowing scaling in game.
     */
    public int scalingFactor;
    float lastPSCheck;

    bool hitStun = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _c = GetComponent<SpriteRenderer>().color;
        lastPSCheck = 0;

        StartCoroutine("fireArrow");
    }

    void scaleStats(float playerScore)
    {
        if (playerScore > 0)
        {
            lastPSCheck += playerScore;

            float scaleFun = Mathf.Pow(2, playerScore) / scalingFactor;

            if (scaleFun > scalingFactor) { scaleFun = 1.5f; }
            health = scaleFun * health;
            skeleDmg *= scaleFun;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (roomVars.playerPresent)
        {
            scaleStats(player.GetComponent<PlayerMovement>().score - lastPSCheck);

            Vector3 targ = player.transform.position;
            targ.z = 0f;

            Vector3 objectPos = transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        if (health <= 0)
        {
            player.GetComponent<PlayerMovement>()._rangeStren += 1;
            Destroy(gameObject);
        }
    }

    IEnumerator fireArrow()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(fireRate-2f, fireRate+2f));

            if (roomVars.playerPresent && !hitStun && Vector2.Distance(transform.position, player.transform.position) < range)
            {
                GameObject arrow = Instantiate(arrowPrefab, transform.GetChild(0).transform.position, Quaternion.identity);
                arrow.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 90);
                arrow.GetComponent<Rigidbody2D>().AddForce(arrow.transform.up * -50);
                arrow.GetComponent<ArrowScript>().damage = skeleDmg;
            }
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
