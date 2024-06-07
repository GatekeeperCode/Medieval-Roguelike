using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawnScript : MonoBehaviour
{
    public GameObject _basicRoom;
    public GameObject _sideRoom;
    public GameObject _topRoom;

    public void spawnRoom(Vector2 centerPos, Vector2 dir)
    {
        int rand = Random.Range(0, 3);

        if (rand == 0)
        {
            Instantiate(_basicRoom, centerPos, Quaternion.identity);
        }
        else
        {
            if(dir == Vector2.down)
            {
                Instantiate(_topRoom, centerPos, Quaternion.identity);
            }
            else if(dir == Vector2.up)
            {
                GameObject room = Instantiate(_topRoom, centerPos, Quaternion.identity);
                room.transform.eulerAngles = new Vector3(
                    room.transform.eulerAngles.x,
                    room.transform.eulerAngles.y,
                    room.transform.eulerAngles.z + 180
                );
                room.GetComponent<roomVarScript>().isFlipped = true;
            }
            else if(dir == Vector2.left)
            {
                Instantiate(_sideRoom, centerPos, Quaternion.identity);
            }
            else
            {
                GameObject room = Instantiate(_sideRoom, centerPos, Quaternion.identity);
                room.transform.eulerAngles = new Vector3(
                    room.transform.eulerAngles.x,
                    room.transform.eulerAngles.y,
                    room.transform.eulerAngles.z + 180
                );
                room.GetComponent<roomVarScript>().isFlipped = true;
            }
        }
    }
}
