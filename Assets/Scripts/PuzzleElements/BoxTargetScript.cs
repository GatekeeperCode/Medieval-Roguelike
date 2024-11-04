using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTargetScript : MonoBehaviour
{
    public BoxSlideActivationScript bsas;
    public int index;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PushBox")
        {
            bsas.boxTargets[index] = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PushBox")
        {
            bsas.boxTargets[index] = false;
        }
    }
}
