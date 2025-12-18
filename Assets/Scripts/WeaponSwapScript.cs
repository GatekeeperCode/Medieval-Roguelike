using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwapScript : MonoBehaviour
{
    public string swapToTag;
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

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 3)
        {
            tip.GetComponent<TooltipManager>().SetAndShowTooltip(swapToTag);
        }
        else
        {
            tip.GetComponent<TooltipManager>().HideTooltip();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            string oldWeapon = collision.GetComponent<PlayerMovement>().activeWeaponString;

            switch(oldWeapon)
            {
                case "Sword":
                    collision.GetComponent<swordSwingScript>().enabled = false;
                    break;
                case "Spear":
                    collision.GetComponent<SpearThrustScript>().enabled = false;
                    break;
                case "Crossbow":
                    collision.GetComponent<CrossbowScript>().enabled = false;
                    break;
                default:
                    collision.GetComponent<BowScript>().enabled = false;
                    break;
            }

            switch (swapToTag)
            {
                case "Sword":
                    collision.GetComponent<swordSwingScript>().enabled = true;
                    break;
                case "Spear":
                    collision.GetComponent<SpearThrustScript>().enabled = true;
                    break;
                case "Crossbow":
                    collision.GetComponent<CrossbowScript>().enabled = true;
                    break;
                default:
                    collision.GetComponent<BowScript>().enabled = true;
                    break;
            }

            collision.GetComponent<PlayerMovement>().activeWeaponString = swapToTag;
        }
    }
}
