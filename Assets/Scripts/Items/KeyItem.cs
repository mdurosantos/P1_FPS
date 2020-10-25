using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : Item
{
    public override void Pick(FPSController player)
    {
        base.Pick(player);
        PlayerKey playerKey= player.GetComponent<PlayerKey>();
        playerKey.setKey(true);
        destroyItem();
    }
}
