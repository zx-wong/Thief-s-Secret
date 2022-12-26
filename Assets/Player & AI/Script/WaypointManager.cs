using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//public class WaypointManager : MonoBehaviour
//{
//    public AiManager aiManager;

//    public GameObject[] waypointSetA;
//    public GameObject[] waypointSetB;

//    public float minDistance = 0.2f;

//    public int targetIndex = 1;
//    public int lastIndex;

//    public Transform target;

//    // Start is called before the first frame update
//    public void Start()
//    {
//        lastIndex = waypointSetA.Length - 1;

//        waypointSetA = GameObject.FindGameObjectsWithTag("WaypointA");
//        waypointSetB = GameObject.FindGameObjectsWithTag("WaypointB");

//        target = waypointSetA[targetIndex].transform;
//    }

//    // Update is called once per frame
//    public void Update()
//    {
//        if (waypointSetA.Length == 0) Debug.Log("No Waypoint for A");
//        if (waypointSetB.Length == 0) Debug.Log("No Waypoint for B");
//    }

//    public void CheckDistance()
//    {
//        float currentDistance = Vector3.Distance(aiManager.navMeshAgent.transform.position, target.position);

//        if (currentDistance <= minDistance)
//        {
//            targetIndex++;
//            UpdateWaypoint();
//        }
//    }

//    public void UpdateWaypoint()
//    {
//        if (targetIndex > lastIndex)
//        {
//            targetIndex = 0;
//        }

//        target = waypointSetA[targetIndex].transform;
//    }
//}

public class WaypointManager : MonoBehaviour
{
    public GameObject[] waypoints;
    public Transform targetWaypoint;
    private int targetWaypointIndex = 0;
    private float minDistance = 0.1f; //If the distance between the enemy and the waypoint is less than this, then it has reacehd the waypoint
    private int lastWaypointIndex;

    NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        waypoints = GameObject.FindGameObjectsWithTag("WaypointA");

        lastWaypointIndex = waypoints.Length - 1;
        targetWaypoint = waypoints[targetWaypointIndex].transform; //Set the first target waypoint at the start so the enemy starts moving towards a waypoint
    }

    // Update is called once per frame
    public void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 50f, Color.green, 0f); //Draws a ray forward in the direction the enemy is facing

        float distance = Vector3.Distance(transform.position, targetWaypoint.position);
        CheckDistanceToWaypoint(distance);

        targetWaypoint = waypoints[targetWaypointIndex].transform;
        agent.destination = targetWaypoint.position;
    }

    void CheckDistanceToWaypoint(float currentDistance)
    {
        if (currentDistance <= minDistance)
        {
            targetWaypointIndex++;
            UpdateTargetWaypoint();
        }
    }

    void UpdateTargetWaypoint()
    {
        if (targetWaypointIndex > lastWaypointIndex)
        {
            targetWaypointIndex = 0;
        }

        targetWaypoint = waypoints[targetWaypointIndex].transform;
    }
}
