using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooScript : EnemyBase
{
    public GameObject gooPrefab;

    bool hitStun = false;
    float startHealth;
    bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _c = GetComponent<SpriteRenderer>().color;
        startHealth = health;
        print("Health: " + health + ", Start Health: " + startHealth + ", Scale: " + transform.lossyScale.x);
    }

    // Update is called once per frame
    void Update()
    {
        if (!hitStun)
        {

            Quaternion rotation = Quaternion.LookRotation(
                player.transform.position - transform.position,
                transform.TransformDirection(Vector3.up)
            );
            transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);

            if(!isMoving)
            {
                StartCoroutine(creeping());
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
                }
                else if(transform.localScale.x == .75f)
                {
                    mini1.GetComponent<GooScript>().health = startHealth * 4/3 * mini1.transform.localScale.x;
                }
                else
                {
                    mini1.GetComponent<GooScript>().health = startHealth * 2 * mini1.transform.localScale.x;
                }
                mini2.GetComponent<GooScript>().health = mini1.GetComponent<GooScript>().health;

                mini1.GetComponent<SpriteRenderer>().color = _c;
                mini2.GetComponent<SpriteRenderer>().color = _c;
            }

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
