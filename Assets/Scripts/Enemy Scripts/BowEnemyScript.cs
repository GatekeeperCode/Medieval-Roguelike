using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowEnemyScript : EnemyBase
{
    public GameObject bowGO;
    public GameObject projectile;

    public float dmg;
    public float fireRate;
    public float range;

    GameObject bowObject;
    bool hitStun = false;

    // Start is called before the first frame update
    void Start()
    {
        bowObject = bowGO.transform.GetChild(0).gameObject;
        bowObject.SetActive(true);

        player = GameObject.FindGameObjectWithTag("Player");
        _c = GetComponent<SpriteRenderer>().color;

        StartCoroutine("fireArrow");
    }

    // Update is called once per frame
    void Update()
    {
        if (roomVars.playerPresent)
        {
            Vector3 targ = player.transform.position;
            targ.z = 0f;

            Vector3 objectPos = transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            bowGO.transform.parent.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            if (Vector2.Distance(transform.position, player.transform.position) > range && !hitStun)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
        }
        else
        {
            transform.position = resetPoint.transform.position;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator fireArrow()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(fireRate - 2f, fireRate + 2f));

            if (roomVars.playerPresent && !hitStun && Vector2.Distance(transform.position, player.transform.position) < range)
            {
                GameObject arrow = Instantiate(projectile, bowObject.transform.position, Quaternion.identity);
                arrow.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 90);
                arrow.GetComponent<Rigidbody2D>().AddForce(arrow.transform.up * -50);
                arrow.GetComponent<ArrowScript>().damage = dmg;
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
