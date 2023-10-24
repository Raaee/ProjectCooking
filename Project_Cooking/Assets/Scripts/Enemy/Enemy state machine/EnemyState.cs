using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    protected EnemyStateHandler enemyStateHandler;
    public virtual void Awake()
    {
        enemyStateHandler = GetComponent<EnemyStateHandler>();
    }
    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();
}
