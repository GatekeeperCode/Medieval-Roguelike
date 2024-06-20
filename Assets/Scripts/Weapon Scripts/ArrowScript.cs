using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Floor" && collision.tag != "Bow" && collision.tag != "Player" && collision.tag != "Crossbow")
        {
            print(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
