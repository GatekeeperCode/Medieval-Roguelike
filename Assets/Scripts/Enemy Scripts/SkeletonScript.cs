using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonScript : EnemyBase
{
    public GameObject arrowPrefab;
    public float fireRate;
    public float skeleDmg;

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
            Quaternion rotation = Quaternion.LookRotation(
                player.transform.position - transform.position,
                transform.TransformDirection(Vector3.up)
                );
            transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
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
            yield return new WaitForSeconds(fireRate);

            if (roomVars.playerPresent && !hitStun)
            {
                GameObject arrow = Instantiate(arrowPrefab, transform.GetChild(0).transform.position, Quaternion.identity);
                arrow.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 90);
                arrow.GetComponent<Rigidbody2D>().AddForce(arrow.transform.up * 50);
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
