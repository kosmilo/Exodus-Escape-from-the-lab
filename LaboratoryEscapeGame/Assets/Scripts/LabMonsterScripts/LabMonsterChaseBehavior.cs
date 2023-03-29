using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LabMonsterChaseBehavior : StateMachineBehaviour
{
    float timer;
    NavMeshAgent agent;
    Transform player;
    LabMonsterController stateController;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        stateController = animator.GetComponent<LabMonsterController>();

        stateController.StateChase();
        timer = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Update destination to players current position
        agent.SetDestination(player.position);

        // Check if player has left the chase range or entered attack range
        stateController.DidLeaveChaseRange();

        if (!animator.GetBool("isAttacking")) {
            stateController.DidEnterAttackRange();
        }

        // Check with two raycasts if enemy can see the player
        bool playerInSight;
        Ray ray_1 = new Ray(animator.transform.position + new Vector3(0, 1f, 0), (player.position + new Vector3(0, 1f, 0)) - (animator.transform.position + new Vector3(0, 1f, 0)));
        Physics.Raycast(ray_1, out RaycastHit hit_1);
        Ray ray_2 = new Ray(animator.transform.position + new Vector3(0, 1.6f, 0), (player.position + new Vector3(0, 1.6f, 0)) - (animator.transform.position + new Vector3(0, 1.6f, 0)));
        Physics.Raycast(ray_2, out RaycastHit hit_2);

        playerInSight = hit_1.collider.gameObject.CompareTag("Player") || hit_2.collider.gameObject.CompareTag("Player");

        // Count the time enemy has not seen player in
        if (playerInSight) {
            timer = 0;
        }
        else {
            timer += Time.deltaTime;
        }
        Debug.Log(timer);

        // Stop chasing when enemy hasn't seen player in 6 seconds
        if (timer >= 12) {
            animator.SetBool("isChasing", false);
        }
    }
}
