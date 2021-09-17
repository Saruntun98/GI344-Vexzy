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
    public float stamina;
    [SerializeField]
    public bool isCombo;
    [SerializeField]
    public float aDamage;
    [SerializeField] 
    public float timeLastHit = 2f;

    public static PlayerStatus _instance;
    public static int Rounds;
    
    private float timer = 0f;
    
    public float maxStamina;
    public float comBoOne;
    public float comBoTwo;
    public float comBoThree;

    void Awake()
    {
        _instance = this;
        maxStamina = stamina;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        HealthCheck();
        SprintCheck();
        SprintUsingStamina();
    }

    private void Start()
    {
        Rounds = 0;
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
        // tempSpeed = playerSpeed;
        if (Input.GetKeyDown(KeyCode.Alpha1) && stamina > 10)
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
        }
    }
    void SprintUsingStamina()
    {
        if (isCombo && comBoOne == aDamage)
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
        }
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
            if (other.tag == "weapon")
            {
                TakeHit();
                timer = 0;
            }
        }
    }
    private void TakeHit()
    {
        if (curHealth>0)
        {
            //anim.Play("Hurt");
            //blood.Play();
            curHealth -= 20;
            //audio.PlayOneShot(audio.clip);
            GameManager.instance.HealthCheck();
        }
        if (curHealth<=0)
        {
            Die();
        }
    }
    void Die ()
    {
        GameManager.instance.HealthCheck();
        //blood.Play();
        //anim.SetTrigger("HeroDie");
        Player._instance._controller.enabled = false;
    }
}
