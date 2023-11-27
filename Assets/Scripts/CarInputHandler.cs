using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputHandler : MonoBehaviour
{
    private bool isBoostKeyHeld = false;
    private bool isMapKeyHeld = false;


    public Canvas mapCanvas;
    CarController carController;

    void Awake()
    {
        carController = GetComponent<CarController>();

        if (mapCanvas != null)
            mapCanvas.enabled = false;
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
}
