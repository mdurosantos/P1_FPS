using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneController : MonoBehaviour, DamageTaker
{
    public void TakeDamage(float damage)
    {
        Debug.Log("Damage taken by Drone");
    }
}
