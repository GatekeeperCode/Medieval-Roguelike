using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectiveDoorOpenScript : MonoBehaviour
{
    public bool[] targets;
    public GameObject[] walls;
    public bool hasOpenedDoor;

    // Update is called once per frame
    void Update()
    {
        if(!hasOpenedDoor)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i])
                {
                    walls[i].SetActive(false);
                    hasOpenedDoor = true;
                }
            }
        }
    }
}
