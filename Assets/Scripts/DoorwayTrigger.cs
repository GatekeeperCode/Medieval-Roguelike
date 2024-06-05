using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorwayTrigger : MonoBehaviour
{
    public Vector2 _dir;
    public LayerMask _backgroundLayer;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 faceDirection;

        if(_dir == Vector2.left * 11.5f)
        {
            faceDirection = Vector2.left;
        }
        else if(_dir == Vector2.right * 11.5f)
        {
            faceDirection = Vector2.right;
        }
        else if(_dir == Vector2.up * 4.5f)
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
        }
    }

    /// <summary>
    /// Raycast in the direction the trigger is relative to the room to see if another room is there.
    /// </summary>
    /// <returns>True if another room is present where the next room will go.</returns>
    bool isInContact(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, direction, 7f, _backgroundLayer);

        return (hit.collider != null);
    }
}
