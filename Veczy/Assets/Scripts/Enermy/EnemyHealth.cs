using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int startHealth = 30;
    [SerializeField] float timeLastHit = 2f;
    private float timer = 0f;
    private NavMeshAgent nav;
    private Animator anim;
    [SerializeField] private int currentHealth;
    private AudioSource audio;
    private float disappearSpeed = 2f;
    private bool disappearEnemy = false;
    [SerializeField] private bool isAlive;
    private Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider;
    private ParticleSystem blood;

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
        timer += Time.deltaTime;
        if (disappearEnemy)
        {
            transform.Translate(-Vector3.up * disappearSpeed * Time.deltaTime);
        }
    }
    public bool IsAlive
    {
        get
        {
            return isAlive;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (timer>=timeLastHit && !GameManager.instance.GameOver)
        {
            if (other.tag=="playerweapon")
            {
                takeHit();
                timer = 0;
            }
        }
        
    }

    private void takeHit()
    {
        if (currentHealth>0)
        {
            audio.PlayOneShot(audio.clip);
            anim.Play("Hurt");
            blood.Play();
            currentHealth -= 10;
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
        capsuleCollider.enabled = false;
        nav.enabled = false;
        blood.Play();
        anim.SetTrigger("EnemyDie");
        rigidbody.isKinematic = true;
        StartCoroutine(removeEnemy());
    }

    IEnumerator removeEnemy()
    {
        yield return new WaitForSeconds(4f);
        disappearEnemy = true;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    
}