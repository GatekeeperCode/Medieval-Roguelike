using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectiveDoorOpenScript : MonoBehaviour
{
    public bool[] targets;
    public GameObject[] walls;
    public GameObject[] targetObjects;
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

                    if(targetObjects.Length>0)
                    {
                        if (i == 0)
                        {
                            targetObjects[1].SetActive(false);
                        }
                        else
                        {
                            targetObjects[0].SetActive(false);
                        }
                    }
                    hasOpenedDoor = true;
                }
            }
        }
    }
}
