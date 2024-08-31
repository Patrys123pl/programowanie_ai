using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autopilot : MonoBehaviour
{
    public GameObject target;
    public float moveSpeed = 5f;

    void Update()
    {
       
        Vector3 targetPositionXZ = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        float distance = Vector3.Distance(transform.position, targetPositionXZ);

        Debug.Log("Distance to target: " + distance);

        if (distance > 0.1f)
        {
           
            Vector3 direction = (targetPositionXZ - transform.position).normalized;
            direction.y = 0;

            
            transform.position += direction * moveSpeed * Time.deltaTime;
        }

      
        Vector3 V = transform.forward;
        Vector3 W = (target.transform.position - transform.position).normalized;
        Debug.Log("Dot Product: " + DotProduct(V, W));
        Debug.Log("Angle: " + CalculateAngle(V, W));
        Debug.Log("Cross Product: " + CrossProduct(V, W));
    }

    float DotProduct(Vector3 V, Vector3 W)
    {
        return V.x * W.x + V.y * W.y + V.z * W.z;
    }

    float CalculateAngle(Vector3 V, Vector3 W)
    {
        float dotProduct = DotProduct(V, W);
        float magnitudeV = V.magnitude;
        float magnitudeW = W.magnitude;
        float angle = Mathf.Acos(dotProduct / (magnitudeV * magnitudeW));
        return angle * Mathf.Rad2Deg; 
    }

    Vector3 CrossProduct(Vector3 V, Vector3 W)
    {
        return new Vector3(
            V.y * W.z - V.z * W.y,
            V.z * W.x - V.x * W.z,
            V.x * W.y - V.y * W.x
        );
    }
}