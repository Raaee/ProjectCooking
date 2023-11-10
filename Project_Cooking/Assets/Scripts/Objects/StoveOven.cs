using System.Collections;
using UnityEngine;
public class StoveOven : Workstation {
    public override void OnInteractionComplete() {
        AddOutputFromInteraction();
    }
    
}
