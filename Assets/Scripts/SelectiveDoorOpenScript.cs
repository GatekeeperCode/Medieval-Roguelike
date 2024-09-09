using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectiveDoorOpenScript : MonoBehaviour
{
    public bool[] targets;
    public GameObject[] walls;
    public bool hasOpenedDoor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
