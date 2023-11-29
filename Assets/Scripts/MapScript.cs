using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{

    private Transform car;

    private void Awake()
    {
        car = GameObject.Find("Car")?.GetComponent<Transform>();
        if (car == null) Debug.Log("car canvas not found");
    }

    private void LateUpdate()
    {
        Vector2 newPosition = car.position;
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }
}
