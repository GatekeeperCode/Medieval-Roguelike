using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordSwingScript : MonoBehaviour
{
    public GameObject swordGo;
    Camera mainCamera;

    bool rotating;
    Transform sword;
    GameObject swordObject;

    float lerpDuration = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        rotating = false;
        sword = swordGo.transform;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        swordObject = swordGo.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        PointToMouse(swordGo.transform);

        if (Input.GetMouseButtonDown(0) && !rotating)
        {
            swordObject.SetActive(true);
            StartCoroutine(Rotate90());
        }
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

    IEnumerator Rotate90()
    {
        rotating = true;
        float timeElapsed = 0;
        Quaternion startRotation = swordGo.transform.rotation * Quaternion.Euler(0, 0, 180);
        Quaternion targetRotation = swordGo.transform.rotation;

        while (timeElapsed < lerpDuration)
        {
            sword.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        sword.rotation = targetRotation;
        rotating = false;
        swordObject.SetActive(false);
    }
}
