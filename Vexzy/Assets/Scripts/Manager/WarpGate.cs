using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpGate : MonoBehaviour
{
    public GameObject Warp;

    private void Update()
    {
        if(GameManager.instance.currentKeyItem >= GameManager.instance.keyItemCount)
        {
            Warp.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other)
    
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("You are in the Gate");
            if (PlayerStatus.instance != null)
            {
                PlayerStatus.instance.isOnGate = true;
                //PlayerStatus.instance.countGate = +1;
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

                //Player.instance.player.position = new Vector3(-62.35f, 42.99f, 69.99f);
                //Debug.Log("Player Positions is " + Player.instance.player.position);

                //PlayerPrefs.SetString("LastExitName", GameManager.instance.exitName);
                //SceneManager.LoadScene(GameManager.instance.nextScenes); 
                

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
