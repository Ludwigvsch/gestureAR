using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickHelper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void handleClick()
    {
        GameManager.Instance.OnGuess();
    }

    public void NextGestureClick()
    {
        if (GameManager.Instance.buildingVisible)
        {
            Destroy(GameManager.Instance.building);
            GameManager.Instance.buildingVisible = false;
        }
        if(GameManager.Instance.currentGesture++ <= 3)
        {
            Debug.Log("Next Gesture");
            GameManager.Instance.onClickFlagUI();
        }
        else
        {
            Debug.Log("Game Over");
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
