using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedDmgScript : MonoBehaviour
{
    public float weaponModifier;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement pmScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        damage = pmScript._rangeStren * weaponModifier;
    }
}
