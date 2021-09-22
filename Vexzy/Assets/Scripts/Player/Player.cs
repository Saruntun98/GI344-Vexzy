using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    enum MovementMode
    {
        Platformer,
        Strafe
    }

    [SerializeField]
    private MovementMode _movementMode = MovementMode.Strafe;
    [SerializeField]
    private float _walkSpeed = 3f;
    [SerializeField]
    private float _runningSpeed = 6f;
    [SerializeField]
    private float _gravity = 9.81f;
    [SerializeField]
    private float _gravityPlatformer = -12f;
    [SerializeField]
    private float _jumpSpeed = 3.5f;
    [SerializeField]
    private float _doubleJumpMultiplier = 0.5f;
    [SerializeField]
    private GameObject _cameraRig;
    [SerializeField] 
    private bool isCamera;
    [SerializeField] 
    private bool isRunning;

    public float jumpHeight = 1;

    public CharacterController _controller;

    private float _directionY;
    private float _currentSpeed;
    private Vector3 moveDirection;
    private bool _canDoubleJump = false;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public static Player _instance;
    public float speedSmoothTime = 0.1f;
    public Transform player;
    float speedSmoothVelocity;

    private float velocityY;
    private float _defaultSpeed;
    //private float targetSpeed;
    void Awake()
    {
        _defaultSpeed = _runningSpeed;
        _currentSpeed = _runningSpeed * 2;
        _instance = this;
    }
    
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //NodeSprintCheck();
        TakeAttack();
        CameraIsPressingKey = Input.GetKey(KeyCode.T);

        if (_movementMode == MovementMode.Strafe)
        {
            MovementStafe();
            NodeSprintUsingStamina();
        }

        if (_movementMode == MovementMode.Platformer)
        {
            MovementPlatformer();
            NodeSprintUsingStamina();
        }
        
    }
    
    private bool isPressingKey;
    private bool CameraIsPressingKey
    {
        get { return isPressingKey; }
        set
        {
            if(value == isPressingKey)
                return;
 
            isPressingKey = value;
            
            if(isPressingKey)
                isCamera = !isCamera;
        }
    }

    private void LateUpdate()
    {
        if (isCamera != true)
        {
            if (IsPlayerMoving() && _movementMode == MovementMode.Strafe) ;
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
        else
        {
            if (IsPlayerMoving() && _movementMode == MovementMode.Strafe) ;
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, _cameraRig.transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
    }

    private void TakeAttack()
    {
        if (Input.GetMouseButtonDown (0)) {
            //anim.SetBool ("Attack", true);
            Debug.Log ("Attack");
        } else {
            //anim.SetBool ("Attack", false);
        }
    }
    private bool IsPlayerMoving()
    {
        return Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;
    }

    public  void NodeSprintCheck()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && PlayerStatus._instance.stamina > 10)
        {
            isRunning = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || PlayerStatus._instance.stamina <= 10)
        {
            isRunning = false;
        }
    }
    
    public void NodeSprintUsingStamina()
    {
        if (isRunning && moveDirection != Vector3.zero)
        {
            _currentSpeed = _runningSpeed;
            PlayerStatus._instance.stamina -= (10 * Time.deltaTime);

            //bool running = Input.GetKey(KeyCode.LeftShift);
            //MovementStafe()
            //targetSpeed = (running) ? _runningSpeed : _walkSpeed;;
        }
        // else if (!isRunning && movement != Vector3.zero)
        // {
        //     playerSpeed = defaultSpeed;
        //     RegenStamina();
        // }
        else
        {
            _runningSpeed = _defaultSpeed;
            RegenStamina();
        }
    }

    void RegenStamina()
    {
        if (!isRunning)
        {
            if (PlayerStatus._instance.stamina < PlayerStatus._instance.maxStamina)
            {
                PlayerStatus._instance.stamina += (10 * Time.deltaTime);
            }
            else
            {
                PlayerStatus._instance.stamina = PlayerStatus._instance.maxStamina;
            }
        }

    }

    private void MovementStafe()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontalInput, 0, verticalInput);

        if (_controller.isGrounded)
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

        bool running = Input.GetKeyDown(KeyCode.LeftShift);
        float targetSpeed = (running) ? _runningSpeed : _walkSpeed;
        NodeSprintCheck();
        
        _currentSpeed = Mathf.SmoothDamp(_currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        moveDirection.y = _directionY;

        _controller.Move(_currentSpeed * Time.deltaTime * moveDirection);
        _controller.transform.Rotate(Vector3.up * horizontalInput * (200 * Time.deltaTime));
    }
    
    private void MovementPlatformer()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;
        bool running = Input.GetKey(KeyCode.LeftShift);

        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + _cameraRig.transform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }

        float targetSpeed = ((running) ? _runningSpeed : _walkSpeed) * inputDir.magnitude;
        
        NodeSprintCheck();
        _currentSpeed = Mathf.SmoothDamp(_currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        velocityY += Time.deltaTime * _gravityPlatformer;
        Vector3 velocity = transform.forward * _currentSpeed + Vector3.up * velocityY;

        _controller.Move(velocity * Time.deltaTime);
        _currentSpeed = new Vector2(_controller.velocity.x, _controller.velocity.z).magnitude;

        if (_controller.isGrounded)
        {
            velocityY = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_controller.isGrounded)
            {
                float jumpVelocity = Mathf.Sqrt(-2 * _gravityPlatformer * jumpHeight);
                velocityY = jumpVelocity;
            }
        }
    }
}