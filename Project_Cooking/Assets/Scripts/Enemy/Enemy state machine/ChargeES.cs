using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeES : EnemyState
{
    [Header("Dash Settings")]
    [SerializeField] private float delayTime = 2.5f;
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashDuration = 0.75f;
    [SerializeField] private float dashCooldown = 0.75f;



    [Header("References")]
    [SerializeField] private SlimeEnemyAnimation slimeEnemyAnimation;
   
    [SerializeField] private EnemyMovement enemyMovement;
     private Transform playerTransform;



    public override void Awake()
    {
        base.Awake();
        playerTransform = FindObjectOfType<Movement>().gameObject.transform;
    }
    public override void OnStateEnter()
    {
        //stop moving 
        enemyMovement.StopChasing();
        //switch to idle anim
        slimeEnemyAnimation.SetWalkingState(false);
        //do charging ienume
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        StartCoroutine(enemyMovement.Dash(direction, dashSpeed, dashDuration));
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
    }

    private void Charge()
    {
       
       
    }
    //charge IENUM 
        //wait x seconds
        //direction to player
        //dash in that area 
        //wait x seconds 
        //go to new state (idle?)
}
