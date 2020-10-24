using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoItem : Item
{
    public override void Pick(FPSController player)
    {
        base.Pick(player);
        //Si player está 100% 
        //      Nada
        //else
        //      Suma vida al player
        Debug.Log("Item ammo picked");
        destroyItem();
    }
}
