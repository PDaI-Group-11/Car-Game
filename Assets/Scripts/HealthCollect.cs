using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollect : MonoBehaviour
{
   
    HealthManager healthManager;
    void Start()
    {
        healthManager = FindAnyObjectByType<HealthManager>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
          Debug.Log("Added Health");
          healthManager.AddHealth(100);
          Destroy(gameObject);     
    }
}
