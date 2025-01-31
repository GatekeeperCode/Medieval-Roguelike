using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowScript : MonoBehaviour
{
    public GameObject bowGO;
    public GameObject projectile;

    Transform crossbow;
    Camera mainCamera;
    GameObject crossbowObject;
    PlayerMovement pm;
    bool canFire;

    // Start is called before the first frame update
    void Start()
    {
        crossbow = bowGO.transform;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        crossbowObject = bowGO.transform.GetChild(3).gameObject;
        crossbowObject.SetActive(true);
        canFire = true;
    }

    private void OnEnable()
    {
        crossbow = bowGO.transform;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        crossbowObject = bowGO.transform.GetChild(3).gameObject;
        crossbowObject.SetActive(true);
        canFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!pm.paused)
        {
            PointToMouse(crossbow);

            if (Input.GetMouseButtonDown(0) && canFire && !GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().paused)
            {
                GameObject arrow = Instantiate(projectile, crossbowObject.transform.GetChild(0).transform.position, Quaternion.identity);
                arrow.transform.rotation = crossbowObject.transform.rotation * Quaternion.Euler(0, 0, 90);
                arrow.GetComponent<Rigidbody2D>().AddForce(arrow.transform.up * -150);
                arrow.GetComponent<ArrowScript>().damage = crossbowObject.GetComponent<RangedDmgScript>().damage;

                canFire = false;
                StartCoroutine(reloadCrossbow());
            }
        }
    }

    private void OnDisable()
    {
        crossbowObject.SetActive(false);
    }

    IEnumerator reloadCrossbow()
    {
        yield return new WaitForSeconds(3);
        canFire = true;
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
