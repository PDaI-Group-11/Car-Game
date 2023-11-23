using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManagerScript : MonoBehaviour
{
    public List<WayPointTrigger> waypoints = new List<WayPointTrigger>();
    public GameOverTrigger gameOverTrigger;

    private void Awake()
    {
        // Ensure all waypoints are initialized properly  // Add all of the waypoints to the waypoint list
        waypoints.AddRange(FindObjectsOfType<WayPointTrigger>());
    }


    // Function used to remove a waypoint from the waypoint list
    public void WaypointUsed(WayPointTrigger waypoint)
    {
        waypoints.Remove(waypoint);
    }

    public bool AllWaypointsUsed()
    {
        if (waypoints.Count == 0) { 
            return true;
        }
        else return false;
    }
}
