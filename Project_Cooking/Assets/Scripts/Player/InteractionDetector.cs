using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public class InteractionDetector : MonoBehaviour {

    public List<IInteractable> interactablesInRange = new List<IInteractable>();
    private Actions actions;
    private Inventory inventory;

    private void Awake() {
        actions = GetComponent<Actions>();
        inventory = GetComponent<Inventory>();
        actions.OnInteract.AddListener(Interacted);
    }
    public void Interacted() {
        if (interactablesInRange.Count > 0) {
            var interactable = interactablesInRange[0];
            interactable.Interact();
            if (!interactable.CanInteract()) {
                interactablesInRange.Remove(interactable);
            }
            
            if (interactable.IsItem()) {
                Debug.Log("interactable: " + interactable);
                // I need interactable object to be put into the inventory if it is an item.
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        var interactable = collision.GetComponent<IInteractable>();

        if (interactable != null && interactable.CanInteract()) {
            interactablesInRange.Add(interactable);
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        var interactable = collision.GetComponent<IInteractable>();
        
        if (interactablesInRange.Contains(interactable)) {
            interactablesInRange.Remove(interactable);
        }
    }
}
