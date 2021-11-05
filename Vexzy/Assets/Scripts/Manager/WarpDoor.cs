using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpDoor : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("You are in the Gate");
            if (PlayerStatus._instance != null)
            {
                //PlayerStatus._instance.isOnGate = true;
            }

            // Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("You exit the Gate");
            if (PlayerStatus._instance != null)
            {
                //PlayerStatus._instance.isOnGate = false;
            }

            // Destroy(gameObject);
        }
    }
}
