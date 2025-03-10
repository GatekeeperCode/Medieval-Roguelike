using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawnScript : MonoBehaviour
{
    public GameObject[] _basicRoomFight;
    public GameObject[] _basicRoomPuzzle;
    public GameObject[] _basicRoomNavigate;

    public GameObject[] _sideRoomFight;
    public GameObject[] _sideRoomPuzzle;
    public GameObject[] _sideRoomNavigate;

    public GameObject[] _topRoomFight;
    public GameObject[] _topRoomPuzzle;
    public GameObject[] _topRoomNavigate;

    public void spawnRoom(Vector2 centerPos, Vector2 dir)
    {
        int rand = Random.Range(0, 1);

        if (rand == 1)
        {
            rand = Random.Range(0, 2);

            switch (rand)
            {
                case 1:
                    rand = Random.Range(0, _basicRoomPuzzle.Length);
                    Instantiate(_basicRoomPuzzle[rand], centerPos, Quaternion.identity);
                    break;
                case 2:
                    rand = Random.Range(0, _basicRoomNavigate.Length);
                    Instantiate(_basicRoomNavigate[rand], centerPos, Quaternion.identity);
                    break;
                default:
                    rand = Random.Range(0, _basicRoomFight.Length);
                    Instantiate(_basicRoomFight[rand], centerPos, Quaternion.identity);
                    break;
            }
        }
        else
        {
            if(dir == Vector2.down)
            {
                rand = Random.Range(0, 2);

                switch(rand)
                {
                    case 1:
                        rand = Random.Range(0, _topRoomNavigate.Length);
                        Instantiate(_topRoomNavigate[rand], centerPos, Quaternion.identity);
                        break;
                    case 2:
                        rand = Random.Range(0, _topRoomPuzzle.Length);
                        Instantiate(_topRoomPuzzle[rand], centerPos, Quaternion.identity);
                        break;
                    default:
                        rand = Random.Range(0, _topRoomFight.Length);
                        Instantiate(_topRoomFight[rand], centerPos, Quaternion.identity);
                        break;
                }
            }
            else if(dir == Vector2.up)
            {
                GameObject room;

                rand = Random.Range(0, 2);

                switch (rand)
                {
                    case 1:
                        rand = Random.Range(0, _topRoomFight.Length);
                        room = Instantiate(_topRoomFight[rand], centerPos, Quaternion.identity);
                        break;
                    case 2:
                        rand = Random.Range(0, _topRoomPuzzle.Length);
                        room = Instantiate(_topRoomPuzzle[rand], centerPos, Quaternion.identity);
                        break;
                    default:
                        rand = Random.Range(0, _topRoomNavigate.Length);
                        room = Instantiate(_topRoomNavigate[rand], centerPos, Quaternion.identity);
                        break;
                }

                room.transform.eulerAngles = new Vector3(
                    room.transform.eulerAngles.x,
                    room.transform.eulerAngles.y,
                    room.transform.eulerAngles.z + 180
                );
                room.GetComponent<roomVarScript>().isFlipped = true;
            }
            else if(dir == Vector2.left)
            {
                rand = Random.Range(0, 2);

                switch (rand)
                {
                    case 1:
                        rand = Random.Range(0, _sideRoomPuzzle.Length);
                        Instantiate(_sideRoomPuzzle[rand], centerPos, Quaternion.identity);
                        break;
                    case 2:
                        rand = Random.Range(0, _sideRoomNavigate.Length);
                        Instantiate(_sideRoomNavigate[rand], centerPos, Quaternion.identity);
                        break;
                    default:
                        rand = Random.Range(0, _sideRoomFight.Length);
                        Instantiate(_sideRoomFight[rand], centerPos, Quaternion.identity);
                        break;
                }
            }
            else
            {
                GameObject room;

                rand = Random.Range(0, 2);

                switch (rand)
                {
                    case 1:
                        rand = Random.Range(0, _sideRoomNavigate.Length);
                        room = Instantiate(_sideRoomNavigate[rand], centerPos, Quaternion.identity);
                        break;
                    case 2:
                        rand = Random.Range(0, _sideRoomPuzzle.Length);
                        room = Instantiate(_sideRoomPuzzle[rand], centerPos, Quaternion.identity);
                        break;
                    default:
                        rand = Random.Range(0, _sideRoomFight.Length);
                        room = Instantiate(_sideRoomFight[rand], centerPos, Quaternion.identity);
                        break;
                }

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
