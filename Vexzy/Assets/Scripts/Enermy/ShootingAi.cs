
using UnityEngine;
using UnityEngine.AI;
public class ShootingAi : MonoBehaviour
{
    public NavMeshAgent agent;

    public Animator animator;
    public CharacterController cc;
    public Transform player, wallCheck;
    public GameObject gun;

    //Movement && Stats
    public int health;
    public float walkSpeed, runSpeed, gravityMultiplier;

    //Check for Ground/Obstacles
    public LayerMask whatIsGround, whatIsPlayer,whatIsObject,whatIsAll;

    //Patroling
    public Vector3 walkPoint;
    public Vector2 distanceToWalkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    //Attack Player
    public float timeBetweenAttacks;
    bool alreadyShot;

    //State machine
    public bool isPatroling, isChasing, isAttacking, isDead;
    public float sightRange, attackRange;
    public bool grounded, playerInSightRange, playerInAttackRange, blockingObject;

    //Stupid gunAnimFix :D
    public GameObject walkPointGraphic;
    public GameObject gunRunning, gunWalking, gunShooting;

    private void Awake()
    {
        player = GameObject.Find("PlayerObj").transform;
    }
    private void Update()
    {
        StateMachine();
        Movement();
        AnimationController();

        //SetWalkPoint
        //if (walkPointGraphic != null)
        //walkPointGraphic.transform.position = new Vector3(walkPoint.x, 1.738182f, walkPoint.z);
    }
    private void AnimationController()
    {
        if (isPatroling) animator.SetBool("walking", true);
        else animator.SetBool("walking", false);

        if (isAttacking) animator.SetBool("shooting", true);
        else animator.SetBool("shooting", false);

        if (isChasing) animator.SetBool("running", true);
        else animator.SetBool("running", false);

        if (isDead) animator.SetBool("dead", true);
        else animator.SetBool("dead", false);

        //Don't copy to other projects...
        if (isPatroling) gunWalking.SetActive(true);
        else gunWalking.SetActive(false);
        if (isAttacking) gunShooting.SetActive(true);
        else gunWalking.SetActive(false);
        if (isChasing) gunRunning.SetActive(true);
        else gunRunning.SetActive(false);
    }
    private void Movement()
    {
        //extra gravity
        cc.Move(-transform.up * Time.deltaTime * gravityMultiplier);
    }
        private void StateMachine()
    {
        if (!isDead){
            //Check if Player in sightrange
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

            //Check if Player in attackrange
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if ((!playerInSightRange && !playerInAttackRange)|| blockingObject) Patroling();
            if (playerInSightRange && !playerInAttackRange && !blockingObject) ChasePlayer();
            if (playerInAttackRange && playerInSightRange && !blockingObject) AttackPlayer();
        }
    }
    private void Patroling()
    {
        if (isDead) return;

        isPatroling = true;
        isChasing = false;
        isAttacking = false;

        //Calculates DistanceToWalkPoint
        distanceToWalkPoint = new Vector2(Mathf.Abs(walkPoint.x) - Mathf.Abs(transform.position.x), Mathf.Abs(walkPoint.z) - Mathf.Abs(transform.position.z));

        if (!walkPointSet) SearchWalkPoint();

        //Calculate direction and walk to Point
        if (walkPointSet){
            agent.SetDestination(walkPoint);

            Vector3 direction = walkPoint - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.15f);
        }
        //Walkpoint reached
        if (distanceToWalkPoint.x < 1f && distanceToWalkPoint.y < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        if (isDead) return;

        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint,-transform.up, 2,whatIsGround))
        walkPointSet = true;
    }
    private void ChasePlayer()
    {
        if (isDead) return;

        isPatroling = false;
        isChasing = true;
        isAttacking = false;

        //Direction Calculate && move
        agent.SetDestination(player.position);

        Vector3 direction = player.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.15f);
    }
    private void AttackPlayer()
    {
        if (isDead) return;

        isPatroling = false;
        isChasing = false;
        isAttacking = true;

        transform.LookAt(new Vector3(player.position.x,player.position.y, player.position.z));

        if (!alreadyShot){
            //gun.GetComponent<Gun>().Shoot();
            alreadyShot = true;
            Invoke("ResetShot", timeBetweenAttacks);
        }
    }
    private void ResetShot()
    {
        if (isDead) return;

        alreadyShot = false;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health < 0){
            isDead = true;
            isAttacking = false;
            isChasing = false;
            isPatroling = false;
            Invoke("Destroyy", 2.8f);
        }
    }
    private void Destroyy()
    {
        Destroy(gameObject);
    }
}
