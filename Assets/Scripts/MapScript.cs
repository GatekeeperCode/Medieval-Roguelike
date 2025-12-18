using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    public GameObject tooltip;
    GameObject tip;
    GameObject player;
    Vector3 position;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        position = new Vector3(transform.position.x, transform.position.y + .5f, 0);
        tip = Instantiate(tooltip, position, Quaternion.identity);
    }

private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
        tip.GetComponent<TooltipManager>().SetAndShowTooltip("Press E to use map.");
        collision.GetComponent<PlayerMovement>().canUseMap = true;
            //TooltipManager._instance.SetAndShowTooltip("Press E to use map.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            tip.GetComponent<TooltipManager>().HideTooltip();
            collision.GetComponent<PlayerMovement>().canUseMap = false;
            //TooltipManager._instance.HideTooltip();
        }
    }
}
