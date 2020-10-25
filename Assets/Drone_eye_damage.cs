using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone_eye_damage : MonoBehaviour, DamageTaker
{
    [SerializeField] private Transform drone;
    [SerializeField] private float damageMultiplier; 
    
    public void Awake()
    {
        //drone = GetComponentInParent<GameObject>();
    }

    public void TakeDamage(float damage)
    {
        drone.GetComponent<Enemy>().setHIT();
        drone.GetComponent<Drone_Health>().DealDamage(damage * damageMultiplier);
        //drone.GetComponent<Drone_Health>().DealDamage(damage * damageMultiplier);
    }
}
