    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ChangeToSampleScene()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + +1);
        //SceneManager.LoadScene(1);
        SceneManager.LoadScene("SampleScene");
    }

    // Quit the game if the game is running in the editor or as a standalone build
    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}