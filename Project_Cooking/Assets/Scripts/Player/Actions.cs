using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Actions : MonoBehaviour    {
    
    [SerializeField] private Input input;

    private void Update() {
        input.interact.performed += Interact;
        input.attack.performed += Attack;
    }

    public void Interact(InputAction.CallbackContext context) {
        // This is where u put what interacting does
        // Default keybind is E [Keyboard]
        Debug.Log("Interacted.");
    }
    public void Attack(InputAction.CallbackContext context) {
        // This is where u put what attacking does
        // Default keybind is Left Button [Mouse]
        Debug.Log("Attack!");
    }
}
