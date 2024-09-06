using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePuzzleTargetScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isHit;
    public bool canMultiHit;

    Color startColor;

    private void Start()
    {
        startColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    private void Update()
    {
        if(isHit)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = startColor;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(canMultiHit)
        {
            isHit = !isHit;
        }
        else
        {
            isHit = true;
        }
    }
}
