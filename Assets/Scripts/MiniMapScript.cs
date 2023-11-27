using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapScript : MonoBehaviour
{

    public Transform car;

    /*private void LateUpdate()
    {
            Vector3 newPosition = car.position;
            newPosition.y = transform.position.y;
            transform.position = newPosition;
    }*/

    /*private void LateUpdate()
    {
        Vector3 newPosition = car.position;
        newPosition.z = transform.position.z; // Keep the existing z position
        transform.position = newPosition;
    }*/

    private void LateUpdate()
    {
        Vector2 newPosition = car.position;
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }
}
