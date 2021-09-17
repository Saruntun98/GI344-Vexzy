using UnityEngine;
using UnityEngine.AI;
public class EnemyMove : MonoBehaviour
{
    public float lookRadius = 10f;
    public Transform targetPlayer;
    public Transform targetEgg;
    private NavMeshAgent nav;
    
    void Start()
    {
        targetPlayer = Player._instance.player.transform;
        targetEgg = EggStatus.Instance.eggPosition.transform;
        nav = GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {
        //nav.SetDestination(targetPlayer.position);
        nav.SetDestination(targetEgg.position);
        
        
        float distance = Vector3.Distance(targetPlayer.position, transform.position);
 
        if (distance <= lookRadius)
        {
            nav.SetDestination(targetPlayer.position);
        }
    }
    
    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
 
    }
}