using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRangeAttackScope : MonoBehaviour
{
    GameObject p;

    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //var mouseScreenPos = p.transform.position;
        //var startingScreenPos = transform.position;
        //mouseScreenPos.x -= startingScreenPos.x;
        //mouseScreenPos.y -= startingScreenPos.y;
        //var angle = Mathf.Atan2(mouseScreenPos.y, mouseScreenPos.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Vector3 from = transform.up;
        Vector3 to = p.transform.position - transform.position;

        float angle = Vector3.SignedAngle(from, to, transform.forward);
        transform.Rotate(0.0f, 0.0f, angle);
    }
}
