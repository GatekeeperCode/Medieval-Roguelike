using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemyScript : EnemyBase
{
    bool rotating;
    float lerpDuration = 0.5f;
    Transform sword;
    bool hitStun = false;
    float timeSinceLastSwing;
    Quaternion rotReset;

    public GameObject swordGo;
    public float SwingTime;

    // Start is called before the first frame update
    void Start()
    {
        rotating = false;
        player = GameObject.FindGameObjectWithTag("Player");
        _c = GetComponent<SpriteRenderer>().color;
        sword = swordGo.transform;
        rotReset = sword.rotation;
        timeSinceLastSwing = 10;
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
            if (Vector2.Distance(transform.position, player.transform.position) < 1)
            {
                if (!rotating && timeSinceLastSwing>=SwingTime)
                {
                    StartCoroutine(swing());
                    timeSinceLastSwing = 0;
                }
                else
                {
                    if(timeSinceLastSwing > SwingTime/3)
                    {
                        sword.rotation = rotReset;
                    }            
                    timeSinceLastSwing += Time.deltaTime;
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

            swordGo.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator swing()
    {
        rotating = true;
        float timeElapsed = 0;
        Quaternion startRotation = swordGo.transform.rotation;

        Quaternion targetRotation;

        if ((player.transform.position.x - transform.position.x)>0)
        {
            targetRotation = swordGo.transform.rotation * Quaternion.Euler(0, 0, 125);
        }
        else
        {
            targetRotation = swordGo.transform.rotation * Quaternion.Euler(0, 0, -125);
        }

        rotReset = startRotation;

        while (timeElapsed < lerpDuration)
        {
            sword.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        sword.rotation = targetRotation;
        rotating = false;
    }

    public override IEnumerator hitReg()
    {
        yield return new WaitForSeconds(.25f);
        GetComponent<SpriteRenderer>().color = _c;
        hitStun = false;
    }
}
