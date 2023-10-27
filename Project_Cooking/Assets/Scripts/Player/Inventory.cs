using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour  {

    public static Inventory instance { get; private set; }
   
    private Actions actions;
    private Input input;
    [SerializeField] private int MAX_INV_SPACES = 3;
    public List<Items> inventory = new List<Items>();
    private Items currentItem = Items.NONE;
    private int invIndex = 0;
    private int invSlotAvailable = 0;
    private const float SCROLL_THRESHOLD = 120f;


    private void Awake() {
        Init();
        actions = GetComponent<Actions>();
        input = GetComponent<Input>();
        actions.OnItemSelect.AddListener(CurrItemSelectedFromScroll);
        actions.OnItemDrop.AddListener(CurrItemDropped);
    }
   

    public void AddItem(Items item) {
        if (!CheckForSpace()) {
            Debug.Log("Inventory is full.");
            return;
        }
        inventory[invSlotAvailable] = item;
        Debug.Log("Item added to inventory: " + item);
    }
    public bool CheckForSpace() {
        for (int i = 0; i < MAX_INV_SPACES; i++) {
            if (inventory[i] == Items.NONE) {
                invSlotAvailable = i;
                return true;
            }
        }
        return false;
    }
    public void RemoveItem() {
        inventory[invIndex] = Items.NONE;
        Debug.Log("**** ITEM DROPPED: " + currentItem);
    }
    public void ClearInventory() {
        for (int i = 0; i < MAX_INV_SPACES; i++) {
            inventory[i] = Items.NONE;
        }
    }
    public void CurrItemDropped() {
        RemoveItem();
    }
    public void CurrItemSelectedFromScroll() {
        // Scroll UP:
        if (input.slotSelect.ReadValue<float>() >= SCROLL_THRESHOLD) {
            invIndex++;
            if (invIndex >= MAX_INV_SPACES) {
                invIndex = 0;
            }
        }

        // Scroll DOWN:
        if (input.slotSelect.ReadValue<float>() < SCROLL_THRESHOLD) {
            invIndex--;
            if (invIndex < 0) {
                invIndex = MAX_INV_SPACES - 1;
            }
        }

        currentItem = inventory[invIndex];
        Debug.Log("Item Selected: " + currentItem); 
    }
    public Items GetCurrentItem() {
        return currentItem;
    }
    public int GetCurrentItemIndex() {
        return invIndex;
    }
    private void Init()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        ClearInventory();
    }
}
