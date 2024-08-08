using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditorInternal.ReorderableList;

public class Fish : MonoBehaviour
{
    NavMeshAgent agent; 
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        InvokeRepeating("SetRandomDestination", 0f, 5f);
    }

    void SetRandomDestination()
    {
        // Get a random direction within the specified radius
        Vector3 randomDirection = Random.insideUnitSphere * 50f;
        randomDirection += transform.position;

        NavMeshHit hit;
        // Find a valid point on the NavMesh within the random direction
        if (NavMesh.SamplePosition(randomDirection, out hit, 50f, NavMesh.AllAreas))
        {
            Vector3 destination = new Vector3(hit.position.x, 0.75f, hit.position.z);
            agent.SetDestination(destination);
        }
    }

}
