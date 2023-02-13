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

    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform playerTransform;
    [SerializeField] Animator animator;
    [SerializeField] LayerMask whatIsGround, whatIsPlayer;

    [SerializeField] float sightRange, attackRange;
    bool playerInSight, playerInAttackRange, isStunned;
    [SerializeField] Vector3 walkPoint;
    [SerializeField] float walkPointRange;
    bool walkPointSet;
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
            if (Physics.CheckSphere(transform.position, sightRange, whatIsPlayer))
            {
                // Check with two raycasts if enemy can see the player
                Ray ray_1 = new Ray(transform.position, playerTransform.position - transform.position);
                Physics.Raycast(ray_1, out RaycastHit hit_1);
                Ray ray_2 = new Ray(transform.position + lookOffset, playerTransform.position + lookOffset - transform.position);
                Physics.Raycast(ray_2, out RaycastHit hit_2);
                playerInSight = hit_1.collider.gameObject.CompareTag("Player") || hit_2.collider.gameObject.CompareTag("Player");
            }
            else { playerInSight = false; }

            // Check if player is in attack range
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            // Set state
            if (!playerInSight)
            {
                if (currentState == EnemyStates.Chase || currentState == EnemyStates.Attack)
                {
                    Invoke("SetStateToRoam", chaseTime);
                }
                else
                {
                    currentState = EnemyStates.Roam;
                }
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

        if (currentState == EnemyStates.Attack)
        {
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }

    // ROAMING STATE

    void Roaming()
    {
        agent.speed = roamSpeed * speedMultiplier;
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

    // CHASE STATE

    void ChasePlayer()
    {
        // Change speed and set destination to player
        agent.speed = chaseSpeed * speedMultiplier;
        agent.SetDestination(playerTransform.position);
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

    void ResetStun()
    {
        isStunned = false;
    }

    // SLOW EFFECT

    public void slowEnemy(float newSpeedMultiplier, float slowTime)
    {
        speedMultiplier = newSpeedMultiplier;
        CancelInvoke("ResetSpeed");
        Invoke("ResetSpeed", slowTime);
    }

    void ResetSpeed()
    {
        speedMultiplier = 1f;
    }
    

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
