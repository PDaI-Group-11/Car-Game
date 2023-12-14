using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBoostParticleSystemHandler : MonoBehaviour
{
    float particleEmissionRate = 0;


    CarController carController;
    ParticleSystem particleSystem;
    ParticleSystem.EmissionModule emissionModule;


    void Awake()
    {
        carController = GetComponentInParent<CarController>();
        particleSystem = GetComponent<ParticleSystem>();

        if (particleSystem == null) 
            Debug.Log("particleSystem is null");

        emissionModule = particleSystem.emission;
        emissionModule.rateOverTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (carController.hasBoost == true )
        {
            particleEmissionRate = 200;
        }
        else if (carController.hasBoost == false)
        {
            particleEmissionRate = Mathf.Max(Mathf.Lerp(particleEmissionRate, 0, Time.deltaTime * 5),0);

            // Ensure the value is exactly 0 if it's close enough
            if (particleEmissionRate < 0.01f)
            {
                particleEmissionRate = 0;
            }
        }

        emissionModule.rateOverTime = particleEmissionRate;
    }
}
