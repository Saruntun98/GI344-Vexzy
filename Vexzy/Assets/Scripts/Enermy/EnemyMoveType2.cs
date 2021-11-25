using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveType2 : MonoBehaviour
{
    public static EnemyMoveType2 instance;    
    public float lookRadius = 10f;
    private Transform targetEgg;
    
    private UnityEngine.AI.NavMeshAgent nav;

	//private EnemyHealth enemyHealth;
    
     void Awake()
    {
        instance = this;
    }

	void Start()
	{
		targetEgg = EggStatus.Instance.eggPosition.transform;
		nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}

	void Update ()
	{
		float distance = Vector3.Distance(targetEgg.position, transform.position);

		if(nav.enabled)
		{
			nav.SetDestination(targetEgg.position);
			if (distance <= nav.stoppingDistance)
			{
			FaceTarget();
			}
		}
	}

	// Point towards the player
	void FaceTarget ()
	{
		Vector3 direction = (targetEgg.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}

}
