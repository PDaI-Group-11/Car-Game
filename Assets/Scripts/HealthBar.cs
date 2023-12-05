using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Transform target;
    public Vector2 offset;
    [SerializeField] public Slider slider;
    private RectTransform rectTransform;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
        

    }
    public void SetHealth(float health)
    {
        slider.value = health;
    }
    private void LateUpdate()
    {
        if (target != null)
        {
           transform.localPosition = Camera.main.WorldToScreenPoint(target.position + (Vector3)offset);
        }
    }

}
