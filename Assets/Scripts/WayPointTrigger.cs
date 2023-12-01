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
        if (timer != null)
        {
            if (!hasBeenUsed && other.CompareTag("Car") && timer.isCounting == true)
            {
                toggleCarHasTheBomb();
                timer.AddTime(timeToAdd);
                hasBeenUsed = true;
                // Notify the manager that this waypoint has been used
                FindObjectOfType<WaypointManagerScript>().WaypointUsed(this);
            }
        }
        else
            Debug.Log("timer is null, Is timer present?");
    }

    private void toggleCarHasTheBomb()
    {
        if (explosionParticleHandler != null)
        {
            // Invert the current value of carHasTheBomb
            explosionParticleHandler.carHasTheBomb = !explosionParticleHandler.carHasTheBomb;
        }
        else
        {
            Debug.Log("explosionParticleHandler is null");
        }
    }
}
