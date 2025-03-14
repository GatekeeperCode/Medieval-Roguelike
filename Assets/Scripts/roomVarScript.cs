using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomVarScript : MonoBehaviour
{
    public bool isFlipped = false;
    public bool playerPresent = false;

    public GameObject[] monsters;

    bool check = false;

    private void Update()
    {
        if(playerPresent && !check)
        {
            if(monsters.Length != 0)
            {
                for(int i=0; i<monsters.Length; i++)
                {
                    monsters[i].SetActive(true);
                }
            }

            check = true;
        }
    }
}
