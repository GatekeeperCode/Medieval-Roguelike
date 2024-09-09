using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePuzzleTargetScript : MonoBehaviour
{
    // Start is called before the first frame update
    public SelectiveDoorOpenScript sdos;
    public int index;

    public bool canMultiHit;

    Color startColor;

    private void Start()
    {
        startColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    private void Update()
    {
        if(sdos.targets[index])
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
            sdos.targets[index] = !sdos.targets[index];
        }
        else
        {
            sdos.targets[index] = true;
        }
    }
}
