using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoItem : Item
{
    [SerializeField]
    int ammo = 50;
    public override void Pick(FPSController player)
    {
        base.Pick(player);
        PlayerShoot playerShoot = player.GetComponent<PlayerShoot>();
        if (playerShoot.getBulletsLeft() != playerShoot.getMaxBulletsLeft())
            playerShoot.TakeAmmo(ammo);

        //SOUND
        destroyItem();
    }
}
