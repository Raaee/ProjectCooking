using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StoveOven : Workstation {
    public override void OnInteractionComplete() {

        //play stove oven sound 
        //play feedback 
        CheckIfInventoryHasAll2();
        Inventory.instance.ClearInventory();

        if (hasAllIngredients) {
            //  Inventory.instance.AddItem(currentRecipe.workstationOutput.item);
            Debug.Log("Correct Output Here");
        } else {
            Inventory.instance.AddItem(Items.CHARCOAL);
        }
        hasAllIngredients = false;
    }
    public void CheckIfInventoryHasAll() {
        for(int i = 0; i < Inventory.instance.inventory.Count; i++) {
            if (!Inventory.instance.inventory.Contains(currentRecipe.workstationInput[i].item)) {
                hasAllIngredients = false;
            }
        }
    }

    // WIP:
    public void CheckIfInventoryHasAll2() {
        for (int i = 0; i < workstationRecipesSO.Count; i++) {
            for (int n = 0; n < workstationRecipesSO[i].workstationInput.Count; n++) {
                if (!Inventory.instance.inventory.Contains(workstationRecipesSO[i].workstationInput[n].item)) {
                    hasAllIngredients = false;
                }
                Debug.LogWarning(workstationRecipesSO[i].workstationInput[n].item);
            }
            Debug.LogError(hasAllIngredients);

            if (hasAllIngredients)
                return;
        }
    }
}
