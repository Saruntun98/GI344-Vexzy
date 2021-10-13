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
                EggStatus.Instance.currentHealth -= 100;
                //EggStatus.instance.startHealth -= 100;
            }
            //EnemyMove.instance.nav.Stop();
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Player")
        {
            if (PlayerStatus._instance != null)
            {
                PlayerStatus._instance.curHealth -= 20;
            }
            //EnemyMove.instance.nav.Stop();
            Destroy(gameObject);
        }

        /*if (other.gameObject.name == "Weapon") {
            if (EnemyHealth._instance != null)
            {
                EnemyHealth._instance.currentHealth -= PlayerStatus._instance.aDamage;
                Debug.Log ("HP Enemy: "+EnemyHealth._instance.currentHealth);
                Debug.Log ("Player damage: "+PlayerStatus._instance.aDamage);               
            }
            //Destroy(gameObject);
        }*/
    }
}
