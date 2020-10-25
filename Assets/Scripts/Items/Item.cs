using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
   public virtual void Pick(FPSController player)
    {

    }

    public void destroyItem()
    {
        Destroy(gameObject);
        AudioManager.PlaySound("item");
    }
}
