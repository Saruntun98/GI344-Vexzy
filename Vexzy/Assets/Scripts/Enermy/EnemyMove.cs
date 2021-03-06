using UnityEngine;
using UnityEngine.AI;
public class EnemyMove : MonoBehaviour
{
    public static EnemyMove instance;    
    public float lookRadius = 10f;
    private Transform targetPlayer;
    public Transform targetEgg;
    
    private NavMeshAgent nav;

	//private EnemyHealth enemyHealth;
    
     void Awake()
    {
        instance = this;
    }

	void Start()
	{
		//targetPlayer = Player.instance.player.transform;
        //targetEgg = EggStatus.Instance.eggPosition.transform;
		if(EggStatus.Instance.currentHealth != 0)
		{
			targetEgg = EggStatus.Instance.eggPosition.transform;
		}
		else
		{
			EggStatus.Instance._isDead = true;
			targetPlayer = Player.instance.player.transform;
		}

		targetPlayer = Player.instance.player.transform;
		nav = GetComponent<NavMeshAgent>();
		//combatManager = GetComponent<CharacterCombat>();
	}

	void Update ()
	{
		float distance = Vector3.Distance(targetPlayer.position, transform.position);
		if(nav.enabled && EggStatus.Instance.currentHealth != 0)
		{
        nav.SetDestination(targetEgg.position);

			if(!EggStatus.Instance._isDead)
			{

			
		// Get the distance to the player

		// If inside the radius
				if (distance <= lookRadius)
				{
					// Move towards the player
					if(nav.enabled)
					{
						nav.SetDestination(targetPlayer.position);
						if (distance <= nav.stoppingDistance)
						{
						// Attack
						//combatManager.Attack(Player.instance.playerStats);
							FaceTarget();
						}
					}
				}
			}
			else
			{
				if(nav.enabled)
				{
					nav.SetDestination(targetPlayer.position);
				}
			}

		}
		/*else if(eggStatus._isDead = true) 
		{
			nav.enabled = false;
		}*/
        else
        {
			if(nav.enabled)
			{
				nav.SetDestination(targetPlayer.position);
			}
            //nav.enabled = false;
            //anim.Play("Idle");
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