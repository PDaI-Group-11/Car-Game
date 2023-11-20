using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarCollision : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float collisionThreshold; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
                
                Destroy(this.gameObject);
                Debug.Log("Car destroyed due to collision!");
            }
            Debug.Log("Car destroyed due to collision!");
        }
    }
}