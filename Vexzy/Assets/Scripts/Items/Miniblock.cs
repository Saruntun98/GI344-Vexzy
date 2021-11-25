using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miniblock : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationSpeed = 10.0f;
    
    public int spawnHeal = 10;
    public bool skillboob = false;
    public int timeSkill = 3;
    [SerializeField] float DelayTime = 15f;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        StartCoroutine(DelayDel());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            skillboob = true;
            Debug.Log("Power Boom!!");
            PlayerStatus.instance.curHealth += spawnHeal;
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.Play();
            gameObject.SetActive(false);
            //StartCoroutine(hide());
        }
    }
    /*IEnumerator hide()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }*/
    IEnumerator DelayDel()
    {
        yield return new WaitForSeconds(DelayTime);
        gameObject.SetActive(false);
        Destroy(this.gameObject);
        //gameObject.GetComponent<SphereCollider>().enabled = true;
    }
}
