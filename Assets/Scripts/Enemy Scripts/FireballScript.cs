using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    public float damage; //Set by WizardScript
    public float timeDelay;
    GameObject player;
    bool hasDetonated = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("fireballExplosion");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, player.transform.position)<2)
        {
            hasDetonated = true;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
            transform.localScale = new Vector3(5, 5);
            StartCoroutine("dissapate");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.GetComponent<PlayerMovement>()._health -= (damage - Random.Range(0f, player.GetComponent<PlayerMovement>()._defense / 2));
        }
    }

    IEnumerator fireballExplosion()
    {
        yield return new WaitForSeconds(timeDelay);
        if(!hasDetonated)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
            transform.localScale = new Vector3(5, 5);
            StartCoroutine("dissapate");
        }
    }

    IEnumerator dissapate()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
