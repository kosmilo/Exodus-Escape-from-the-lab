using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

// Handles movement, states, attacking and animations
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

    NavMeshAgent agent;
    Animator animator;
    [SerializeField] Transform playerTransform;
    [SerializeField] LayerMask whatIsGround, whatIsPlayer;

    [SerializeField] float sightRange, attackRange;
    [SerializeField] float walkPointRange;
    bool playerInSight, playerInAttackRange, isStunned;
    Vector3 walkPoint;
    bool walkPointSet;

    // Attacking
    [SerializeField] float timeBetweenAttacks;
    bool alreadyAttacked;

    // Moving
    [SerializeField] float roamSpeed, chaseSpeed;
    [SerializeField] float chaseTime;
    float speedMultiplier = 1;
    Vector3 lookOffset = new Vector3(0, 0.5f, 0);


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SetEnemyState();
        SetEnemyAnimParameters();

        // Act
        switch (currentState)
        {
            case EnemyStates.Roam:
                Roaming();
                break;
            case EnemyStates.Chase:
                ChasePlayer();
                break;
            case EnemyStates.Attack:
                AttackPlayer();
                break;
            case EnemyStates.Stunned:
                Stunned();
                break;
        }
    }

    // SETTING THE ENEMY STATE

    void SetEnemyState()
    {
        if (isStunned)
        {
            currentState = EnemyStates.Stunned;
        }
        else
        {
            // Search for player in a set area with raycasts
            if ((playerTransform.position - transform.position).sqrMagnitude < sightRange * sightRange)
            {
                // Check with two raycasts if enemy can see the player
                // Use (pos1 - pos2).sqrMagnitude < x*x for better performance
                Ray ray_1 = new Ray(transform.position, playerTransform.position - transform.position);
                Physics.Raycast(ray_1, out RaycastHit hit_1);
                Ray ray_2 = new Ray(transform.position + lookOffset, playerTransform.position + lookOffset - transform.position);
                Physics.Raycast(ray_2, out RaycastHit hit_2);
                playerInSight = hit_1.collider.gameObject.CompareTag("Player") || hit_2.collider.gameObject.CompareTag("Player");

                // Check if player is in attack range
                playerInAttackRange = ((playerTransform.position - transform.position).sqrMagnitude < (attackRange * attackRange));
            }
            else { 
                playerInSight = false; 
                playerInAttackRange = false;
            }

            // Set state
            if (!playerInSight)
            {
                if (currentState == EnemyStates.Chase || currentState == EnemyStates.Attack)
                {
                    Invoke("SetStateToRoam", chaseTime);
                }
                else { currentState = EnemyStates.Roam; }
            }
            else
            {
                currentState = !playerInAttackRange ? EnemyStates.Chase : EnemyStates.Attack;
                CancelInvoke("SetStateToRoam");
            }
        }
    }

    // Invoked after enemy hasn't seen player in [chaseTime]
    void SetStateToRoam() { currentState = EnemyStates.Roam; }

    // SETTING ANIMATION PARAMETERS 

    void SetEnemyAnimParameters()
    {
        animator.SetFloat("movingAtSpeed", agent.velocity.magnitude);
        animator.SetBool("isStunned", isStunned);
        animator.SetBool("isAttacking", (currentState == EnemyStates.Attack));
    }

    // ROAMING STATE

    void Roaming()
    {
        agent.speed = roamSpeed * speedMultiplier; // Change speed to walking speed

        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        else
        {
            agent.SetDestination(walkPoint);
        }

        // Check if walkpoint was reached and a new one is needed
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f) { walkPointSet = false; }
    }

    // Look for a new walkpoint
    void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // Check if walkpoint is valid (not in air)
        walkPointSet = Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround) ? true : false;
    }

    // CHASE STATE

    void ChasePlayer()
    {
        // Change speed and set destination to player
        agent.speed = chaseSpeed * speedMultiplier;
        agent.SetDestination(playerTransform.position); // Set navigation point to player
    }

    // ATTACK STATE

    void AttackPlayer()
    {
        // Stop moving and look at player
        agent.SetDestination(transform.position);
        transform.LookAt(playerTransform);
    }

    // STUNNED STATE

    void Stunned()
    {
        agent.SetDestination(transform.position);
    }

    public void stunEnemy(float stunTime)
    {
        isStunned = true;
        CancelInvoke("ResetStun");
        Invoke("ResetStun", stunTime);
    }

    void ResetStun() { isStunned = false; }

    // SLOW EFFECT

    // Change enemy speed multiplier to create a slow effect
    public void slowEnemy(float newSpeedMultiplier, float slowTime)
    {
        speedMultiplier = newSpeedMultiplier;
        CancelInvoke("ResetSpeed");
        Invoke("ResetSpeed", slowTime);
    }

    // Set speed back to normal when slow effect ends
    void ResetSpeed() { speedMultiplier = 1f; }

    // Draw ranges on editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        if (playerInSight)
        {
            Gizmos.DrawRay(transform.position, playerTransform.position - transform.position);
            Gizmos.DrawRay(transform.position + lookOffset, playerTransform.position + lookOffset - transform.position);
        }
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
