using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSlideActivationScript : MonoBehaviour
{
    public bool[] boxTargets;
    public GameObject[] openDoor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0; i<boxTargets.Length; i++)
        {
            if(boxTargets[i])
            {
                openDoor[i].SetActive(false);
            }
            else
            {
                openDoor[i].SetActive(true);
            }
        }
    }
}
