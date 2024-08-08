using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dog : MonoBehaviour
{
    public List<Transform> zones;
    private int index = 0;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (zones.Count > 0)
        {
            MoveToNextZone();
        }
        else
        {
            Debug.LogWarning("La liste des zones est vide.");
        }
    }

    private void MoveToNextZone()
    {
        if (zones.Count == 0)
        {
            return;
        }

        index = index % zones.Count;
        agent.SetDestination(zones[index].position);
        index++;
    }

    private void Update()
    {
        if (zones.Count == 0)
        {
            return;
        }

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            MoveToNextZone();
        }
    }
}
