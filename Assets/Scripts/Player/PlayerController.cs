using System;
using System.Collections;
using System.Collections.Generic;
using InterationObject;
using Stat;
using UnityEngine;
using UnityEngine.InputSystem;
using Utill;

public class PlayerController : MonoBehaviour
{
    
    
    [Header("Movement")]
    public float moveSpeed;
    private Vector2 curMovementInput;
    public LayerMask groundLayerMask;
    private bool isButtonHeld;
    private float defaultMoveSpeed;

    [Header("Look")] 
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;

    public LayerMask objectLayerMask;
    // private Vector2 camCurYRot;
    private Vector2 mouseDelta;
    
    private Rigidbody _rigidbody;
    private HealthSystem _healthSystem;
    private JumpPowerSystem _jumpPowerSystem;
    private InteractableDetector _interactableDetector;

    private void Awake()
    {
        defaultMoveSpeed = moveSpeed;
        this.AssignComponent(ref _rigidbody);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        this.AssignComponent(ref _healthSystem);
        this.AssignComponent(ref _jumpPowerSystem);
        this.AssignComponent(ref _interactableDetector);
    }

    private void FixedUpdate()
    {
        if (isButtonHeld)
        {
            _jumpPowerSystem.ChargeJumpPower(Time.deltaTime);
        }
        Move();
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    public void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;
        
        _rigidbody.velocity = dir;
    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
        
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {
            if (context.phase == InputActionPhase.Performed)
            {
                isButtonHeld = true;
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                isButtonHeld = false;
                _rigidbody.AddForce(Vector2.up * _jumpPowerSystem.CurrentJumpPower, ForceMode.Impulse);
                _jumpPowerSystem.ChangeDefaultJumpPower();
            }
        }
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i],0.1f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }

    public void ResetMoveSpeed()
    {
        moveSpeed = defaultMoveSpeed;
    }
}
