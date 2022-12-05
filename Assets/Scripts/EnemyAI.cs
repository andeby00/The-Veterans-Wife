using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;


    public float health;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    [SerializeField] Transform attackPoint;
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public float shootForce = 10f;

    //States
    public float sightRange, attackRange;
    bool playerInSightRange, playerInAttackRange;

    // Coins
    [SerializeField] GameObject coin;
    [SerializeField] int min = 3;
    [SerializeField] int max = 6;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

   
    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            GameObject gameObject = Instantiate(projectile, attackPoint.position, Quaternion.identity);
            gameObject.transform.forward = player.position - attackPoint.position;
            gameObject.GetOrAddComponent<BulletCollision>().Damage = -1f;
            gameObject.GetComponent<BulletCollision>().Explosive = true;
            gameObject.GetComponent<BulletCollision>().enemyLayer = whatIsPlayer;

            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * shootForce, ForceMode.Impulse);
            // rb.AddForce(transform.up * 1f, ForceMode.Impulse);
            Destroy(gameObject, 5f);


            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(float damage)
    {
        if (damage == -1f)
        {
            Invoke(nameof(DestroyEnemy), 0.5f);
            return;
        }
        health -= damage;
        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);

        for (int i = 0; i < new System.Random().Next(min, max); i++)
        {
            GameObject currentCoin = Instantiate(coin, transform.position, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
            var currentRB = currentCoin.GetComponent<Rigidbody>();
            currentRB.AddForce((Random.Range(0, 20000) - 10000f) / 100f, Random.Range(0, 20000) / 100f, (Random.Range(0, 20000) - 10000f) / 100f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}

