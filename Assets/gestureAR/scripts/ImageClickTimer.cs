using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ImageClickTimer : MonoBehaviour
{
    public TMP_Text timerText;
    int myTime = 3;
    Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        timerText.text = myTime.ToString();
        timer = gameObject.AddComponent<Timer>();
        timer.StartTime(3.5f, DestroyTimerIcon);
    }

    // Update is called once per frame
    void Update()
    {
        int t = (int) timer.GetTime();
        if(3-t < myTime)
        {
            myTime = 3 - t;
            if(myTime >= 1)
            {
                timerText.text = myTime.ToString();
            }  
        }

    }

    void DestroyTimerIcon()
    {
        ImageCapture.Instance.OnTimerTimeout();
        Destroy(gameObject);
    }
}
