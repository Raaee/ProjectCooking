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
    [SerializeField] [Range(1f, 5f)] private float movementSpeed = 15f;
    [SerializeField] [Range(1.01f, 3f)] private float aggroSpeedMultipler = 1.5f;
    private Transform currentTarget;

    private Rigidbody2D rb2d;

    private bool isChasing = false; //might need to change to switch/state machine to add features like dash and dodging 



    private Vector3 moveDirection;
    private float originalSpeed;
    private const float ACCELERATION = 250f;
    private const float ANGULAR_SPEED = 250f;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();

        var playerObj = FindObjectOfType<Movement>(); 
        if(!playerObj)
            Debug.LogWarning("Is there a player object in this scene to chase??");
        currentTarget = playerObj.gameObject.transform;
        originalSpeed = movementSpeed;
    }

   
   
    // Update is called once per frame
    void Update()
    {
        if (isChasing == false)
        {
            return;
        }
        AngleTowardsTarget();

    }

    private void FixedUpdate()
    {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        if (!currentTarget)
            return;
        rb2d.velocity = new Vector2(moveDirection.x, moveDirection.y) * movementSpeed;

    }


    private void AngleTowardsTarget()
    {
        if (!currentTarget)
            return;

        Vector3 direction = (currentTarget.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb2d.rotation = angle;
        moveDirection = direction;
    }

    public void ChaseTarget()
    {
        movementSpeed = originalSpeed;
        isChasing = true;
      
    }

    public void ToggleMovement() //for dev mode
    {
        isChasing = !isChasing;
    }

    public void StopChasing()
    {
        isChasing = false;
    }

    public void AggroChase()
    {
        movementSpeed = movementSpeed + aggroSpeedMultipler;
        Debug.Log("aggro chase!");
    }

   
    public void SetMovementSpeed( float newMovementSpeed)
    {
        movementSpeed = newMovementSpeed;
    }

}
