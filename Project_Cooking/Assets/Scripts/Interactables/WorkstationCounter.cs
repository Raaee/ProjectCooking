public class WorkstationCounter : Workstation
{
    public override void OnInteractionComplete()
    {
        AddOutputFromInteraction();
    }

}
