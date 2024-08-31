using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public enum AIState
    {
        Seek,
        SeekAndFlee,
        Pursuit,
        Evade,
        Hide
    }

    public AIState currentState = AIState.Seek;
    public Transform player;
    public float speed = 5f;
    public float fleeDistance = 10f;
    public float safeDistance = 15f;
    public float predictionTime = 2f;
    public LayerMask obstaclesLayer;

    private NavMeshAgent agent;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agent.speed = speed;
        }
    }

    private void Update()
    {
        switch (currentState)
        {
            case AIState.Seek:
                Seek();
                break;
            case AIState.SeekAndFlee:
                SeekAndFlee();
                break;
            case AIState.Pursuit:
                Pursuit();
                break;
            case AIState.Evade:
                Evade();
                break;
            case AIState.Hide:
                Hide();
                break;
        }
    }

    // SEEK: AI �ciga gracza przewiduj�c jego ruch.
    private void Seek()
    {
        Vector3 targetPosition = player.position + player.GetComponent<Rigidbody>().velocity * predictionTime;
        if (agent != null)
        {
            agent.SetDestination(targetPosition);
        }
        else
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            rb.velocity = direction * speed;
        }
    }

    // SEEK AND FLEE: AI przelatuje w kierunku gracza, a nast�pnie ucieka na okre�lon� odleg�o��.
    private void SeekAndFlee()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < fleeDistance)
        {
            Vector3 fleeDirection = -directionToPlayer;
            if (agent != null)
            {
                agent.SetDestination(transform.position + fleeDirection * fleeDistance);
            }
            else
            {
                rb.velocity = fleeDirection * speed;
            }
        }
        else
        {
            if (agent != null)
            {
                agent.SetDestination(player.position);
            }
            else
            {
                rb.velocity = directionToPlayer * speed;
            }
        }
    }

    // PURSUIT: AI �ciga gracza, przewiduj�c jego przysz�e po�o�enie na podstawie pr�dko�ci i kierunku.
    private void Pursuit()
    {
        Vector3 futurePosition = player.position + player.GetComponent<Rigidbody>().velocity * predictionTime;
        if (agent != null)
        {
            agent.SetDestination(futurePosition);
        }
        else
        {
            Vector3 direction = (futurePosition - transform.position).normalized;
            rb.velocity = direction * speed;
        }
    }

    // EVADE: AI ucieka od gracza w losowym kierunku na okre�lon� bezpieczn� odleg�o��.
    private void Evade()
    {
        Vector3 directionToPlayer = player.position - transform.position;

        if (directionToPlayer.magnitude < safeDistance)
        {
            Vector3 evadeDirection = (transform.position - player.position).normalized + Random.insideUnitSphere;
            evadeDirection.y = 0; // utrzymujemy poziom AI na tej samej wysoko�ci

            if (agent != null)
            {
                agent.SetDestination(transform.position + evadeDirection.normalized * safeDistance);
            }
            else
            {
                rb.velocity = evadeDirection.normalized * speed;
            }
        }
        else
        {
            if (agent != null)
            {
                agent.ResetPath(); // Zatrzymaj AI, gdy jest w bezpiecznej odleg�o�ci
            }
            else
            {
                rb.velocity = Vector3.zero; // zatrzymaj AI, gdy jest w bezpiecznej odleg�o�ci
            }
        }
    }

    // HIDE: AI szuka najbli�szego obiektu mi�dzy nim a graczem i ukrywa si� za nim.
    private void Hide()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, directionToPlayer, safeDistance, obstaclesLayer);

        if (hits.Length > 0)
        {
            RaycastHit closestHit = hits[0];

            foreach (RaycastHit hit in hits)
            {
                if (hit.distance < closestHit.distance)
                {
                    closestHit = hit;
                }
            }

            Vector3 hidePosition = closestHit.point + closestHit.normal * 2f;

            if (agent != null)
            {
                agent.SetDestination(hidePosition);
            }
            else
            {
                Vector3 direction = (hidePosition - transform.position).normalized;
                rb.velocity = direction * speed;
            }
        }
        else
        {
            // Je�li nie ma obiektu do ukrycia si�, AI kontynuuje ucieczk� od gracza
            Vector3 fleeDirection = -directionToPlayer;

            if (agent != null)
            {
                agent.SetDestination(transform.position + fleeDirection * safeDistance);
            }
            else
            {
                rb.velocity = fleeDirection * speed;
            }
        }
    }
}
