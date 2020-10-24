using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : Item
{
    [SerializeField]
    float health = 50.0f; //mellor en scriptable objects
    public override void Pick(FPSController player)
    {
        base.Pick(player);
        //Si player está 100% 
        //      Nada
        //else
        //      Suma vida al player
        Debug.Log("Item health picked");
        destroyItem();
    }
}
