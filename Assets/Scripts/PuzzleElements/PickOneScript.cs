using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickOneScript : MonoBehaviour
{
    public bool option1Chosen;
    public bool option2Chosen;

    public GameObject[] option1Walls;
    public GameObject[] option2Walls;

    bool optionPicked;

    // Start is called before the first frame update
    void Start()
    {
        optionPicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(option1Chosen && !optionPicked)
        {
            for(int i=0; i<option2Walls.Length; i++)
            {
                option2Walls[i].SetActive(true);
            }

            optionPicked = true;
        }

        if (option2Chosen && !optionPicked)
        {
            for (int i = 0; i < option1Walls.Length; i++)
            {
                option1Walls[i].SetActive(true);
            }

            optionPicked = true;
        }
    }
}
