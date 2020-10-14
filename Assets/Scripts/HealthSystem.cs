using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DamageTaker
{
    void TakeDamage(float damage);
}

public class HealthSystem : MonoBehaviour, DamageTaker
{
    [SerializeField]
    float maxHealth = 100.0f;
    [SerializeField]
    float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0.0f) Destroy(gameObject);
    }

}
