using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour
{
    public Text textComponet;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = true;
        gameObject.SetActive(false);
    }

    public void SetAndShowTooltip(string message)
    {
        gameObject.SetActive(true);
        textComponet.text = message;
    }

    public void HideTooltip()
    {
        textComponet.text = string.Empty;
        gameObject.SetActive(false);
    }
}
