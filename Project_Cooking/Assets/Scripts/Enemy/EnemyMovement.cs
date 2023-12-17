using System;
using System.Collections;
using UnityEngine;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine.Events;
/// <summary>
/// The enemy component that works with the movement for the enemy 
/// </summary>
public class EnemyMovement : MonoBehaviour
{
    [Header("MOVEMENT STATS")]
    [SerializeField] [Range(0.2f, 2f)] private float movementSpeed = .75f;
   

    private Transform currentTarget;
    private Rigidbody2D rb2d;
    private Vector3 moveDirection;
   
    public bool isFrozen = false;

    [Header("CHARGE STATS")]
    [SerializeField] private float chargeDelay = 1f;
    [SerializeField] private float dashDuration = 1f;
    [SerializeField] private float dashSpeed = 1f;

    [Header("REFERENCES")]
    [SerializeField] private SlimeEnemyAnimation slimeEnemyAnim;
    private SpriteRenderer sr;
    [HideInInspector] public UnityEvent OnEnemyCharge;

    [Header("DEBUG")]
    [SerializeField] private EnemyMoveState currentState;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        currentTarget = FindObjectOfType<Movement>().gameObject.transform;
        currentState = EnemyMoveState.IDLE;
       
    }
    private void FixedUpdate()
    {

        switch(currentState)
        {
            case EnemyMoveState.IDLE:
                break;
            case EnemyMoveState.CHASE:
                UpdateMovement();
                break;
            case EnemyMoveState.CHARGE:
                break;
            case EnemyMoveState.FROZEN:
                rb2d.velocity = Vector2.zero;
                break;

        }
    }

  

    private void UpdateMovement()
    {
        moveDirection = (currentTarget.position - transform.position).normalized;
        rb2d.velocity = new Vector2(moveDirection.x, moveDirection.y) * movementSpeed;
        if (moveDirection.x > 0f)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
        slimeEnemyAnim.SetWalkingState(true);
    }

    private void SetState(EnemyMoveState newState)
    {
        currentState = newState;
    }

    public void ChaseTarget()
    {
        SetState(EnemyMoveState.CHASE);
    }


    private float RandomizeChargeDelay() //helper method to very slightly give random charge delays, so enemys not attacking at exact same time
    {
        float rand = UnityEngine.Random.Range(chargeDelay - 0.1f, chargeDelay + 0.1f);
        return rand;
    }
    public IEnumerator ChargeAtPlayer()
    {

        SetState(EnemyMoveState.CHARGE);
        slimeEnemyAnim.AttackAnimation();
        yield return new WaitForSeconds(RandomizeChargeDelay());
        moveDirection = (currentTarget.position - transform.position).normalized;
        rb2d.velocity = new Vector2(moveDirection.x * dashSpeed, moveDirection.y * dashSpeed);
        OnEnemyCharge?.Invoke();
        yield return new WaitForSeconds(dashDuration);

    }
    [ProButton]
    public void FreezeEnemy() {
        SetState(EnemyMoveState.FROZEN);
    }
    [ProButton]
    public void UnFreezeEnemy() {
        SetState(EnemyMoveState.CHASE);
    }
}
public enum EnemyMoveState
{
    IDLE,
    CHASE,
    CHARGE,
    FROZEN

}