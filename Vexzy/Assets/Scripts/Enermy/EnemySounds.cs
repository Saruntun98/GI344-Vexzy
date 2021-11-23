using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    public static AudioClip enemyDie;
    public static EnemySounds instance;
    static AudioSource audioSrc;

    void Awake() 
    {
       instance = this;
    }
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();        
        enemyDie = Resources.Load<AudioClip> ("enemyDie");
        audioSrc.playOnAwake = false;
    }

    // Update is called once per frame

    public void EnemyDieTap()
    {
        audioSrc.PlayOneShot(enemyDie);            
    }
}
