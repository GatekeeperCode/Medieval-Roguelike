using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public float damage;
    public bool shouldStayActive;
    public bool miniBossAttack = false;
    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(miniBossAttack)
        {
            if(collision.tag == "Player" || Vector2.Distance(transform.position, player.transform.position)>20)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (collision.tag != "Floor" && collision.tag != "Bow" && collision.tag != "Sword" && collision.tag != "Crossbow" && collision.tag != "Spear" && collision.tag != "Decorative Obstacle")
            {
                //print(collision.gameObject);

                if (!shouldStayActive)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
