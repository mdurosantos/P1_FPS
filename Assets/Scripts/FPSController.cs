using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSController : MonoBehaviour
{
    float mYaw;
    float mPitch;
    bool mDoJump = false;
    [Header("Rotation")]
    [SerializeField]
    float mSpeedYaw;
    [SerializeField]
    float mSpeedPitch;
    [SerializeField]
    float mMinPitch;
    [SerializeField]
    float mMaxPitch;
    [SerializeField]
    GameObject mPitchController;
    [SerializeField]
    bool mInvertPitch;
    [SerializeField]
    bool mInvertYaw;

    [Header("Move")]
    private CharacterController mCharacterController;
    [SerializeField]
    float mMoveSpeed;
    public KeyCode mForwardKey = KeyCode.W;
    public KeyCode mBackKey = KeyCode.S;
    public KeyCode mRightKey = KeyCode.D;
    public KeyCode mLeftKey = KeyCode.A;
    public KeyCode mJumpKey = KeyCode.Space;
    public KeyCode mRunKey = KeyCode.LeftShift;

    [SerializeField]
    bool mOnGround;
    [SerializeField]
    bool mContactCeiling;

    [SerializeField]
    float mRunMultiplier;
    float mVerticalSpeed = 0.0f;
    [Header("Jump")]
    [SerializeField]
    float mHeightJump;
    [SerializeField]
    float mHalfLengthJump;
    [SerializeField]
    float mDownGravityMultiplier;

    [Header("Weapon")]
    [SerializeField]
    WeaponStats weaponStats;
    [SerializeField]
    LayerMask layerMask;
    [SerializeField]
    Transform gunPositioner;
    [SerializeField]
    Transform gunExpeller;
    [SerializeField]
    float initialSpeed_x = 50.0f;
    [SerializeField]
    float initialSpeed_y = 100.0f;

    [SerializeField]
    float fireRate = 1.0f;
    Animator weaponAnimator;
    float nextFire = 0.0f;

    private int bulletsInMag = 36;
    private int bulletsLeft = 150;


    [SerializeField]
    Text ammoText;


    private void OnEnable()
    {
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        Cursor.visible = true;
    }

    private void Awake()
    {
        mYaw = transform.rotation.eulerAngles.y;
        mPitch = mPitchController.transform.rotation.eulerAngles.x;
        mCharacterController = GetComponent<CharacterController>();
        weaponAnimator = GetComponentInChildren<Animator>();
        ammoText.text = bulletsInMag + " / " + bulletsLeft;
    }

    private void Update()
    {

        if (CanJump()) mDoJump = true;
        if (Input.GetMouseButton(0) && Time.time > nextFire) //Input.GetMouseButtonDown(0)
        {
            nextFire = Time.time + fireRate;
            weaponAnimator.SetBool("shooting", true);
            Shoot();  
        }
        else 
        {
            weaponAnimator.SetBool("shooting", false);
        }
    }


    private void FixedUpdate()
    {
        Rotate();
        Move();
    }

    private void Rotate()
    {
        float xMouseAxis = Input.GetAxis("Mouse X");
        float yMouseAxis = Input.GetAxis("Mouse Y");
        mYaw += xMouseAxis * mSpeedYaw * (mInvertYaw ? -1:1);
        mPitch += yMouseAxis * mSpeedPitch * (mInvertPitch ? -1 : 1);
        mPitch = Mathf.Clamp(mPitch, mMinPitch, mMaxPitch);
        transform.rotation = Quaternion.Euler(0.0f, mYaw, 0.0f);
        mPitchController.transform.localRotation = Quaternion.Euler(mPitch, 0.0f, 0.0f);
    }

    private bool CanJump()
    {
        return Input.GetKeyDown(mJumpKey) && mOnGround;
    }

    private void Move()
    {
        Vector3 forward = new Vector3(Mathf.Sin(mYaw*Mathf.Deg2Rad), 0.0f, Mathf.Cos(mYaw*Mathf.Deg2Rad));//en función del yaw
        Vector3 right = new Vector3(Mathf.Sin((mYaw+90.0f)*Mathf.Deg2Rad), 0.0f, Mathf.Cos((mYaw+90.0f)*Mathf.Deg2Rad));//en función del yaw+90
        Vector3 lMovement = new Vector3();

        if (Input.GetKey(mForwardKey)) lMovement = forward;
        else if (Input.GetKey(mBackKey)) lMovement -= forward;
        if (Input.GetKey(mRightKey)) lMovement += right;
        else if (Input.GetKey(mLeftKey)) lMovement -= right;


        lMovement.Normalize();
        lMovement *= mMoveSpeed * Time.fixedDeltaTime * (Input.GetKey(mRunKey) ? mRunMultiplier:1.0f);

        float gravity = -2 * mHeightJump * mMoveSpeed * mRunMultiplier * mMoveSpeed * mRunMultiplier / (mHalfLengthJump * mHalfLengthJump);
        if (mVerticalSpeed < 0) gravity *= mDownGravityMultiplier;
        mVerticalSpeed +=  gravity * Time.fixedDeltaTime;
        lMovement.y = mVerticalSpeed * Time.fixedDeltaTime + 0.5f * gravity * Time.deltaTime * Time.deltaTime;

        CollisionFlags colls = mCharacterController.Move(lMovement);

        //mOnGround = (colls & CollisionFlags.Below) != 0;
        mOnGround = mCharacterController.isGrounded;
        mContactCeiling = (colls & CollisionFlags.Above) != 0;

        if (mOnGround) mVerticalSpeed = 0.0f;
        if (mContactCeiling && mVerticalSpeed > 0.0f) mVerticalSpeed = 0.0f;

        

        if (mDoJump)
        {
            mVerticalSpeed = 2 * mHeightJump * mMoveSpeed * mRunMultiplier / mHalfLengthJump;
            mDoJump = false;
        }
    }

    private void Shoot()
    {
        if(Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f)), out RaycastHit hitInfo, weaponStats.maxWeaponDist, layerMask))
        {
            
            if (bulletsInMag > 0)
            {
                bulletsInMag --; 
                //ShootParticles();
                ExpellBulets();
                ImpactParticles(hitInfo.point, hitInfo.normal);

                if (hitInfo.transform.gameObject.GetComponent<DamageTaker>() != null)
                {
                    hitInfo.transform.gameObject.GetComponent<DamageTaker>().TakeDamage(weaponStats.damage);
                }

                Debug.Log(bulletsInMag + " / " + bulletsLeft);
                ammoText.text = bulletsInMag + " / " + bulletsLeft;
            }

            else
            {
                Recharge();
            }
        }
    }

    private void ExpellBulets() 
    {
        GameObject clone = Instantiate(weaponStats.gunShells, gunExpeller.position, Quaternion.identity);
        clone.GetComponent<Rigidbody>().AddForce(transform.right * initialSpeed_x + transform.up * initialSpeed_y);
    }

    private void ShootParticles()
    {
        Instantiate(weaponStats.gunImpactParticles, gunPositioner.position, Quaternion.identity);
    }

    private void ImpactParticles(Vector3 point, Vector3 normal)
    {
        Instantiate(weaponStats.impactParticles, point, Quaternion.LookRotation(normal));
    }

    private void Recharge ()
    {
        if (bulletsLeft > 0)
        {
            int count = bulletsLeft - weaponStats.maxBulletsPerRound;
            if (count >= 0)
            {
                bulletsLeft -= weaponStats.maxBulletsPerRound; 
                bulletsInMag = weaponStats.maxBulletsPerRound;
            }
            else 
            {
                bulletsLeft = 0;
                bulletsInMag = count + weaponStats.maxBulletsPerRound;
            }
        }

        else 
        {
            Debug.Log("Out of Ammo");
        }
        
    }

}
