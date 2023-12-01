using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointTrigger : MonoBehaviour
{
    
    bool hasBeenUsed = false;
    public int timeToAdd = 5;

    Timer timer;
    ExplosionParticleHandler explosionParticleHandler;

    private void Awake()
    {
        timer = FindObjectOfType<Timer>();
        explosionParticleHandler = FindObjectOfType<ExplosionParticleHandler>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasBeenUsed && other.CompareTag("Car") && timer.isCounting == true)
        {
            // Invert the current value of carHasTheBomb
            explosionParticleHandler.carHasTheBomb = !explosionParticleHandler.carHasTheBomb;
            timer.AddTime(timeToAdd);
            hasBeenUsed = true;
            // Notify the manager that this waypoint has been used
            FindObjectOfType<WaypointManagerScript>().WaypointUsed(this);
        }
    }
}
