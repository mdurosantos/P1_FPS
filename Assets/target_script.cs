using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target_script : MonoBehaviour, DamageTaker
{

    [SerializeField] int points;
    [SerializeField] string animation_name;
    [SerializeField] Transform shootingGallery;
    Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(animation_name, true);
    }

    public void TakeDamage(float damage)
    {
        shootingGallery.GetComponent<ShootingGallery>().ScorePoitns(points);
    }
}
