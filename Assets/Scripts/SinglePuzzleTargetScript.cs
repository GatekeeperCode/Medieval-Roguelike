using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePuzzleTargetScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] walls;
    public bool isHit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isHit = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;

        for(int i = 0; i<walls.Length; i++)
        {
            walls[i].SetActive(false);
        }
    }
}
