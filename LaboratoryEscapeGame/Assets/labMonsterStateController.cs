using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class labMonsterStateController : MonoBehaviour
{
    [SerializeField] public float attackDistance;
    [SerializeField] public float attackStopDistance;
    [SerializeField] public float chaseDistance;
    [SerializeField] public float chaseStopDistance;
    [SerializeField] public float chaseSpeed;
    [SerializeField] public float roamSpeed;
    Transform player;

    Animator animator;
    NavMeshAgent agent;
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void StateRoam() {
        agent.speed = roamSpeed;
    }
    public void StateIdle()
    {
        agent.speed = 0;
    }
    public void StateChase()
    {
        agent.speed = chaseSpeed;
    }
    public void StateAttack()
    {
        agent.speed = 0;
    }
    public void StateStun()
    {
        agent.speed = 0;
    }
}
