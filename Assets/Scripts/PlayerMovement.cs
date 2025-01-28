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
    float baseSpeed;

    Camera cam;

    void Start()
    {
        activeWeaponString = "Sword";

        _rbody = GetComponent<Rigidbody2D>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        _health = 20;
        baseScore = (_speed + _physicalStren + _rangeStren + _defense + _health)/36;
        baseSpeed = _speed;
    }

    // Update is called once per frame
    void Update()
    {
        score = (_speed + _physicalStren + _rangeStren + _defense + _health) / baseScore;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        //_rbody.velocity = new Vector2(x, y) * _speed;
        _rbody.velocity = transform.up * y * _speed + transform.right * x * _speed;

        if (Input.GetMouseButton(1))
        {
            _shield.SetActive(true);
        }
        else
        {
            _shield.SetActive(false);
        }

        //I could probably do this better but this is how I did it.
        if (canUseMap && Input.GetKey(KeyCode.E))
        {
            cam.orthographicSize = 25f;
        }
        else
        {
            cam.orthographicSize = 5f;
        }    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("BEE"))
        {
            _health -= collision.gameObject.GetComponent<BeeEnemyScript>().beeDmg;
        }
        else if(collision.transform.CompareTag("Gobbo"))
        {
            _health -= collision.gameObject.GetComponent<GobboScript>().gobboDamage;
        }
        else if (collision.transform.CompareTag("Goo"))
        {
            _health -= collision.gameObject.GetComponent<GooScript>().gooDamage;
        }
        else if (collision.transform.CompareTag("Minotaur"))
        {
            _health -= collision.gameObject.GetComponent<MinotaurScript>().minoDamage;
        }
        else if (collision.transform.CompareTag("Spear") || collision.transform.tag == "Sword")
        {
            _health -= collision.gameObject.GetComponent<MeleeDmgScript>().damage;
        }
        else if (collision.gameObject.CompareTag("Arrow"))
        {
            _health -= collision.gameObject.GetComponent<ArrowScript>().damage;
        }
        else if (collision.gameObject.CompareTag("Fireball"))
        {
            _health -= collision.gameObject.GetComponent<FireballScript>().damage;
        }
        else if(collision.gameObject.CompareTag("ElementSphere"))
        {
            _health -= collision.gameObject.GetComponent<ElementCircleScript>().damage;
        }
    }

    public IEnumerator waterSphereSlow()
    {
        _speed *= .75f;
        StartCoroutine("resumeSpeed");
        yield return null;
    }

    IEnumerator resumeSpeed()
    {
        yield return new WaitForSeconds(1.5f);
        _speed = baseSpeed;
    }
}
