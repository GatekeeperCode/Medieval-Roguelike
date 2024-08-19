using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonScript : EnemyBase
{
    public GameObject arrowPrefab;
    public float fireRate;
    public float skeleDmg;
    public float range;

    bool hitStun = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _c = GetComponent<SpriteRenderer>().color;

        StartCoroutine("fireArrow");
    }

    // Update is called once per frame
    void Update()
    {
        if(roomVars.playerPresent)
        {
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
        yield return new WaitForSeconds(.25f);
        GetComponent<SpriteRenderer>().color = _c;
        hitStun = false;
    }
}
