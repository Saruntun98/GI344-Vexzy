using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDestroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (PlayerStatus._instance.curHealth <= 10 && other.gameObject.tag == "Enemy")
        {
            //Destroy(gameObject);
            Debug.Log("Player is Dead Destroy");
        }
    }
}
