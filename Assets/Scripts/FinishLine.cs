using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("WIN");
            Timer timer = FindObjectOfType<Timer>();
            if (timer != null)
            {
                timer.StopTimer();
            }
        }
    }
}