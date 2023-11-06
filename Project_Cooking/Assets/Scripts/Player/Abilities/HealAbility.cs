using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAbility : Ability  {
    // Default keybind: Q [Keyboard]

    private Health playerHealth;
    [SerializeField] [Range(1, 3)] private int healAmount = 1;

    public override void Awake()    {
        base.Awake();
        playerHealth = GetComponent<Health>();
        actions.OnHeal.AddListener(PerformAbility);
    }
    public override void OnAbilityStart() {
        Debug.Log("*~ Healing ~*");
        playerHealth.Heal(healAmount);
    }
    public override void OnAbilityEnd() {

    }
    public override void OnNotEnoughBlood() {
        Debug.Log("Not Enough blood for HEAL");
    }
    public override void OnCantPerform() {
        Debug.Log("You JUST healed. Give it a second");
    }
}
