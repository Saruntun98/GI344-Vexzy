using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timingWorld : MonoBehaviour
{
    public static timingWorld instance;    
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public Text timeText;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // Starts the timer automatically
        //timerIsRunning = true;
    }

    public void IsTime()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                //GameManager.instance.RuleCheck();
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
