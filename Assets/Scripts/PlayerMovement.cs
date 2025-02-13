using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject pauseMenu;
    public GameObject deathMenu;
    public Text scoreText;

    Rigidbody2D _rbody;
    float baseScore;
    float baseSpeed;
    public bool paused = false;
    List<string> itemsHeld = new List<string>();

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

        if(!paused)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            //_rbody.velocity = new Vector2(x, y) * _speed;
            _rbody.velocity = transform.up * y * _speed + transform.right * x * _speed;
        }

        if (Input.GetMouseButton(1) && !paused)
        {
            _shield.SetActive(true);
        }
        else
        {
            _shield.SetActive(false);
        }

        //I could probably do this better but this is how I did it.
        if (canUseMap && Input.GetKey(KeyCode.E) && !paused)
        {
            cam.orthographicSize = 25f;
        }
        else
        {
            cam.orthographicSize = 4.9f;
        }
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(paused)
            {
                unpause();
            }
            else
            {
                pause();
            }
        }

        if(_health<=0)
        {
            handleDeath();
        }
    }

    private void handleDeath()
    {
        Time.timeScale = 0;
        deathMenu.SetActive(true);
        scoreText.text = "Score: " + score;
    }

    //Pauses game and opens pause menu
    private void pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        paused = true;
    }

    //Unpauses game and closes pause menu
    private void unpause()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        paused = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "BEE":
                _health -= collision.gameObject.GetComponent<BeeEnemyScript>().beeDmg;
                break;
            case "Gobbo":
                _health -= collision.gameObject.GetComponent<GobboScript>().gobboDamage;
                break;
            case "Goo":
                _health -= collision.gameObject.GetComponent<GooScript>().gooDamage;
                break;
            case "Minotaur":
                _health -= collision.gameObject.GetComponent<MinotaurScript>().minoDamage;
                break;
            case "Arrow":
                _health -= collision.gameObject.GetComponent<ArrowScript>().damage;
                break;
            case "Fireball":
                _health -= collision.gameObject.GetComponent<FireballScript>().damage;
                break;
            case "ElementSphere":
                _health -= collision.gameObject.GetComponent<ElementCircleScript>().damage;
                break;
            case "King":
                _health -= collision.gameObject.GetComponent<FinalBossScript>().bossDmg;
                break;
            default:
                _health -= collision.gameObject.GetComponent<MeleeDmgScript>().damage;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            itemScript i = collision.gameObject.GetComponent<itemScript>();
            itemsHeld.Add(i.itemName);
            print(itemsHeld[0]);

            itemBoost(i.boostedStat.ToString(), i.boost);
            itemBoost(i.SecondStat.ToString(), i.boost/2);
            itemBoost(i.hiddenStat.ToString(), i.boost * Random.Range(.1f, 2f));
            Destroy(collision.gameObject);
        }
    }

    void itemBoost(string stat, float boost)
    {
        if(stat == "none")
        {
            return;
        }
        else
        {
            if(stat == "speed")
            {
                _speed += boost;
            }
            else if(stat == "physicalStren")
            {
                _physicalStren += boost;
            }
            else if(stat == "rangeStren")
            {
                _rangeStren += boost;
            }
            else if(stat == "magicalStren")
            {
                _magicalStren += boost;
            }
            else if(stat == "defense")
            {
                _defense += boost;
            }
            else if(stat == "health")
            {
                _health += boost;
            }
            else if(stat == "gold")
            {
                _gold += (int)boost;
            }
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
