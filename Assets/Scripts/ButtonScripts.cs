using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour
{
    public GameObject mainScreen;
    public GameObject optionsScren;
    GameObject pmove;

    // Start is called before the first frame update
    void Start()
    {
        pmove = GameObject.FindGameObjectWithTag("Player");
    }

    public void onNewGamePressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void onMenuButtonPressed()
    {
        //change scene to main menu
        //print("Menu scene transition not enabled yet.");
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void onResetButtonPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void onOptionPressed()
    {
        mainScreen.SetActive(false);
        optionsScren.SetActive(true);
    }

    public void onResumePressed()
    {
        Time.timeScale = 1.0f;
        mainScreen.SetActive(false);
        pmove.GetComponent<PlayerMovement>().paused = false;
    }

    public void pauseBackButton()
    {
        mainScreen.SetActive(true);
        optionsScren.SetActive(false);
    }

    public void onStartPressed()
    {
        SceneManager.LoadScene(1);
    }

    public void quitButtonPressed()
    {
        Application.Quit();
    }
}
