using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// basically the the "frozen" state
/// </summary>
public class IdleES : EnemyState
{
  
    private Transform playerTransform;
    private const float PLAYER_DETECT_RADIUS = 25f;
    private EnemyMovement enemyMovement;

    [SerializeField] private EnemyState chaseState;
    public override void Awake()
    {
        base.Awake();
        Init();
    }

    public override void OnStateEnter()
    {
        //not much to do, we chilling and idling
        enemyMovement.StopChasing();
    }

    public override void OnStateExit()
    {
        //not much to do, we chilling
    }

    public override void OnStateUpdate()
    {
        //check if the player is within range
        float distance = Vector2.Distance(playerTransform.position, transform.position);
        if(distance < PLAYER_DETECT_RADIUS)
        {
            enemyStateHandler.ChangeState(chaseState);
        }
    }

    private void Init()
    {
        var playerObj = FindAnyObjectByType<Movement>();
        if (!playerObj)
            Debug.LogWarning("Is there a player object in this scene?");

        playerTransform = playerObj.gameObject.transform;

        if (!chaseState)
            Debug.LogWarning("chase state not set up bruh, must be Raeus fault");
        enemyMovement = GetComponent<EnemyMovement>();
    }
}
