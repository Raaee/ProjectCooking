using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseES : EnemyState
{
    [SerializeField] EnemyState aggroChaseState;
    [SerializeField] EnemyState chargeState;
    private EnemyMovement enemyMovement;

    private Transform playerTransform;
    private const float CHARGE_DETECT_RADIUS = 1f;
    public override void Awake()
    {
        base.Awake();
        enemyMovement = GetComponent<EnemyMovement>();
       //TODO: duplicate code, might be better to put it in the enemystatehandler
        var playerObj = FindAnyObjectByType<Movement>();
        if (!playerObj)
            Debug.LogWarning("Is there a player object in this scene?");

        playerTransform = playerObj.gameObject.transform;
    }
    public override void OnStateEnter()
    {
        enemyMovement.ChaseTarget();
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
        float distance = Vector2.Distance(playerTransform.position, transform.position);
        if (distance < CHARGE_DETECT_RADIUS)
        {
            enemyStateHandler.ChangeState(chargeState);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
      
        var tempColor = Color.yellow;
        tempColor.a = 0.125f;
        Gizmos.color = tempColor;
        Gizmos.DrawSphere(transform.position, CHARGE_DETECT_RADIUS);
    }

}
