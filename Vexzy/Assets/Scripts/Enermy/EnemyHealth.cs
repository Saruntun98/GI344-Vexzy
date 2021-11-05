using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public float startHealth = 30;
    [SerializeField] float timeLastHit = 2f;
    private float timer = 0f;
    private NavMeshAgent nav;
    private Animator anim;
    [SerializeField] public float currentHealth;
    private AudioSource audio;
    private float disappearSpeed = 2f;
    private bool disappearEnemy = false;
    [SerializeField] private bool isAlive;
    private Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider;
    private ParticleSystem blood;
    private Transform target;
    
    public static EnemyHealth _instance;
    public bool dead = false;
    public int damage = 50;
    public float explosionRadius = 0f;
    public GameObject impactEffect;
    
    void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        currentHealth = startHealth;
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        nav = GetComponent<NavMeshAgent>();
        blood = GetComponentInChildren<ParticleSystem>();
        GameManager.instance.RegisterEnemy(this);
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        HealthCheck();
        timer += Time.deltaTime;
        if (disappearEnemy)
        {
            transform.Translate(-Vector3.up * disappearSpeed * Time.deltaTime);
        }
        
        /*if (currentHealth <= 0) {
            dead = true;
        } 
        else {
            dead = false;
        }
        if (dead == true) {
            death();
        }*/
    }
    public bool IsAlive
    {
        get
        {
            return isAlive;
        }
    }
    void HealthCheck()
    {
        if (currentHealth > startHealth)
        {
            currentHealth = startHealth;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.name == "weapon") 
        {
            currentHealth = currentHealth -= PlayerStatus._instance.aDamage;
            Debug.Log ("HP Enemy: "+currentHealth);
            Debug.Log ("Player damage: "+PlayerStatus._instance.aDamage);               
            TakeHit();
        }      
        /*if (timer>=timeLastHit && !GameManager.instance.GameOver)
        {
            if (other.gameObject.name=="Weapon")
            {
                TakeHit();
                timer = 0;
            }
        }*/
        /*if (other.gameObject.name == "Weapon") {
            startHealth = startHealth -= 30;
            Debug.Log ("HP: "+startHealth);
            //Health
        }*/
    }

    private void TakeHit()
    {
        if (currentHealth>0)
        {
            //audio.PlayOneShot(audio.clip);
            //anim.Play("Hurt");
            //blood.Play();
            //currentHealth -= 30;
            currentHealth = currentHealth - PlayerStatus._instance.aDamage;
            Debug.Log ("HP Enemy: "+currentHealth);
            Debug.Log ("Player damage: "+PlayerStatus._instance.aDamage);
        }
        if (currentHealth<=0)
        {
            isAlive = false;
            killEnemy();
        }
    }

    private void killEnemy()
    {
        GameManager.instance.killedEnemy(this);
        //capsuleCollider.enabled = false;
        nav.enabled = false;
        //blood.Play();
        //anim.SetTrigger("EnemyDie");
        rigidbody.isKinematic = true;
        StartCoroutine(RemoveEnemy());
    }

    IEnumerator RemoveEnemy()
    {
        yield return new WaitForSeconds(4f);
        disappearEnemy = true;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    
    void death(){

        //anim.SetBool ("AttackPlayer", false);
        //anim.SetBool ("FoundPlayer", false);
        //anim.SetTrigger ("Died");
        
    }
}