using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StoveOven : Workstation {
    public override void OnInteractionComplete() {

        //play stove oven sound 
        //play feedback 


        //if player has [dough, tomato paste, shreded cheese] in any order in inventory 
        //remove items from inventory 
        //place into inventory pizza
        //else
        //REMOVE ITEMS FROM IVENTORY
        //place into inventory charcoal
        CheckIfInventoryHasAll();
        Inventory.instance.ClearInventory();

        if (hasAllIngredients) {
            Inventory.instance.AddItem(workstationRecipesSO[0].workstationOutput.item);
        } else {
            Inventory.instance.AddItem(Items.CHARCOAL);
        }
        //if player has the correect ingredients (in any order) 
        //remove items from inventory 
        //place into inventory pizza
        //else
        //REMOVE ITEMS FROM IVENTORY
        //place into inventory charcoal 
    }
    public void CheckIfInventoryHasAll() {
        for(int i = 0; i < workstationRecipesSO[0].workstationInput.Count; i++) {
            if (!Inventory.instance.inventoryList.Contains(workstationRecipesSO[0].workstationInput[i].item)) {
                hasAllIngredients = false;
            }
        }
    }
}
