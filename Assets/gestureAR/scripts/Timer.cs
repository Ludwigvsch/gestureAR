using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    /*
       Script purpose: start a timer and on stop, trigger a callback function when stopped
    */

    private float elapsedTime = 0f;
    private bool isCounting = false;
    private float secondsToCount;
    private System.Action callback;

    public Timer() { }

    void Update()
    {
        if (isCounting)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= secondsToCount)
            {
                if (callback != null)
                {
                    callback();
                }
                End();
            }
        }
    }
    public void StartTime(float seconds, System.Action callback)
    {
        this.secondsToCount = seconds;
        isCounting = true;
        this.callback = callback;
    }

    public void End()
    {
        isCounting = false;
        elapsedTime = 0f;
    }

    public float GetTime()
    {
        return elapsedTime;
    }
}
