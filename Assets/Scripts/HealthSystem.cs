using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour, DamageTaker
{
    [SerializeField]
    float maxHealth = 100.0f;
    [SerializeField]
    float initialHealth = 100.0f;
    
    float currentHealth;
    [SerializeField]
    float maxShield = 100.0f;
    [SerializeField]
    float initialShield = 0f;
    
    float currentShield;
    [SerializeField] Text healthText;
    [SerializeField] Text shieldText;

    private void Awake()
    {
        currentHealth = initialHealth;
        currentShield = initialShield;
        healthText.text = "+ " + currentHealth;
        shieldText.text = "+ " + currentShield;
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
        healthText.text = "+ " + currentHealth;
        shieldText.text = "+ " + currentShield;
        //if(currentHealth <= 0.0f) //gameover
    }

    public void TakeHealth(float health)
    {
        currentHealth += health;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        healthText.text = "+ " + currentHealth;
        
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
        shieldText.text = "+ " + currentShield;
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
