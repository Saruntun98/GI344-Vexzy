using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPet : MonoBehaviour
{
    public GameObject[] pet;
    public EggStatus egg;

    public static spawnPet Instance;  
    int randomPet;

    void Awake()
    {
        Instance = this;
    }

    public void Spawn()
    {
       randomPet = Random.Range(0, pet.Length);
       Instantiate(pet[randomPet], egg.eggPosition.position, egg.eggPosition.rotation);
    }

}
