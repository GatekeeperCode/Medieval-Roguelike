using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GobboScript : EnemyBase
{
    bool hitStun = false;
    public float gobboDamage;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _c = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if(!roomVars.playerPresent)
        {
            transform.position = resetPoint.transform.position;
        }

        if(!hitStun && roomVars.playerPresent)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

        if (health<=0)
        {
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
