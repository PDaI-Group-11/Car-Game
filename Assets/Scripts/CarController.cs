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
    public float maxSpeed = 20;

    float accelerationInput  = 0;
    float steeringInput = 0;
    float rotationAngle = 0;
    float velocityVsUp = 0;

    [Header("Boost settings")]
    public float boostForce = 2.5f;
    public float boostDuration = 3f;
    public float boostCooldown = 5f;

    public bool isBoosting = false;
    public float boostCooldownTimer = 0f;
    private float boostDurationTimer = 0f;

    Rigidbody2D rigidbody;
    

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        ApplyEngineForce();

        KillSideVelocity();

        ApplySteering();


        // Decrease boostCooldownTimer by the time that has passed.
        boostCooldownTimer -= Time.fixedDeltaTime;
    }

    void ApplyEngineForce()
    {
        velocityVsUp = Vector2.Dot(transform.up, rigidbody.velocity);

        // Limit the max speed
        if (velocityVsUp > maxSpeed && accelerationInput > 0)
            return;

        // Limit the max speed backwards to 50%
        if (velocityVsUp < -maxSpeed * 0.5 && accelerationInput > 0)
            return;
        
        // Limit for speed to any direction while accelerating
        if (rigidbody.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0 ) 
            return;


        if (accelerationInput == 0)
        {
            rigidbody.drag = Mathf.Lerp(rigidbody.drag, 3.0f, Time.fixedDeltaTime * 3);
        }
        else
        {
            rigidbody.drag = 0;
        }

        Vector2 engineForceVector = transform.up * accelerationInput * turnFactor;
        rigidbody.AddForce(engineForceVector,ForceMode2D.Force);
    }

    void ApplySteering()
    {

        float minSpeedBeforeAllowTurningFactor = (rigidbody.velocity.magnitude / 8);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);


        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;
        rigidbody.MoveRotation(rotationAngle);
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

    public void KillSideVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rigidbody.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(rigidbody.velocity, transform.right);

        rigidbody.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    float getLateralVelocity()
    {
        // Returns how fast the car is moving sideways.
        return Vector2.Dot(transform.right, rigidbody.velocity);
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
        if (Mathf.Abs(getLateralVelocity()) >  4.0f)
        {
            return true;
        }

        return false;
    }

    public float GetVelocityMagnitude()
    {
        return rigidbody.velocity.magnitude * 3;
    }

    private void StartBoost()
    {
        isBoosting = true;

        // Apply a boost force in the direction of the car's forward vector.
        rigidbody.AddForce(transform.up * boostForce, ForceMode2D.Impulse);

        // Set the boost duration and cooldown timers.
        boostDurationTimer = boostDuration;
        boostCooldownTimer = boostCooldown;

        StartCoroutine(StopBoost());

    } 

    IEnumerator StopBoost()
    {
        yield return new WaitForSeconds(0.1f); // Delay for 1 seconds
        isBoosting = false;
    }

    public void HandleBoostInput()
    {
        // Check if the car is allowed to boost (not currently boosting and boost not on cooldown).
        if (!isBoosting && boostCooldownTimer <= 0)
        {
            StartBoost();
        }
    }
}
