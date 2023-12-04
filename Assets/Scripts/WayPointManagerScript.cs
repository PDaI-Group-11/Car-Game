using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManagerScript : MonoBehaviour
{
    public List<WayPointTrigger> waypoints = new List<WayPointTrigger>();
    public GameOverTrigger gameOverTrigger;

    private void Start()
    {
        SortWaypointsBasedOnOrder();
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

    public bool IsNextWaypoint(WayPointTrigger waypoint)
    {
        // Check if the specified waypoint is the next one in order
        return waypoints.Count > 0 && waypoints[0] == waypoint;
    }

    private void SortWaypointsBasedOnOrder()
    {
        // Ensure all waypoints are initialized properly  
        waypoints.AddRange(FindObjectsOfType<WayPointTrigger>());

        // Sort waypoints based on the wayPointOrder property
        waypoints.Sort((a, b) => a.wayPointOrder.CompareTo(b.wayPointOrder));
    }

}
