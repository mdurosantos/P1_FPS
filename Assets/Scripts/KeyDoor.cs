using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    Animation anim;

    private void Awake()
    {
        anim = GetComponent<Animation>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerKey>(out PlayerKey playerKey))
        {
            if (playerKey.getKey())
            {
                anim.CrossFade("KeyDoorOpen");
                AudioManager.PlaySound("door");
                playerKey.setKey(false);
            }
        }
    }


    
}
