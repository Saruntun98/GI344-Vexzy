using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combo : MonoBehaviour
{
    Animator animator; 

    int cantidad_click; 
    bool puedo_dar_click; 

    void Start()
    {
        animator = GetComponent<Animator>();
        cantidad_click = 0;
        puedo_dar_click = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { Iniciar_combo(); }
    }

    void Iniciar_combo()
    {
        if (puedo_dar_click)
        {
            cantidad_click++;
        }

        if (cantidad_click == 1)
        {
            animator.SetInteger("attack", 1);
        }
    }

    public void Verificar_combo()
    {

        puedo_dar_click = false;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack_1") && cantidad_click == 1)
        {
            animator.SetInteger("attack", 0);
            puedo_dar_click = true;
            cantidad_click = 0;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack_1") && cantidad_click >= 2)
        {       
            animator.SetInteger("attack", 2);
            puedo_dar_click = true;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack_2") && cantidad_click == 2)
        {       
            animator.SetInteger("attack", 0);
            puedo_dar_click = true;
            cantidad_click = 0;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack_2") && cantidad_click >= 3)
        {       
            animator.SetInteger("attack", 3);
            puedo_dar_click = true;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack_3"))
        {      
            animator.SetInteger("attack", 0);
            puedo_dar_click = true;
            cantidad_click = 0;
        }
    }
}