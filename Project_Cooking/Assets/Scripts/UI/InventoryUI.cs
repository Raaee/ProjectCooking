using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

    [SerializeField] private Inventory inventory;
    [SerializeField] public List<UISlotData> uiSlots;

    [SerializeField] private Color defaultBackgroundColor;
    [SerializeField] private Color selectedBackgroundColor;
    private int index = 0;
    private void Awake()
    {
        if (!inventory)
            Debug.LogError("NO INVENTORY IN SCENE");
        inventory.OnCurrentItemChanged.AddListener(UpdateSelected);
    }
    private void Start()
    {
        DisableAllHighlighted();
    }

    public void UpdateInventoryUI()
    {
        // get the item type at this slot
        //
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
