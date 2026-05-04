using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPlayerDetector : MonoBehaviour
{
    roomVarScript room;

    // Start is called before the first frame update
    void Start()
    {
        room = gameObject.GetComponentInParent<roomVarScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            room.playerPresent = true;

            GameObject camObject = GameObject.FindGameObjectWithTag("MainCamera");
            camObject.transform.position = new Vector3(transform.position.x, transform.position.y, camObject.transform.position.z);
        }    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            room.playerPresent = false;
        }
    }
}
