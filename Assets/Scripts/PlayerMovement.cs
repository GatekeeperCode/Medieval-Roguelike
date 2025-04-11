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
    public float _health = 50f;
    public int _gold;
    public string activeWeaponString;
    public bool canUseMap;
    public float score;
    public Text healthDisplay;

    public GameObject _shield;
    public GameObject pauseMenu;
    public GameObject deathMenu;
    public Text scoreText;

    Rigidbody2D _rbody;
    float baseScore;
    float baseSpeed;
    public bool paused = false;
    public int[] itemsHeld;

    Camera cam;

    void Start()
    {
        activeWeaponString = "Sword";

        _rbody = GetComponent<Rigidbody2D>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        baseScore = (_speed + _physicalStren + _rangeStren + _defense + _health)/36;
        baseSpeed = _speed;

        //Spawning in a "SpawnRoom"
        StartCoroutine("SpawnPoint");
    }

    public IEnumerator SpawnPoint()
    {
        yield return new WaitForSeconds(.5f);
        GameObject[] spawnrooms = GameObject.FindGameObjectsWithTag("SpawnRoom");
        GameObject spawnRoom = spawnrooms[Random.Range(0, spawnrooms.Length)];

        transform.position = spawnRoom.transform.position;
        cam.transform.position = new Vector3(spawnRoom.transform.position.x, spawnRoom.transform.position.y, cam.transform.position.z);
        spawnRoom.GetComponent<roomVarScript>().playerPresent = true;
    }

    // Update is called once per frame
    void Update()
    {
        score = (_speed + _physicalStren + _rangeStren + _defense + _health) / baseScore;

        healthDisplay.text = "Health: " + _health + "\nGold: " + _gold;

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

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            updateInventoryScreen();
        }

        //I could probably do this better but this is how I did it.
        if (canUseMap && Input.GetKey(KeyCode.E) && !paused)
        {
            cam.orthographicSize = 25f;
        }
        else
        {
            cam.orthographicSize = 5f;
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

    private void updateInventoryScreen()
    {
        string inventory = "Speed: " + _speed + "\nMelee Strength: " + _physicalStren;
        inventory += "\nRanged Strength: " + _rangeStren + "\nMagical Strength: " + _magicalStren;
        inventory += "\nDefense: " + _defense + "\nGold: " + _gold;

        //Handle any Special Items Here
        inventory += "\n\n\nSpecial Items:";

        print(inventory);
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
        float dmgReduction = Random.Range(0f, _defense);

        switch (collision.gameObject.tag)
        {
            case "BEE":
                _health -= (collision.gameObject.GetComponent<BeeEnemyScript>().beeDmg - dmgReduction);
                break;
            case "Gobbo":
                _health -= (collision.gameObject.GetComponent<GobboScript>().gobboDamage - dmgReduction);
                break;
            case "Goo":
                _health -= (collision.gameObject.GetComponent<GooScript>().gooDamage - dmgReduction);
                break;
            case "Minotaur":
                _health -= (collision.gameObject.GetComponent<MinotaurScript>().minoDamage - dmgReduction);
                break;
            case "Arrow":
                _health -= (collision.gameObject.GetComponent<ArrowScript>().damage - dmgReduction);
                break;
            case "Fireball":
                _health -= (collision.gameObject.GetComponent<FireballScript>().damage - dmgReduction);
                break;
            case "ElementSphere":
                _health -= (collision.gameObject.GetComponent<ElementCircleScript>().damage - dmgReduction);
                break;
            case "King":
                _health -= (collision.gameObject.GetComponent<FinalBossScript>().bossDmg - dmgReduction);
                break;
            case "Sword":
                _health -= (collision.gameObject.GetComponent<MeleeDmgScript>().damage - dmgReduction);
                break;
            case "Spear":
                _health -= (collision.gameObject.GetComponent<MeleeDmgScript>().damage - dmgReduction);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            itemScript i = collision.gameObject.GetComponent<itemScript>();
            itemsHeld[i.itemID] += 1;

            itemBoost(i.boostedStat.ToString(), i.boost);
            itemBoost(i.SecondStat.ToString(), i.boost/2);
            itemBoost(i.hiddenStat.ToString(), i.boost * Random.Range(.1f, 2f));
            Destroy(collision.gameObject);
            updateInventoryScreen();
        }
    }

    void itemBoost(string stat, float boost)
    {
        switch (stat)
        {
            case "speed":
                _speed += boost;
                break;
            case "physicalStren":
                _physicalStren += boost;
                break;
            case "rangeStren":
                _rangeStren += boost;
                break;
            case "magicalStren":
                _magicalStren += boost;
                break;
            case "defense":
                _defense += boost;
                break;
            case "health":
                _health += boost;
                break;
            case "gold":
                _gold += (int)boost;
                break;
            default:
                break;
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

    private void OnMouseEnter()
    {
        //print("Hover");
        //TooltipManager._instance.SetAndShowTooltip("Player");
    }

    private void OnMouseExit()
    {
        //print("Gone");
        //TooltipManager._instance.HideTooltip();
    }
}
