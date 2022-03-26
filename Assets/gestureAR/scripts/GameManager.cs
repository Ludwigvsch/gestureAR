using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private FadeObject fadeTitle;
    private GameObject gameMenu;
    private GameObject titleText;
    private Timer timerInstance;
    public GameObject cube;
    private MovementScript mover;
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
        RockNRoll = new Gesture("What hand signal would you use at a concert?", "Did you know that in (Insert a country name), it means devil’s horn and sexual insult?", "In concert in US, a popular hand signal used is this ROCK & ROLL gesture to indicate support like “Hell yeah”, “rock on”, and “good times”");
        Ok = new Gesture("What hand signal would you use to indicate that you are fine with something in the US?", "Did you know that in (Insert a country name), it can mean “screw you” in these following countries", "In concert in US, a popular hand signal used gesture to indicate approval.");
        crossedFingers = new Gesture("What hand signal would you use to symbolize hope?"
        , "Did you know that in (Insert a country name), it can mean “screw you” in Vietnam.", "In US, this mean “good luck”. However, in Vietnam, this same gesture is actually an obscene gesture.");
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
        questionTemplate = Instantiate(Resources.Load("QuestionText") as GameObject, gameMenu.transform.position, gameMenu.transform.rotation);
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



}
