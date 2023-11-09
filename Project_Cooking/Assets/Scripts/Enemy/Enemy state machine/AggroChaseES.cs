using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroChaseES : EnemyState
{
    private EnemyMovement enemyMovement;
    [SerializeField] private float cooldownTime;
   [SerializeField] private EnemyState chaseState;
   

    public override void Awake()
    {
        base.Awake();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    public override void OnStateEnter()
    {
        enemyMovement.AggroChase();
        StartCoroutine(StartAggroCooldown());
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
    }

    private IEnumerator StartAggroCooldown() {

        yield return new WaitForSeconds(cooldownTime);
        enemyStateHandler.ChangeState(chaseState);
      
    }
}
