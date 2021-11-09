using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSpawn : MonoBehaviour
{
    public GameObject spawner;
    public bool stopSpqwning = false;
    public float spawnTime;
    public float spawnDelay;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }

    public void SpawnObject()
    {
        Instantiate(spawner, transform.position, transform.rotation);
        if (stopSpqwning)
        {
            CancelInvoke("SpawnObject");
        }
    }
}
