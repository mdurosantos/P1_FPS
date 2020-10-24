using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    FPSController fpsController;
    void Awake()
    {
        fpsController = GetComponent<FPSController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject.GetComponent<Item>() != null)
        {
            other.gameObject.GetComponent<Item>().Pick(fpsController);
        }*/
        if (other.gameObject.TryGetComponent<Item>(out Item item))
        {
            item.Pick(fpsController);
        }
    }
}
