using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectableBoostScript : MonoBehaviour
{
    CarController carController;

    private void Awake()
    {
        carController = FindObjectOfType<CarController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (carController != null)
        {
            carController.StartCollectableBoost();
            Destroy(gameObject);
        }
        else Debug.Log("carController is null");
    }
}
