using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] 
    GameObject projectile;
    [SerializeField] 
    Transform shootPoint;    
    [SerializeField] 
    float turnSpeed = 5;
    
    Transform target;

    float fireRate = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
        target = Player._instance.player.transform;
        //target = GameObject.FindGameObjectWithTag("Player").transform;
        //target = gameObject.tag == "Player"
    }

    // Update is called once per frame
    void Update()
    {
        fireRate -= Time.deltaTime;

        Vector3 direction = transform.position - target.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);

        if(fireRate <= 0)
        {
            fireRate = 0.2f;
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(projectile, shootPoint.position, shootPoint.rotation);
    }
}
