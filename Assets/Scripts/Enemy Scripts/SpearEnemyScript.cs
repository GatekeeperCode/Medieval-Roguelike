using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearEnemyScript : MonoBehaviour
{
    bool attacking;
    GameObject spearObject;
    float lerpDuration = 0.5f;
    float stabSpeed = 2;

    GameObject player;
    public float health;
    public float speed;
    public GameObject resetPoint;
    public roomVarScript roomVars;
    public GameObject spearGO;

    bool hitStun = false;
    Color _c;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _c = GetComponent<SpriteRenderer>().color;
        spearObject = spearGO.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
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

            transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Floor")
        {
            if (collision.gameObject.tag == "Sword" || collision.gameObject.tag == "Spear")
            {
                health -= collision.gameObject.GetComponent<MeleeDmgScript>().damage;
                GetComponent<SpriteRenderer>().color = Color.red;
                hitStun = true;
                StartCoroutine(hitReg());
            }

            if (collision.gameObject.tag == "Arrow")
            {
                health -= collision.gameObject.GetComponent<ArrowScript>().damage;
                GetComponent<SpriteRenderer>().color = Color.red;
                hitStun = true;
                StartCoroutine(hitReg());
            }
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

    private IEnumerator hitReg()
    {
        yield return new WaitForSeconds(.25f);
        GetComponent<SpriteRenderer>().color = _c;
        hitStun = false;
    }
}
