using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] 
    int damage = 20;
    [SerializeField] 
    float speed = 500f;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Transform target = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 direction = target.position - transform.position;
        rb.AddForce(direction * speed * Time.deltaTime);
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            PlayerStatus curHealth = other.transform.GetComponent<PlayerStatus>();
            PlayerStatus._instance.curHealth -= 100;
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        /*if (other.gameObject.name == "Weapon") {
            if (EnemyHealth._instance != null)
            {
                EnemyHealth._instance.currentHealth -= 30;
                Debug.Log ("HP Enemy: "+EnemyHealth._instance.currentHealth);
            }
            Destroy(gameObject);
        }*/
    }
}
