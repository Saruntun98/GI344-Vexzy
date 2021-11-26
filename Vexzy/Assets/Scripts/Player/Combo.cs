using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    Animator playerAnim;
    public float timeSkill = 2f;
    public float damagaCom2 = 90f;
    public float damagaCom2time = 20f;
    public float damagaCom3 = 100f;
    public float damagaCom3time = 50f;
    [SerializeField] public BoxCollider[] swordCollider;
    [SerializeField] public BoxCollider[] swordCom2;
    [SerializeField] GameObject skillCom2;
    [SerializeField] public BoxCollider[] swordCom3;
    [SerializeField] GameObject skillCom3;
    public bool comboPossible;
    public int comboStep;
    bool inputSmash;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
        //swordCollider = GetComponentsInChildren<BoxCollider>();
        //swordCom2 = GetComponentsInChildren<BoxCollider>();
        //swordCom3 = GetComponentsInChildren<BoxCollider>();
        EndAttack();
        CCom2();
        CCom3();
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
    void Awake()
    {
        instance = this;
    }
    public void TakeAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            NormalAttack();
            BeginAttack();
        }
    }

    void Update()
    {
        /*if (Input.GetMouseButtonDown(1))
            SmashAttack();*/
        
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

    public void Com2()
    {
        foreach (var Com2 in swordCom2)
        {
            Com2.enabled = true;
            skillCom2.SetActive(true);
            StartCoroutine(DelayHideSkills());
        }
    }
    public void CCom2()
    {
        foreach (var Com2 in swordCom2)
        {
            Com2.enabled = false;
            skillCom2.SetActive(false);
        }
    }
    void SkilCom2()
    {
           Com2();
    }
    public void Com3()
    {
        foreach (var Com3 in swordCom3)
        {
            Com3.enabled = true;
            skillCom3.SetActive(true);
            StartCoroutine(DelayHideSkills());
        }
    }
    public void CCom3()
    {
        foreach (var Com3 in swordCom3)
        {
            Com3.enabled = false;
            skillCom3.SetActive(false);
        }
    }
    void SkilCom3()
    {
           Com3();
    }
    IEnumerator DelayHideSkills()
    {
        yield return new WaitForSeconds(timeSkill);
        //EndAttack();
        CCom2();
        CCom3();
        Debug.Log("Hide Com Skill");
        //Destroy(this.gameObject);
        //gameObject.GetComponent<SphereCollider>().enabled = true;
    }
}
