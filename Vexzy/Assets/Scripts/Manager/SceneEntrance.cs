using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEntrance : MonoBehaviour
{
    public string LastExitName;
    //private GameObject setspawn;


    void Awake()
    {
        
    }

    void Start()
    {
             
        if(PlayerPrefs.GetString("LastExitName") == LastExitName)
        {
            //Player.instance.player.position = new Vector3(-62.35f, 42.99f, 69.99f);
            Player.instance.player.position = transform.position;
            Player.instance.player.eulerAngles = transform.eulerAngles;  
            Debug.Log("TEST1"+Player.instance.player.position);    
            Debug.Log("TEST2"+Player.instance.player.eulerAngles);        
            //Player.instance.transform.position = transform.position;
            //Player.instance.transform.eulerAngles = transform.eulerAngles;
            //PlayerStatus.instance.transform.position = transform.position;
            //PlayerStatus.instance.transform.eulerAngles = transform.eulerAngles;
            //Pet.instance.transform.position = transform.position;
            //Pet.instance.transform.eulerAngles = transform.eulerAngles;
        }
    }

}
