using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class LabMonsterAttackBehavior : StateMachineBehaviour
{
    Transform player;
    NavMeshAgent agent;
    LabMonsterController stateController;
    AnimatorStateInfo anStateInfo;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        stateController = animator.GetComponent<LabMonsterController>();
        anStateInfo = stateInfo;

        stateController.StateAttack();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        stateController.DidLeaveAttackRange();
    }
}
