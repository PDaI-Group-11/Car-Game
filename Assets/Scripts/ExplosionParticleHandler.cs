using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
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
    private SpriteRenderer C4spriteRenderer;


    // Start is called before the first frame update
    void Awake()
    {
        ExplosionAudioSource = GetComponent<AudioSource>();
        explosionParticles = GetComponent<ParticleSystem>();
        carSfxHandler = FindObjectOfType<CarSfxHandler>();
        carController = FindObjectOfType<CarController>();
        GameObject c4Object = GameObject.Find("C4Sprite");
        C4spriteRenderer = c4Object.GetComponent<SpriteRenderer>();

        car = GameObject.Find("Car");

        if (car == null) Debug.Log("car object not found ");

        // Make sure the explosionParticles is assigned
        if (explosionParticles == null)
        {
            Debug.Log("Explosion Particle System is not assigned!");
        }
    }

    private void Update()
    {
        CheckForBomb();
    }

    void CheckForBomb()
    {
        if (C4spriteRenderer != null && carHasTheBomb && !carController.isCarDestroyed)
        {
            C4spriteRenderer.enabled = true;
        }
        else if (C4spriteRenderer != null && !carHasTheBomb && !carController.isCarDestroyed)
        {
            C4spriteRenderer.enabled = false;
        }
        else if (C4spriteRenderer != null && carHasTheBomb && carController.isCarDestroyed)
        {
            C4spriteRenderer.enabled = false;
        }
        else Debug.Log("C4spriteRenderer is null");
    }



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

            // Destroy the car after a delay
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
