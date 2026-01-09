using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraderScript : MonoBehaviour
{
    public GameObject tooltip;

    public GameObject tradingWindow;
    GameObject tip;
    GameObject player;
    
    Vector3 position;


    // Start is called before the first frame update
    void Start()
    {
        tradingWindow = GameObject.FindGameObjectWithTag("TraderMenu");
        player = GameObject.FindGameObjectWithTag("Player");
        position = new Vector3(transform.position.x, transform.position.y + .5f, 0);
        tip = Instantiate(tooltip, position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            tip.GetComponent<TooltipManager>().SetAndShowTooltip("Press E to talk to trader.");
            collision.GetComponent<PlayerMovement>().canTrade = true;
            player.GetComponent<PlayerMovement>().localTrader = gameObject;
            //TooltipManager._instance.SetAndShowTooltip("Press E to use map.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openTradeWindow()
    {
        tradingWindow.SetActive(true);
    }
}
