using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGraph : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("updteGph");   
    }

    private IEnumerator updteGph()
    {
        yield return new WaitForSeconds(1);
        AstarPath.active.Scan();
    }
}
