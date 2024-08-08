using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.AI;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0,360)]
    public float angle;

    public GameObject[] playerRefTab;
    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    private Animator animator;
    private NavMeshAgent agent;

    void Start()
    {
        playerRefTab = GameObject.FindGameObjectsWithTag("Dog");
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        
        InvokeRepeating("FieldOfViewCheck", 0.1f, 0.1f);
    }

    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);

        playerRef = SeekClosestPlayer(playerRefTab);
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if(rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;

                
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }

    public GameObject SeekClosestPlayer(GameObject[] listePlayer)
    {
        GameObject bestTarget = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach(GameObject potentialTarget in listePlayer)
        {
            Vector3 directionToTarget = potentialTarget.gameObject.transform.position - currentPosition;
            float dSqrtToTarget = directionToTarget.sqrMagnitude;
            if (dSqrtToTarget < closestDistance)
            {
                closestDistance = dSqrtToTarget;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }
}
