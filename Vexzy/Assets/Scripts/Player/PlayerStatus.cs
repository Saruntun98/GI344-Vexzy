using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStatus : MonoBehaviour
{
    [SerializeField]
    public float maxHealth = 150;
    [SerializeField]
    public float curHealth = 150;
    [SerializeField] 
    public float stamina = 100;
    [SerializeField]
    public bool isCombo;
    [SerializeField]
    public float aDamage = 30;
    [SerializeField] 
    public float timeLastHit = 2f;

    public static PlayerStatus instance;
    public static int Rounds;
    //public HealthBar healthBar;
    //public Image staminaBar;
    
    private float timer = 0f;
    
    public float maxStamina;
    public float comBoOne;
    public float comBoTwo;
    public float comBoThree;

    public bool isOnGate = false;
    //public bool IsDead{ get{ return curHealth == 0;} }

    [SerializeField] 
    private bool IsDead;


    void Awake()
    {
        instance = this;
        //maxStamina = stamina;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //healthBar.SetHealth(curHealth);
        //staminaBar.fillAmount = stamina;
        HealthCheck();
        //SprintCheck();
        SprintUsingStamina();

        if (curHealth <= 0) {
            IsDead = true;
        } 
        else {
            IsDead = false;
        }
        if (IsDead == true) {
            Die();
        }
    }

    public bool CheckIsDead
    {
        get
        {
            return IsDead;
        }
    }

    private void Start()
    {
        Rounds = 0;
        curHealth = maxHealth;
        maxStamina = stamina;
        IsDead = false;
        //healthBar.SetMaxHealth(maxHealth);
        //staminaBar.fillAmount = maxStamina;
        //Die();
    }
    void HealthCheck()
    {
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
    }
    void SprintCheck()
    {
        //Player._instance.();
        // tempSpeed = playerSpeed;
        /*if (Input.GetKeyDown(KeyCode.Alpha1) && stamina > 10)
        {
            isCombo = true;
        }
        // else
        // {
        //     isRunning = false;
        // }

        if (Input.GetKeyUp(KeyCode.Alpha1) || stamina <= 10)
        {
            isCombo = false;
        }*/
    }
    void SprintUsingStamina()
    {
        //Player._instance.NodeSprintUsingStamina();
        /*if (isCombo && comBoOne == aDamage)
        {
            aDamage = 150;
            stamina -= (10 * Time.deltaTime);
        }
        // else if (!isRunning && movement != Vector3.zero)
        // {
        //     playerSpeed = defaultSpeed;
        //     RegenStamina();
        // }
        else
        {
            aDamage = 0;
            RegenStamina();
        }*/
    }

    void RegenStamina()
    {
        if (!isCombo)
        {
            if (stamina < maxStamina)
            {
                stamina += (10 * Time.deltaTime);
            }
            else
            {
                stamina = maxStamina;
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.instance.GameOver && timer>=timeLastHit)
        {
            if (other.gameObject.name == "weaponA")
            {
                Debug.Log("hit");
                TakeHit();
                timer = 0;
            }
        }
    }

    private void TakeHit()
    {
        if (curHealth > 0)
        {
            Debug.Log("hit in Take hit");
            //anim.Play("Hurt");
            //blood.Play();
            curHealth -= 10;
            //audio.PlayOneShot(audio.clip);
            //GameManager.instance.HealthCheck();
        }
        if (curHealth <= 0)
        {
            IsDead = true; 
             Die();
        }        
    }

    void Die ()
    {
        //GameManager.instance.RuleCheck();
        Debug.Log("Die");
        //blood.Play();
        //Player._instance.animator.SetBool("PlayerDie", false);
        Player.instance.Die();
        Player.instance._controller.enabled = false;
    }
}
