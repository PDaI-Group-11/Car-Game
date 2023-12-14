using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    public Transform[] Targets;
    public float HideDistance;

    private int currentTargetIndex = 0;
    private SpriteRenderer arrowSpriteRender;


    private void Start()
    {
        GameObject arrow = GameObject.Find("Arrow");
        arrowSpriteRender = arrow.GetComponent<SpriteRenderer>();

        if (Targets != null && Targets.Length > 0)
        {
            bool allTargetsSet = true;

            for (int i = 0; i < Targets.Length; i++)
            {
                if (Targets[i] == null)
                {
                    Debug.Log("Target at index " + i + " is missing or null");
                    allTargetsSet = false;
                    break;
                }
            }

            if (allTargetsSet)
            {
                arrowSpriteRender.enabled = true;
            }
            else
            {
                Debug.Log("One or more targets are missing or null");
            }
        }
        else
        {
            Debug.Log("Targets array is null or empty");
        }
    }

    void Update()
    {
        if (Targets != null && currentTargetIndex < Targets.Length)
        {
            var target = Targets[currentTargetIndex];
            if (target != null)
            {
                var dir = target.position - transform.position;

                if (dir.magnitude < HideDistance)
                {
                    SetChildrenActive(false);
                    currentTargetIndex++;
                }
                else
                {
                    SetChildrenActive(true);
                }

                var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else
            {
                Debug.Log("No target");
            }
        }
    }

    void SetChildrenActive(bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);
        }
    }
}
