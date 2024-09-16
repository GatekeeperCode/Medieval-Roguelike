using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneMoveScript : MonoBehaviour
{
    public roomVarScript roomVars;
    public GameObject resetPoint;

    Rigidbody2D _rbody;
    GameObject player;
    float _speed;

    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        _speed = player.GetComponent<PlayerMovement>()._speed;

        if (roomVars.playerPresent)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            //_rbody.velocity = new Vector2(x, y) * _speed;
            _rbody.velocity = transform.up * y * _speed + transform.right * x * _speed;
        }
        else
        {
            transform.position = resetPoint.transform.position;
        }
    }
}
