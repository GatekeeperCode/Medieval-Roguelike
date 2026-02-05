using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighValueShopItem : MonoBehaviour
{
    public GameObject[] soldItems;
    public Text shopText;

    public int price;
    public GameObject item;

    PlayerMovement player;
    bool canBuy;

    // Start is called before the first frame update
    void Start()
    {
        item = soldItems[Random.Range(0, soldItems.Length)];
        price = Random.Range(item.GetComponent<itemScript>().shopCost-5, item.GetComponent<itemScript>().shopCost + 5);
        if (price <= 0) { price = 1; }

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        canBuy = false;

        shopText.text = item.name + "\nCost: " + price + " Gold";
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

    private void OnEnable()
    {
        item = soldItems[Random.Range(0, soldItems.Length)];
        price = Random.Range(item.GetComponent<itemScript>().shopCost - 5, item.GetComponent<itemScript>().shopCost + 5);
        if (price <= 0) { price = 1; }

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        canBuy = false;

        shopText.text = item.name + "\nCost: " + price + " Gold";
    }
}
