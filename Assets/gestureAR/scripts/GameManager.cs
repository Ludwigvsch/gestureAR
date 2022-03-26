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
    private Gesture thumbsUp;
    private Gesture RockNRoll;
    private Gesture Ok;
    private Gesture crossedFingers;

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

    private Dictionary<string, Gesture> gestureLookup;

    void Start()
    {
        //get references
        fadeTitle = gameObject.AddComponent<FadeObject>();
        gameMenu = GameObject.FindGameObjectWithTag("GameMenu");
        titleText = GameObject.FindGameObjectWithTag("TitleText");
        timerInstance = gameObject.AddComponent<Timer>();
        mover = gameObject.AddComponent<MovementScript>();
        thumbsUp = new Gesture("How would you signal that you approve of something?", "Did you know that in Thailand, the thumbs up gesture is quite offensive? It is equivalent of saying “screw you” in Bangladesh, Iran, and Thailand.", "The gesture you made is incorrect. Here is the correct answer. Please observe.");
        RockNRoll = new Gesture("What hand signal would you use at a concert?", "Did you know that in Cuba, Brazil, Italy, Argentina and Spain, it means devil’s horn and sexual insult?", "In concert in US, a popular hand signal used is this ROCK & ROLL gesture to indicate support like “Hell yeah”, “rock on”, and “good times”");
        Ok = new Gesture("What hand signal would you use to indicate that you are fine with something in the US?", "Did you know that in France, Germany and Brazil, it can mean “screw you” in these following countries", "In concert in US, a popular hand signal used gesture to indicate approval.");
        crossedFingers = new Gesture("What hand signal would you use to symbolize hope?"
        , "Did you know that in Vietnam, it can mean “screw you”?", "In the US, this mean “good luck”. However, in Vietnam, this same gesture is actually an obscene gesture.");
        gestureLookup = new Dictionary<string, Gesture>();
        gestureLookup.Add("Thumb", thumbsUp);
        gestureLookup.Add("fine", Ok);
        gestureLookup.Add("rockNroll", RockNRoll);
        gestureLookup.Add("crossedFingers", crossedFingers);


        //generate scripts 


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
        if (tagName.Equals("Thumb"))
        {
            // correct answer, trigger correct screen
            ShowCultureInfo();
            Debug.Log("Correct Answer");
        }
        else
        {
            tryNumber++;
            if (tryNumber >= 4)
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

    public void StartTraining(string key)
    {
        //start training here
        switch (key)
        {
            case "Thumb":
                Instantiate(Resources.Load("thumbsUp") as GameObject);
                break;
            case "fine":
                Instantiate(Resources.Load("ok") as GameObject);
                break;
            case "crossedFingers":
                Instantiate(Resources.Load("crossedFingers") as GameObject);
                break;
            case "rockNroll":
                Instantiate(Resources.Load("rockNroll") as GameObject);
                break;
        }
    }
}
