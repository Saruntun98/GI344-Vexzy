using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLostWon : MonoBehaviour
{
    public static SoundLostWon instance;
    //static AudioSource audioSrc;
    public AudioClip soundWon;
    public AudioClip soundLost;

    void Awake() 
    {
       instance = this;
    }
    void Update() 
    {
    }

    public void Won()
    {
        GameManager.instance._audioSource.PlayOneShot(soundWon);
    }
    public void Lost()
    {
        if(GameManager.instance.gameOver)
        GameManager.instance._audioSource.PlayOneShot(soundLost);
    }
}