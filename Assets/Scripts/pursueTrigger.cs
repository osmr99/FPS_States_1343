using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pursueTrigger : MonoBehaviour
{
    Enemy enemy;

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out FPSController FPSController))
        {
            enemy.startPursueIndex();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out FPSController FPSController))
        {
            enemy.startWanderIndex();
        }
    }
}
