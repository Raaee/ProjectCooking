using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// vampire chef go brrrr
/// </summary>
public class SpeedAbility : Ability {
    // Default keybind: E [Keyboard]

    [SerializeField] [Range(1.01f, 4f)] private float speedMultiplier = 2f;
    private Movement playerMovement;
    public override void Awake()    {
        base.Awake();
        playerMovement = GetComponent<Movement>();
        actions.OnSpeed.AddListener(PerformAbility);
    }
    private void IncreaseSpeed()    {
        playerMovement.SpeedMode(speedMultiplier);
    }

    private void NormalSpeed()  {
        playerMovement.NormalSpeed();
    }
    public override void OnAbilityStart() {
        Debug.Log("ZOOOMM!");
        IncreaseSpeed();
    }
    public override void OnAbilityEnd() {
            NormalSpeed();

    }
    public override void OnNotEnoughBlood() {
        Debug.Log("Not Enough blood for SPEED");
    }
    public override void OnCantPerform() {
        Debug.Log("Catch your breath first");
    }
}
