using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Floor" && collision.tag != "Bow" && collision.tag != "Sword" && collision.tag != "Crossbow" && collision.tag != "Spear")
        {
            print(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
