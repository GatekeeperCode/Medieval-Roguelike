using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GobboScript : MonoBehaviour
{
    GameObject player;
    public float health;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
