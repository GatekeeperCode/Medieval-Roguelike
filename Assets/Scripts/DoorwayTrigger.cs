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
        if(!isInContact(_dir))
        {
            GameObject.FindGameObjectWithTag("SceneManager").GetComponent<RoomSpawnScript>().spawnRoom(new Vector2(transform.position.x + _dir.x, transform.position.y + _dir.y));
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
