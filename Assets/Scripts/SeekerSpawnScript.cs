using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerSpawnScript : MonoBehaviour
{
    public GameObject seekerSphere;
    public int stacks = 1;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnOrb", 0, 5 / stacks);
    }

    void spawnOrb()
    {
        GameObject s = Instantiate(seekerSphere, new Vector3(Random.Range(.2f, 1f), Random.Range(.2f, 1f), 0), Quaternion.identity);
        s.GetComponent<SeekerScript>().damage = gameObject.GetComponentInParent<PlayerMovement>()._magicalStren;
    }
}
