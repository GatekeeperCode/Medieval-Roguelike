using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwapScript : MonoBehaviour
{
    public string swapToTag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            string oldWeapon = collision.GetComponent<PlayerMovement>().activeWeaponString;
            
            if(oldWeapon == "Sword")
            {
                collision.GetComponent<swordSwingScript>().enabled = false;
            }
            else if (oldWeapon == "Spear")
            {
                collision.GetComponent<SpearThrustScript>().enabled = false;
            }
            else if (oldWeapon == "Crossbow")
            {
                collision.GetComponent<CrossbowScript>().enabled = false;
            }
            else
            {
                collision.GetComponent<BowScript>().enabled = false;
            }

            if (swapToTag == "Sword")
            {
                collision.GetComponent<swordSwingScript>().enabled = true;
            }
            else if (swapToTag == "Spear")
            {
                collision.GetComponent<SpearThrustScript>().enabled = true;
            }
            else if (swapToTag == "Crossbow")
            {
                collision.GetComponent<CrossbowScript>().enabled = true;
            }
            else
            {
                collision.GetComponent<BowScript>().enabled = true;
            }

            collision.GetComponent<PlayerMovement>().activeWeaponString = swapToTag;
        }
    }
}
