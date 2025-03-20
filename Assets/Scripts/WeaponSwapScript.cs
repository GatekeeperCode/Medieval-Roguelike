using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwapScript : MonoBehaviour
{
    public string swapToTag;

    private void OnMouseEnter()
    {
        print("Hover");
        TooltipManager._instance.SetAndShowTooltip(swapToTag);
    }

    private void OnMouseExit()
    {
        print("Gone");
        TooltipManager._instance.HideTooltip();
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
