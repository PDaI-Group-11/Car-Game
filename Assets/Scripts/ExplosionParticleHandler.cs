using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExplosionParticleHandler : MonoBehaviour
{
    private bool hasExploded = false;

    public bool carHasTheBomb = false;


    private AudioSource ExplosionAudioSource;

    private ParticleSystem explosionParticles;
    private CarSfxHandler carSfxHandler;
    private GameObject car;
    private CarController carController;

    // Start is called before the first frame update
    void Awake()
    {
        ExplosionAudioSource = GetComponent<AudioSource>();
        explosionParticles = GetComponent<ParticleSystem>();
        carSfxHandler = FindObjectOfType<CarSfxHandler>();
        carController = FindObjectOfType<CarController>();


        car = GameObject.Find("Car");

        if (car == null) Debug.Log("car object not found ");

        // Make sure the explosionParticles is assigned
        if (explosionParticles == null)
        {
            Debug.Log("Explosion Particle System is not assigned!");
        }

    }

    // Function to trigger the explosion
    public void Explode()
    {
        // Check if the Particle System is assigned
        if (explosionParticles != null && !hasExploded && carHasTheBomb)
        {
            // Play the explosion particle effect
            explosionParticles.Play();

            //Play explosion sound
            PlayExplosionSound();

            // Set the flag to true to prevent repeated explosions
            hasExploded = true;

            DisableCar();

            // Destroy the car after a delay (adjust this delay as needed)
            Destroy(car, 3f);
        }
    }

    private void DisableCar()
    {
        carController.stopTheCar();
    }


    void PlayExplosionSound()
    {
        carSfxHandler.PauseSounds();

        if (carSfxHandler != null)
        {
            carSfxHandler.playExplosionSoundSFX();
        }
        else Debug.Log("carSfxHandler not found");
    }
}
