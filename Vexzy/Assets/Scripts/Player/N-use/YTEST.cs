using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YTEST : MonoBehaviour
{
    
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

    [SerializeField]
    public CharacterController _controller;

    private Animator animator;
    private float _directionY;
    private float _currentSpeed;
    private Vector3 moveDirection;
    private bool _canDoubleJump = false;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public static YTEST instance;
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
        instance = this;
    }
    
    void Start()
    {
        //animator = GetComponent<Animator>();
        animator = GetComponentInChildren<Animator>();
		//animator = transform.GetChild(1).GetChild(0).GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MovementStafe();
        //NodeSprintCheck();
        TakeAttack();
        CameraIsPressingKey = Input.GetKey(KeyCode.T);
        //SetAnim();
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
            if (IsPlayerMoving()) ;
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
        else
        {
            if (IsPlayerMoving()) ;
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, _cameraRig.transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
    }

    private void TakeAttack()
    {      
        if (Input.GetMouseButton(0)) 
        {
            //animator.SetLayerWeight(animator.GetLayerIndex("");
            animator.SetTrigger("Attack");
            Debug.Log ("Attack");
        }
        else
        {
            Idle();
        }
    }
    private bool IsPlayerMoving()
    {
        return Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;
    }

    public  void NodeSprintCheck()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && PlayerStatus.instance.stamina > 10)
        {
            isRunning = true;
            //Run();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || PlayerStatus.instance.stamina <= 10)
        {
            isRunning = false;
            //Idle();
        }
    }
    
    public void NodeSprintUsingStamina()
    {
        if (isRunning && moveDirection != Vector3.zero)
        {
            _currentSpeed = _runningSpeed;
            PlayerStatus.instance.stamina -= (10 * Time.deltaTime);

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
            if (PlayerStatus.instance.stamina < PlayerStatus.instance.maxStamina)
            {
                PlayerStatus.instance.stamina += (10 * Time.deltaTime);
            }
            else
            {
                PlayerStatus.instance.stamina = PlayerStatus.instance.maxStamina;
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

        //Idle();
        //
        if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            Walk();
        }
        else if(moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
        {
            Run();
        }
        else if(moveDirection == Vector3.zero)
        {
            Idle();
        }
        //
        
        _directionY -= _gravity * Time.deltaTime;

        moveDirection = transform.TransformDirection(moveDirection);

        bool running = Input.GetKeyDown(KeyCode.LeftShift);
        float targetSpeed = (running) ? _runningSpeed : _walkSpeed;
        NodeSprintCheck();

        _currentSpeed = Mathf.SmoothDamp(_currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        moveDirection.y = _directionY;

        _controller.Move(_currentSpeed * Time.deltaTime * moveDirection);
        _controller.transform.Rotate(Vector3.up * horizontalInput * (200 * Time.deltaTime));
        //Idle();

    if (moveDirection != Vector3.zero)
        {
            // animator.SetBool("Reverse", true);
            animator.SetBool("Run", true);
        }
    }
    /*void SetAnim()
    {
        animator.SetBool("run", false);
        // animator.SetBool("Reverse", false);
        Debug.Log("run");
        if (Input.GetKey(KeyCode.W))
        {
            // animator.SetBool("Reverse", true);
            animator.SetBool("run", true);
            Debug.Log("No run");
        }
    }*/

    private void Idle()
    {
        animator.SetFloat("Speed" ,0,0.1f, Time.deltaTime);
    }
    
    private void Walk()
    {
        animator.SetFloat("Speed" ,0.7f,0.1f, Time.deltaTime);
    }
    
    private void Run()
    {
        _currentSpeed = _runningSpeed * 2;
        animator.SetFloat("Speed" ,1,0.1f, Time.deltaTime);
    }
}
