using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TombScript : MonoBehaviour
{
    public GameObject[] enemies;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        for(int i=0; i<enemies.Length; i++)
        {
            enemies[i].SetActive(true);
        }
    }
}
