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

    string msg;

    // Start is called before the first frame update
    void Start()
    {
        tip = Instantiate(tooltip);

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

    private void OnMouseEnter()
    {
        print("Hover");
        tip.GetComponent<TooltipManager>().SetAndShowTooltip(msg);
    }

    private void OnMouseExit()
    {
        print("Gone");
        tip.GetComponent<TooltipManager>().HideTooltip();
    }
}
