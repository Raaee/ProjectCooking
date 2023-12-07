using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    [SerializeField] private Inventory inventory;
    [SerializeField] public List<UISlotData> uiSlots;

    public _AllIngredientsSO allIngredientsSO;

    public List<Sprite> uiSlotSelectionSprites; //0 is empty, 1 is highlighted. "BuT PeTe tHaTs hArdCodIng" - Raeus
    private int index = 0;

    [Header("Set up inventory ui on start")]
    public bool setUP = true; //check to make sure this is false in the ability icosn

    private void Awake()
    {
        if (!inventory)
            Debug.LogError("NO INVENTORY IN SCENE");
        inventory.OnCurrentItemChanged.AddListener(UpdateSelected);
        inventory.OnInventoryChange.AddListener(UpdateInventoryUI);
    }
    private void Start()
    {

        InitIventoryUI();
    }

    public void UpdateInventoryUI()
    {

        for (int i = 0; i < inventory.GetMaxInvSpace(); i++)
        {
            Items item = inventory.inventoryList[i];
            bool found = false;
            //try to find it in the master ingredeintso list 
            foreach (var ingredientSO in allIngredientsSO.ingredientSos)
            {
                if (item == Items.NONE)
                {
                    //skip to the end
                    break;
                }
                if (item == ingredientSO.item)
                {
                    uiSlots[i].ItemImagePlaceholder.sprite = ingredientSO.normalSprite;
                    uiSlots[i].ItemImagePlaceholder.gameObject.SetActive(true);
                    uiSlots[i].ItemImagePlaceholder.enabled = true;
                    found = true;
                    break;
                }
            }

            //if the item is NONE or we didnt make a ingredient SO for it yet 
            if (!found)
            {

                uiSlots[i].ItemImagePlaceholder.sprite = null;
                uiSlots[i].ItemImagePlaceholder.gameObject.SetActive(false);
            }

        }
    }
    public void UpdateSelected()
    {

        index = inventory.GetCurrentItemIndex();
        var itemAtIndex = inventory.GetCurrentItem();

        DisableAllHighlighted();

        uiSlots[index].ImageBackground.sprite = uiSlotSelectionSprites[1];


    }
    private void InitIventoryUI()
    {
        if (!setUP) return;
        DisableAllHighlighted();
        DisableAllImagePlaceholder();
        uiSlots[0].ImageBackground.sprite = uiSlotSelectionSprites[1]; // first slot should start highlighted
    }

    private void DisableAllHighlighted()
    {

        foreach (var uiSlot in uiSlots)
        {
            uiSlot.ImageBackground.sprite = uiSlotSelectionSprites[0];
        }

    }

    private void DisableAllImagePlaceholder()
    {

        foreach (var uiSlot in uiSlots)
        {
            uiSlot.ItemImagePlaceholder.gameObject.SetActive(false);

        }

    }
}
