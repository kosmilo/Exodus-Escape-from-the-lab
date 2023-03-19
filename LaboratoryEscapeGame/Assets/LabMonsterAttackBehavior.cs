using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LabMonsterAttackBehavior : StateMachineBehaviour
{
    Transform player;
    labMonsterStateController stateController;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        stateController = animator.GetComponent<labMonsterStateController>();

        stateController.StateAttack();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.LookAt(player);

        float distance = Vector3.Distance(animator.gameObject.transform.position, player.position);

        if (distance > stateController.attackStopDistance) {
            animator.SetBool("isAttacking", false);
        }
    }
}
