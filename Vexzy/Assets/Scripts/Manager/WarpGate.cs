using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpGate : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("You are in the Gate");
            if (PlayerStatus.instance != null)
            {
                PlayerStatus.instance.isOnGate = true;
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        
            /*if (GameManager.instance.currentKeyItem >= GameManager.instance.keyItemCount)
            {
                //if (player.GetComponentInChildren<PlayerStatus>().isOnGate)
                if (PlayerStatus.instance.isOnGate = true);
                {
                    Debug.Log(true);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }*/
        

            }

            // Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("You exit the Gate");
            if (PlayerStatus.instance != null)
            {
                PlayerStatus.instance.isOnGate = false;
            }

            // Destroy(gameObject);
        }
    }
}
