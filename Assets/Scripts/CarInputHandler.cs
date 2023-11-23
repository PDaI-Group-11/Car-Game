using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputHandler : MonoBehaviour
{
    CarController carController;

    void Awake()
    {
        carController = GetComponent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        carController.SetInputVector(inputVector);

        checkBoostInput();
    }

    private bool isBoostKeyHeld = false;

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
}
