using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDmgScript : MonoBehaviour
{
    public float damage;
    public float weaponModifier;

    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement pmScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        damage = pmScript._physicalStren * weaponModifier;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            GameObject go = collision.gameObject;
            Vector3 dir = (-(go.transform.position - transform.position)).normalized;
            go.transform.position += (dir * 2f);
        }
    }
}
