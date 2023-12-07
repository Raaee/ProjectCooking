using System.Collections.Generic;
using UnityEngine;

public class AbilityIconsUIManager : MonoBehaviour
{
    public List<UISlotData> uiSlots;
    public List<AbilityIconUI> abilityIconUIs;
    [SerializeField] private ProgressBar progressBar;
 
    void Start()
    {
      
    }

    private void OverrideIventoryUI()
    {
      

    }

    public ProgressBar GetProgressBar()
    {
        return progressBar;
    }
}
