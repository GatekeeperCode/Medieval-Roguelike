using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateScript : MonoBehaviour
{
    public GameObject target;
    public bool doorOpen;
    public bool canClose;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "PlayerClone")
        {
            if(doorOpen)
            {
                target.SetActive(false);
            }
            else
            {
                target.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (canClose && (collision.tag == "Player" || collision.tag == "PlayerClone"))
        {
            if (!doorOpen)
            {
                target.SetActive(false);
            }
            else
            {
                target.SetActive(true);
            }
        }
    }
}
