using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private FadeObject fadeTitle;
    private GameObject gameMenu;
    private GameObject titleText;
    private Timer timerInstance;
    public GameObject cube;
    private MovementScript mover;
    private int tryNumber = 1;

    private GameObject questionTemplate;

    private void Awake()
    {
        Instance = this;
    }

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

        //instantiate question template
        questionTemplate = Instantiate(Resources.Load("QuestionText") as GameObject, gameMenu.transform.position, gameMenu.transform.rotation);
        questionTemplate.SetActive(false);
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
        questionTemplate.SetActive(true);
        Interactable interactable = questionTemplate.GetComponentInChildren<Interactable>();
        //interactable.InteractableEvents.Add(OnGuess);
        gameMenu.SetActive(false);
    }

    public void OnGuess()
    {
        questionTemplate.SetActive(false);
        TriggerImageCapture tic = new TriggerImageCapture();
        tic.TriggerCapture();
    }

    public void ProcessAiOutput(string tagName)
    {
        // change with map.key
        if(tagName.Equals("Thumb"))
        {
            // correct answer, trigger correct screen
            ShowCultureInfo();
            Debug.Log("Correct Answer");
        }
        else 
        {
            tryNumber++;
            if(tryNumber >= 4)
            {
                // training begins
                Debug.Log("Out of tries");
            }
            else
            {
                // wrong answer, try again
                Debug.Log("Try Again");
                onClickFlagUI();
            }
        }
    }

    public void ShowCultureInfo()
    {
        //instantiate part 1 final panel
        GameObject part1FinalPanel = Instantiate(Resources.Load("Part1FinalPanel") as GameObject, gameMenu.transform.position, gameMenu.transform.rotation);
        // map.get(curentGesture).correct
        part1FinalPanel.GetComponentsInChildren<TMP_Text>()[1].text = "" + "Winner winner chicken dinner";
    }



}
