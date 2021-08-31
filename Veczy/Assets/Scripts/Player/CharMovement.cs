using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour
{
    enum MovementMode
        {
            Platformer,
            Strafe
        }
    public static CharMovement Instance;

    [SerializeField]
    private MovementMode _movementMode = MovementMode.Strafe;
    [SerializeField] CharacterController _characterController;
    
    private Vector3 playerVelocity;
    private Vector3 movement;
    private Animator animator;
    private Vector3 gravity;

    [SerializeField] float playerSpeed;
    [SerializeField] private float _gravity = 9.81f;
    [SerializeField] private float _gravityPlatformer  = 9.81f;
    [SerializeField] private float _jumpSpeed   = 9.81f;
    [SerializeField] private float _doubleJumpMultiplier    = 9.81f;
    [SerializeField] bool isRunning = false;
    private bool _canDoubleJump = false;
    
    private float runningSpeed;
    private float defaultSpeed;
    private float _directionY;
    private float _currentSpeed;
    [SerializeField] float jumpHeight = 1;

    public float staminaPoint = 100;
    public float maxStaminaPoint;

    [HideInInspector] Transform cam;
    [HideInInspector] float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private float velocityY;
    float speedSmoothVelocity;
    
    void Awake()
    {
        Instance = this;

        gravity = Physics.gravity;
        cam = Camera.main.transform;

        defaultSpeed = playerSpeed;
        runningSpeed = playerSpeed * 2;
        maxStaminaPoint = staminaPoint;
    }
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        animator = transform.GetChild(1).GetChild(0).GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        SprintCheck();
        SprintUsingStamina();
        Movement();
        MovementStafe();
        SetAnim();
        if (_movementMode == MovementMode.Strafe)
        {
            MovementStafe();
        }

        if (_movementMode == MovementMode.Platformer)
        {
            Movement();
        }
    }
        void SprintCheck()
    {
        // tempSpeed = playerSpeed;
        if (Input.GetKeyDown(KeyCode.LeftShift) && staminaPoint > 10)
        {
            isRunning = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || staminaPoint <= 10)
        {
            isRunning = false;
        }
    }

    void SprintUsingStamina()
    {
        if (isRunning && movement != Vector3.zero)
        {
            playerSpeed = runningSpeed;
            staminaPoint -= (10 * Time.deltaTime);
        }
        // else if (!isRunning && movement != Vector3.zero)
        // {
        //     playerSpeed = defaultSpeed;
        //     RegenStamina();
        // }
        else
        {
            playerSpeed = defaultSpeed;
            RegenStamina();
        }
    }

    void RegenStamina()
    {
        if (!isRunning)
        {
            if (staminaPoint < maxStaminaPoint)
            {
                staminaPoint += (10 * Time.deltaTime);
            }
            else
            {
                staminaPoint = maxStaminaPoint;
            }
        }

    }

    private void MovementStafe()
    {

        Vector3 moveDirection = new Vector3(0, 0, 0);

        if (_characterController.isGrounded)
        {
            _canDoubleJump = true;

            if (Input.GetButtonDown("Jump"))
            {
                _directionY = _jumpSpeed;
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump") && _canDoubleJump)
            {
                _directionY = _jumpSpeed * _doubleJumpMultiplier;
                _canDoubleJump = false;
            }
        }

        _directionY -= _gravity * Time.deltaTime;

        moveDirection = transform.TransformDirection(moveDirection);

        bool running = Input.GetKey(KeyCode.LeftShift);
        float targetSpeed = (running) ? runningSpeed : playerSpeed;
        _currentSpeed = Mathf.SmoothDamp(_currentSpeed, targetSpeed, ref speedSmoothVelocity, turnSmoothTime);

        moveDirection.y = _directionY;

        _characterController.Move(_currentSpeed * Time.deltaTime * moveDirection);
    }
    
    private void MovementPlatformer()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;
        bool running = Input.GetKey(KeyCode.LeftShift);

        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }

        float targetSpeed = ((running) ? runningSpeed : playerSpeed) * inputDir.magnitude;
        _currentSpeed = Mathf.SmoothDamp(_currentSpeed, targetSpeed, ref speedSmoothVelocity, turnSmoothTime);

        velocityY += Time.deltaTime * _gravityPlatformer;
        Vector3 velocity = transform.forward * _currentSpeed + Vector3.up * velocityY;

        _characterController.Move(velocity * Time.deltaTime);
        _currentSpeed = new Vector2(_characterController.velocity.x, _characterController.velocity.z).magnitude;

        if (_characterController.isGrounded)
        {
            velocityY = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_characterController.isGrounded)
            {
                float jumpVelocity = Mathf.Sqrt(-2 * _gravityPlatformer * jumpHeight);
                velocityY = jumpVelocity;
            }
        }
    }
    
    void SetAnim()
    {
        animator.SetBool("Run", false);
        // animator.SetBool("Reverse", false);

        if (movement != Vector3.zero)
        {
            // animator.SetBool("Reverse", true);
            animator.SetBool("Run", true);
        }

    }
    void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
     
        movement = new Vector3(horizontal, 0, vertical).normalized;

        if (movement.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDir = Vector3.zero;
            if (vertical >= 0)
            {
                moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            }
            else
            {
                moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.back;
            }
            _characterController.Move(moveDir.normalized * Time.deltaTime * playerSpeed);
        }
    }
}

