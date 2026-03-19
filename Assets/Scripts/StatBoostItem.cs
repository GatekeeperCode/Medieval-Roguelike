using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stat
{
    _speed,
    _physicalStren,
    _rangeStren,
    _magicalStren,
    _defense,
    _health
}

public class StatBoostItem : MonoBehaviour
{
    public Stat chosenStat;
    public GameObject tooltip;
    GameObject tip;
    public GameObject player;
    public string msg;
    Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        position = new Vector3(transform.position.x, transform.position.y + .5f, 0);
        tip = Instantiate(tooltip, position, Quaternion.identity);
        msg = "Boosts a stat by 25%";
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 3)
        {
            tip.GetComponent<TooltipManager>().SetAndShowTooltip(msg);
        }
        else
        {
            tip.GetComponent<TooltipManager>().HideTooltip();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            switch(chosenStat)
            {
                case Stat._speed:
                    player.GetComponent<PlayerMovement>()._speed += player.GetComponent<PlayerMovement>()._speed * .25f;
                    break;
                case Stat._physicalStren:
                    player.GetComponent<PlayerMovement>()._physicalStren += player.GetComponent<PlayerMovement>()._physicalStren * .25f;
                    break;
                case Stat._rangeStren:
                    player.GetComponent<PlayerMovement>()._rangeStren += player.GetComponent<PlayerMovement>()._rangeStren * .25f;
                    break;
                case Stat._magicalStren:
                    player.GetComponent<PlayerMovement>()._magicalStren += player.GetComponent<PlayerMovement>()._magicalStren * .25f;
                    break;
                case Stat._defense:
                    player.GetComponent<PlayerMovement>()._defense += player.GetComponent<PlayerMovement>()._defense * .25f;
                    break;
                case Stat._health:
                    player.GetComponent<PlayerMovement>()._health += player.GetComponent<PlayerMovement>()._health * .25f; ;
                    break;
                default:
                    break;
            }

            Destroy(collision.gameObject);
        }
    }
}
