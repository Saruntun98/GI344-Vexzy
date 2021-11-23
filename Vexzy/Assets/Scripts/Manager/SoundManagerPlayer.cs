using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerPlayer : MonoBehaviour
{
    public static AudioClip slash, jump, speed;
    public static SoundManagerPlayer instance;
    static AudioSource audioSrc;

    void Awake() 
    {
       instance = this;
    }
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();        
        slash = Resources.Load<AudioClip> ("Slash");
        jump = Resources.Load<AudioClip> ("Jump");
        speed = Resources.Load<AudioClip> ("Speed");
        audioSrc.playOnAwake = false;
    }

    // Update is called once per frame
    public static void PlaySound(string clip)
    {
        switch(clip)
        {
            case "Jump":
                audioSrc.PlayOneShot(jump);
                break;            
        }
    }

    public void Run()
    {
        //audioSrc.PlayOneShot(speed);            
    }
    public void SlashTap()
    {
        audioSrc.PlayOneShot(slash);           
    }
    public void PlayerFootstep()
    {
        //audioSrc.Play();
        audioSrc.PlayOneShot(speed);
        //Debug.Log("Sound foot on");
    }
    public void PlayerFootStopStep()
    {
        //audioSrc.Play();
        audioSrc.Stop();
        //Debug.Log("Sound foot stop");
    }
}
