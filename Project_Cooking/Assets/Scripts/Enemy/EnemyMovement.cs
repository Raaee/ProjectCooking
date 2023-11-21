using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The enemy component that works with the movement for the enemy 
/// </summary>
public class EnemyMovement : MonoBehaviour
{
    
    [SerializeField] [Range(0.5f, 2f)] private float movementSpeed = .75f;
    [SerializeField] [Range(1.01f, 3f)] private float aggroSpeedMultipler = 1.5f;
    private Transform currentTarget;

    private Rigidbody2D rb2d;

    private bool isChasing = false; //might need to change to switch/state machine to add features like dash and dodging 
    private bool isDashing = false;


    private Vector3 moveDirection;
    private float originalSpeed;
    private const float ACCELERATION = 250f;
    private const float ANGULAR_SPEED = 250f;

    [SerializeField] private SlimeEnemyAnimation slimeEnemyAnim;
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
        Debug.Log(rb2d.velocity);
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
        slimeEnemyAnim.SetWalkingState(true);
    }


    private void AngleTowardsTarget()
    {
        if (!currentTarget)
            return;

        Vector3 direction = (currentTarget.position - transform.position).normalized;
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

    public IEnumerator Dash(Vector2 dashDirection, float dashSpeed, float dashDuration)
    {
        Debug.Log("tried to dash dir " + dashDirection + " dshspeed: " + dashSpeed + " dashDur: " + dashDuration);
        isDashing = true;
        AggroChase();
       // rb2d.velocity = new Vector2(dashDirection.x * dashSpeed*50f, dashDirection.y * dashSpeed*50f);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        isChasing = true;
        Debug.Log("finshed dash");
        ChaseTarget();
    }

}
