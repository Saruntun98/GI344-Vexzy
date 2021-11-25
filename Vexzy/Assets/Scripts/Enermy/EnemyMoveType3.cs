using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveType3 : MonoBehaviour
{
    public static EnemyMoveType3 instance;    
    public float lookRadius = 10f;
    private Transform targetPlayer;
    
    private UnityEngine.AI.NavMeshAgent nav;

	//private EnemyHealth enemyHealth;
    
     void Awake()
    {
        instance = this;
    }

	void Start()
	{
		targetPlayer = Player.instance.player.transform;
		nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}

	void Update ()
	{
		float distance = Vector3.Distance(targetPlayer.position, transform.position);

		if(nav.enabled)
		{
			nav.SetDestination(targetPlayer.position);
			if (distance <= nav.stoppingDistance)
			{
			FaceTarget();
			}
		}
	}

	// Point towards the player
	void FaceTarget ()
	{
		Vector3 direction = (targetPlayer.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}

}
