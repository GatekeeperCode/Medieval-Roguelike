using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBoxResetScript : MonoBehaviour
{
    public roomVarScript roomVars;
    public float xVal;
    public float yVal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!roomVars.playerPresent)
        {
            transform.position = new Vector2(xVal, yVal);
        }
    }
}
