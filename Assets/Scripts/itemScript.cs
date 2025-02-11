using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemScript : MonoBehaviour
{
    public enum Stats
    {
        speed,
        physicalStren,
        rangeStren,
        magicalStren,
        defense,
        health,
        gold,
        none
    }

    public Stats boostedStat;
    public Stats SecondStat;
    public Stats hiddenStat;

    public string itemName;

    public int boost;

    // Start is called before the first frame update
    void Start()
    {
        boost = Random.Range(1, 3);
    }
}
