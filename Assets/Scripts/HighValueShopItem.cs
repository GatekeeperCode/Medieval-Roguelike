using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighValueShopItem : MonoBehaviour
{
    public int minGold;
    public int maxGold;
    public GameObject[] soldItems;

    public int price;
    public GameObject item;

    PlayerMovement player;
    bool canBuy;

    // Start is called before the first frame update
    void Start()
    {
        price = Random.Range(minGold, maxGold+1);
        item = soldItems[Random.Range(0, soldItems.Length)];
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        canBuy = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player._gold >= price)
        {
            canBuy = true;
            GetComponent<Image>().color = Color.green;
        }
        else
        {
            canBuy = false;
            GetComponent<Image>().color = Color.red;
        }
    }

    public void itemSold()
    {
        if(canBuy)
        {
            player._gold -= price;
            Instantiate(item, new Vector3(transform.position.x + Random.Range(-3,3), transform.position.y + 3, 0), Quaternion.identity);
        }
    }
}
