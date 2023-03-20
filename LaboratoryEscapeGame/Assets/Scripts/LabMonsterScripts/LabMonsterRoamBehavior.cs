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
    LabMonsterController stateController;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;

        // Get references
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        stateController = animator.GetComponent<LabMonsterController>();

        // Get way points
        Transform wayPointsObj = GameObject.FindGameObjectWithTag("WayPoints").transform;
        foreach (Transform wayPointT in wayPointsObj) { 
            wayPoints.Add(wayPointT);
        }

        stateController.StateRoam();

        // Set a random destination
        agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].position);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;

        // Check if has roamed long enough and should enter idle state
        if (timer > 50 || agent.remainingDistance <= agent.stoppingDistance) {
            animator.SetBool("isRoaming", false);
        }
        
        stateController.DidEnterChaseRange();
    }
}
