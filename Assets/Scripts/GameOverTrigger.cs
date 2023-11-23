using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverTrigger : MonoBehaviour
{
    public Image star1;
    public Image star2;
    public Image star3;

    public CanvasGroup menuCanvasGroup;
    public TMP_Text TimeText;

    Timer timer;
    WaypointManagerScript waypointManager;


    private void Awake()
    {
        timer = FindObjectOfType<Timer>();
        waypointManager = FindObjectOfType<WaypointManagerScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Car") && timer.isCounting && waypointManager.AllWaypointsUsed())
        {
            DisplayMenu();
        }
    }

    public void DisplayMenu()
    {
        UpdateTimerText();
        ShowStars();
        PauseGame();

        CanvasGroup menuCanvasGroup = FindObjectOfType<CanvasGroup>();

        // Show the menu
        if (menuCanvasGroup != null)
        {
            menuCanvasGroup.alpha = 1;
            menuCanvasGroup.interactable = true;
            menuCanvasGroup.blocksRaycasts = true;
        }
        else
        {
            Debug.LogError("menuCanvasGroup is null in DisplayMenu()");
        }
    }

    private void UpdateTimerText()
    {
        if (timer != null)
        {
            if (TimeText != null)
            {
                // Get the formatted time from the Timer script and update the time text in the ending menu.
                string formattedTime = timer.GetFormattedTime();
                TimeText.text = formattedTime;
            }
            else
            {
                Debug.LogError("TimeText is null");
            }
        }
    }

    private void ShowStars()
    {
        int amountOfStars = timer.CalculateStarRating();

        if (star1 != null && star2 != null && star3 != null)
        {
            // Reset
            star1.enabled = false;
            star2.enabled = false;
            star3.enabled = false;

            switch (amountOfStars)
            {
                case 3:
                    star1.enabled = true;
                    star2.enabled = true;
                    star3.enabled = true;
                    break;
                case 2:
                    star1.enabled = true;
                    star2.enabled = true;
                    star3.enabled = false;
                    break;
                case 1:
                    star1.enabled = true;
                    star2.enabled = false;
                    star3.enabled = false;
                    break;
                default:
                    star1.enabled = false;
                    star2.enabled = false;
                    star3.enabled = false;
                    break;
            }
        }
        else
        {
            Debug.LogError("One or more star Image components are null.");
        }
    }

    private void SetStarImage(Image starImage, bool filled)
    {
        if (starImage != null)
        {
            starImage.fillAmount = filled ? 1f : 0f;
        }
        else Debug.Log("starImage is null");
    }



    public void PauseGame()
    {
        Time.timeScale = 0; // Pause the game
        StopSounds();
    }

    private void StopSounds()
    {
        // Stop all audio
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.Stop();
        }
    }

    public void ResumeGame()
    {
        star1.enabled = false;
        star2.enabled = false;
        star3.enabled = false;
        Time.timeScale = 1; // Resume the game
    }




    // Button functions
    public void RestartScene()
    {
        ResumeGame();
        SceneManager.LoadScene("SampleScene");
    }

    public void NextScene()
    {
        ResumeGame();
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        ResumeGame();
        SceneManager.LoadScene("MainMenu");
    }
}