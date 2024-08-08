using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAI : MonoBehaviour
{
    public GameObject player; 
    private NavMeshAgent agent;
    private bool isChasing = false;
    private float spinTimer = 0f;
    public float spinDuration = 2f;
    public float rotationSpeed = 5f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (player != null)
        {
            // Calculate the direction from agent to player
            Vector3 direction = player.transform.position - transform.position;
            direction.y = 0f;

            if (direction != Vector3.zero)
            {
                
                // Check if the player is in line of sight
                if (IsPlayerVisible())
                {
                    // Start chasing the player
                    isChasing = true;
                    Quaternion rotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
                }
                else
                {
                    // Player is not in line of sight, start spinning
                    isChasing = false;
                    spinTimer = 0f;
                }
            }

            // Perform the chase or spin behavior
            if (isChasing)
            {
                // Move towards the player using the NavMeshAgent
                agent.SetDestination(player.transform.position);
            }
            else
            {
                // Start spinning when the player is not visible
                spinTimer += Time.deltaTime;
                if (spinTimer >= spinDuration)
                {
                    // Stop spinning after reaching the desired spin duration
                    spinTimer = 0f;
                }
                else
                {
                    // Spin the agent
                    transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
                }
            }
        }
    }

    // Check if the player is visible
    private bool IsPlayerVisible()
    {
        if(gameObject.GetComponent<FieldOfView>().canSeePlayer)
            return true;
        else
            return false;
    }



}
