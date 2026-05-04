using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsOnDestruction : MonoBehaviour
{
    public GameObject[] summons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        foreach(GameObject item in summons)
        {
            item.SetActive(true);
        }
    }
}
