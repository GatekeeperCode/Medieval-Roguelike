using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScripts : MonoBehaviour
{
    PlayerMovement pmove;

    // Start is called before the first frame update
    void Start()
    {
        pmove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    
    public void onMenuButtonPressed()
    {
        //change scene to main menu
        print("Menu scene transition not enabled yet.");
    }

    public void onOptionPressed()
    {
        //Open Options Screen
        print("Options Menu Not Enabled Yet");
    }

    public void onResumePressed()
    {
        Time.timeScale = 1.0f;
        pmove.pauseMenu.SetActive(false);
        pmove.paused = false;
    }
}
