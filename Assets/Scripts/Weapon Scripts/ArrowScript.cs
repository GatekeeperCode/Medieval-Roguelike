using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public float damage;
    public bool shouldStayActive;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Floor" && collision.tag != "Bow" && collision.tag != "Sword" && collision.tag != "Crossbow" && collision.tag != "Spear" && collision.tag != "Decorative Obstacle")
        {
            print(collision.gameObject);

            if(!shouldStayActive)
            {
                Destroy(gameObject);
            }
        }
    }
}
