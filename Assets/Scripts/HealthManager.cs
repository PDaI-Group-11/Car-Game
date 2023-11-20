using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] float maxHealth;
    public float currentHealth;


    void Start()
    {
        maxHealth = 100;
    }

    // Update is called once per frame
    void Update()
    {

        
    }

  public void TakeDamage(float currentHealth)
    {
        currentHealth = currentHealth - 10;
       
    }
}
