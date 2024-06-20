using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRotateScript : MonoBehaviour
{
    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        PointToMouse(transform);
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
