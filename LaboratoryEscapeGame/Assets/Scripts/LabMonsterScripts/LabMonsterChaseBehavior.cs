using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LabMonsterChaseBehavior : StateMachineBehaviour
{
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
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Update destination to players current position
        agent.SetDestination(player.position);

        // Check if player has left the chase range or entered attack range
        stateController.DidLeaveChaseRange();
        stateController.DidEnterAttackRange();
    }
}
