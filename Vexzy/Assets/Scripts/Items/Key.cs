using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public AudioClip soundFile;

    public float rotationSpeed = 10.0f;
    /*public static Key Instance;

    void Awake()
    {
        Instance = this;
    }*/

    private void Update()
    {
        gameChecked();
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.tag == "Player")
        {           
            Debug.Log("You got 1 key");
            if (PlayerStatus.instance != null)
            {            
                //SoundManagerPlayer.instance.s .PlayOneShot(soundFile);
                //KeySound.Instance._audioSource.PlayOneShot(soundFile);
                GameManager.instance._audioSource.PlayOneShot(soundFile);
                GameManager.instance.currentKeyItem += 1;
            }
            Destroy(gameObject);
        }
    }

    public void gameChecked()
    {
        //  GameManager.instance.RuleCheck();
        if (EggStatus.Instance.currentHealth > 0 && timingWorld.instance.timeRemaining > 0)
        {
            Debug.Log("Keys online");
        }
        else
        {
            Debug.Log("Egg die and time lost... del Keys");
            Destroy(gameObject);
        }
    }
}
