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
    public UnityEvent OnInteractHeld_Started;
    public UnityEvent OnInteractHeld_Cancelled;
    public UnityEvent OnInteractHeld_Performed;

    private void Awake() {
        input = GetComponent<Input>();
    }
    private void Update() {
        input.interact.performed += Interact;
        input.interactHeld.started += InteractHeld_Started;
        input.interactHeld.canceled += InteractHeld_Cancelled;
        input.interactHeld.performed += InteractHeld_Performed;
        input.attack.performed += Attack;
        input.drop.performed += Drop;
        input.slotSelect.performed += SlotSelect;
    }

    public void Interact(InputAction.CallbackContext context) {
        // This is where u put what interacting does
        // Default keybind is E [Keyboard]
        Debug.Log("Pressed");
        OnInteract.Invoke();        
    }
    public void InteractHeld_Started(InputAction.CallbackContext context) {
        // Default keybind is E [Keyboard]
       
        Debug.Log("Held Started");
        OnInteractHeld_Started.Invoke();
    }
    public void InteractHeld_Cancelled(InputAction.CallbackContext context) {
        // Default keybind is E [Keyboard]
        
        Debug.Log("Held Cancelled");
        OnInteractHeld_Cancelled.Invoke();
    }
    public void InteractHeld_Performed(InputAction.CallbackContext context) {
        // Default keybind is E [Keyboard]

        Debug.Log("Held Performed");
        OnInteractHeld_Performed.Invoke();
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
