using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorwayBlockSpawn : MonoBehaviour
{
    public Vector2 faceDirection;
    public GameObject _wall;
    public GameObject[] enemies;
    public GameObject[] actionTriggers;
    public bool wallSpawn = false;
    
    public bool roomClear = false;
    public bool wallsGone = false;
    public bool manualOverride;
    GameObject spawnedWall;
    Transform _trans;

    // Start is called before the first frame update
    void Start()
    {
        spawnedWall = gameObject;
        _trans = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (roomClear && !wallsGone)
        {
            Destroy(spawnedWall);
            wallsGone = true;
        }

        if(!roomClear && !manualOverride)
        {
            int count = 0;
            for(int i = 0; i < enemies.Length; i++)
            {
                try
                {
                    if(enemies[i].activeSelf)
                    {
                        count++;
                    }
                }
                catch (System.Exception){}
            }

            if(count==0)
            {
                roomClear = true;
            }
        }

        if(!wallSpawn)
        {
            for(int i = 0; i<actionTriggers.Length; i++)
            {
                GameObject t = actionTriggers[i];
                
                if(t.GetComponent<DoorwayBlockSpawn>().wallSpawn && !wallSpawn)
                {
                    spawnWall();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "floor")
        {
            print("Hello");
            if (!wallSpawn)
            {
                spawnWall();
            }
        }
    }

    void spawnWall()
    {
        Vector3 placeDirection = faceDirection * -0.5f;
        Vector3 placePos = new Vector3(_trans.position.x + placeDirection.x, _trans.position.y + placeDirection.y, _trans.position.z);
        spawnedWall = Instantiate(_wall, placePos, Quaternion.identity);
        spawnedWall.GetComponent<SpriteRenderer>().color = Color.gray;
        Transform wallTrans = spawnedWall.transform;

        wallTrans.eulerAngles = new Vector3(
                wallTrans.eulerAngles.x + _trans.eulerAngles.x,
                wallTrans.eulerAngles.y + _trans.eulerAngles.y,
                wallTrans.eulerAngles.z + _trans.eulerAngles.z
            );

        wallTrans.localScale = new Vector2(transform.localScale.x, transform.localScale.y);

        wallSpawn = true;
    }
}
