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
            GetComponent<SpriteRenderer>().color = Color.green;
            StartCoroutine("colorReset");
        }
    }

    private void OnTriggerEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Floor")
        {
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
