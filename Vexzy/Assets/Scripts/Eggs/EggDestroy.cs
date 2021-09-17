using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (EggStatus.Instance.currentHealth <= 0 && other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
