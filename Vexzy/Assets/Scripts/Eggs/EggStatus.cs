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
    [SerializeField] private BoxCollider[] onEgg;
    //[SerializeField] private Canvas interactingCanvasUi;
    [SerializeField] 
    public bool isFoundEgg = false;    
    [SerializeField] GameObject showText;
    public GameObject openEffect;
    
    public bool _isDead = false;
    //private Collider collider;
    
    public spawnPet Pat;

    void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        //HealthBar.Instance.SetHealth(currentHealth);
        healthBar.SetHealth(currentHealth);
        //if (currentHealth <= 0 && !_isDead || collider.gameObject.tag == "Enemy")
        if (currentHealth <= 0 && !_isDead)
        {
            Die();
        }
        if(isReadyForInstantiate  && Input.GetKeyDown(KeyCode.F))
        {
            if(isFoundEgg)
            {
                if(spawnPet.Instance != null)
                {
                    spawnPet.Instance.Spawn();
                    OnpenEgg();
                }
                /*if(spawnPet.Instance.isSpawned == false)
                {
                    spawnPet.Instance.Spawn();
                    OnpenEgg();
                }*/
                else
                {
                    spawnPet.Instance.Despawned();
                }
            }
        }
    }
    
    bool isReadyForInstantiate = false;
    private void Start()
    {
        isReadyForInstantiate = true;
        currentHealth = startHealth;
        healthBar.SetMaxHealth(startHealth);
        //HealthBar.Instance.SetMaxHealth(startHealth);
        onEgg = GetComponentsInChildren<BoxCollider>();
        EndEgg();
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
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        //WaveSpawner.EnemiesAlive--;

        //Key.Instance.eggChecked();
        //Destroy(gameObject);
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.Play();
        StartCoroutine(RemoveEnemy());
    }

    IEnumerator RemoveEnemy()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
	/*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            BeginEgg();
            //Destroy(gameObject);
            //Die();
            
            Debug.Log("Pet spawn");
            //Destroy(gameObject);
            //spawnPet.Instance.petSpawn.Spawn();
            //Pat.Spawn();
            //Instantiate(petSpawn, transform.position, Quaternion.identity);
        }
    }*/
    
    [SerializeField] AudioClip EggF;
    public void OnpenEgg()
    {
        Debug.Log("Pet spawn del egg");
        GetComponent<AudioSource>().PlayOneShot(EggF);
        //Destroy(gameObject);
        StartCoroutine(DelayHideEgg());
    }

    IEnumerator DelayHideEgg()
    {
        isReadyForInstantiate = false;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        isReadyForInstantiate = true;
    }

    private void OnTriggerStay(Collider other)
    {
        /*if (other.CompareTag("Player"))
        {
            //BeginEgg();
            //interactingCanvasUi.transform.LookAt(Camera.main.transform);
        }*/
        if(GameManager.instance.gameWiner)
        {
            if (other.CompareTag("Player"))
            {
            BeginEgg();
            //interactingCanvasUi.transform.LookAt(Camera.main.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //interactingCanvasUi.gameObject.SetActive(false);
            EndEgg();
        }
    }
    /*private void OnTriggerEnter(Collider other)
    {
        //BeginEgg();
        if(other.gameObject.name == "Checker" && isFoundEgg)
        {
            //interactingCanvasUi.gameObject.SetActive(true);
            BeginEgg(); //Use real is enermy die and win
            //showText.SetActive(true);
            Debug.Log("55+");
        }   
    }*/
    public void BeginEgg()
    {
        openEffect.SetActive(true);
        foreach (var egg in onEgg)
        {
            egg.enabled = true;
            isFoundEgg = true;
            showText.SetActive(true);
        }
    }

    public void EndEgg()
    {
        openEffect.SetActive(false);
        foreach (var egg in onEgg)
        {
            egg.enabled = false;
            isFoundEgg = false;
        }
    }
}