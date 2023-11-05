using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// vampire chef go brrrr
/// </summary>
public class SpeedAbility : Ability
{

    private float timer = 0f;
    [SerializeField][Range(1.01f, 4f)] private float speedMultiplier = 2f;
    [SerializeField] private Movement playerMovement;
    public override void Awake()
    {
        base.Awake();
        playerMovement = GetComponent<Movement>();
    }
    public override void PerformAbility()
    {
        //check if amount avaible 
        if (isPerformingAbility == true)
            return;


        if (bloodProgressBar.GetCurrentBarAmt() < bloodCost)
        {
            Debug.Log("Not Enough blood for speed ability");
            return;
        }

        bloodProgressBar.Decrease(bloodCost);    

        //movement and attack speed increases for a set amount of time 
        isPerformingAbility = true;
        IncreaseSpeed();
    }

   
    private void Update()
    {
        if(isPerformingAbility)
        {
            timer += Time.deltaTime;
        }
       

        if (timer >= abilityDuration)
        {
            timer = 0f;
            isPerformingAbility = false;
            NormalSpeed();
        }
    }

    private void IncreaseSpeed()
    {
        playerMovement.SpeedMode(speedMultiplier);
    }

    private void NormalSpeed()
    {
        playerMovement.NormalSpeed();

    }

    public override void OnAbilityEnd()
    {
        throw new NotImplementedException();
    }
}
