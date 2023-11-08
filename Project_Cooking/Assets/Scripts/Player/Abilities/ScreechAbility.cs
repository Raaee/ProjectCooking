using System.Collections;
using UnityEngine;

public class ScreechAbility : Ability {
    // Default keybind: F [Keyboard]

    [SerializeField] private float enemyStunDuration = 5f;

    public override void Awake() {
        base.Awake();
        actions.OnScreech.AddListener(PerformAbility);
    }
    public override void OnAbilityStart() {
        Debug.Log("SCREEEEECH");
    }
    public override void OnAbilityEnd() {

    }
    public override void OnNotEnoughBlood() {
        Debug.Log("Not Enough blood for SCREECH");
    }
    public override void OnCantPerform() {
        Debug.Log("NO");
    }
}
