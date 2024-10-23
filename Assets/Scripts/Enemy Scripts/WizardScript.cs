using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardScript : EnemyBase
{
    public int scalingFactor;
    public float damage;
    public GameObject fireballObject;
    public GameObject fireballAim;
    public float attackDelay;

    bool playerInRange;
    bool canFire;
    float lastPSCheck;
    float lastAttack;

    // Start is called before the first frame update
    void Start()
    {
        playerInRange = false;
        canFire = true;
        player = GameObject.FindGameObjectWithTag("Player");
        _c = GetComponent<SpriteRenderer>().color;
        lastPSCheck = 0;
        lastAttack = 0;
    }

    void scaleStats(float playerScore)
    {
        if (playerScore > 0)
        {
            lastPSCheck += playerScore;

            float scaleFun = Mathf.Pow(2, playerScore) / scalingFactor;

            if (scaleFun > scalingFactor) { scaleFun = 1.5f; }

            health = scaleFun * health;
            damage *= scaleFun;
        }
    }

    // Update is called once per frame
    void Update()
    {
        scaleStats(player.GetComponent<PlayerMovement>().score - lastPSCheck);

        //Aiming at Player
        Vector3 targ = player.transform.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        fireballAim.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (roomVars.playerPresent)
        {
            if (!playerInRange && canFire)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
            else
            {
                if(canFire)
                {
                    GameObject fireball = Instantiate(fireballObject, fireballAim.transform.GetChild(0).transform.position, Quaternion.identity);
                    fireball.transform.rotation = fireballAim.transform.rotation * Quaternion.Euler(0, 0, 90);
                    fireball.GetComponent<Rigidbody2D>().AddForce(fireball.transform.up * -50);
                    fireball.GetComponent<FireballScript>().damage = damage;
                    lastAttack = 0;
                    canFire = false;
                }
            }
        }
        else
        {
            transform.position = resetPoint.transform.position;
        }

        if(lastAttack >= attackDelay)
        {
            canFire = true;
        }

        lastAttack += Time.deltaTime;

        if (health <= 0)
        {
            player.GetComponent<PlayerMovement>()._magicalStren += 1f;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInRange = false;
        }
    }

    public override IEnumerator hitReg()
    {
        yield return new WaitForSeconds(.25f);
        GetComponent<SpriteRenderer>().color = _c;
    }
}
