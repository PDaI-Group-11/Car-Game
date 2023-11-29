using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Timer settings")]
    public float initialTime = 10.0f;   // Initial time in seconds
    public float oneStarTime = 10.0f;   // 10 seconds
    public float twoStarTime = 8.0f;    // 8 seconds
    public float threeStarTime = 5.0f;  // 5 seconds
    public float initialCountDownTime = 5.0f; // 5 seconds countdown at the start

    [HideInInspector]
    public float countdownTime;

    [SerializeField] TextMeshProUGUI timerText;
    float remainingTime;
    public bool isCounting = false;

    GameOverTrigger gameOverTrigger;


    private void Awake()
    {
        Time.timeScale = 1;
        gameOverTrigger = FindObjectOfType<GameOverTrigger>();
        remainingTime = initialTime;
        countdownTime = initialCountDownTime;
    }

    void Update()
    {
        if (countdownTime > 0)
        {
            // Countdown
            countdownTime -= Time.deltaTime;

            int seconds = Mathf.CeilToInt(countdownTime);
            timerText.text = string.Format("{0}", seconds);

            isCounting = false; // During the countdown, set isCounting to false
        }
        else
        {
            // Timer is counting down
            remainingTime -= Time.deltaTime;

            if (remainingTime <= 0)
            {
                remainingTime = 0;
                isCounting = false;

                gameOverTrigger.DisplayMenu();
            }
            else
            {
                isCounting = true; // Enable isCounting after the countdown
            }

            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
    }

    public void RestartTimer()
    {
        remainingTime = initialTime;
        countdownTime = initialCountDownTime;
        isCounting = true;
    }

    public void AddTime(int seconds)
    {
        if (isCounting)
        {
            remainingTime += seconds;
        }
    }

    public int CalculateStarRating()
    {
        if (remainingTime >= threeStarTime)
        {
            return 3; // 3 stars
        }
        else if (remainingTime >= twoStarTime) 
        {
            return 2; // 2 stars
        }
        else if (remainingTime >= oneStarTime) 
        {
            return 1; // 1 star
        }
        else
        {
            return 0; // No stars
        }
    }

    public void StopTimer()
    {
        isCounting = false;
    }

    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        return FormatTime(minutes, seconds);
    }

    public float GetremainingTime()
    {
        // Return the elapsed time in seconds.
        return remainingTime;
    }

    private string FormatTime(int minutes, int seconds)
    {
        return string.Format("{0:0}:{1:00}", minutes, seconds);
    }
}
