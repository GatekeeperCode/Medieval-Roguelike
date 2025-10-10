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

    public GameObject[] _rareRooms;

    public void spawnRoom(Vector2 centerPos, Vector2 dir)
    {
        float rand = Random.Range(0f, 1f);

        if(rand <= .5f)
        {
            int randy = Random.Range(0, 6);

            switch (randy)
            {
                case 1:
                case 3:
                    randy = Random.Range(0, _basicRoomPuzzle.Length);
                    Instantiate(_basicRoomPuzzle[randy], centerPos, Quaternion.identity);
                    break;
                case 2:
                case 4:
                    randy = Random.Range(0, _basicRoomNavigate.Length);
                    Instantiate(_basicRoomNavigate[randy], centerPos, Quaternion.identity);
                    break;
                case 5:
                    randy = Random.Range(0, _rareRooms.Length);
                    Instantiate(_rareRooms[randy], centerPos, Quaternion.identity);
                    break;
                default:
                    randy = Random.Range(0, _basicRoomFight.Length);
                    Instantiate(_basicRoomFight[randy], centerPos, Quaternion.identity);
                    break;
            }
        }
        else
        {
            if(dir == Vector2.down)
            {
                int randy = Random.Range(0, 2);

                switch(randy)
                {
                    case 1:
                        randy = Random.Range(0, _topRoomNavigate.Length);
                        Instantiate(_topRoomNavigate[randy], centerPos, Quaternion.identity);
                        break;
                    case 2:
                        randy = Random.Range(0, _topRoomPuzzle.Length);
                        Instantiate(_topRoomPuzzle[randy], centerPos, Quaternion.identity);
                        break;
                    default:
                        randy = Random.Range(0, _topRoomFight.Length);
                        Instantiate(_topRoomFight[randy], centerPos, Quaternion.identity);
                        break;
                }
            }
            else if(dir == Vector2.up)
            {
                GameObject room;

                int randy = Random.Range(0, 2);

                switch (randy)
                {
                    case 1:
                        randy = Random.Range(0, _topRoomFight.Length);
                        room = Instantiate(_topRoomFight[randy], centerPos, Quaternion.identity);
                        break;
                    case 2:
                        randy = Random.Range(0, _topRoomPuzzle.Length);
                        room = Instantiate(_topRoomPuzzle[randy], centerPos, Quaternion.identity);
                        break;
                    default:
                        randy = Random.Range(0, _topRoomNavigate.Length);
                        room = Instantiate(_topRoomNavigate[randy], centerPos, Quaternion.identity);
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
                int randy  = Random.Range(0, 2);

                switch (randy)
                {
                    case 1:
                        randy = Random.Range(0, _sideRoomPuzzle.Length);
                        Instantiate(_sideRoomPuzzle[randy], centerPos, Quaternion.identity);
                        break;
                    case 2:
                        randy = Random.Range(0, _sideRoomNavigate.Length);
                        Instantiate(_sideRoomNavigate[randy], centerPos, Quaternion.identity);
                        break;
                    default:
                        randy = Random.Range(0, _sideRoomFight.Length);
                        Instantiate(_sideRoomFight[randy], centerPos, Quaternion.identity);
                        break;
                }
            }
            else
            {
                GameObject room;

                int randy = Random.Range(0, 2);

                switch (randy)
                {
                    case 1:
                        randy = Random.Range(0, _sideRoomNavigate.Length);
                        room = Instantiate(_sideRoomNavigate[randy], centerPos, Quaternion.identity);
                        break;
                    case 2:
                        randy = Random.Range(0, _sideRoomPuzzle.Length);
                        room = Instantiate(_sideRoomPuzzle[randy], centerPos, Quaternion.identity);
                        break;
                    default:
                        randy = Random.Range(0, _sideRoomFight.Length);
                        room = Instantiate(_sideRoomFight[randy], centerPos, Quaternion.identity);
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
