using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerImageCapture : MonoBehaviour
{
    public Canvas timerCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerCapture()
    {
        Instantiate(timerCanvas);
    }
}
