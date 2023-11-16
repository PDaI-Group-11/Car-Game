using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilTrigger : MonoBehaviour
{
    // Adjust the factor based on how much you want to affect the steering
    public float steeringFactor = 1.0f;
    // How long the steering will be effected
    public float steeringFactorDuration = 0.7f;

    private CarController carController;

    private void Awake()
    {
        carController = FindObjectOfType<CarController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Car") && carController != null)
        {
            carController.ApplySteeringEffect(steeringFactor, steeringFactorDuration);
        }
    }
}
