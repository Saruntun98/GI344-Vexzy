using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggStatus : MonoBehaviour
{
    [SerializeField]
    public int startHealth = 100;
    [SerializeField]
    public int currentHealth;
    public static EggStatus Instance;
    public GameObject deathEffect;
    public Transform eggPosition;
    public HealthBar healthBar; 
    
    public bool _isDead = false;
    //private Collider collider;
    
    public spawnPet Pat;

    void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        
        healthBar.SetHealth(currentHealth);
        //if (currentHealth <= 0 && !_isDead || collider.gameObject.tag == "Enemy")
        if (currentHealth <= 0 && !_isDead)
        {
            Die();
        }    
    }
    
    private void Start()
    {
        currentHealth = startHealth;
        healthBar.SetMaxHealth(startHealth);
        //HealthCheck();
    }
    
    /*void HealthCheck()
    {
        if (currentHealth > startHealth)
        {
            currentHealth = startHealth;
        }
        healthBar.SetMaxHealth(startHealth);
    }*/
    
    void Die ()
    {
        //GameManager.instance.RuleCheck();
        _isDead = true;
		Debug.Log("Egg Die");
        //GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        //Destroy(effect, 5f);
        //WaveSpawner.EnemiesAlive--;

        //Key.Instance.eggChecked();
        //Destroy(gameObject);
    }
	
	private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            Die();
            
            Debug.Log("Pet spawn");
            //Destroy(gameObject);
            //spawnPet.Instance.petSpawn.Spawn();
            Pat.Spawn();
            //Instantiate(petSpawn, transform.position, Quaternion.identity);
        }
    }
}