using UnityEngine;
using UnityEngine.Events;


public class WorkstationCounter : Workstation
{
   
    public override void OnInteractionComplete()
    {
        AddOutputFromInteraction();
    }

}
