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
}
