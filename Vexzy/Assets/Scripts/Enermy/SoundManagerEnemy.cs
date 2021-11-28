using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerEnemy : MonoBehaviour
{
    public static AudioClip enemy01, enemy02, enemy03;
    public static SoundManagerEnemy instance;
    static AudioSource audioSrc;
    void Awake() 
    {
       instance = this;
    }
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();        
        enemy01 = Resources.Load<AudioClip> ("enemy01");
        enemy02 = Resources.Load<AudioClip> ("enemy02");
        enemy03 = Resources.Load<AudioClip> ("enemy03");
        audioSrc.playOnAwake = false;
    }

    // Update is called once per frame
   public void Enemy01()
    {
        //audioSrc.PlayOneShot(enemy01);           
    }
   public void Enemy02()
    {
        //audioSrc.PlayOneShot(enemy02);           
    }
}
