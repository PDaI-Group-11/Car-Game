using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Car settings")]
    public float accelerationFactor = 40.0f;
    public float driftFactor = 0.95f;
    public float turnFactor = 6f;
    public float maxSpeed = 10;

    float accelerationInput = 0;
    float steeringInput = 0;
    float rotationAngle = 0;
    float velocityVsUp = 0;

    [HideInInspector]
    public bool isCarDestroyed = false;

    [Header("Boost settings")]
    public float boostForce = 2.5f;
    public float boostDuration = 3f;
    public float boostCooldown = 5f;
    public bool isBoosting = false;
    public bool hasBoost = false;
    public float boostCooldownTimer = 0f;
    private float boostDurationTimer = 0f;

    [Header("Collectable Boost settings")]
    public float collectableBoostForce = 4f;
    public float collectableBoostDuration = 2.0f;


    // Oil factors
    [HideInInspector]
    public bool isOnOil = false;
    private float originalDriftFactor;

    Rigidbody2D carRigidbody;
    CarSfxHandler carSfxHandler;


    void Awake()
    {
        carRigidbody = GetComponent<Rigidbody2D>();
        carSfxHandler = GetComponent<CarSfxHandler>();
    }

    private float logTimer = 0f;
    private float logInterval = 1f; // Log every 1 second

    private void FixedUpdate()
    {
        logTimer += Time.fixedDeltaTime;

        if (logTimer >= logInterval)
        {
            Debug.Log("Current Speed: " + carRigidbody.velocity.magnitude);
            logTimer = 0f; // Reset the timer
        }

        if (!isCarDestroyed)
        {
            ApplyEngineForce();

            KillSideVelocity();

            ApplySteering();
        }

        // Decrease boostCooldownTimer by the time that has passed.
        boostCooldownTimer -= Time.fixedDeltaTime;
    }

    void ApplyEngineForce()
    {
        if (accelerationInput == 0)
        {
            carRigidbody.drag = Mathf.Lerp(carRigidbody.drag, 3.0f, Time.fixedDeltaTime * 3);
        }
        else
        {
            carRigidbody.drag = 0;
        }

        velocityVsUp = Vector2.Dot(transform.up, carRigidbody.velocity);

        // Limit the max speed
        if (velocityVsUp > maxSpeed && accelerationInput > 0)
            return;

        // Limit the max speed backwards to 50%
        if (velocityVsUp < -maxSpeed * 0.5 && accelerationInput > 0)
            return;

        // Limit for speed to any direction while accelerating
        if (carRigidbody.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
            return;

        Vector2 engineForceVector = transform.up * accelerationInput * turnFactor;
        carRigidbody.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {

        float minSpeedBeforeAllowTurningFactor = (carRigidbody.velocity.magnitude / 8);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;
        carRigidbody.MoveRotation(rotationAngle);
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
        accelerationInput = inputVector.y;
    }

    public void KillSideVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody.velocity, transform.right);

        float currentDriftFactor = driftFactor;

        if (isOnOil)
        {
            currentDriftFactor = 1;
        }

        carRigidbody.velocity = forwardVelocity + rightVelocity * currentDriftFactor;
    }

    float getLateralVelocity()
    {
        // Returns how fast the car is moving sideways.
        return Vector2.Dot(transform.right, carRigidbody.velocity);
    }

    public bool IsTireScreeching(out float lateralVelocity, out bool isBraking)
    {
        lateralVelocity = getLateralVelocity();
        isBraking = false;

        // If the player is braking and the car is going forwards return true 
        if (accelerationInput < 0 && velocityVsUp > 0)
        {
            isBraking = true;
            return true;
        }

        // If the car has alot of side movement return true
        if (Mathf.Abs(getLateralVelocity()) > 4.0f)
        {
            return true;
        }

        return false;
    }

    public float GetVelocityMagnitude()
    {
        return carRigidbody.velocity.magnitude * 3;
    }


    public void StartBoost()
    {
        if (!isBoosting && boostCooldownTimer <= 0)
        {
            isBoosting = true;

            carSfxHandler.PlayBoostAudio();

            // Apply a boost force in the direction of the car's forward vector.
            carRigidbody.AddForce(transform.up * boostForce, ForceMode2D.Impulse);

            // Set the boost duration and cooldown timers.
            boostDurationTimer = boostDuration;
            boostCooldownTimer = boostCooldown;

            StartCoroutine(StopBoost());
        }
    }

    IEnumerator StopBoost()
    {
        yield return new WaitForSeconds(2);
        isBoosting = false;

        // Gradually slow down the car to the specified max speed
        while (carRigidbody.velocity.magnitude > maxSpeed)
        {
            carRigidbody.velocity = carRigidbody.velocity.normalized * maxSpeed;
            yield return null;
        }
    }


    public void StartCollectableBoost()
    {
        hasBoost = true;
            
        carSfxHandler.PlayBoostAudio();

        // Apply a boost force in the direction of the car's forward vector.
        carRigidbody.AddForce(transform.up * collectableBoostForce, ForceMode2D.Impulse);

        StartCoroutine(StopCollectableBoost());
    }

    IEnumerator StopCollectableBoost()
    {
        yield return new WaitForSeconds(collectableBoostDuration);

        hasBoost = false;

        // Gradually slow down the car to the specified max speed
        while (carRigidbody.velocity.magnitude > maxSpeed)
        {
            carRigidbody.velocity = carRigidbody.velocity.normalized * maxSpeed;
            yield return null;
        }
    }


    public void ApplySteeringEffect(float factor, float duration)
    {
        originalDriftFactor = driftFactor; // Store the original drift factor
        driftFactor *= factor;
        isOnOil = true;

        // Reset the steering effects after a certain time
        StartCoroutine(ResetSteeringEffects(duration));
    }

    IEnumerator ResetSteeringEffects(float duration)
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        driftFactor = originalDriftFactor;
        isOnOil = false;
    }



    public void stopTheCar()
    {
        
        isCarDestroyed = true;

        // Check for Renderer
        Renderer carRenderer = GetComponentInChildren<SpriteRenderer>();
        if (carRenderer != null)
        {
            carRenderer.enabled = false;
        }
        else
        {
            Debug.LogError("Renderer not found on GameObject: " + gameObject.name);
        }

        // Check for PolygonCollider2D
        PolygonCollider2D carCollider = GetComponentInChildren<PolygonCollider2D>();
        if (carCollider != null)
        {
            carCollider.enabled = false;
        }
        else
        {
            Debug.LogError("PolygonCollider2D not found on GameObject: " + gameObject.name);
        }

        carRigidbody.velocity = Vector2.zero;
    }
}
