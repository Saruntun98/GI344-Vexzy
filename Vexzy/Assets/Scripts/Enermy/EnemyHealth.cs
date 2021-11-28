using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public float startHealth = 30;
    //[SerializeField] float timeLastHit = 2f;
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
    //private ParticleSystem blood;
    //private Transform target;
    
    public static EnemyHealth instance;
    //public bool dead = false;
    public int damage = 50;
    public int damageAttackPlayer = 20;
    public float explosionRadius = 0f;
    //public GameObject impactEffect;
    
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentHealth = startHealth;
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        nav = GetComponent<NavMeshAgent>();
        //blood = GetComponentInChildren<ParticleSystem>();
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
        
        /*if (other.gameObject.name == "bullet(Clone)")  
        {
            currentHealth = currentHealth -= Bullet.instance.aDamage;
            Debug.Log ("Bullet damage: "+Bullet.instance.aDamage);  
            Debug.Log ("HP Enemy: "+currentHealth);
            audio.PlayOneShot(audio.clip);
            TakeHit();
        }*/

        if (other.gameObject.name == "weapon")  
        {
            currentHealth = currentHealth -= PlayerStatus.instance.aDamage;
            Debug.Log ("Player damage: "+PlayerStatus.instance.aDamage);  
            Debug.Log ("HP Enemy: "+currentHealth);
            audio.PlayOneShot(audio.clip);             
            TakeHit();
        } 

        if (other.gameObject.name == "Com2")
        {
                currentHealth -= Combo.instance.damagaCom2;
                //audio.PlayOneShot(audio.clip);   
                //Debug.Log ("Skill: "+SkillBoob.instance.aDamageSkill);  
                Debug.Log ("Com2");   
                TakeHit();             
        }
        if (other.gameObject.name == "Com3")
        {
                currentHealth -= Combo.instance.damagaCom3;
                //audio.PlayOneShot(audio.clip);   
                //Debug.Log ("Skill: "+SkillBoob.instance.aDamageSkill);  
                Debug.Log ("Com3");   
                TakeHit();             
        }
        /*if (other.gameObject.tag == "SkillsPlayer")  
        {
            currentHealth = currentHealth -= SkillBoob.instance.aDamageSkill;
            Debug.Log ("Skill Boom: "+SkillBoob.instance.aDamageSkill);  
            Debug.Log ("HP Enemy: "+currentHealth);
            audio.PlayOneShot(audio.clip);             
            TakeHit();
        }        */   

        /*if (timer>=timeLastHit && !GameManager.instance.GameOver)
        {
            if (other.gameObject.name=="Weapon")
            {
                TakeHit();
                timer = 0;  //weapon(Clone)
            }
        }*/
        /*if (other.gameObject.name == "Weapon") {
            startHealth = startHealth -= 30;
            Debug.Log ("HP: "+startHealth);
            //Health
        }*/
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "SkillsPlayer")
        {
                currentHealth -= SkillBoob.instance.aDamageSkill * Time.deltaTime;
                //audio.PlayOneShot(audio.clip);   
                //Debug.Log ("Skill: "+SkillBoob.instance.aDamageSkill);  
                //Debug.Log ("HP Enemy: "+PlayerStatus.instance.curHealth);   
                TakeHit();             
        }
        if (other.gameObject.name == "Com2")
        {
                currentHealth -= Combo.instance.damagaCom2time * Time.deltaTime;
                //audio.PlayOneShot(audio.clip);   
                //Debug.Log ("Skill: "+SkillBoob.instance.aDamageSkill);  
                Debug.Log ("Com2time");   
                TakeHit();             
        }
        if (other.gameObject.name == "Com3")
        {
                currentHealth -= Combo.instance.damagaCom3time * Time.deltaTime;
                //audio.PlayOneShot(audio.clip);   
                //Debug.Log ("Skill: "+SkillBoob.instance.aDamageSkill);  
                Debug.Log ("Com3time");   
                TakeHit();             
        }
    }

    private void TakeHit()
    {
        if (currentHealth>0)
        {
            //audio.PlayOneShot(audio.clip);
            //anim.Play("Hurt");
            //blood.Play();
            //currentHealth -= 30;
            //currentHealth = currentHealth - PlayerStatus.instance.aDamage;
            //currentHealth = currentHealth -= Bullet.instance.aDamage;
            //currentHealth = currentHealth -= PlayerStatus.instance.aDamage;
            
            //Debug.Log ("HP Enemy: "+currentHealth);
            //Debug.Log ("Player damage: "+PlayerStatus.instance.aDamage);

            //Debug.Log ("HP Enemy: "+currentHealth);
        }
        if (currentHealth<=0)
        {
            isAlive = false;
            Debug.Log ("Bot die");  
            killEnemy();
        }
    }

    private void killEnemy()
    {
        //GameManager.instance.killedEnemy(this);
        GameManager.instance.killedEnemy(this);
        //capsuleCollider.enabled = false;
        nav.enabled = false;
        //blood.Play();
        //EnemySounds.instance.EnemyDieTap();
        //anim.SetTrigger("EnemyDie");
        rigidbody.isKinematic = true;
        this.gameObject.GetComponent<Animator>().enabled = false;
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