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

    /*[SerializeField] 
    public bool isFoundEgg = false;*/
    [SerializeField]
    private MovementMode _movementMode = MovementMode.Strafe;
    [SerializeField]
    private float _defaultSpeed;
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
    public float jumpHeight = 1;
    [SerializeField]
    private float _doubleJumpMultiplier = 0.5f;
    [SerializeField]
    private GameObject _cameraRig;
    [SerializeField] 
    private bool isCamera;
    [SerializeField] 
    private bool isRunning;

    [SerializeField] 
    private bool jump;
    //[SerializeField] 
    //private int jumpBool; 

    [SerializeField] 
    private bool isDead = false;

    [SerializeField]
    public CharacterController _controller;

    private Animator animator;
    private float _directionY;
    private float _currentSpeed;
    private Vector3 moveDirection;
    private bool _canDoubleJump = false;


    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public static Player instance;
    public float speedSmoothTime = 0.1f;
    public Transform player;
    float speedSmoothVelocity;

    private float velocityY;
    //private float targetSpeed;

    private bool _inputLocked;
    private float inputlockingTime = 0.1111f;

    void Awake()
    {
        instance = this;
        _defaultSpeed = _walkSpeed;
        _currentSpeed = _runningSpeed * 2;
    }
    
    void Start()
    {
        //jumpBool = Animator.StringToHash("Jump");
        animator = GetComponent<Animator>();
		//animator = transform.GetChild(1).GetChild(0).GetComponent<Animator>();
        //_controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        _cameraRig = GameObject.Find("Main Camera");
    }

    void Update()
    {

        NodeSprintUsingStamina();
		if (jump && Input.GetKeyUp(KeyCode.Space))
		{
			jump = false;
            Idle();
            animator.SetBool("Jump", false);
            //animator.SetTrigger("Jump", false);
		}

        //NodeSprintCheck();
        CameraIsPressingKey = Input.GetKey(KeyCode.T);


        if(!isDead)
        {
        if (_movementMode == MovementMode.Strafe)
        {
            MovementStafe();
            //NodeSprintUsingStamina();
        }

        if (_movementMode == MovementMode.Platformer)
        {
            MovementPlatformer();
            //NodeSprintUsingStamina();
        }
            Combo.instance.TakeAttack();
        }
        //CheckSpawnPet();
        /*if (_movementMode == MovementMode.Strafe)
        {
            MovementStafe();
            //NodeSprintUsingStamina();
        }

        if (_movementMode == MovementMode.Platformer)
        {
            MovementPlatformer();
            //NodeSprintUsingStamina();
        }
        TakeAttack();*/
    }
    
    /*public void CheckSpawnPet()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(EggStatus.Instance.isFoundEgg)
            {
                if(spawnPet.Instance.isSpawned == false)
                {
                    spawnPet.Instance.Spawn();
                }
                else
                {
                    spawnPet.Instance.Despawned();
                }
            }
        }
    }*/

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

    /*private void TakeAttack()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)) //if (Input.GetMouseButton(0)) 
        {
            StartCoroutine(Attack());
            //Attack(); 
            //BeginAttack();           
            Debug.Log ("Attack");
        }
    }*/
    private bool IsPlayerMoving()
    {
        return Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;
    }

    public  void NodeSprintCheck()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && PlayerStatus.instance.stamina > 10)
        {
            isRunning = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || PlayerStatus.instance.stamina <= 10)
        {
            isRunning = false;
        }
    }
    
    public void NodeSprintUsingStamina()
    {
        if (isRunning && moveDirection != Vector3.zero)
        {
            _currentSpeed = _runningSpeed;
            PlayerStatus.instance.stamina -= (10 * Time.deltaTime);
            //SoundManagerPlayer.PlaySound("Speed");
            //SoundManagerPlayer.instance.Run();
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
            _defaultSpeed = _walkSpeed;
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

    IEnumerator DelayJump()
    {
        yield return new WaitForSeconds(0.25f);
        animator.SetBool("Jump", false);
        jump = false;
    }

    private void MovementStafe()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        _directionY -= _gravity * Time.deltaTime;
        moveDirection = transform.TransformDirection(moveDirection);

        if (_controller.isGrounded)
        {
            _canDoubleJump = true;

        //Animator Fix
         if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
         {
            Walk();
         }
         else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
         {
            Run();
         }
         else if (moveDirection == Vector3.zero)
         {
            Idle();
         }
         
         moveDirection *= _defaultSpeed;

         if (Input.GetKeyDown(KeyCode.Space)) //Input.GetKeyDown(KeyCode.Space
         {
            Jump();
            SoundManagerPlayer.PlaySound("Jump");
            StartCoroutine(DelayJump());
         }

        if (Input.GetButtonDown("Jump"))
        {
           _directionY = _jumpSpeed;
           Jump();
           StartCoroutine(DelayJump());
        }
        //Animator Fix
        } 
        else
        {
            if (Input.GetButtonDown("Jump") && _canDoubleJump)
            {
                _directionY = _jumpSpeed * _doubleJumpMultiplier;
                _canDoubleJump = false;
                SoundManagerPlayer.PlaySound("Jump");
                StartCoroutine(DelayJump());
            }
        }

        //bool running = Input.GetKeyDown(KeyCode.LeftShift);
        float targetSpeed = (isRunning) ? _runningSpeed : _walkSpeed;
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

        //float targetSpeed = ((running) ? _runningSpeed : _walkSpeed) * inputDir.magnitude;
        float targetSpeed = ((isRunning) ? _runningSpeed : _walkSpeed) * inputDir.magnitude;
        
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
                StartCoroutine(DelayJump());
            }
        }
    }


//Animator
   private void Idle()
   {
      animator.SetFloat("Speed", 0.01f, 0.1f, Time.deltaTime);
   }
   
   private void Walk()
   {
      _defaultSpeed = _walkSpeed;
      animator.SetFloat("Speed", 0.35f, 0.1f, Time.deltaTime);
   }

   private void SoundWalkTap()
   {
       //SoundManagerPlayer.instance.PlayerFootstep();
       /*if(_defaultSpeed == _walkSpeed)
       {
           //SoundManagerPlayer.instance.PlayerFootstep();
       }*/
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
           //Walk();
           SoundManagerPlayer.instance.PlayerFootstep();
        }
        else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
        {
           //Run();
           SoundManagerPlayer.instance.PlayerFootstep();
        }
        else if (moveDirection == Vector3.zero)
        {
           SoundManagerPlayer.instance.PlayerFootStopStep();
        }
    }

   private void Run()
   {
      _defaultSpeed = _runningSpeed;
      animator.SetFloat("Speed", 1f, 0.1f, Time.deltaTime);
   }

   private void Jump()
   {
      animator.SetBool("Jump", true);
      //animator.SetTrigger("Jump");
      jump = true;
      //velocity.y = Mathf.Sqrt(jumpHeight * -2 * _gravity);   
   }

    public void Die()
    {
        animator.SetTrigger("PlayerDie");
        isDead = true;
        //_controller.enabled = false;
    }


   /*private void Attack()
   {
      animator.SetLayerWeight(animator.GetLayerIndex("Attack Layer"), 1);
      animator.SetTrigger("Attack");
      
      animator.SetLayerWeight(animator.GetLayerIndex("Attack Layer"), 0);
   }*/

   /*private IEnumerator Attack()
   {
      //animator.SetLayerWeight(animator.GetLayerIndex("Attack Layer"), 1);
      animator.SetTrigger("Attack");
      //BeginAttack();
      yield return new WaitForSeconds(3);
      //EndAttack();
      //animator.SetLayerWeight(animator.GetLayerIndex("Attack Layer"), 0);
   }*/

    /*void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Eggs"))
        {
            isFoundEgg = true;
        }
    }*/

    /*void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Eggs"))
        {
            isFoundEgg = false;
        }
    }*/
}