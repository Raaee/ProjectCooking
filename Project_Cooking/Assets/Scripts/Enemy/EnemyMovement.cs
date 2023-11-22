using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The enemy component that works with the movement for the enemy 
/// </summary>
public class EnemyMovement : MonoBehaviour
{
    [Header("MOVEMENT STATS")]
    [SerializeField] [Range(0.5f, 2f)] private float movementSpeed = .75f;
    [SerializeField] [Range(1.01f, 3f)] private float aggroSpeedMultipler = 1.5f;

    private Transform currentTarget;
    private Rigidbody2D rb2d;
    private bool isChasing = false; //might need to change to switch/state machine to add features like dash and dodging 
    private bool isCharging = false;
    private Vector3 moveDirection;
    private float originalSpeed;

    [Header("CHARGE STATS")]
    [SerializeField] private float chargeDelay = 1f;
    [SerializeField] private float dashDuration = 1f;
    [SerializeField] private float dashSpeed = 1f;

    [Header("REFERENCES")]
    [SerializeField] private SlimeEnemyAnimation slimeEnemyAnim;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();

        var playerObj = FindObjectOfType<Movement>();
        if (!playerObj)
            Debug.LogWarning("Is there a player object in this scene to chase??");
        currentTarget = playerObj.gameObject.transform;
        originalSpeed = movementSpeed;
    }
    private void FixedUpdate()
    {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {

        if (!currentTarget)
            return;
        if (isCharging)
            return;
        if (!isChasing)
            return;

        UpdateDirectionToPlayer();

        rb2d.velocity = new Vector2(moveDirection.x, moveDirection.y) * movementSpeed;
        slimeEnemyAnim.SetWalkingState(true);
    }

    private void UpdateDirectionToPlayer()
    {
        moveDirection = (currentTarget.position - transform.position).normalized;
    }

    public void ChaseTarget()
    {
        movementSpeed = originalSpeed;
        isChasing = true;      
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

    public IEnumerator ChargeAtPlayer()
    {

        isCharging = true;
        yield return new WaitForSeconds(chargeDelay);
        UpdateDirectionToPlayer();
        slimeEnemyAnim.AttackAnimation();
        rb2d.velocity = new Vector2(moveDirection.x * dashSpeed, moveDirection.y * dashSpeed);
        yield return new WaitForSeconds(dashDuration);
        isCharging = false;
    
    }

}
