using com.cyborgAssets.inspectorButtonPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPanelUI : PanelUI {

    [SerializeField] private Health playerHealth;

    [Header("DEATH PANEL AUDIO")]
    [SerializeField] private FMODUnity.EventReference deathPanelSfx;
    
    private void Awake()
    {
        bell.OnGameLost.AddListener(ShowDeathPanel);

        if (!playerHealth)
            Debug.LogError("Please assign Player Health in my inspector", this.gameObject);
        playerHealth.OnDeath.AddListener(ShowDeathPanel);
        
    }
    public override void Start()
    {
        base.Start();
    }

    [ProButton]
    public void ShowDeathPanel()
    {
        var animCurveFunction = AnimationCurveHelper.GetAnimationCurve(curveFunction);
        if (!playerHealth.IsDead()) {
            StartCoroutine(ShowPanel(fadeInTime, animCurveFunction));
            AllRecipeData.instance.ShuffleRecipes();
        }
    }
    public override void PlayAgainButton()
    {

        FadeManager.instance?.FadeOutAndLoadScene(1);
    }
    
    public override void PlaySFX() {
        FMODUnity.RuntimeManager.PlayOneShot(deathPanelSfx, transform.position);

    }
    public void PlayRestartGameSfx()
    {
        FMODUnity.RuntimeManager.PlayOneShot(restartGameSfx, transform.position);
    }
    
}
