using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowScript : MonoBehaviour
{
    public GameObject bowGO;
    public GameObject projectile;

    public float _chargeFactor;

    Transform bow;
    Camera mainCamera;
    GameObject bowObject;
    PlayerMovement pm;
    float _charge;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        bow = bowGO.transform;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        bowObject = bowGO.transform.GetChild(2).gameObject;
        sr = bowObject.GetComponent<SpriteRenderer>();
        bowObject.SetActive(true);
        _charge = 1;
    }

    private void OnEnable()
    {
        bow = bowGO.transform;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        bowObject = bowGO.transform.GetChild(2).gameObject;
        sr = bowObject.GetComponent<SpriteRenderer>();
        bowObject.SetActive(true);
        _charge = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(!pm.paused)
        {
            PointToMouse(bow);

            if (Input.GetMouseButton(0) && !GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().paused)
            {
                if (_charge < 10)
                {
                    _charge += Time.deltaTime * _chargeFactor;

                    if (_charge >= 10)
                    {
                        _charge = 10;
                        sr.color = Color.black;
                    }
                    else if (_charge > 7)
                    {
                        sr.color = Color.green;
                    }
                    else if (_charge > 4)
                    {
                        sr.color = Color.yellow;
                    }
                    else
                    {
                        sr.color = Color.red;
                    }
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                GameObject arrow = Instantiate(projectile, bowObject.transform.GetChild(0).transform.position, Quaternion.identity);
                arrow.transform.rotation = bowObject.transform.rotation * Quaternion.Euler(0, 0, 90);
                arrow.GetComponent<Rigidbody2D>().AddForce(arrow.transform.up * -50 * _charge);
                arrow.GetComponent<ArrowScript>().damage = bowObject.GetComponent<RangedDmgScript>().damage;

                _charge = 1;
                sr.color = Color.red;
            }
        }
    }

    private void OnDisable()
    {
        bowObject.SetActive(false);
    }

    //https://www.antoniovalentini.com/rotate-an-object-towards-the-mouse-position-2d/
    private void PointToMouse(Transform player)
    {
        var mouseScreenPos = Input.mousePosition;
        var startingScreenPos = mainCamera.WorldToScreenPoint(player.position);
        mouseScreenPos.x -= startingScreenPos.x;
        mouseScreenPos.y -= startingScreenPos.y;
        var angle = Mathf.Atan2(mouseScreenPos.y, mouseScreenPos.x) * Mathf.Rad2Deg;
        player.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
