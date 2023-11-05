using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAbility : Ability
{
    [SerializeField] private Health playerHealth;
    public override void Awake()
    {
        base.Awake();
        playerHealth = GetComponent<Health>();
    }

    public override void OnAbilityEnd()
    {
        throw new System.NotImplementedException();
    }

    public override void PerformAbility()
    {
        throw new System.NotImplementedException();
    }
}
