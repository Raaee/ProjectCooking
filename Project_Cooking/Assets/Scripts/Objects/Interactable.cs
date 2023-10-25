using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour {
    
    protected Inventory inventory;
    protected Actions actions;
    private void Awake() {
        actions = FindObjectOfType<Actions>();
        inventory = FindObjectOfType<Inventory>();
    }
    public abstract void Interact();
}
