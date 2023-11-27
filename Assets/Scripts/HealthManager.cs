using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] GameObject Healthinstaniate;
    public float maxHealth = 100;
    public float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        HealthBarGenerator();
    }


  public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
    }

    void HealthBarGenerator()
    {
        if (Healthinstaniate != null) 
        { 
         GameObject healthinstan = Instantiate(Healthinstaniate);
        
        }
        else
        {
            Debug.Log("Prefab not assigned to Inspector.");
        }
    }
}
