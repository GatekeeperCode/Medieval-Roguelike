using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    public GameObject[] _speedItems;
    public GameObject[] _physicalStrenItems;
    public GameObject[] _rangeStrenItems;
    public GameObject[] _magicalStrenItems;
    public GameObject[] _defenseItems;
    public GameObject[] _healthItems;
    public GameObject[] _goldItems;
    public GameObject[] _specialItems;

    // Start is called before the first frame update
    void Start()
    {
        GameObject spawnedItem = gameObject;

        //Item Type Randomization
        float rand = Random.Range(0f, 1f);

        if (rand<.05f) //Special Items
        {
            //spawnedItem = _specialItems[Random.Range(0, _specialItems.Length)];
            spawnedItem = _speedItems[Random.Range(0, _speedItems.Length)]; //Temp Code until I have made special items
        }
        else if(rand >= 0.95f && rand < 1f) //Speed Items
        {
            spawnedItem = _speedItems[Random.Range(0, _speedItems.Length)];
        }
        else if(rand >= 0.7f && rand < 0.95f) //Physical Items
        {
            spawnedItem = _physicalStrenItems[Random.Range(0, _physicalStrenItems.Length)];
        }
        else if(rand >= 0.55f && rand < 0.7f) //Range Items
        {
            spawnedItem = _rangeStrenItems[Random.Range(0, _rangeStrenItems.Length)];
        }
        else if(rand >= 0.45f && rand < 0.55f)//magical items
        {
            spawnedItem = _magicalStrenItems[Random.Range(0, _magicalStrenItems.Length)];
        }
        else if(rand >= 0.35f && rand < 0.45f)//defense items
        {
            spawnedItem = _defenseItems[Random.Range(0, _defenseItems.Length)];
        }
        else if(rand>=0.1f && rand<0.3f)//health items
        {
            spawnedItem = _healthItems[Random.Range(0, _healthItems.Length)];
        }
        else if(rand>=0.05f && rand<0.1f)//gold items
        {
            spawnedItem = _goldItems[Random.Range(0, _goldItems.Length)];
        }
        else
        {
            spawnedItem = _physicalStrenItems[Random.Range(0, _physicalStrenItems.Length)];
        }

        GameObject go = Instantiate(spawnedItem, transform.position, Quaternion.identity);

        //Mystery Stat Generation
        //if(Random.Range(0f, 1f)<0.2f)
        if (true)
        {
            int hold = Random.Range(0, 7);
            go.GetComponent<itemScript>().hiddenStat = (itemScript.Stats)hold;
        }    

        Destroy(gameObject);
    }
}
