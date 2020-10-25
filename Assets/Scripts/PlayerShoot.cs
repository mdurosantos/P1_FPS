using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    public KeyCode mReloadKey = KeyCode.R;
    [Header("Weapon")]
    [SerializeField] WeaponStats weaponStats;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform gunPositioner;
    [SerializeField] Transform gunExpeller;
    [SerializeField] float initialSpeed_x = 50.0f;
    [SerializeField] float initialSpeed_y = 100.0f;
    [SerializeField] float fireRate = 0.15f;
    float nextFire = 0;
    Animator weaponAnimator;


    [SerializeField] Text ammoText;

    [SerializeField] ObjectPool decalsPool;
    [SerializeField] ObjectPool bulletsPool;
    
    [SerializeField]
    private int bulletsInMag = 32;
    [SerializeField]
    private int bulletsLeft = 150;
    private bool recharging = false;
    private bool shooting = false;


    private void Awake()
    {
        ammoText.text = bulletsInMag + " / " + bulletsLeft;
        weaponAnimator = GetComponentInChildren<Animator>();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(mReloadKey) && bulletsInMag < weaponStats.maxBulletsPerRound && bulletsLeft > 0)
        {
            Recharge();
        }

        if (Input.GetMouseButton(0) && bulletsInMag > 0)
        {
            if (nextFire<=0)
            {
                nextFire = fireRate;
                Shoot();
            }
        }
       
        else
        {
            shooting = false;
            weaponAnimator.SetBool("shooting", false);
        }

        nextFire -= Time.deltaTime;
    }

    private void Shoot()
    {
        if (!recharging)
        {
            if (bulletsInMag > 0)
            {
                shooting = true;
                weaponAnimator.SetBool("shooting", true);
                bulletsInMag--;
                ExpellBulets();
                ShootParticles();
                AudioManager.PlaySound("shoot");

                if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f)), out RaycastHit hitInfo, weaponStats.maxWeaponDist, layerMask))
                {
                    ImpactParticles(hitInfo.point, hitInfo.normal);
                    if (hitInfo.transform.gameObject.GetComponent<DamageTaker>() != null)
                    {
                        hitInfo.transform.gameObject.GetComponent<DamageTaker>().TakeDamage(weaponStats.damage);
                    }
                    else
                    {
                        Decal(hitInfo.point, hitInfo.normal);
                    }
                }

                UpdateBulletsCounter();

                if (bulletsInMag == 0 && bulletsLeft > 0)
                {
                    Recharge();
                }
            }
        }
    }

    private void ExpellBulets()
    {
        //GameObject clone = Instantiate(weaponStats.gunShells, gunExpeller.position, Quaternion.identity);
        GameObject clone = bulletsPool.GetNextElement();
        clone.transform.position = gunExpeller.position;
        clone.transform.rotation = Quaternion.identity;

        clone.GetComponent<Rigidbody>().AddForce(transform.right * initialSpeed_x + transform.up * initialSpeed_y);
        Physics.IgnoreCollision(clone.GetComponent<Collider>(), GetComponent<Collider>());
    }

    private void ShootParticles()
    {
        GameObject clone = Instantiate(weaponStats.gunImpactParticles, gunPositioner.position, Quaternion.identity);
        //clone.transform.parent = gunPositioner.transform;
        Destroy(clone, clone.GetComponent<ParticleSystem>().main.duration);

    }

    private void ImpactParticles(Vector3 point, Vector3 normal)
    {
        GameObject clone = Instantiate(weaponStats.impactParticles, point, Quaternion.LookRotation(normal));
        Destroy(clone, clone.GetComponent<ParticleSystem>().main.duration);
        
    }

    private void Decal(Vector3 point, Vector3 normal)
    {
        var decal = decalsPool.GetNextElement();
        decal.transform.position = point;
        decal.transform.forward = normal;
    }

    private void Recharge()
    {
        //shooting = false;
        weaponAnimator.SetBool("shooting", false);

        recharging = true;
        weaponAnimator.SetBool("recharging", true);

        int bulletsToReload = weaponStats.maxBulletsPerRound - bulletsInMag;

        if (bulletsLeft - bulletsToReload >= 0)
        {
            bulletsInMag += bulletsToReload;
            bulletsLeft -= bulletsToReload;
        }
        else
        {
            bulletsInMag += bulletsLeft;
            bulletsLeft = 0;
        }
    }

    public void EndReload()
    {
        recharging = false;
        weaponAnimator.SetBool("recharging", false);
        UpdateBulletsCounter();
    }

    private void UpdateBulletsCounter()
    {
        ammoText.text = bulletsInMag + " / " + bulletsLeft;
        if (bulletsLeft == 0 && bulletsInMag == 0)
            ammoText.color = Color.red;
        
        else
            ammoText.color = Color.white;
    }

    public bool getShooting()
    {
        return shooting;
    }

    public void TakeAmmo(int ammo)
    {
        bulletsLeft += ammo;
        if (bulletsLeft > weaponStats.maxBulletsLeft) bulletsLeft = weaponStats.maxBulletsLeft;
        UpdateBulletsCounter();
        if (bulletsInMag == 0)
            Recharge();
    }

    public float getBulletsLeft()
    {
        return bulletsLeft;
    }

    public float getMaxBulletsLeft()
    {
        return weaponStats.maxBulletsLeft;
    }
}
