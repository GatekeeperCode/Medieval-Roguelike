using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShiftScript : MonoBehaviour
{
    public Vector2 _dir;
    public roomVarScript room;

    private void Start()
    {
        if(room.isFlipped)
        {
            _dir = _dir * -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameObject camObject = GameObject.FindGameObjectWithTag("MainCamera");
            camObject.transform.position = new Vector3(transform.position.x + _dir.x, transform.position.y + _dir.y, camObject.transform.position.z);
        }
    }
}
