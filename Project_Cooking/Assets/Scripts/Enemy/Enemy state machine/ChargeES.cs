using System.Collections;
using UnityEngine;

public class ChargeES : EnemyState
{

    [Header("References")]


    [SerializeField] private EnemyMovement enemyMovement;

    [SerializeField] private EnemyState chaseState;


    public override void Awake()
    {
        base.Awake();

    }
    public override void OnStateEnter()
    {

        StartCoroutine(ChargeSystem());
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
    }


    private IEnumerator ChargeSystem()
    {
        enemyMovement.StopChasing();
        yield return enemyMovement.ChargeAtPlayer();
        yield return new WaitForSeconds(0.25f);
        enemyStateHandler.ChangeState(chaseState);

    }

}
