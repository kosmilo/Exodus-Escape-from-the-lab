using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LabMonsterRoamBehavior : StateMachineBehaviour
{
    float timer;
    List<Transform> wayPoints = new List<Transform>();
    NavMeshAgent agent;
    Transform player;
    labMonsterStateController stateController;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;

        // Get way points
        Transform wayPointsObj = GameObject.FindGameObjectWithTag("WayPoints").transform;
        foreach (Transform wayPointT in wayPointsObj) { 
            wayPoints.Add(wayPointT);
        }

        // Get references
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        stateController = animator.GetComponent<labMonsterStateController>();

        stateController.StateRoam();
        agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].position);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;

        if (timer > 50 || agent.remainingDistance <= agent.stoppingDistance) {
            Debug.Log("Idleing");
            animator.SetBool("isRoaming", false);
        }

        float distance = Vector3.Distance(animator.transform.position, player.position);

        if (distance < stateController.chaseDistance) {
            animator.SetBool("isChasing", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent.SetDestination(agent.transform.position);
    }
}
