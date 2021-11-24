using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetDontDestroyOnLoad : MonoBehaviour
{
    public static PetDontDestroyOnLoad instance;
    void Awake()
    {
         if(!instance)
             instance = this ;
         //otherwise, if we do, kill this thing
         else
             Destroy(this.gameObject) ;

         DontDestroyOnLoad(this.gameObject) ;

    }
}
