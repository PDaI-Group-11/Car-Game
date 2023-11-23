using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointTrigger : MonoBehaviour
{
    Timer timer;
    bool hasBeenUsed = false;
    public int timeToAdd = 5;

    private void Awake()
    {
        timer = FindObjectOfType<Timer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasBeenUsed && other.CompareTag("Car") && timer.isCounting == true)
        {
            timer.AddTime(timeToAdd);
            hasBeenUsed = true;
            // Notify the manager that this waypoint has been used
            FindObjectOfType<WaypointManagerScript>().WaypointUsed(this);
        }
    }
}
