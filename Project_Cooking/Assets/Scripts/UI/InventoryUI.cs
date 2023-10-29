using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

    [SerializeField] private Inventory inventory;
    [SerializeField] public List<UISlotData> uiSlots;

    [SerializeField] private Color defaultBackgroundColor;
    [SerializeField] private Color selectedBackgroundColor;

    public _AllIngredientsSO allIngredientsSO;
    private int index = 0;
    private void Awake()
    {
        if (!inventory)
            Debug.LogError("NO INVENTORY IN SCENE");
        inventory.OnCurrentItemChanged.AddListener(UpdateSelected);
        inventory.OnInventoryChange.AddListener(UpdateInventoryUI);
    }
    private void Start()
    {
        DisableAllHighlighted();
    }

    public void UpdateInventoryUI()
    {
        
        for (int i = 0; i < inventory.GetMaxInvSpace(); i++)
        {
            Items item = inventory.inventoryList[i];
            //try to find it in the master ingredeintso list 
            foreach (var ingredientSO in allIngredientsSO.ingredientSos)
            {
                if (item == ingredientSO.item)
                {
                    uiSlots[i].ItemImagePlaceholder.sprite = ingredientSO.normalSprite;
                    return; 
                }
            }

            //if the item is NONE or we didnt make a ingredient SO for it yet 
            uiSlots[i].ItemImagePlaceholder.sprite = null;
           
        }
    }
    public void UpdateSelected() 
    {
        index = inventory.GetCurrentItemIndex();
        DisableAllHighlighted();
        uiSlots[index].ImageBackground.color = selectedBackgroundColor;
        uiSlots[index].ArrowSelect.gameObject.SetActive(true);
    }
  
    private void DisableAllHighlighted() 
    { 
        for(int i = 0; i < inventory.GetMaxInvSpace(); i++)
        {
            uiSlots[i].ImageBackground.color = defaultBackgroundColor;
            uiSlots[i].ArrowSelect.gameObject.SetActive(false);
        }
    }
}
