using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private GameObject explosion;
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        AudioManager.PlaySound("enemy_weapon_shoot");
        rb.velocity = speed * transform.forward;
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Enemy")
        {
            DestroyProjectile();
        }
    }



    void DestroyProjectile()
    {
        AudioManager.PlaySound("enemy_weapon_impact");
        GameObject clone = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(clone, clone.GetComponent<ParticleSystem>().main.duration);
        Destroy(gameObject);
    }
}
