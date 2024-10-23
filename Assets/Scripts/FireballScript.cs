using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    public float damage; //Set by WizardScript
    public float timeDelay; 

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("fireballExplosion");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator fireballExplosion()
    {
        yield return new WaitForSeconds(timeDelay);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        transform.localScale = new Vector3(5, 5);
        StartCoroutine("dissapate");
    }

    IEnumerator dissapate()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
