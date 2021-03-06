using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EggStatus.Instance.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        
        if(PlayerStatus.instance.curHealth <= 0)
        {
            Destroy(gameObject);
        }

        if(GameManager.instance.gameRound)
        {
            if (GameManager.instance.currentKeyItem == GameManager.instance.piller3)
            {
                Destroy(gameObject);
            }            
        }
        /*if(!GameManager.instance.isEnemyTime)
        {
            Destroy(gameObject);          
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (PlayerStatus._instance != null)
        {
            PlayerStatus._instance.hp -= 100;
        }*/
        
        if (other.gameObject.tag == "Eggs")
        {
            if (EggStatus.Instance != null)
            {
                EggStatus.Instance.currentHealth -= EnemyHealth.instance.damage;
                //EggStatus._instance.startHealth -= 100;
            }
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Player")
        {
            //EnemySounds.instance.EnemyDieTap();
            if (PlayerStatus.instance != null)
            {
                PlayerStatus.instance.curHealth -= EnemyHealth.instance.damageAttackPlayer;
                Debug.Log("hit by Enemy");
                PlayerStatus.instance.TakeHit();
            }
            Destroy(gameObject);
        }
        if (other.gameObject.name == "bullet(Clone)") 
        {
            Destroy(gameObject);
        }
        /*if (other.gameObject.name == "Weapon") {
            if (EnemyHealth._instance != null)
            {
                EnemyHealth._instance.currentHealth -= 30;
                Debug.Log ("HP Enemy: "+EnemyHealth._instance.currentHealth);
            }
            Destroy(gameObject);
        }*/
    }
}
