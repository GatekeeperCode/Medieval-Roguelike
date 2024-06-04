using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawnScript : MonoBehaviour
{
    public GameObject _basicRoom;

    public void spawnRoom(Vector2 centerPos)
    {
        print("Room Spawned");
        Instantiate(_basicRoom, centerPos, Quaternion.identity);
    }
}
