using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float _speed;
    public float _physicalStren;
    public float _rangeStren;
    public float _magicalStren;
    public float _defense;
    public float _health;
    public int _gold;
    public string activeWeaponString;
    public bool canUseMap;
    public float score;

    public GameObject _shield;

    Rigidbody2D _rbody;
    float baseScore;

    void Start()
    {
        activeWeaponString = "Sword";
        _rbody = GetComponent<Rigidbody2D>();
        _health = 20;
        baseScore = (_speed + _physicalStren + _rangeStren + _defense + _health)/36;
    }

    // Update is called once per frame
    void Update()
    {
        score = (_speed + _physicalStren + _rangeStren + _defense + _health)/baseScore;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        //_rbody.velocity = new Vector2(x, y) * _speed;
        _rbody.velocity = transform.up * y * _speed + transform.right * x * _speed;

        if(Input.GetMouseButton(1))
        {
            _shield.SetActive(true);
        }
        else
        {
            _shield.SetActive(false);
        }

        if(canUseMap && Input.GetKey(KeyCode.E))
        {
            GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");

            cam.GetComponent<Camera>().orthographicSize = 25;
        }
        else
        {
            GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
            cam.GetComponent<Camera>().orthographicSize = 4.701583f;
        }    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "BEE")
        {
            _health = _health - collision.gameObject.GetComponent<BeeEnemyScript>().beeDmg;
        }
        else if(collision.transform.tag == "Gobbo")
        {
            _health = _health - collision.gameObject.GetComponent<GobboScript>().gobboDamage;
        }
        else if (collision.transform.tag == "Goo")
        {
            _health = _health - collision.gameObject.GetComponent<GooScript>().gooDamage;
        }
        else if (collision.transform.tag == "Minotaur")
        {
            _health = _health - collision.gameObject.GetComponent<MinotaurScript>().minoDamage;
        }
        else if (collision.transform.tag == "Spear" || collision.transform.tag == "Sword")
        {
            _health = _health - collision.gameObject.GetComponent<MeleeDmgScript>().damage;
        }
        else if (collision.gameObject.tag == "Arrow")
        {
            _health -= collision.gameObject.GetComponent<ArrowScript>().damage;
        }
    }
}
