using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestory : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerStatus._instance != null)
        {
            PlayerStatus._instance.hp -= 50;
        }
        
        if (other.gameObject.tag == "Eggs")
        {
            Destroy(gameObject);
        }
    }
}
