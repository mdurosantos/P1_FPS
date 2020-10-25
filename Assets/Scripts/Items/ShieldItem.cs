using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldItem : Item
{
    [SerializeField]
    float shield = 50.0f;
    public override void Pick(FPSController player)
    {
        base.Pick(player);
        HealthSystem healthSystem = player.GetComponent<HealthSystem>();
        if (healthSystem.getCurrentShield() != healthSystem.getMaxShield())
        {
            healthSystem.TakeShield(shield);
            destroyItem();
        }
    }
}
