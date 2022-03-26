using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private FadeObject fadeTitle;
    private GameObject gameMenu;
    private GameObject titleText;
    private Timer timerInstance;
    public GameObject cube;
    private MovementScript mover;

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
        mover = gameObject.AddComponent<MovementScript>();

        SubscribeInstanceEvents();

        //alter environement for start
        inactiveGameMenu();
        timerInstance.StartTime(7f, FadeTitleWrapper);
        StartCoroutine(mover.Move(7f, titleText, 1f));
    }



    private void inactiveGameMenu()
    {
        gameMenu.SetActive(false);
    }

    private void ActivateGameMenu()
    {
        gameMenu.SetActive(true);
    }

    //callback wrappers for use with timer
    public void FadeTitleWrapper()
    {
        StartCoroutine(fadeTitle.Fade(2f, titleText));
    }

    public void onClickFlagUI()
    {
        Transform position = gameMenu.transform;
        gameMenu.SetActive(false);

    }



}
