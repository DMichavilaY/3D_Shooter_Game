using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    private PlayerControls controls;
    private Vector2 moveInput;
    private bool isJumping = false;
    private float jumpStartTime;
    private bool isAiming = false;

    public float moveSpeed = 5f;
    public float aimMoveSpeed = 2f;
    public float runMultiplier = 2f;
    public float jumpForce = 10f;
    public float jumpDuration = 1.0f;

    private Animator animator;

    public Camera terceraPersonaCamera;
    public Camera primeraPersonaCamera;
    private bool terceraPersonaActiva = true;

    private float rotationInput;
    private bool isRunning = false;

    private void Start()
    {
        ActivateTerceraPersonaCamera();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Character.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Character.Move.canceled += ctx => moveInput = Vector2.zero;

        controls.Character.Look.performed += ctx => rotationInput = ctx.ReadValue<Vector2>().x;
        controls.Character.Look.canceled += _ => rotationInput = 0f;

        controls.Character.Jump.performed += _ => Jump();
        controls.Character.Punch.performed += _ => Punch();
        controls.Character.Run.started += _ => StartRunning();
        controls.Character.Run.canceled += _ => StopRunning();

        controls.Character.Aim.performed += _ => ToggleAim();
        controls.Character.ToggleCamera.performed += _ => ToggleCamera();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        MoveCharacter();
        ManageAnimations();
    }

    private void MoveCharacter()
    {
        float currentMoveSpeed = isRunning ? moveSpeed * runMultiplier : moveSpeed;
        float currentSpeed = isAiming ? aimMoveSpeed : currentMoveSpeed;

        Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y) * currentSpeed * Time.deltaTime;
        transform.Translate(movement);

        float rotationAmount = rotationInput * currentSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * rotationAmount);

        if (isJumping)
        {
            float normalizedTime = (Time.time - jumpStartTime) / jumpDuration;
            float jumpHeight = Mathf.Sin(normalizedTime * Mathf.PI) * jumpForce;
            transform.Translate(Vector3.up * jumpHeight * Time.deltaTime);

            if (normalizedTime >= 1.0f)
            {
                OnJumpAnimationEnd();
            }
        }
    }

    private void ManageAnimations()
    {
        bool isMoving = moveInput.magnitude > 0.1f;
        bool isIdle = !isMoving && !isAiming;
        bool isAimWalking = isAiming && isMoving;

        animator.SetBool("Walk", isMoving && !isAiming);
        animator.SetBool("Idle", isIdle);
        animator.SetBool("AimWalk", isAimWalking);
        animator.SetBool("Run", isRunning && isMoving && !isAiming);
        animator.SetBool("Aim", isAiming && !isMoving);
    }

    private void Jump()
    {
        if (!isJumping)
        {
            animator.SetTrigger("Jump");
            isJumping = true;
            jumpStartTime = Time.time;
        }
    }

    private void OnJumpAnimationEnd()
    {
        isJumping = false;
    }

    private void Punch()
    {
        animator.SetTrigger("Punch");
    }

    private void StartRunning()
    {
        isRunning = true;
    }

    private void StopRunning()
    {
        isRunning = false;
    }

    private void ToggleAim()
    {
        isAiming = !isAiming;
    }

    private void ToggleCamera()
    {
        if (terceraPersonaActiva)
            ActivatePrimeraPersonaCamera();
        else
            ActivateTerceraPersonaCamera();
    }

    private void ActivateTerceraPersonaCamera()
    {
        terceraPersonaCamera.gameObject.SetActive(true);
        primeraPersonaCamera.gameObject.SetActive(false);
        terceraPersonaActiva = true;
    }

    private void ActivatePrimeraPersonaCamera()
    {
        terceraPersonaCamera.gameObject.SetActive(false);
        primeraPersonaCamera.gameObject.SetActive(true);
        terceraPersonaActiva = false;
    }
}
