using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeEnemyScript : MonoBehaviour
{
    public float health;
    public float speed;
    public GameObject BeeHive;
    public roomVarScript roomVars;
    public float moveMin;
    public float moveMax;

    bool isMoving = false;
    Rigidbody2D rb;
    Color _c;

    // Start is called before the first frame update
    void Start()
    {
        _c = GetComponent<SpriteRenderer>().color;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
                StartCoroutine(hitReg());
            }

            if (collision.gameObject.tag == "Arrow")
            {
                health -= collision.gameObject.GetComponent<ArrowScript>().damage;
                GetComponent<SpriteRenderer>().color = Color.red;
                StartCoroutine(hitReg());
            }
        }
    }

    private IEnumerator hitReg()
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
