using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private GameObject healthBarPrefab;
    public float maxHealth = 100;
    public float currentHealth;
    private HealthBar healthBar;

    void Start()
    {

        if (healthBarPrefab != null)
        {
            healthBar = Instantiate(healthBarPrefab).GetComponentInChildren<HealthBar>();
        }
        healthBar.target = transform;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

       
    }

  public void TakeDamage(float damageAmount) 
    {
        currentHealth -= damageAmount;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Debug.Log("You dead! D:");
        }
        
    }

    

    public void Initialize()
    {
        

    }
}
