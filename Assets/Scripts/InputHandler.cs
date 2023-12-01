using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    private bool isBoostKeyHeld = false;
    private bool isMapKeyHeld = false;

    private Canvas mapCanvas;
    private Canvas pauseCanvas;

    CarController carController;
    PauseMenuHandler pauseMenuHandler;

    void Awake()
    {
        carController = GetComponent<CarController>();
        pauseMenuHandler = FindObjectOfType<PauseMenuHandler>();
        if (pauseMenuHandler == null)
            Debug.Log("pauseMenuHandler not found, Is Pause Menu Canvas present");



        mapCanvas = GameObject.Find("Map Canvas")?.GetComponent<Canvas>();
        pauseCanvas = GameObject.Find("Pause Menu Canvas")?.GetComponent<Canvas>();

        if (mapCanvas == null) Debug.Log("map canvas not found");
        if (pauseCanvas == null) Debug.Log("pause canvas not found");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        carController.SetInputVector(inputVector);

        checkBoostInput();
        checkMapInput();
        checkPauseInput();
    }

    void checkBoostInput()
    {
        // Check for boost input.
        if (Input.GetKeyDown(KeyCode.Space) && isBoostKeyHeld == false)
        {
            isBoostKeyHeld = true;
            carController.HandleBoostInput();
        }
        else if (Input.GetKeyUp(KeyCode.Space) && isBoostKeyHeld == true)
        {
            isBoostKeyHeld = false;
        }
    }

    void checkMapInput()
    {
        if (Input.GetKeyDown(KeyCode.M) && isMapKeyHeld == false)
        {
            isMapKeyHeld = true;

            if (mapCanvas != null)
                mapCanvas.enabled = true;
        }
        else if (Input.GetKeyUp(KeyCode.M) && isMapKeyHeld == true)
        {
            isMapKeyHeld = false;

            if (mapCanvas != null)
                mapCanvas.enabled = false;
        }
    }

    void checkPauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle pauseCanvas.enabled directly
            if (pauseCanvas != null)
            {
                pauseCanvas.enabled = !pauseCanvas.enabled;
            }

            // Call PauseGame or ResumeGame based on the pauseCanvas.enabled state
            if (pauseMenuHandler != null)
            {
                if (pauseCanvas.enabled)
                {
                    pauseMenuHandler.PauseGame();
                }
                else
                {
                    pauseMenuHandler.ResumeGame();
                }
            }
        }
    }
}
