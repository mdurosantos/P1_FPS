using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKey : MonoBehaviour
{
    [SerializeField]
    private bool hasKey = false;
    

    public void setKey(bool key)
    {
        hasKey = key;
    }

    public void openDoor()
    {
        hasKey = false;
    }

    public bool getKey()
    {
        return hasKey;
    }
}
