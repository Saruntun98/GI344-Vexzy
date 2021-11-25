using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pet : MonoBehaviour
{
    public static Pet instance;    
    public float lookRadius = 10f;
    //private Transform targetPlayer;
    
    private NavMeshAgent nav;
	//private EnemyHealth enemyHealth;
    
     void Awake()
    {
        //instance = this;

    }

	void Start()
	{
		//targetPlayer = Player.instance.player.transform;
		nav = GetComponent<NavMeshAgent>();
		//nav.SetDestination ( targetPlayer.transform.position);

		//combatManager = GetComponent<CharacterCombat>();
	}
 	void OnTriggerEnter (Collider collision) 
	{
		nav.SetDestination ( Player.instance.player.transform.position);
     	/*if (collision.tag == "Player")
        if(GameObject.FindGameObjectWithTag ("Player"))
		{
			targetPlayer = Player.instance.player.transform;
		}*/
 	}
	void Update ()
	{
		// Get the distance to the player
		float distance = Vector3.Distance(Player.instance.player.transform.position, transform.position);

		// If inside the radius
		if (distance <= lookRadius)
		{
			// Move towards the player
			if(nav.enabled)
			{
				nav.SetDestination(Player.instance.player.transform.position);
				if (distance <= nav.stoppingDistance)
				{
				// Attack
				//combatManager.Attack(Player.instance.playerStats);
					FaceTarget();
				}
			}
		}
	}

	// Point towards the player
	void FaceTarget ()
	{
		Vector3 direction = (Player.instance.player.transform.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}
}
