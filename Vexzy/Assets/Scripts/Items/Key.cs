using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    
    public static Key Instance;

    void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        eggChecked();
    }
    
    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("You got 1 key");
            if (PlayerStatus.instance != null)
            {
                //GameManager.instance._audioSource.PlayOneShot(soundFile);
                GameManager.instance.currentKeyItem += 1;
            }
            Destroy(gameObject);
        }
    }

    public void eggChecked()
    {
        if (EggStatus.Instance.currentHealth < 0)
        {
            Debug.Log("Egg die del Keys");
            Destroy(gameObject);
        }
    }
}
