using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningItemScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            if (collision.GetComponent<LightningAttackScript>().isActiveAndEnabled.Equals(true))
            {
                collision.GetComponent<LightningAttackScript>().stacks += 1;
            }
            else
            {
                collision.GetComponent<LightningAttackScript>().enabled = true;
            }
        }
    }
}
