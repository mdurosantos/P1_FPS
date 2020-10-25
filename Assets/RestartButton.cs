using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour, DamageTaker
{
    [SerializeField] Transform shootingGallery;
    public void TakeDamage(float damage)
    {
        shootingGallery.GetComponent<ShootingGallery>().Repeat();
    }
}
