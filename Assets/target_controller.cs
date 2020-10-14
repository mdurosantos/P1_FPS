using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target_controller : MonoBehaviour, DamageTaker
{
    public void TakeDamage(float damage)
    {
        Debug.Log("Damage taken");
    }
}
