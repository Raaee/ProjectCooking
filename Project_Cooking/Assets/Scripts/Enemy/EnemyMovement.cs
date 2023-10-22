using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// The enemy component that works with the movement for the enemy 
/// </summary>
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] [Range(5f, 50f)] private float movementSpeed = 15f;

    private Transform currentTarget;
    private bool isChasing = false; //might need to change to switch/state machine to add features like dash and dodging 



    private NavMeshAgent navMeshAgent;
    private const float ACCELERATION = 250f;
    private const float ANGULAR_SPEED = 250f;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (!navMeshAgent)
            Debug.LogWarning("THIS GAMEOBJECT REQUIRES A NavMeshAgent");

        var playerObj = FindObjectOfType<PlayerAnimControl>(); 
        if(!playerObj)
            Debug.LogWarning("Is there a player object in this scene to chase??");
        currentTarget = playerObj.gameObject.transform;

        SetupMovementData();
    }

   
   
    // Update is called once per frame
    void Update()
    {
        if (isChasing == false)
        {
            return;
        }


        navMeshAgent.SetDestination(currentTarget.position);

    }

    public void ChaseTarget()
    {
        isChasing = true;
    }


    public void Stop()
    {
        isChasing = false;
    }

    private void SetupMovementData()
    {

        navMeshAgent.speed = movementSpeed;
        navMeshAgent.acceleration = ACCELERATION;
        navMeshAgent.angularSpeed = ANGULAR_SPEED;
        navMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.MedQualityObstacleAvoidance;
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }
    public void SetMovementSpeed( float newMovementSpeed)
    {
        movementSpeed = newMovementSpeed;
    }

}
