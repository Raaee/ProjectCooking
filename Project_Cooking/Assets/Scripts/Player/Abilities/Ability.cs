using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// base abstract class for abilities 
/// </summary>
public abstract class Ability : MonoBehaviour
{
    protected const float QUICK_COOLDOWN = 1.0f;
    //TODO: add a range attribute for game design changes
    [SerializeField] protected float abilityDuration;
    [SerializeField] protected int bloodCost;
    protected bool isPerformingAbility = false;

    //references 
    protected ProgressBar bloodProgressBar;

    public abstract void PerformAbility();

    public virtual void Awake()
    {
        bloodProgressBar = FindAnyObjectByType<ProgressBar>();
        if (!bloodProgressBar)
            Debug.LogError("Raeus broke the game in Ability class (cant find progress bar component)");
    }
}
