using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuHandler : MonoBehaviour
{
    public TMP_Text TimeText;
    private Canvas pauseCanvas;

    Timer timer;

    private void Awake()
    {
        timer = FindObjectOfType<Timer>();
        pauseCanvas = GameObject.Find("Pause Menu Canvas")?.GetComponent<Canvas>();
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

    private void StopSounds()
    {
        // Stop all audio
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.mute = true;
        }
    }

    private void UnpauseSounds()
    {
        // Resume audio
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.mute = false;
        }
    }

    public void PauseGame()
    {
        StopSounds();
        if (timer.countdownTime <= 0)
            UpdateTimerText();
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {   
        Time.timeScale = 1;
        pauseCanvas.enabled = false;
        UnpauseSounds();
    }


    // Button functions
    public void RestartScene()
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
