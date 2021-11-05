using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerAttack : MonoBehaviour
{
    [SerializeField] private float range = 10f;
    [SerializeField] private float timebetweenAttack = 1f;
    //private Animator anim;
    private GameObject player;
    [SerializeField] private bool playerInRange;
    private EnemyHealth enemyHealth;
    private GameObject arrow;
    [SerializeField] Transform fireLocation;

    void Start()
    {
        player = GameManager.instance.Player;
        arrow = GameManagerTEST.instance.Arrow;
        //anim = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        StartCoroutine(attack());
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < range && enemyHealth.IsAlive)
        {
            //anim.SetBool("PlayerInRangeAttack", true);
            playerInRange = true;
            RotateTowards(player.transform);
        }
        else
        {
            //anim.SetBool("PlayerInRangeAttack", false);
            playerInRange = false;
        }
    }

    IEnumerator attack()
    {
        if (playerInRange && !GameManager.instance.GameOver)
        {
            //anim.Play("Attack");
            yield return new WaitForSeconds(timebetweenAttack);
        }
        yield return null;
        StartCoroutine(attack());
    }

    public void RotateTowards(Transform player)
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation,Time.deltaTime*10f);
    }
    private void FireArrow()
    {
        GameObject newArrow = Instantiate(arrow) as GameObject;
        newArrow.transform.position = fireLocation.position;
        newArrow.transform.rotation = transform.rotation;
        newArrow.GetComponent<Rigidbody>().velocity = transform.forward * 25f;
    }
}
