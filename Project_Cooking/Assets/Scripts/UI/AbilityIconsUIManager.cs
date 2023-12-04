using System.Collections.Generic;
using UnityEngine;

public class AbilityIconsUIManager : MonoBehaviour
{
    public List<UISlotData> uiSlots;
    public List<AbilityIconUI> abilityIconUIs;

    [Header("Override the inventory UI")]
    public bool overrideInventory = false;//check to make sure this is diff in the inventoryUI


    void Start()
    {
        OverrideIventoryUI();
    }

    private void OverrideIventoryUI()
    {
        if (!overrideInventory) return;
        for (int i = 0; i < uiSlots.Count; i++)
        {
            uiSlots[i].ImageBackground.sprite = abilityIconUIs[i].GetAbilitySprite();
            uiSlots[i].ItemImagePlaceholder.enabled = false;
        }

    }
}
