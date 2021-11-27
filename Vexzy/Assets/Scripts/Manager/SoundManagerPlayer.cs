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
<<<<<<< Updated upstream
        slash = Resources.Load<AudioClip> ("Slash");
=======
        attack = Resources.Load<AudioClip> ("combostep1");
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
    public void SlashTap()
    {
        audioSrc.PlayOneShot(slash);           
    }
=======
>>>>>>> Stashed changes
    public void PlayerFootstep()
    {
        //audioSrc.Play();
        audioSrc.PlayOneShot(speed);
<<<<<<< Updated upstream
        //Debug.Log("Sound foot on");
=======
        Debug.Log("Sound foot on");
>>>>>>> Stashed changes
    }
    public void PlayerFootStopStep()
    {
        //audioSrc.Play();
        audioSrc.Stop();
<<<<<<< Updated upstream
        //Debug.Log("Sound foot stop");
=======
        Debug.Log("Sound foot stop");
>>>>>>> Stashed changes
    }
}
