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

    public GameObject swordGO;
    Transform sword;

    Rigidbody2D _rbody;
    float lerpDuration = 0.5f;
    bool rotating;

    Camera mainCamera;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        sword = swordGO.transform;
        _rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        //_rbody.velocity = new Vector2(x, y) * _speed;
        _rbody.velocity = transform.up * y * _speed + transform.right * x * _speed;


        if (Input.GetMouseButtonDown(0) && !rotating)
        {
            swordGO.SetActive(true);
            StartCoroutine(Rotate90());
        }

        if(Input.GetKey(KeyCode.Q))
        {
            //transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.rotation.z + 5));
            transform.Rotate(0, 0, 0.5f);
        }

        if(Input.GetKey(KeyCode.E))
        {
            //transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.rotation.z - 5));
            transform.Rotate(0, 0, -0.5f);
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
        Quaternion startRotation = transform.rotation * Quaternion.Euler(0, 0, 180);
        Quaternion targetRotation = transform.rotation;

        while (timeElapsed < lerpDuration)
        {
            sword.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        sword.rotation = targetRotation;
        rotating = false;
        swordGO.SetActive(false);
    }
}
