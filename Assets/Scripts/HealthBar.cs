using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider slider;
    HealthManager healthManager;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        healthManager = GetComponent<HealthManager>();
        if (healthManager != null)
        {
            slider.maxValue = healthManager.maxHealth;
        }
        else
        {
            Debug.LogError("HealthManager not found in the parent GameObject or its ancestors.");
        }
        }
        void Update()
    {
            if (slider != null && healthManager != null)
            {
                healthManager.currentHealth = slider.value;
            }

        }
}
