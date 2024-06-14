using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float _speed;
    public float _physStren;
    public float _rangStren;
    public float _magStren;
    public float _defense;
    public int _gold;

    Rigidbody2D _rbody;

    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        //_rbody.velocity = new Vector2(x, y) * _speed;
        _rbody.velocity = transform.up * y * _speed + transform.right * x * _speed;
    }
}
