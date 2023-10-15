using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour  {

    public PlayerControls playerControls;
    public InputAction move;
    public InputAction interact;
    public InputAction attack;

    private void Awake() {
        playerControls = new PlayerControls();
    }
    private void OnEnable() {
        move = playerControls.Player.Move;
        EnableMovement();

        interact = playerControls.Player.Interact;
        EnableInteract();

        attack = playerControls.Player.Attack;
        EnableAttack();
    }
    private void OnDisable() {
        DisableMovement();
        DisableInteract();
        DisableAttack();
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
}
