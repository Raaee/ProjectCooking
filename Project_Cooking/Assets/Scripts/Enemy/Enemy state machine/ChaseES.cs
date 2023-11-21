using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseES : EnemyState
{
    [SerializeField] EnemyState aggroChaseState;
    [SerializeField] EnemyState chargeState;
    private EnemyMovement enemyMovement;

    private Transform playerTransform;
    private const float AGGRO_DETECT_RADIUS = 2.5f;
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
        if (distance < AGGRO_DETECT_RADIUS)
        {
            enemyStateHandler.ChangeState(chargeState);
        }
    }

}
