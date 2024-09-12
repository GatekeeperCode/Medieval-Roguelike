using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWaveRoom : MonoBehaviour
{
    public DoorwayBlockSpawn[] actionTriggers;
    public GameObject[] wave1;
    public GameObject[] wave2;
    public GameObject[] wave3;
    public GameObject[] chests;

    roomVarScript vars;
    int waveNum;

    // Start is called before the first frame update
    void Start()
    {
        vars = GetComponent<roomVarScript>();
        waveNum = 0;

        for(int i = 0; i<actionTriggers.Length; i++)
        {
            actionTriggers[i].manualOverride = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (vars.playerPresent)
        {
            if(waveNum == 0)
            {
                spawnMonsters(wave1);
                waveNum++;
            }
            else if(waveNum == 1)
            {
                if(monsterAliveCount(wave1))
                {
                    spawnMonsters(wave2);
                    waveNum++;
                }
            }
            else if(waveNum == 2)
            {
                if (monsterAliveCount(wave2))
                {
                    spawnMonsters(wave3);
                    waveNum++;
                }
            }
            else
            {
                if(monsterAliveCount(wave3))
                {
                    for (int i = 0; i < actionTriggers.Length; i++)
                    {
                        actionTriggers[i].roomClear = true;
                    }

                    for(int i=0; i<chests.Length; i++)
                    {
                        chests[i].SetActive(true);
                    }
                }
            }
        }
    }

    void spawnMonsters(GameObject[] wave)
    {
        for (int i = 0; i < wave.Length; i++)
        {
            wave[i].SetActive(true);
        }
    }

    bool monsterAliveCount(GameObject[] enemies)
    {
        int count = 0;
        for (int i = 0; i < enemies.Length; i++)
        {
            try
            {
                if (enemies[i].activeSelf)
                {
                    count++;
                }
            }
            catch (System.Exception) { }
        }

        if(count == 0)
        {
            return true;
        }

        return false;
    }
}
