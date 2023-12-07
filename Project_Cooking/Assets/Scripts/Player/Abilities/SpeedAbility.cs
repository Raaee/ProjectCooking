using System;
using UnityEngine;
/// <summary>
/// vampire chef go brrrr
/// </summary>
public class SpeedAbility : Ability
{
    // Default keybind: E [Keyboard]

    [SerializeField] [Range(1.01f, 4f)] private float speedMultiplier = 2f;
    private Movement playerMovement;
    private PlayerAttack playerAttack;
    [SerializeField] private GameObject trailEffectGO;
    public override void Awake()
    {
        base.Awake();
        playerMovement = GetComponent<Movement>();
        playerAttack = GetComponent<PlayerAttack>();
        actions.OnSpeed.AddListener(PerformAbility);
        trailEffectGO.SetActive(false);
    }
    private void IncreaseSpeed()
    {
        playerMovement.SpeedMode(speedMultiplier);
        playerAttack.IncreaseAttackSpeed(speedMultiplier);
    }

    private void NormalSpeed()
    {
        playerMovement.NormalSpeed();
        playerAttack.NormalAttackSpeed();
    }
    public override void OnAbilityStart()
    {
        Debug.Log("ZOOOMM!");
        IncreaseSpeed();
        trailEffectGO.SetActive(true);
    }
    public override void OnAbilityEnd()
    {
        NormalSpeed();
        trailEffectGO.SetActive(false);

    }
    public override void OnNotEnoughBlood()
    {
        Debug.Log("Not Enough blood for SPEED");
    }
    public override void OnCantPerform()
    {
        Debug.Log("Catch your breath first");
    }
}
