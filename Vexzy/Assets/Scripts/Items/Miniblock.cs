using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miniblock : MonoBehaviour
{
    // Start is called before the first frame update

    public static Miniblock instance;
    public float rotationSpeed = 10.0f;
    public int spawnHeal = 10;
    public int spawnHealEgg = 20;
    [SerializeField] float DelayTime = 15f;
    private void Awake() 
    {
        instance = this;
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
            SkillBoob.instance.skillboob = true;
            PlayerStatus.instance.curHealth += spawnHeal;
            EggStatus.Instance.currentHealth += spawnHealEgg;
            AudioSource audioSource = GetComponent<AudioSource>();
            //audioSource.Play();
            Destroy(this.gameObject);
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
