using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour  {

    [Header("Player Inputs")]
    public PlayerControls playerControls;
    public InputAction move;
    public InputAction interact;
    public InputAction interactHeld;
    public InputAction attack;
    public InputAction drop;
    public InputAction slotSelect;

    [Header("Ability Inputs")]
    public InputAction speedAbilityIA;
    public InputAction healAbilityIA;
    public InputAction screechAbilityIA;
    private void Awake() {
        playerControls = new PlayerControls();
    }
    private void OnEnable() {
        move = playerControls.Player.Move;
        EnableMovement();

        interact = playerControls.Player.Interact;
        EnableInteract();

        interactHeld = playerControls.Player.InteractHeld;
        EnableInteractHeld();

        attack = playerControls.Player.Attack;
        EnableAttack();

        drop = playerControls.Player.Drop;
        EnableDrop();

        slotSelect = playerControls.Player.SlotSelect;
        EnableSlotSelect();

        speedAbilityIA = playerControls.Player.SpeedAbility;
        EnableSpeedAbility();

        healAbilityIA = playerControls.Player.HealAbility;
        EnableHealAbility();

        screechAbilityIA = playerControls.Player.ScreechAbility;
        EnableScreechAbility();
    }
    private void OnDisable() {
        DisableMovement();
        DisableInteract();
        DisableInteractHeld();
        DisableAttack();
        DisableDrop();
        DisableSlotSelect();
        DisableSpeedAbility();
        DisableHealAbility();
        DisableScreechAbility();
    }
    //---------------AREA INPUTS ---------------------
    public void EnableKitchenInputs()
    {
        EnableInteract();
        EnableInteractHeld();
        EnableDrop();
        EnableSlotSelect();
    }
    public void DisableKitchenInputs()
    {
        DisableInteract();
        DisableInteractHeld();
        DisableDrop();
        DisableSlotSelect();
    }

    public void EnableDungeonInputs()
    {
        EnableAttack();
        EnableHealAbility();
        EnableSpeedAbility();
        EnableScreechAbility();

    }

    public void DisableDungeonInputs()
    {
        DisableAttack();
        DisableHealAbility();
        DisableSpeedAbility();
        DisableScreechAbility();
    }

    public void EnableInteractHeld() {
        interactHeld.Enable();
    }
    public void DisableInteractHeld() {
        interactHeld.Disable();
    }
    public void EnableDrop() {
        drop.Enable();
    }
    public void DisableDrop() {
        drop.Disable();
    }
    public void EnableSlotSelect() {
        slotSelect.Enable();
    }
    public void DisableSlotSelect() {
        slotSelect.Disable();
    }
    public void EnableInteract() {
        interact.Enable();
    }
    public void EnableAttack() {
        attack.Enable();
    }
    public void EnableMovement() {
        move.Enable();
    }
    public void DisableInteract() {
        interact.Disable();
    }
    public void DisableAttack() {
        attack.Disable();
    }
    public void DisableMovement() {
        move.Disable();
    }
    public void DisableSpeedAbility() {
        speedAbilityIA.Disable();
    }
    public void DisableHealAbility() {
        healAbilityIA.Disable();
    }
    public void DisableScreechAbility() {
        screechAbilityIA.Disable();
    }
    public void EnableSpeedAbility() {
        speedAbilityIA.Enable();
    }
    public void EnableHealAbility() {
        healAbilityIA.Enable();
    }
    public void EnableScreechAbility() {
        screechAbilityIA.Enable();
    }
}
