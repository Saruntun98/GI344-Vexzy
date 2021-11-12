using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPet : MonoBehaviour
{
    public GameObject[] pet;
    public EggStatus egg;
    public bool isSpawned = false;
    public static spawnPet Instance;  
    [SerializeField] private Canvas interactingCanvasUi;
    int randomPet;

    void Awake()
    {
        Instance = this;
    }

    public void Spawn()
    {
       randomPet = Random.Range(0, pet.Length);
       Instantiate(pet[randomPet], egg.eggPosition.position, egg.eggPosition.rotation);
       isSpawned = true;
    }

    public void Despawned()
    {
        isSpawned = false;
    }
}
