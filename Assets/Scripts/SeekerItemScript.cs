using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerItemScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            if (collision.GetComponent<SeekerSpawnScript>().isActiveAndEnabled.Equals(true))
            {
                collision.GetComponent<SeekerSpawnScript>().stacks += 1;
            }
            else
            {
                collision.GetComponent<SeekerSpawnScript>().enabled = true;
            }
        }
    }
}
