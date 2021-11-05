using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.tag == "Player")
        {
            if (PlayerStatus.instance != null)
            {
                PlayerStatus.instance.curHealth += 25;
            }
            Destroy(gameObject);
        }
    }
}
