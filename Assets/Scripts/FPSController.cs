using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSController : MonoBehaviour
{
    public KeyCode m_DebugLockAngleKeyCode = KeyCode.I;
    public KeyCode m_DebugLockKeyCode = KeyCode.O;
    bool m_AimLocked = true;
    bool m_AngleLocked = false;

    float mYaw;
    float mPitch;
    bool mDoJump = false;
    [Header("Rotation")]
    [SerializeField] float mSpeedYaw;
    [SerializeField] float mSpeedPitch;
    [SerializeField] float mMinPitch;
    [SerializeField] float mMaxPitch;
    [SerializeField] GameObject mPitchController;
    [SerializeField] bool mInvertPitch;
    [SerializeField] bool mInvertYaw;

    [Header("Move")]
    [SerializeField] float mMoveSpeed;
    private CharacterController mCharacterController;
    public KeyCode mForwardKey = KeyCode.W;
    public KeyCode mBackKey = KeyCode.S;
    public KeyCode mRightKey = KeyCode.D;
    public KeyCode mLeftKey = KeyCode.A;
    
    public KeyCode mJumpKey = KeyCode.Space;
    public KeyCode mRunKey = KeyCode.LeftShift;
    [SerializeField] bool mOnGround;
    [SerializeField] bool mContactCeiling;
    [SerializeField] float mRunMultiplier;
    [SerializeField] float mJumpMultiplier;
    private bool running = false;
    float mVerticalSpeed = 0.0f;
    
    [Header("Jump")]
    [SerializeField] float mHeightJump;
    [SerializeField] float mHalfLengthJump;
    [SerializeField] float mDownGravityMultiplier;

    Animator weaponAnimator;
    private PlayerShoot shooting;



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
        shooting = GetComponent<PlayerShoot>();
        
    }

    private void Update()
    {

        if (CanJump()) mDoJump = true;
        
        

        CheckLockCursor();
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
        Vector3 forward = new Vector3(Mathf.Sin(mYaw * Mathf.Deg2Rad), 0.0f, Mathf.Cos(mYaw * Mathf.Deg2Rad));//en función del yaw
        Vector3 right = new Vector3(Mathf.Sin((mYaw + 90.0f) * Mathf.Deg2Rad), 0.0f, Mathf.Cos((mYaw + 90.0f) * Mathf.Deg2Rad));//en función del yaw+90
        Vector3 lMovement = new Vector3();

        if (Input.GetKey(mForwardKey)) lMovement = forward;
        else if (Input.GetKey(mBackKey)) lMovement -= forward;
        if (Input.GetKey(mRightKey)) lMovement += right;
        else if (Input.GetKey(mLeftKey)) lMovement -= right;


        lMovement.Normalize();

        if (Input.GetKey(mRunKey) && !lMovement.Equals(new Vector3()))
        {
            running = !shooting.getShooting();
        }
        else
        {
            running = false;
        }
        weaponAnimator.SetBool("running", running);

        lMovement *= mMoveSpeed * Time.fixedDeltaTime * (running ? mRunMultiplier : 1.0f);
        Jump(lMovement);
    }

    private void Jump(Vector3 lMovement)
    {
        float gravity = -2 * mHeightJump * mMoveSpeed * mJumpMultiplier * mMoveSpeed * mJumpMultiplier / (mHalfLengthJump * mHalfLengthJump);
        if (mVerticalSpeed < 0) gravity *= mDownGravityMultiplier;
        mVerticalSpeed += gravity * Time.fixedDeltaTime;
        lMovement.y = mVerticalSpeed * Time.fixedDeltaTime + 0.5f * gravity * Time.deltaTime * Time.deltaTime;

        CollisionFlags colls = mCharacterController.Move(lMovement);

        //mOnGround = (colls & CollisionFlags.Below) != 0;
        mOnGround = mCharacterController.isGrounded;
        mContactCeiling = (colls & CollisionFlags.Above) != 0;

        if (mOnGround) mVerticalSpeed = 0.0f;
        if (mContactCeiling && mVerticalSpeed > 0.0f) mVerticalSpeed = 0.0f;


        if (mDoJump)
        {
            mVerticalSpeed = 2 * mHeightJump * mMoveSpeed * mJumpMultiplier / mHalfLengthJump;
            mDoJump = false;
        }
    }


    private void OnApplicationFocus(bool focus)
    {
        if (m_AimLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    private void CheckLockCursor()
    {
        if (Input.GetKeyDown(m_DebugLockAngleKeyCode)) m_AngleLocked = !m_AngleLocked;
        if (Input.GetKeyDown(m_DebugLockKeyCode))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
            m_AimLocked = Cursor.lockState == CursorLockMode.Locked;
        }
    }

}
