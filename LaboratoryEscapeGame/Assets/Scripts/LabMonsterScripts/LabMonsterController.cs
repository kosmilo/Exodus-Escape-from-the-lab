using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Methods for lab monster behavior scripts
public class LabMonsterController : MonoBehaviour
{
    [SerializeField] float attackDistance;
    [SerializeField] float attackStopDistance;
    [SerializeField] float chaseDistance;
    [SerializeField] float chaseStopDistance;
    [SerializeField] float chaseSpeed;
    [SerializeField] float roamSpeed;
    float speedMultiplier = 1f;

    Transform player;
    Animator animator;
    NavMeshAgent agent;

    // For testing 
    void Update() {
        if (Input.GetKey(KeyCode.DownArrow)) {
            SlowEnemy(0.2f, 1f);
        }
    }

    void Start()
    {
        // Get references
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Set nav mesh settings for dif states

    public void StateRoam() {
        agent.speed = roamSpeed * speedMultiplier;
        agent.angularSpeed = 180f;
    }
    public void StateIdle() {
        agent.speed = 0 * speedMultiplier;
        agent.angularSpeed = 180f;
    }
    public void StateChase() {
        agent.speed = chaseSpeed * speedMultiplier;
    }
    public void StateAttack() {
        agent.speed = 0.01f * speedMultiplier;
        agent.angularSpeed = 360f;

        // Make lab monster face the player
        animator.applyRootMotion = false;
        transform.LookAt(player.position);
        Invoke("ResetRootMotion", 0.1f);
    }
    public void StateStun() {
        agent.speed = 0 * speedMultiplier;
        agent.angularSpeed = 180f;
    }
    public void StateScream() {
        agent.speed = 0 * speedMultiplier;
        agent.angularSpeed = 180f;
    }

    // Invoke to turn on root motion after turning lab monster between attacks
    void ResetRootMotion() {
        animator.applyRootMotion = true;
    }

    // In range checks

    public void DidEnterChaseRange() {
        float distance = Vector3.Distance(transform.position, player.position);

        // Check with two raycasts if enemy can see the player
        bool playerInSight;

        if (distance < chaseDistance) {
            Ray ray_1 = new Ray(transform.position + new Vector3(0, 1f, 0), (player.position + new Vector3(0, 1f, 0)) - (transform.position + new Vector3(0, 1f, 0)));
            Physics.Raycast(ray_1, out RaycastHit hit_1);
            Ray ray_2 = new Ray(transform.position + new Vector3(0, 1.6f, 0), (player.position + new Vector3(0, 1.6f, 0)) - (transform.position + new Vector3(0, 1.6f, 0)));
            Physics.Raycast(ray_2, out RaycastHit hit_2);

            playerInSight = hit_1.collider.gameObject.CompareTag("Player") || hit_2.collider.gameObject.CompareTag("Player");
        }
        else { playerInSight = false; }

        animator.SetBool("isChasing", distance < chaseDistance && playerInSight);
    }

    public void DidEnterAttackRange() {
        float distance = Vector3.Distance(transform.position, player.position);
        animator.SetBool("isAttacking", distance < attackDistance);
    }

    public void DidLeaveChaseRange() {
        float distance = Vector3.Distance(transform.position, player.position);
        animator.SetBool("isChasing", distance < chaseStopDistance);
    }

    public void DidLeaveAttackRange() {
        float distance = Vector3.Distance(transform.position, player.position);
        animator.SetBool("isAttacking", distance < attackStopDistance);
    }

    // Change enemy speed multiplier to create a slow effect
    public void SlowEnemy(float newSpeedMultiplier, float slowTime)
    {
        // Reset speed instantly before applying the new speed modifier
        if (agent.speed != 0) { agent.speed = agent.speed / speedMultiplier; }
        speedMultiplier = newSpeedMultiplier;
        agent.speed = agent.speed * speedMultiplier;
        CancelInvoke("ResetSpeed");

        Invoke("ResetSpeed", slowTime);
    }

    // Invoke to set speed back to normal when slow effect ends
    void ResetSpeed() { 
        if (agent.speed != 0) { agent.speed = agent.speed / speedMultiplier; }
        speedMultiplier = 1f; 
    }
    
    // Draw ranges and rays on editor
    private void OnDrawGizmos()
    {
        if (player != null) {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position + new Vector3(0, 1f, 0), (player.position + new Vector3(0, 1f, 0)) - (transform.position + new Vector3(0, 1f, 0)));
            Gizmos.DrawRay(transform.position + new Vector3(0, 1.6f, 0), (player.position + new Vector3(0, 1.6f, 0)) - (transform.position + new Vector3(0, 1.6f, 0)));
        }
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}

