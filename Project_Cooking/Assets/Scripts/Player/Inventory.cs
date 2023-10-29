using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour  {

    public static Inventory instance { get; private set; }
   
   

   
    [SerializeField] private int MAX_INV_SPACES = 3;
    public List<Items> inventoryList = new List<Items>();
    private Items currentItem = Items.NONE;
    private int invIndex = 0;
    private int invSlotAvailable = 0;
    private const float SCROLL_THRESHOLD = 120f;


    [Header("REFERENCES")]
    [SerializeField] private Actions actions;
    [SerializeField] private Input input;


    [Header("REFERENCES")]
    public UnityEvent OnCurrentItemChanged;

    private void Awake() {
        Init();

        if (!actions)
        {
            actions = FindObjectOfType<Actions>();
        }

        if (!input)
        {
            input = FindObjectOfType<Input>();
        }

        actions.OnItemSelect.AddListener(CurrItemSelectedFromScroll);
        actions.OnItemDrop.AddListener(CurrItemDropped);
    }
   

    public void AddItem(Items item) {
        if (!CheckForSpace()) {
            Debug.Log("Inventory is full.");
            return;
        }
        inventoryList[invSlotAvailable] = item;
        Debug.Log("Item added to inventory: " + item);
    }
    public bool CheckForSpace() {
        for (int i = 0; i < MAX_INV_SPACES; i++) {
            if (inventoryList[i] == Items.NONE) {
                invSlotAvailable = i;
                return true;
            }
        }
        return false;
    }
    public void RemoveItem() {
        inventoryList[invIndex] = Items.NONE;
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

        currentItem = inventoryList[invIndex];
        OnCurrentItemChanged.Invoke();
    }
    public Items GetCurrentItem() {
        return currentItem;
    }
    public int GetCurrentItemIndex() {
        return invIndex;
    }
    public int GetMaxInvSpace()
    {
        return MAX_INV_SPACES;
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

       
        for (int i = 0; i < MAX_INV_SPACES; i++)
        {
            inventoryList.Add(Items.NONE);
        }
    }
}
