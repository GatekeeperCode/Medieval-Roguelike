using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class traderMenuShutoff : MonoBehaviour
{
    public GameObject tradeMenu;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("menuTurnoff");
    }

    private IEnumerator menuTurnoff()
    {
        yield return new WaitForSeconds(0.2f);
        tradeMenu.SetActive(false);
    }
}
