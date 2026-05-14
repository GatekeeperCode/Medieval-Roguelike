using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public GameObject player;
    public float health;
    public float speed;
    public GameObject resetPoint;
    public roomVarScript roomVars;
    public float damage;
    float playerDefense;

    public Color _c;

    private void Start()
    {
        playerDefense = player.GetComponent<PlayerMovement>()._defense;
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

            if(collision.gameObject.tag == "WaveBurst")
            {
                health -= collision.GetComponentInParent<WaveBurstScript>().powerScale;
                GetComponent<SpriteRenderer>().color = Color.red;
                StartCoroutine(hitReg());
            }

            if(collision.gameObject.tag == "Cluster")
            {
                health -= collision.transform.parent.GetComponentInParent<ClusterScript>().powerScale;
                GetComponent<SpriteRenderer>().color = Color.red;
                StartCoroutine(hitReg());
            }

            if(collision.gameObject.tag == "Seeker")
            {
                health -= collision.transform.parent.GetComponentInParent<SeekerScript>().damage;
                GetComponent<SpriteRenderer>().color = Color.red;
                StartCoroutine(hitReg());
            }

            if (collision.gameObject.tag == "Lightning")
            {
                health -= collision.transform.parent.GetComponentInParent<LightningObjectScript>().damage;
                GetComponent<SpriteRenderer>().color = Color.red;
                StartCoroutine(hitReg());
            }

            if (collision.gameObject.tag == "Player")
            {
                player.GetComponent<PlayerMovement>()._health -= (damage - Random.Range(0f, playerDefense / 2));
            }
        }
    }

    public abstract IEnumerator hitReg();
}
