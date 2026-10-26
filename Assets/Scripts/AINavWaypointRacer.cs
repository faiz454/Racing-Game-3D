using UnityEngine;
using UnityEngine.AI;

public class AINavWaypointRacer : MonoBehaviour
{
    public Transform[] waypoints; // Array to hold WP_1 to WP_6
    public float movementSpeed = 10f;
    public float rotationSpeed = 5f;
    public float reachThreshold = 3f; // Distance to consider waypoint reached

    private int currentWaypointIndex = 0;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.speed = movementSpeed;
            agent.updateRotation = false; // Handled smoothly below
        }

        GotoNextWaypoint();
    }

    void Update()
    {
        if (waypoints.Length == 0) return;

        // Check if the agent is close to the current waypoint
        if (!agent.pathPending && agent.remainingDistance < reachThreshold)
        {
            GotoNextWaypoint();
        }

        // Smoothly rotate the car toward where it's moving
        if (agent.velocity.sqrMagnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(agent.velocity.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void GotoNextWaypoint()
    {
        if (waypoints.Length == 0) return;

        // Set destination to current waypoint
        agent.destination = waypoints[currentWaypointIndex].position;

        // Loop to the next waypoint index
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }
}