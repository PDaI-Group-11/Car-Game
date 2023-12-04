using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointTrigger : MonoBehaviour
{
    [Header("Waypoint settings")]
    public int wayPointOrder;
    public int timeToAdd = 5;

    private bool hasBeenUsed = false;

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
                WaypointManagerScript waypointManager = FindObjectOfType<WaypointManagerScript>();

                if (waypointManager != null && waypointManager.IsNextWaypoint(this))
                {
                    toggleCarHasTheBomb();
                    timer.AddTime(timeToAdd);
                    hasBeenUsed = true;
                    // Notify the manager that this waypoint has been used
                    waypointManager.WaypointUsed(this);
                }
                else
                {
                    Debug.Log("Waypoint not in order. Waiting for the correct waypoint.");
                }
            }
        }
        else
        {
            Debug.Log("timer is null, Is timer present?");
        }
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
