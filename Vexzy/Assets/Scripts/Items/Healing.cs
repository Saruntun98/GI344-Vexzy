using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    [SerializeField] GameObject particleFX;
    [SerializeField] GameObject[] explosivePart;

    [SerializeField] float radius = 2f;
    [SerializeField] float force = 10f;
    [SerializeField] float DelayTime = 15f;

    public bool isActive = false;
    
    // void Update()
    // {
    //     if (isActive)
    //     {
    //         StartCoroutine(DelaySpawn());
    //     }
    // }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObj in colliders)
        {
            Rigidbody rb = nearbyObj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }

        // Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isActive = true;
            Debug.Log("Get Heal!");

            //Explode();

            if (PlayerStatus.instance != null)
            {
                PlayerStatus.instance.curHealth += 50;            
            }

            foreach (var part in explosivePart)
            {
                part.SetActive(false);
            }

            StartCoroutine(DelayParticle());
            //particleFX.SetActive(true);
            // Destroy(this.gameObject, 2);

            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.Play();

            gameObject.GetComponent<SphereCollider>().enabled = false;
            StartCoroutine(DelaySpawn());
        }
    }

    IEnumerator DelayParticle()
     {
        particleFX.SetActive(true);
        yield return new WaitForSeconds(3);
        particleFX.SetActive(false);
         // gameObject.SetActive(false);
    // }

    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.gameObject.tag == "Player")
    //     {
    //         isActive = false;
    //         StartCoroutine(DelaySpawn());
    //     }
    }

    IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(DelayTime);
        foreach (var part in explosivePart)
        {
            part.SetActive(true);
        }
        // gameObject.SetActive(true);
        gameObject.GetComponent<SphereCollider>().enabled = true;
    }
}

