using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drone_Health : MonoBehaviour
{
    private float maxHealth = 100.0f;
    private float currentHealth;

    private GameObject healthBarUI;
    private Slider slider;

    [SerializeField]
    GameObject dropItem;


    public void Awake()
    {
        currentHealth = maxHealth;
        slider = GetComponentInChildren<Slider>();
        UpdateHealthBar();
    }
    public void DealDamage(float damage)
    {
        currentHealth -= damage;

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            if (dropItem != null)
                Instantiate(dropItem, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private float CalculateHealth()
    {
        return currentHealth / maxHealth;
    }

    private void UpdateHealthBar()
    {
        slider.value = CalculateHealth();
    }
    
}
