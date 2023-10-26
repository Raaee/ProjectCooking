using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
/// <summary>
/// This is the class that will be used to call the action methods when pressing buttons.
/// This class speaks to the input when an action is performed.
/// </summary>
public class Actions : MonoBehaviour    {
    
    private Input input;
    public UnityEvent OnItemSelect;
    public UnityEvent OnItemDrop;
    public UnityEvent OnInteract;

    private void Awake() {
        input = GetComponent<Input>();
    }
    private void Update() {
        input.interact.performed += Interact;
        input.attack.performed += Attack;
        input.drop.performed += Drop;
        input.slotSelect.performed += SlotSelect;
    }

    public void Interact(InputAction.CallbackContext context) {
        // This is where u put what interacting does
        // Default keybind is E [Keyboard]
        Debug.Log("Interacted.");
        OnInteract.Invoke();
    }
    public void Attack(InputAction.CallbackContext context) {
        // This is where u put what attacking does
        // Default keybind is Left Button [Mouse]
        Debug.Log("Attack!");
    }
    public void Drop(InputAction.CallbackContext context) {
        // This is where u put what dropping does
        // Default keybind is Q [Keyboard]
        OnItemDrop.Invoke();
    }
    public void SlotSelect(InputAction.CallbackContext context) {
        // This is where u put what slot select does
        // Default keybind is Scroll Wheel Up/Down [Mouse]
        //  UP is 120f,  DOWN is -120f  ----> input.slotSelect.ReadValue<float>()
        OnItemSelect.Invoke();
    }
}
