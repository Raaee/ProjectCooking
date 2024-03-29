using UnityEngine;

public class AbilityIconUI : MonoBehaviour
{

    //private UISlotData uiSlot;
    [SerializeField] private AbilityEnum abilityEnum;
    private Ability ability;
    [SerializeField] private ProgressBar bloodProgress;

    private CanvasGroup canvasGroup;
    private void Awake()
    {
        // uiSlot = GetComponent<UISlotData>();
        canvasGroup = GetComponent<CanvasGroup>();
        AssignAbilityReferences();
    }


    private void Update()
    {
        if (bloodProgress.GetcurrentBloodBarAmt() < ability.GetBloodCost())
        {
            canvasGroup.alpha = 0.1f;
        }
        else
        {
            canvasGroup.alpha = 1f;
        }
    }

    private void AssignAbilityReferences()
    {
        switch (abilityEnum)
        {
            case AbilityEnum.SPEED:
                ability = FindObjectOfType<SpeedAbility>();
                break;
            case AbilityEnum.SCREECH:
                ability = FindObjectOfType<ScreechAbility>();
                break;
            case AbilityEnum.HEAL:
                ability = FindObjectOfType<HealAbility>();
                break;
            case AbilityEnum.NONE:
                Debug.LogWarning("Ability Enum not assigned in a ability Icon UI");
                break;


        }

    }

  

}

public enum AbilityEnum
{
    NONE,
    SPEED,
    SCREECH,
    HEAL
}