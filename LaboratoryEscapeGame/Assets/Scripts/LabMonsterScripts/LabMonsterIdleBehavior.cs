using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LabMonsterIdleBehavior : StateMachineBehaviour
{
    float timer;
    Transform player;
    NavMeshAgent agent;
    LabMonsterController stateController;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        stateController = animator.GetComponent<LabMonsterController>();
        agent = animator.GetComponent<NavMeshAgent>();

        stateController.StateIdle();
        agent.SetDestination(agent.transform.position);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;

        // Check if has been idle long enough and should start enter roaming state
        if (timer > 10) {
            animator.SetBool("isRoaming", true);
        }

        stateController.DidEnterChaseRange();
    }
}
