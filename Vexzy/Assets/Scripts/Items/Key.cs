using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("You got 1 key");
            if (PlayerStatus._instance != null)
            {
                //GameManager.instance._audioSource.PlayOneShot(soundFile);
                GameManager.instance.currentKeyItem += 1;
            }
            Destroy(gameObject);
        }
    }
}
