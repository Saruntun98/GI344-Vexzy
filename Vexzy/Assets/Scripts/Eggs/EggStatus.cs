using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggStatus : MonoBehaviour
{
    [SerializeField]
    public int startHealth = 100;

    public static EggStatus Instance;
    public GameObject deathEffect;
    public Transform eggPosition;
    public HealthBar healthBar; 
    public int currentHealth;
    
    //private GameObject egg;
    private bool _isDead = false;
    private Collider Collider;
    
    void Awake()
    {
        Instance = this;
    }
    

    private void Start()
    {
        currentHealth = startHealth;
        healthBar.SetMaxHealth(startHealth);
        //HealthCheck();
    }
    
    private void Update()
    {
        
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0 && !_isDead || Collider.gameObject.tag == "Enemy")
        {
            Die();
        }    
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
        _isDead = true;
        //GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        //Destroy(effect, 5f);
        //WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
        Debug.Log("Egg Die");
    }
}