using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilTrailRendererHandler : MonoBehaviour
{
    CarController carController;
    TrailRenderer trailRenderer;

    void Awake()
    {
        carController = GetComponentInParent<CarController>();

        trailRenderer = GetComponent<TrailRenderer>();

        trailRenderer.emitting = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (carController.isOnOil == true)
            trailRenderer.emitting = true;
        else trailRenderer.emitting = false;
    }
}
