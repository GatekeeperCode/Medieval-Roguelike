using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummyScript : MonoBehaviour
{
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
        if (collision.gameObject.tag != "Floor")
        {
            if(collision.gameObject.tag == "Sword" || collision.gameObject.tag == "Spear")
            {
                print("Ow! I took: " + collision.gameObject.GetComponent<MeleeDmgScript>().damage + " damage!");
            }

            if(collision.gameObject.tag == "Arrow")
            {
                print("Ow! I took: " + collision.gameObject.GetComponent<ArrowScript>().damage + " damage!");
            }

            GetComponent<SpriteRenderer>().color = Color.green;
            StartCoroutine("colorReset");
        }
    }

    private IEnumerator colorReset()
    {
        yield return new WaitForSeconds(3);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
