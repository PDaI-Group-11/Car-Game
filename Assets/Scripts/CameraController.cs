using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        // Find the CinemachineVirtualCamera component on this GameObject
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        if (virtualCamera != null)
        {
            // Automatically find and set the car GameObject as the follow target
            GameObject car = GameObject.FindWithTag("Car");
            if (car != null)
            {
                virtualCamera.Follow = car.transform;
            }
            else
            {
                Debug.LogWarning("Car GameObject not found in scene");
            }
        }
        else
        {
            Debug.LogError("CinemachineVirtualCamera component not found on this GameObject.");
        }
    }
}
