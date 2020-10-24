using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pruea : MonoBehaviour
{
    [SerializeField] private GameObject player;
    
    public void EndReload()
    {
        player.GetComponent<PlayerShoot>().EndReload();
    }
}
