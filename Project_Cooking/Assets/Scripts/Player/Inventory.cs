using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{

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
    [SerializeField] private FMODUnity.EventReference scrollSfx;
    [SerializeField] private FMODUnity.EventReference pickupSfx;
    [SerializeField] private FMODUnity.EventReference dropSfx;
    [Header("EVENTS")]
    public UnityEvent OnCurrentItemChanged;
    public UnityEvent OnInventoryChange;

    private void Awake()
    {
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
    public bool AddItem(Items item)
    {
        if (!HasSpace())
        {
            Debug.LogWarning("Inventory is full\nATTEMPTED TO ADD:" + item);
            return false;
        }
        inventoryList[invSlotAvailable] = item;

        OnInventoryChange.Invoke();
        PlayPickup();
        return true;
    }
    public bool HasSpace()
    {
        for (int i = 0; i < MAX_INV_SPACES; i++)
        {
            if (inventoryList[i] == Items.NONE)
            {
                invSlotAvailable = i;
                return true;
            }
        }
        return false;
    }
    public void RemoveItem()
    {
        InstantiateItem(inventoryList[invIndex]);
        inventoryList[invIndex] = Items.NONE;

        OnInventoryChange.Invoke();
        PlayDrop();
    }
    public void InstantiateItem(Items item)
    {


        GameObject go = null;
        go = allIngredients.Find((tempGO) => tempGO.GetComponent<Ingredient>().GetItemType() == item); //RAE CURSED THE CODE

        if (!go)
            return;
        else {
            GameObject food = Instantiate(go, input.transform.position, Quaternion.identity);
            food.transform.parent = FindObjectOfType<Cookbook>().transform;
            LevelManager.instance.AddIngredientToKitchen(food);
        }
    }

    public bool IsEmpty()
    {
        for (int i = 0; i < MAX_INV_SPACES; i++)
        {
            if (inventoryList[i] != Items.NONE)
            {
                return false;
            }
        }
        return true;
    }
    public void ClearInventory()
    {
        for (int i = 0; i < MAX_INV_SPACES; i++)
        {
            inventoryList[i] = Items.NONE;
        }
    }
    public void CurrItemDropped()
    {
        RemoveItem();
    }
    public void CurrItemSelectedFromScroll()
    {
        // Scroll UP:
        if (input.slotSelect.ReadValue<float>() < SCROLL_THRESHOLD)
        {
            invIndex++;
            if (invIndex >= MAX_INV_SPACES)
            {
                invIndex = 0;
            }
        }

        // Scroll DOWN:
        if (input.slotSelect.ReadValue<float>() >= SCROLL_THRESHOLD)
        {
            invIndex--;
            if (invIndex < 0)
            {
                invIndex = MAX_INV_SPACES - 1;
            }
        }

        currentItem = inventoryList[invIndex];
        OnCurrentItemChanged.Invoke();
        PlayScrollSound();
    }
    public Items GetCurrentItem()
    {
        return currentItem;
    }
    public int GetCurrentItemIndex()
    {
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
    private void PlayScrollSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(scrollSfx, this.transform.position);
    }

    private void PlayPickup()
    {
        FMODUnity.RuntimeManager.PlayOneShot(pickupSfx, this.transform.position);
    }

    private void PlayDrop()
    {
        FMODUnity.RuntimeManager.PlayOneShot(dropSfx, this.transform.position);
    }
}
