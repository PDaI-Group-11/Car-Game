using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    public Transform[] Targets;
    public float HideDistance;

    private int currentTargetIndex = 0;

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
