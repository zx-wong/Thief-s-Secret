using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DebugNavMeshAgent : MonoBehaviour
{
    //NavMeshAgent agent;

    public bool velocity;
    public bool desiredVelocity;
    public bool path;

    // Start is called before the first frame update
    void Start()
    {
        //agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        if (velocity)
        {
            Gizmos.color = Color.green;
            //Gizmos.DrawLine(transform.position, transform.position + agent.desiredVelocity);
        }

        if (desiredVelocity)
        {
            Gizmos.color = Color.red;
            //Gizmos.DrawLine(transform.position, transform.position + agent.desiredVelocity);
        }

        if (path)
        {
            Gizmos.color = Color.black;
            //var agentPath = agent.path;

            Vector3 previewCorner = transform.position;
            //foreach (var corner in agentPath.corners)
            //{
            //    Gizmos.DrawLine(previewCorner, corner);
            //    Gizmos.DrawSphere(corner, 0.1f);
            //    previewCorner = corner;
            //}
        }
    }

}
