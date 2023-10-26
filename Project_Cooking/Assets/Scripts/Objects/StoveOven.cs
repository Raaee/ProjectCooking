using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StoveOven : Workstation {
    public override void OnInteractionComplete() {
        // Use singleton Inventory.instance to do inventory stuff

        /*
         * Inventory stuff is:
         * - if inventory has THE combination of ingredients when interaction is complete,
         * will remove those items and give the inventory the item that is created from the combination of the items.
         * - else if the correct combination is not given, will remove items and give charcoal item.
         */

        // This override method will be on all children of the workstation.

        Debug.Log("Inventory Stuff");
    }
}
