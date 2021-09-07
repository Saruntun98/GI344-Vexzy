using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField]
    public float hp;
    [SerializeField] 
    public float stamina;
    [SerializeField]
    public bool isCombo;
    [SerializeField]
    public float aDamage;

    public static PlayerStatus _instance;
    
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
        SprintCheck();
        SprintUsingStamina();
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

}
