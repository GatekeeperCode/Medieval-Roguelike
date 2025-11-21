using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerMovement>().canUseMap = true;
            TooltipManager._instance.SetAndShowTooltip("Press E to use map.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerMovement>().canUseMap = false;
            TooltipManager._instance.HideTooltip();
        }
    }
}
