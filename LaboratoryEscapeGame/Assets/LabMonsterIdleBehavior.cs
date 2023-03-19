using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabMonsterIdleBehavior : StateMachineBehaviour
{
    float timer;
    Transform player;
    labMonsterStateController stateController;
    [SerializeField] float chaseRange;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        stateController = animator.GetComponent<labMonsterStateController>();

        stateController.StateIdle();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;

        if (timer > 10) {
            animator.SetBool("isRoaming", true);
        }

        float distance = Vector3.Distance(animator.transform.position, player.position);

        if (distance < chaseRange)
        {
            animator.SetBool("isChasing", true);
        }
    }
}
