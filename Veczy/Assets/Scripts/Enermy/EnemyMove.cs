using UnityEngine;
using UnityEngine.AI;
public class EnemyMove : MonoBehaviour
{

    public Transform targetPlayer;
    //public Transform target2;
    private NavMeshAgent nav;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {
        nav.SetDestination(targetPlayer.position);
    }
}