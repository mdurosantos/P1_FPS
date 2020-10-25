using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : Item
{
    [SerializeField]
    float health = 50.0f;
    public override void Pick(FPSController player)
    {
        base.Pick(player);
        HealthSystem healthSystem = player.GetComponent<HealthSystem>();
        if (healthSystem.getCurrentHealth() != healthSystem.getMaxHealth())
            healthSystem.TakeHealth(health);
        
        destroyItem();
    }
}
