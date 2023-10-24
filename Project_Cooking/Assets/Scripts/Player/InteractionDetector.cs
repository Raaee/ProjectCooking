using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public class InteractionDetector : MonoBehaviour {

    public List<Interactable> interactablesInRange = new List<Interactable>();
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
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        var interactable = collision.GetComponent<Interactable>();

        if (interactable != null && interactable.CanInteract()) {
            interactablesInRange.Add(interactable);
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        var interactable = collision.GetComponent<Interactable>();
        
        if (interactablesInRange.Contains(interactable)) {
            interactablesInRange.Remove(interactable);
        }
    }
}
