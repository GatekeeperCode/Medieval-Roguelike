using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBurstItemScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            collision.GetComponent<WaveBurstScript>().enabled = true;
            collision.GetComponent<WaveBurstScript>().powerScale += 1;
        }
    }
}
