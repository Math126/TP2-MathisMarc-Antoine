using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class PNJRoutine : MonoBehaviour
{
    public List<Transform> wanderZones;
    public bool dog;

    private NavMeshAgent agent;
    private Animator animator;
    private int currentZoneIndex;
    private float timeSinceLastDestinationChange = 0.0f;
    private float minTimeBetweenDestinations = 5.0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent <Animator>();
        PickRandomZoneTarget();
    }

    void Update()
    {
        if(!dog)
            animator.SetFloat("Speed", agent.velocity.magnitude);

        // Vérifiez si le PNJ est arrivé à sa destination actuelle et assez de temps s'est écoulé
        if (!agent.pathPending && agent.remainingDistance < 1.0f && timeSinceLastDestinationChange > minTimeBetweenDestinations)
        {
            PickRandomZoneTarget();
            timeSinceLastDestinationChange = 0.0f;
        }

        timeSinceLastDestinationChange += Time.deltaTime;
    }

    void PickRandomZoneTarget()
    {
        if (wanderZones.Count == 0)
        {
            Debug.LogWarning("Aucune zone de promenade n'a été définie.");
            return;
        }

        // Choisissez une zone aléatoire
        currentZoneIndex = Random.Range(0, wanderZones.Count);
        Transform newZone = wanderZones[currentZoneIndex];

        // Choisissez un point aléatoire dans la zone sur le NavMesh
        Vector3 randomPoint = RandomPointInZone(newZone);
        agent.SetDestination(randomPoint);
    }

    Vector3 RandomPointInZone(Transform zone)
    {
        // Génère un point aléatoire à l'intérieur de la zone sur le NavMesh
        NavMeshHit hit;
        Vector3 randomPoint;
         
        for (int i = 0; i < 30; i++) // Tentez de générer un point valide pendant 30 itérations au maximum
        {
            Vector3 randomDirection = Random.insideUnitSphere * Random.Range(0, 5.0f);
            randomDirection += zone.position;
            if (NavMesh.SamplePosition(randomDirection, out hit, 5.0f, NavMesh.AllAreas))
            {
                randomPoint = hit.position;
                return randomPoint;
            }
        }

        return zone.position;
    }
}
