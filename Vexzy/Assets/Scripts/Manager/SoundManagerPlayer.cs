using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerPlayer : MonoBehaviour
{
    public static AudioClip attack, jump, speed;
    public static SoundManagerPlayer instance;
    static AudioSource audioSrc;
    void Awake() 
    {
       instance = this;
    }
    void Start()
    {
        attack = Resources.Load<AudioClip> ("combostep1");
        jump = Resources.Load<AudioClip> ("Jump");
        speed = Resources.Load<AudioClip> ("Speed");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void PlaySound(string clip)
    {
        switch(clip)
        {
            case "combostep1":
                audioSrc.PlayOneShot(attack);
                break;
            case "Jump":
                audioSrc.PlayOneShot(jump);
                break;
            case "Speed":
                audioSrc.PlayOneShot(speed);
                break;            
        }
    }

    public void Run()
    {
        //audioSrc.PlayOneShot(speed);            
    }
}
