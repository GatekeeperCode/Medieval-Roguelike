using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterItemScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            collision.GetComponent<ClusterScript>().enabled = true;
            collision.GetComponent<ClusterScript>().powerScale += 1;
        }
    }
}
