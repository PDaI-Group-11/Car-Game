using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarCollision : MonoBehaviour
{
    private Rigidbody2D rb;
    public HealthManager healthManager;
    [SerializeField] float collisionThreshold; 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthManager = GetComponent<HealthManager>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision toimii?");
        // Check if the collision is with the car object
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Check the velocity of the car using its Rigidbody component
            float velocityMagnitude = rb.velocity.magnitude;
            // If the velocity is above the threshold, destroy the car object
            if (velocityMagnitude > collisionThreshold)
            {

                healthManager.TakeDamage(10);
                Debug.Log("I took 10 damage");
            }
            Debug.Log("Car destroyed due to collision!");
        }
    }
}