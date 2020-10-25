using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour, DamageTaker
{
    [SerializeField]
    float maxHealth = 100.0f;
    [SerializeField]
    float currentHealth;
    [SerializeField]
    float maxShield = 100.0f;
    [SerializeField]
    float currentShield;

    private void Awake()
    {
        currentHealth = maxHealth;
        currentShield = maxShield;
    }
    
    //Per cada impacte rebut, l’escut rebrà el 75% del mal i la vida un 25%. Quan l’escut arribi al 0%, tot el mal que facin els enemics al jugador, els rebrà la vida del jugador
    public void TakeDamage(float damage)
    {
        if(currentShield == 0)
            currentHealth -= damage;
        else
        {
            currentShield -= damage*0.75f;
            currentHealth -= damage*0.25f;

            if (currentShield < 0) currentShield = 0;
        }
        //if(currentHealth <= 0.0f) //gameover
    }

    public void TakeHealth(float health)
    {
        currentHealth += health;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }

    public float getCurrentHealth()
    {
        return currentHealth;
    }

    public float getMaxHealth()
    {
        return maxHealth;
    }

    public void TakeShield(float shield)
    {
        currentShield += shield;
        if (currentShield> maxShield) currentShield = maxShield;
    }

    public float getCurrentShield()
    {
        return currentShield;
    }

    public float getMaxShield()
    {
        return maxShield;
    }

}
