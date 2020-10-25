using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyGadgets/Weapon", order = 1)]
public class WeaponStats : ScriptableObject
{
    [SerializeField]
    public float maxWeaponDist;
    [SerializeField]
    public GameObject impactParticles;
    [SerializeField]
    public GameObject gunImpactParticles;
    [SerializeField]
    public GameObject gunShells;
    [SerializeField]
    public float damage = 30.0f;
    [SerializeField] public int maxBulletsPerRound;
    [SerializeField] public int maxBulletsLeft = 250;
    [SerializeField] public float rechargeTime;
}
