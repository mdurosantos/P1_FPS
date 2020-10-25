using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoor : MonoBehaviour
{
    Animation anim;

    private void Awake()
    {
        anim = GetComponent<Animation>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<FPSController>() != null)
        {
            anim.CrossFade("AutomaticDoorOpen");
            AudioManager.PlaySound("door");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<FPSController>() != null)
        {
            anim.CrossFade("AutomaticDoorClose");
            AudioManager.PlaySound("door");
        }
    }

    
}
