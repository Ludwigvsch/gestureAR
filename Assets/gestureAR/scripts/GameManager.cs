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
    public GameObject trainingPanel;
    private Gesture thumbsUp;
    private Gesture RockNRoll;
    private Gesture Ok;
    private Gesture crossedFingers;
    private List<string> keysArray;
    public int currentGesture = 0;
    public GameObject correctAnswerPanel;
    private List<GameObject> models;
    public GameObject building;
    public bool buildingVisible = false;

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
        keysArray = new List<string>(gestureLookup.Keys);
        models = new List<GameObject>();


        //generate scripts 


        SubscribeInstanceEvents();

        //alter environement for start
        inactiveGameMenu();
        timerInstance.StartTime(7f, FadeTitleWrapper);
        StartCoroutine(mover.Move(7f, titleText, 1f));

        //instantiate question template
        questionTemplate = Instantiate(Resources.Load("QuestionText") as GameObject, gameMenu.transform.position, gameMenu.transform.rotation);
        questionTemplate.SetActive(false);

        // instantiate correct answer panel
        //instantiate correct answer panel
        correctAnswerPanel = Instantiate(Resources.Load("CorrectAnswerPanel") as GameObject, gameMenu.transform.position, gameMenu.transform.rotation);
        correctAnswerPanel.SetActive(false);

        trainingPanel = Instantiate(Resources.Load("TrainingPanel") as GameObject, gameMenu.transform.position, gameMenu.transform.rotation);
        trainingPanel.SetActive(false);
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
        correctAnswerPanel.SetActive(false);
        gameMenu.SetActive(false);
        questionTemplate.GetComponentsInChildren<TMP_Text>()[1].text =
            gestureLookup[keysArray[currentGesture]].question;
        questionTemplate.SetActive(true);
    }

    public void OnGuess()
    {
        questionTemplate.SetActive(false);
        TriggerImageCapture tic = new TriggerImageCapture();
        tic.TriggerCapture();
    }

    public void ProcessAiOutput(string tagName)
    {
        // if gesture is correct
        if (tagName.Equals(keysArray[currentGesture]))
        {
            // correct answer, trigger correct screen
            tryNumber = 1;
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
                trainingPanel.SetActive(true);
                trainingPanel.GetComponentsInChildren<TMP_Text>()[1].text =
                    gestureLookup[keysArray[currentGesture]].incorrect;
                // plug in hand coach training here
                StartTraining(keysArray[currentGesture]);
                // hand coach should call clickhelper.nextgestureclick()
            }
            else
            {
                // wrong answer, try again
                Debug.Log("Try Again");
                questionTemplate.GetComponentsInChildren<TMP_Text>()[0].text =
                    "Incorrect! Please try again";
                onClickFlagUI();
            }
        }
    }

    public void ShowCultureInfo()
    {
        correctAnswerPanel.SetActive(true);
        // map.get(curentGesture).correct
        correctAnswerPanel.GetComponentsInChildren<TMP_Text>()[1].text =
            gestureLookup[keysArray[currentGesture]].correct;
        switch (currentGesture)
        {
            case 0:
                building = Instantiate(Resources.Load("thailand")) as GameObject;
                buildingVisible = true;
                building.GetComponent<AudioSource>().Play();
                break;
            case 1:
                building = Instantiate(Resources.Load("italy")) as GameObject;
                buildingVisible = true;
                building.GetComponent<AudioSource>().Play();
                break;
            case 2:
                building = Instantiate(Resources.Load("france")) as GameObject;
                buildingVisible = true;
                building.GetComponent<AudioSource>().Play();
                break;
            case 3:
                building = Instantiate(Resources.Load("vietnam")) as GameObject;
                buildingVisible = true;
                building.GetComponent<AudioSource>().Play();
                break;

        }
        Debug.Log("Building created");
    }

    public void ShowFinalPanel()
    {
        GameObject finalPanel = Instantiate(Resources.Load("FinalPanel") as GameObject, gameMenu.transform.position, gameMenu.transform.rotation);
    }

    public void StartTraining(string key)
    {
        //start training here
        switch (key)
        {
            case "Thumb":
                GameObject g1 = Instantiate(Resources.Load("thumbsUp") as GameObject);
                models.Add(g1);
                break;
            case "fine":
                GameObject g2 = Instantiate(Resources.Load("ok") as GameObject);
                models.Add(g2);
                break;
            case "crossedFingers":
                GameObject g3 = Instantiate(Resources.Load("fingersCrossed") as GameObject);
                models.Add(g3);
                break;
            case "rockNroll":
                GameObject g4 = Instantiate(Resources.Load("rockNroll") as GameObject);
                models.Add(g4);
                break;
        }

    }

    public void RemoveModels()
    {
        for (int i = 0; i < models.Count; i++)
        {
            Destroy(models[i]);
        }
    }


}
