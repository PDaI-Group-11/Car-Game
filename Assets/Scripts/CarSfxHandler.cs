using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CarSfxHandler : MonoBehaviour
{

    [Header("Audio sources")]
    public AudioSource tiresSreechingAudioSource;
    public AudioSource engineAudioSource;
    public AudioSource carHitAudioSource;
    public AudioSource carBoostAudioSource;

    float desiredEnginePitch = 0.5f;
    float tireScreechPitch = 0.5f;

    CarController carController;

    void Awake()
    {
        carController = GetComponentInParent<CarController>();
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateEngineSFX();

        UpdateTiresScreechingSFX();

        CheckForBoostAudioSFX();
    }


    void UpdateEngineSFX()
    {
        // Get the cars velocity
        float velocityMagnitude = carController.GetVelocityMagnitude();

        // Increase the engine volume as the car goes faster
        float desiredEngineVolume = velocityMagnitude * 0.05f;

        // Make a car idle engine sound
        desiredEngineVolume = Mathf.Clamp(desiredEngineVolume, 0.2f, 1.0f);

        // Increase the volume gradually to desired engine volume
        engineAudioSource.volume = Mathf.Lerp(engineAudioSource.volume, desiredEngineVolume, Time.deltaTime * 10);

        // Edit the engine pitch by the car velocity
        desiredEnginePitch = velocityMagnitude * 0.2f;
        // Clamp the pitch so it has a minimum and maximum volume
        desiredEnginePitch = Mathf.Clamp(desiredEngineVolume, 0.5f, 2f);
        // Adjust the soure to desired pitch by certain timeframe
        engineAudioSource.pitch = Mathf.Lerp(engineAudioSource.pitch, desiredEnginePitch, Time.deltaTime * 1.5f);
    }

    void UpdateTiresScreechingSFX()
    {
        if (carController.IsTireScreeching(out float lateralVelocity, out bool isBraking))
        {
            // If the player is braking,
            if (isBraking)
            {
                // Increase the volume over time to max
                tiresSreechingAudioSource.volume = Mathf.Lerp(tiresSreechingAudioSource.volume, 0.6f, Time.deltaTime * 10);

                // Increase the tire screech pitch over time to 0.5f
                tireScreechPitch = Mathf.Lerp(tireScreechPitch, 0.5f, Time.deltaTime * 10);
            }
            else // If the player is drifting, edit the sound and the pitch by lateral velocity
            {
                tiresSreechingAudioSource.volume = Mathf.Abs(lateralVelocity) * 0.05f;
                tireScreechPitch = Mathf.Abs(lateralVelocity) * 0.1f;
            }
        }
        // Fade out the tire sreech if the car is not moving
        else tiresSreechingAudioSource.volume = Mathf.Lerp(tiresSreechingAudioSource.volume, 0, Time.deltaTime * 10);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // How fast was the two objects when they collided = relativeVelocity
        float relativeVelocity = collision.relativeVelocity.magnitude;

        float volume = relativeVelocity * 0.1f;

        carHitAudioSource.pitch = UnityEngine.Random.Range(0.95f, 1.05f);

        carHitAudioSource.volume = volume;

        // Only play the hit sound if sound is not being played
        if (!carHitAudioSource.isPlaying)
        {
            carHitAudioSource.Play();
        }

    }


    void CheckForBoostAudioSFX()
    {
        if (carController.isBoosting && !carBoostAudioSource.isPlaying)
        {
            carBoostAudioSource.Play();
            
        }
    }
    
}
