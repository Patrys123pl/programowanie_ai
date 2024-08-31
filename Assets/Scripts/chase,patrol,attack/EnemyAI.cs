using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum State { Patrol, Chase, Attack }
    public State currentState;

    private NavMeshAgent agent;  
    public Transform[] patrolPoints; 
    private int currentPatrolIndex;

    public Transform player;  
    public float chaseDistance = 5f;  
    public float attackDistance = 1.5f;  

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); 
        currentState = State.Patrol;  
        GoToNextPatrolPoint(); 
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Chase:
                Chase();
                break;
            case State.Attack:
                Attack();
                break;
        }
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0)  
            return;

        agent.destination = patrolPoints[currentPatrolIndex].position;  
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;  
    }

    void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)  
        {
            GoToNextPatrolPoint();
        }

        if (Vector3.Distance(player.position, transform.position) < chaseDistance) 
        {
            currentState = State.Chase;
        }
    }

    void Chase()
    {
        agent.destination = player.position;  

        if (Vector3.Distance(player.position, transform.position) < attackDistance) 
        {
            currentState = State.Attack;
        }
        else if (Vector3.Distance(player.position, transform.position) > chaseDistance) 
        {
            currentState = State.Patrol;
            GoToNextPatrolPoint();
        }
    }

    void Attack()
    {
        agent.isStopped = true;  
        Debug.Log("Attack!");  

        if (Vector3.Distance(player.position, transform.position) > attackDistance)  
        {
            currentState = State.Chase;
            agent.isStopped = false; 
        }
    }
}