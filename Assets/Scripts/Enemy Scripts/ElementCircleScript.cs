using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementCircleScript : MonoBehaviour
{
    public float damage;
    SphereWizardScript sws;
    int elementNum;
    int hitsLeft;
    bool canHit = true;

    private void Start()
    {
        sws = transform.parent.transform.parent.GetComponent<SphereWizardScript>();
        elementNum = Random.Range(0, 4);
        hitsLeft = 1;

        if(elementNum == 0) //Water
        {
            GetComponent<SpriteRenderer>().color = Color.cyan;
        }
        else if(elementNum == 1) //Fire
        {
            hitsLeft = 2;
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if(elementNum == 2) //Earth
        {
            GetComponent<SpriteRenderer>().color = Color.black;
            damage *= 1.5f;
        }
        else //Air
        {
            GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(canHit)
        {
            if (collision.tag == "Player" && hitsLeft == 1)
            {
                sws.elements.Remove(gameObject);

                if (elementNum == 0)
                {
                    StartCoroutine(collision.gameObject.GetComponent<PlayerMovement>().waterSphereSlow());
                }
                else if (elementNum == 3)
                {
                    collision.gameObject.transform.position = Vector3.MoveTowards(collision.gameObject.transform.position, transform.position * -2, 2.5f);
                }

                Destroy(gameObject);
            }
            else if (collision.tag == "Player" && hitsLeft == 2)
            {
                transform.localScale *= new Vector2(.75f, .75f);
                damage *= .75f;
                hitsLeft--;
                canHit = false;
                StartCoroutine("resetHit");
            }
        }
    }

    IEnumerator resetHit()
    {
        yield return new WaitForSeconds(.5f);
        canHit = true;
    }
}
