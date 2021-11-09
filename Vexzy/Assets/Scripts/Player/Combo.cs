using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    Animator playerAnim;

    public bool comboPossible;
    public int comboStep;
    bool inputSmash;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
        swordCollider = GetComponentsInChildren<BoxCollider>();
    }

    public void ComboPossible()
    {
        comboPossible = true;
    }


    public void NextAtk()
    {
        if (!inputSmash)
        {
            if (comboStep == 2)
                playerAnim.Play("combostep2");
            if (comboStep == 3)
                playerAnim.Play("combostep3");
        }

        if (inputSmash)
        {
            if (comboStep == 1)
                playerAnim.Play("combostep1");
            if (comboStep == 2)
                playerAnim.Play("combostep2");
            if (comboStep == 3)
                playerAnim.Play("combostep3");
        }
    }
    

    public void ResetCombo()
    {
        comboPossible = false;
        inputSmash = false;
        comboStep = 0;
        EndAttack();
    }


    void NormalAttack()
    {
        if (comboStep == 0)
        {
            playerAnim.Play("combostep1");
            comboStep = 1;
            return;
        }
        
        if (comboStep != 0)
        {
            if (comboPossible)
            {
                comboPossible = false;
                comboStep += 1;
            }
        }
    }

   /* void SmashAttack()
    {
        if (comboPossible)
        {
            comboPossible = false;
            inputSmash = true;
        }
    }*/
    public static Combo instance;
    [SerializeField] private BoxCollider[] swordCollider;
    void Update()
    {
        
    }

    public void TakeAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            NormalAttack();
            BeginAttack();
        }
    }
    public void BeginAttack()
    {
        foreach (var weapon in swordCollider)
        {
            weapon.enabled = true;
        }
    }
    public void EndAttack()
    {
        foreach (var weapon in swordCollider)
        {
            weapon.enabled = false;
        }
    }
    void Awake()
    {
        instance = this;
    }
}
