using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private FadeObject fadeTitle;
    private GameObject gameMenu;
    private GameObject titleText;
    private Timer timerInstance;


    //events
    void onEnable()
    {

    }
    void onDisable()
    {
        fadeTitle.onFadeComplete -= ActivateGameMenu;
    }

    void SubscribeInstanceEvents()
    {
        fadeTitle.onFadeComplete += ActivateGameMenu;
    }

    void Start()
    {
        //get references
        fadeTitle = gameObject.AddComponent<FadeObject>();
        gameMenu = GameObject.FindGameObjectWithTag("GameMenu");
        titleText = GameObject.FindGameObjectWithTag("TitleText");
        timerInstance = gameObject.AddComponent<Timer>();

        SubscribeInstanceEvents();

        //alter environement for start
        inactiveGameMenu();
        FadeTitleWrapper();
    }

    private void inactiveGameMenu()
    {
        gameMenu.SetActive(false);
    }

    private void ActivateGameMenu()
    {
        Debug.Log("activate");
        gameMenu.SetActive(true);
    }

    //callback wrappers for use with timer
    public void FadeTitleWrapper()
    {
        StartCoroutine(fadeTitle.Fade(3f, titleText));
    }

    // Update is called once per frame
    void Update()
    {

    }


}
