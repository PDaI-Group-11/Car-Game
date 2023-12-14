using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Timer : MonoBehaviour
{
    [Header("Timer settings")]
    public float initialTime = 30.0f;   // Initial time in seconds
    public float oneStarTime = 13.0f;
    public float twoStarTime = 11.0f;
    public float threeStarTime = 7.0f;
    public float initialCountDownTime = 5.0f; //  seconds countdown at the start
    [HideInInspector]
    public float countdownTime;

    [SerializeField] TextMeshProUGUI timerText;
    float remainingTime;
    [HideInInspector]
    public bool isCounting = false;

    private TMP_Text objectiveText;

    GameOverTrigger gameOverTrigger;
    ExplosionParticleHandler explosionParticleHandler;


    private void Awake()
    {
        Time.timeScale = 1;
        gameOverTrigger = FindObjectOfType<GameOverTrigger>();
        explosionParticleHandler = FindObjectOfType<ExplosionParticleHandler>();
        remainingTime = initialTime;
        countdownTime = initialCountDownTime;

        objectiveText = GameObject.Find("Objective Text").GetComponent<TextMeshProUGUI>();
        if (objectiveText == null)
        {
            Debug.LogError("objectiveText component not found!");
        }
        else {
            Debug.Log("objectiveText enabled");
            objectiveText.enabled = true;
        }
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
            if (objectiveText.enabled == true)
            {
                objectiveText.enabled = false;
            }

            // Timer is counting down
            remainingTime -= Time.deltaTime;

            if (remainingTime <= 0)
            {
                remainingTime = 0;
                isCounting = false;

                if (explosionParticleHandler.carHasTheBomb)
                {
                    explosionParticleHandler.Explode();
                    // Delay the call to DisplayMenu by 2 seconds
                    Invoke("ShowMenu", 3f);
                }
                else ShowMenu();
                



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

    private void ShowMenu()
    {
        Debug.Log("Show Menu did something.");
        if (gameOverTrigger != null)
        {
            gameOverTrigger.DisplayMenu();
        }
        else Debug.Log("gameOverTrigger = null, Is gameOverCanvas present?");
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
        return remainingTime;
    }

    private string FormatTime(int minutes, int seconds)
    {
        return string.Format("{0:0}:{1:00}", minutes, seconds);
    }
}
