using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// The enemy component that works with the movement for the enemy 
/// </summary>
public class EnemyMovement : MonoBehaviour
{
    [Header("MOVEMENT STATS")]
    [SerializeField] [Range(0.2f, 2f)] private float movementSpeed = .75f;
    [SerializeField] [Range(1.01f, 3f)] private float aggroSpeedMultipler = 1.5f;

    private Transform currentTarget;
    private Rigidbody2D rb2d;
    private bool isChasing = false; 
    private bool isCharging = false;
    private Vector3 moveDirection;
    private float originalSpeed;

    [Header("CHARGE STATS")]
    [SerializeField] private float chargeDelay = 1f;
    [SerializeField] private float dashDuration = 1f;
    [SerializeField] private float dashSpeed = 1f;

    [Header("REFERENCES")]
    [SerializeField] private SlimeEnemyAnimation slimeEnemyAnim;
    private SpriteRenderer sr;
    [HideInInspector] public UnityEvent OnEnemyCharge;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();

        var playerObj = FindObjectOfType<Movement>();
        if (!playerObj)
            Debug.LogWarning("Is there a player object in this scene to chase??");
        currentTarget = playerObj.gameObject.transform;
        originalSpeed = movementSpeed;
    }
    private void FixedUpdate()
    {
        if (isCharging)
            return;

        if (!isChasing)
            return;
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        if (!currentTarget)
            return;
          
        UpdateDirectionToPlayer();

        rb2d.velocity = new Vector2(moveDirection.x, moveDirection.y) * movementSpeed;
        if (moveDirection.x > 0f) {
            sr.flipX = true;
        }
        else {
            sr.flipX = false;
        }
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

    private float RandomizeChargeDelay() //helper method to very slightly give random charge delays, so enemys not attacking at exact same time
    {
        float rand = UnityEngine.Random.Range(chargeDelay - 0.1f, chargeDelay + 0.1f);
        return rand;
    }
    public IEnumerator ChargeAtPlayer()
    {

        isCharging = true;
        slimeEnemyAnim.AttackAnimation();
        yield return new WaitForSeconds(RandomizeChargeDelay());
        UpdateDirectionToPlayer();
        rb2d.velocity = new Vector2(moveDirection.x * dashSpeed, moveDirection.y * dashSpeed);
        OnEnemyCharge?.Invoke();
        yield return new WaitForSeconds(dashDuration);
        isCharging = false;

    }

}
