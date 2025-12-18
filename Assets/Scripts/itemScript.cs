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

    public int itemID;
    public int boost;

    public GameObject tooltip;
    GameObject tip;
    GameObject player;

    string msg;
    Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        position = new Vector3(transform.position.x, transform.position.y + .5f, 0);
        tip = Instantiate(tooltip, position, Quaternion.identity);

        gameObject.layer = 1;
        boost = Random.Range(1, 3);

        msg = boostedStat + ": " + boost + "\n";

        if(SecondStat != Stats.none)
        {
            msg += SecondStat + ": " + boost/2 + "\n";
        }
        if(hiddenStat != Stats.none)
        {
            msg += hiddenStat;
        }
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position)<3)
        {
            tip.GetComponent<TooltipManager>().SetAndShowTooltip(msg);
        }
        else
        {
            tip.GetComponent<TooltipManager>().HideTooltip();
        }
    }

    //private void OnMouseEnter()
    //{
    //    print("Hover");
    //    tip.GetComponent<TooltipManager>().SetAndShowTooltip(msg);
    //}

    //private void OnMouseExit()
    //{
    //    print("Gone");
    //    tip.GetComponent<TooltipManager>().HideTooltip();
    //}
}
