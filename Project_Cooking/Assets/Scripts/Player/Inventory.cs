using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour {

    public static Inventory instance { get; private set; }

    [SerializeField] private int MAX_INV_SPACES = 3;
    public List<Items> inventoryList = new List<Items>();
    public List<GameObject> allIngredients = new List<GameObject>();
    private Items currentItem = Items.NONE;
    private int invIndex = 0;
    private int invSlotAvailable = 0;
    private const float SCROLL_THRESHOLD = 120f;
  
    [Header("REFERENCES")]
    [SerializeField] private Actions actions;
    [SerializeField] private Input input;

    [Header("EVENTS")]
    public UnityEvent OnCurrentItemChanged;
    public UnityEvent OnInventoryChange;

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
    public bool AddItem(Items item) {
        if (!HasSpace()) {
            Debug.LogWarning("Inventory is full\nATTEMPTED TO ADD:" + item);
            return false;
        }
        inventoryList[invSlotAvailable] = item;
        Debug.Log("How many times was I called? in add item");
        OnInventoryChange.Invoke();
        return true;
    }
    public bool HasSpace() {
        for (int i = 0; i < MAX_INV_SPACES; i++) {
            if (inventoryList[i] == Items.NONE) {
                invSlotAvailable = i;
                return true;
            }
        }
        return false;
    }
    public void RemoveItem() {
        InstantiateItem(inventoryList[invIndex]);
        inventoryList[invIndex] = Items.NONE;
        Debug.Log("**** ITEM DROPPED: " + currentItem);
        OnInventoryChange.Invoke();
    }
    public void InstantiateItem(Items item) {
        GameObject go = null;
        go = allIngredients.Find((go) => go.GetComponent<Ingredient>().GetItemType() == item);

        if (!go)
            return;
        else
            Instantiate(go, FindObjectOfType<Input>().transform.position, Quaternion.identity);
    }
    //This is if u want to specify the item to remove:
    public void RemoveItem(Items item) {
        int index = inventoryList.IndexOf(item);
        if (index == -1) {
            Debug.Log("Item not found in inventory.");
        }
        else {
            inventoryList[index] = Items.NONE;  
        }
    }    
    public bool IsEmpty() {
        for (int i = 0; i < MAX_INV_SPACES; i++) {
            if (inventoryList[i] != Items.NONE) {
                return false;
            }
        }
        return true;
    }
    public void ClearInventory() {
        for (int i = 0; i < MAX_INV_SPACES; i++) {
            inventoryList[i] = Items.NONE;
        }
    }
    public void CurrItemDropped() {
        RemoveItem();
    }
    public void CurrItemSelectedFromScroll() {
        // Scroll UP:
        if (input.slotSelect.ReadValue<float>() < SCROLL_THRESHOLD) {
            invIndex++;
            if (invIndex >= MAX_INV_SPACES) {
                invIndex = 0;
            }
        }

        // Scroll DOWN:
        if (input.slotSelect.ReadValue<float>() >= SCROLL_THRESHOLD) {
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
