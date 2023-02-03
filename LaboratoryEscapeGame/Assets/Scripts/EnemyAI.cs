using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyAI : MonoBehaviour
{
    enum EnemyStates
    {
        Roam,
        Chase,
        Attack,
        Stunned
    }
    EnemyStates currentState;

    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform playerTransform;
    [SerializeField] LayerMask whatIsGround, whatIsPlayer;
    [SerializeField] float sightRange, attackRange;
    bool playerInSightRange, playerInAttackRange;

    // Navigating
    [SerializeField] Vector3 walkPoint;
    [SerializeField] float walkPointRange;
    bool walkPointSet;

    // Attacking
    [SerializeField] float timeBetweenAttacks;
    bool alreadyAttacked;

    [SerializeField] float roamSpeed;
    [SerializeField] float chaseSpeed;
    Vector3 lookOffset = new Vector3(0, 0.5f, 0);


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        SetEnemyState();

        // Act
        switch (currentState)
        {
            case EnemyStates.Roam:
                Debug.Log(gameObject.name + " roaming");
                Roaming();
                break;
            case EnemyStates.Chase:
                Debug.Log(gameObject.name + " chasing");
                ChasePlayer();
                break;
            case EnemyStates.Attack:
                Debug.Log(gameObject.name + " attacking");
                AttackPlayer();
                break;
            case EnemyStates.Stunned:
                Stunned();
                break;
        }
    }

    void SetEnemyState()
    {
        if (Physics.CheckSphere(transform.position, sightRange, whatIsPlayer))
        {
            Ray ray_1 = new Ray(transform.position, playerTransform.position - transform.position);
            Physics.Raycast(ray_1, out RaycastHit hit_1);
            Ray ray_2 = new Ray(transform.position + lookOffset, playerTransform.position + lookOffset - transform.position);
            Physics.Raycast(ray_2, out RaycastHit hit_2);
            playerInSightRange = hit_1.collider.gameObject.CompareTag("Player") || hit_2.collider.gameObject.CompareTag("Player");
        }
        else { playerInSightRange = false; }

        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        // Set state
        if (!playerInSightRange)
        {
            if (currentState == EnemyStates.Chase || currentState == EnemyStates.Attack)
            {
                Invoke("SetStateToRoam", 2f);
            }
            else
            {
                SetStateToRoam();
            }
        }
        else
        {
            if (!playerInAttackRange)
            {
                currentState = EnemyStates.Chase;
            }
            else
            {
                currentState = EnemyStates.Attack;
            }
            CancelInvoke("SetStateToRoam");
        }
    }

    void SetStateToRoam() { currentState = EnemyStates.Roam; }

    void Roaming()
    {
        agent.speed = roamSpeed;
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        else
        {
            agent.SetDestination(walkPoint);
        }

        // Check distance to walk point
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    void ChasePlayer()
    {
        agent.speed = chaseSpeed;
        agent.SetDestination(playerTransform.position);
    }

    void AttackPlayer()
    {
        // Stop moving and look at player
        agent.SetDestination(transform.position);
        transform.LookAt(playerTransform);

        if (!alreadyAttacked)
        {
            // TRIGGER ATTACK

            
            alreadyAttacked = true;
            Invoke("ResetAttack", timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    void Stunned()
    {
        // STUNNED STATE
    }

    // Draw ranges on editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        if (playerInSightRange)
        {
            Gizmos.DrawRay(transform.position, playerTransform.position - transform.position);
            Gizmos.DrawRay(transform.position + lookOffset, playerTransform.position + lookOffset - transform.position);
        }
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
