using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorwayTrigger : MonoBehaviour
{
    public Vector2 _dir;
    public LayerMask _backgroundLayer;
    public LayerMask _wallLayer;
    public GameObject _wall;
    public roomVarScript roomVars;

    Vector2 faceDirection;
    Transform _trans;
    bool wallSpawn;
    float startTime;

    // Start is called before the first frame update
    void Start()
    {
        _trans = transform;
        wallSpawn = false;
        startTime = Time.time;

        if(_dir.x == -13.27575f)
        {
            faceDirection = Vector2.left;
        }
        else if(_dir.x == 13.27575f)
        {
            faceDirection = Vector2.right;
        }
        else if(_dir.y == 4.75f)
        {
            faceDirection = Vector2.up;
        }
        else
        {
            faceDirection = Vector2.down;
        }


        if(!isInContact(_dir))
        {
            GameObject.FindGameObjectWithTag("SceneManager").GetComponent<RoomSpawnScript>().spawnRoom(new Vector2(transform.position.x + _dir.x, transform.position.y + _dir.y), faceDirection);
            wallSpawn = true;
        }
        else
        {
            if(!neighborRoomCheck(faceDirection))
            {
                wallSpawn = true;
            }
        }
    }

    private void Update()
    {
        if(!wallSpawn)
        {
            if(neighborRoomCheck(faceDirection))
            {
                spawnWall();
            }
        }

        if(!wallSpawn && (Time.time - startTime)>2)
        {
            wallSpawn = true;
        }
    }

    void spawnWall()
    {
        if (neighborRoomCheck(faceDirection))
        {
            Vector3 placeDirection = faceDirection * -0.5f;
            Vector3 placePos = new Vector3(_trans.position.x + placeDirection.x, _trans.position.y + placeDirection.y, _trans.position.z);
            GameObject newWall = Instantiate(_wall, placePos, Quaternion.identity);
            Transform wallTrans = newWall.transform;

            wallTrans.eulerAngles = new Vector3(
                    wallTrans.eulerAngles.x + _trans.eulerAngles.x,
                    wallTrans.eulerAngles.y + _trans.eulerAngles.y,
                    wallTrans.eulerAngles.z + _trans.eulerAngles.z
                );

            wallTrans.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
        }

        wallSpawn = true;
    }

    /// <summary>
    /// Raycast in the direction the trigger is relative to the room to see if another room is there.
    /// </summary>
    /// <returns>True if another room is present where the next room will go.</returns>
    bool isInContact(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, direction, 4f, _backgroundLayer);

        return (hit.collider != null);
    }

    bool neighborRoomCheck(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, direction, 1f, _wallLayer);

        return (hit.collider != null);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            roomVars.playerPresent = false;
        }
    }
}
