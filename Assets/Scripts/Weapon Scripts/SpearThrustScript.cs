using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearThrustScript : MonoBehaviour
{
    public GameObject spearGO;
    bool attacking;
    Transform spear;
    Camera mainCamera;
    GameObject spearObject;
    PlayerMovement pm;
    float lerpDuration = 0.5f;
    float stabSpeed = 4;

    // Start is called before the first frame update
    void Start()
    {
        attacking = false;
        spear = spearGO.transform;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        spearObject = spearGO.transform.GetChild(1).gameObject; 
        spearObject.SetActive(true);
    }

    private void OnEnable()
    {
        attacking = false;
        spear = spearGO.transform;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        spearObject = spearGO.transform.GetChild(1).gameObject;
        spearObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(!pm.paused)
        {
            PointToMouse(spear);

            if (Input.GetMouseButtonDown(0) && !attacking && !GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().paused)
            {
                StartCoroutine(thrust());
            }
        }
    }

    private void OnDisable()
    {
        spearObject.SetActive(false);
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

    IEnumerator thrust()
    {
        attacking = true;
        float timeElapsed = 0;
        Vector3 spearStartLoc = spearObject.transform.localPosition;

        while (timeElapsed < lerpDuration)
        {
            if(timeElapsed < lerpDuration/2)
            {
                spearObject.transform.position = new Vector2(spearObject.transform.position.x + (spearObject.transform.up.x * stabSpeed * Time.deltaTime), spearObject.transform.position.y + (spearObject.transform.up.y * stabSpeed * Time.deltaTime));
            }
            else
            {
                spearObject.transform.position = new Vector2(spearObject.transform.position.x + (-spearObject.transform.up.x * stabSpeed * Time.deltaTime), spearObject.transform.position.y + (-spearObject.transform.up.y * stabSpeed * Time.deltaTime));
            }

            timeElapsed += Time.deltaTime;
            yield return null;
        }
        spearObject.transform.localPosition = spearStartLoc;
        attacking = false;
    }
}
