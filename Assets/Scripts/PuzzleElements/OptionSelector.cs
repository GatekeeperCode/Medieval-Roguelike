using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionSelector : MonoBehaviour
{
    public PickOneScript pos;
    public bool isOption1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(isOption1)
            {
                pos.option1Chosen = true;
            }
            else
            {
                pos.option2Chosen = true;
            }
        }
    }
}
