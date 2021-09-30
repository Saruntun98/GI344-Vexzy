using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public static playerMovement instance;

    [SerializeField] CharacterController characterController;
    //private Vector3 gravityVector3;

    private Vector3 playerVelocity;
    private Vector3 movement;
    // private Vector3 velocity;
    private Animator animator;
    // private float horizontal;
    // private float vertical;

    private Vector3 gravity;

    [SerializeField] float playerSpeed;
    [SerializeField] bool isRunning = false;
    private float runningSpeed;
    private float defaultSpeed;
    // [SerializeField] float jumpHeight;

    public float staminaPoint = 100;
    public float maxStaminaPoint;

    [HideInInspector] Transform cam;
    [HideInInspector] float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    void Awake()
    {
        instance = this;

        gravity = Physics.gravity;
        cam = Camera.main.transform;

        defaultSpeed = playerSpeed;
        runningSpeed = playerSpeed * 2;
        maxStaminaPoint = staminaPoint;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        //animator = transform.GetChild(1).GetChild(0).GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        SprintCheck();
        SprintUsingStamina();
        Movement();
        SetAnim();
        
        // if (!characterController.isGrounded)
        // {
        //     characterController.Move(gravity * Time.deltaTime);
        //     Debug.Log("Player touch ground");
        //     // playerVelocity.y = 0;
        // }

        // Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        // characterController.Move(movement * Time.deltaTime * playerSpeed);

        // else
        // {
        //     if (Input.GetButtonDown("Jump"))
        //     {
        //         Debug.Log("Player jump");
        //         playerVelocity.y += Mathf.Sqrt(5 * -3f * gravity.y);
        //         characterController.Move(playerVelocity * Time.deltaTime);
        //     }
        // }
    }

    // private void FixedUpdate()
    // {
    //     Movement();
    // }

    void SprintCheck()
    {
        // tempSpeed = playerSpeed;
        if (Input.GetKeyDown(KeyCode.LeftShift) && staminaPoint > 10)
        {
            isRunning = true;
        }
        // else
        // {
        //     isRunning = false;
        // }

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

    void SetAnim()
    {
        animator.SetBool("run", false);
        // animator.SetBool("Reverse", false);

        if (movement != Vector3.zero)
        {
            // animator.SetBool("Reverse", true);
            animator.SetBool("run", true);
        }

        // if (horizontal == 0 && vertical == 0)
        // {
        //     animator.SetBool("Run", false);
        //     animator.SetBool("Reverse", false);
        // }
        // else if (vertical < 0)
        // {
        //     animator.SetBool("Reverse", true);
        //     animator.SetBool("Run", false);
        // }
        // else if (vertical > 0)
        // {
        //     animator.SetBool("Reverse", false);
        //     animator.SetBool("Run", true);
        // }
        // else
        // {
        //     animator.SetBool("Reverse", false);
        //     animator.SetBool("Run", true);
        // }
    }
    void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        movement = new Vector3(horizontal, 0, vertical).normalized;
        // Debug.Log(movement.magnitude);
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

            characterController.Move(moveDir.normalized * Time.deltaTime * playerSpeed);
        }

        // if (characterController.isGrounded)
        // {
        //     velocity.y = 0f;
        // }
        // else
        // {
        //     velocity.y -= gravity * Time.deltaTime;
        // }
        // characterController.Move(velocity);
    }
}
