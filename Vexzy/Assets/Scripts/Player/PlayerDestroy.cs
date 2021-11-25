using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDestroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (PlayerStatus.instance.curHealth <= 10 && other.gameObject.tag == "Enemy")
        {
            //Destroy(gameObject);
            Debug.Log("Player is Dead Destroy");
        }
        /*if (PlayerStatus.instance.countGate <= 3)
        {
            Destroy(gameObject);
            Debug.Log("Destroy player main scene");
        }*/
    }
}
