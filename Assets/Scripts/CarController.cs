using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Car settings")]
    public float accelerationFactor = 40.0f;
    public float driftFactor = 0.95f;
    public float turnFactor = 4.5f;
    public float maxSpeed = 20;

    float accelerationInput  = 0;
    float steeringInput = 0;

    float rotationAngle = 0;

    float velocityVsUp = 0;

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

}
