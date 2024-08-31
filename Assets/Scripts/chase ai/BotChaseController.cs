using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotChaseController : MonoBehaviour
{
    public Transform player; 
    private NavMeshAgent agent; 
    public float moveSpeed = 5f;

    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed; 
    }

    void Update()
    {
        
        agent.SetDestination(player.position);
    }
}