using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelParticleHandler : MonoBehaviour
{
    float particleEmissionRate = 0;

    CarController carController;
    //ParticleSystem particleSystem;
    new ParticleSystem particleSystem;
    ParticleSystem.EmissionModule emissionModule;

    private void Awake()
    {
        

        carController = GetComponentInParent<CarController>();
        particleSystem = GetComponent<ParticleSystem>();

        emissionModule = particleSystem.emission;
        emissionModule.rateOverTime = 0;
    }


    // Update is called once per frame
    void Update()
    {
        // Reduce the amount of particles emited over time
        particleEmissionRate = Mathf.Lerp(particleEmissionRate, 0, Time.deltaTime * 5 );
        emissionModule.rateOverTime = particleEmissionRate;


        if (carController.IsTireScreeching(out float lateralVelocity, out bool isBraking) && !carController.isCarDestroyed)
        {
            // If the player is braking, emitt a lot of smoke
            if (isBraking)
            {
                particleEmissionRate = 20;
            }
            else {
                // If the player is drifting, emitt smoke based on how much the player is drifting * 2
                particleEmissionRate = Mathf.Abs(lateralVelocity) * 2;
            }
        }

    }
}
