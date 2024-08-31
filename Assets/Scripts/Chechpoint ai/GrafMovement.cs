using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class GratMovement : MonoBehaviour
{
    public Transform[] points;
    public float speed;

    private int currentPoint = 0;

    void Update()
    {
        if (currentPoint < points.Length)
        {
            MoveTowardsPoint();
        }
    }
    void MoveTowardsPoint()
    {
        Vector3 targetPosition = points[currentPoint].position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            currentPoint++;
            if (currentPoint >= points.Length)
            {
                currentPoint = 0;
            }
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < points.Length; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(points[i].position, points[i + 1].position);
        }
    }
}