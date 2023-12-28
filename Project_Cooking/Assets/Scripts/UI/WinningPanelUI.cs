using System.Collections;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;


public class WinningPanelUI : PanelUI {

    [Header("WINNING PANEL AUDIO")]
    [SerializeField] private FMODUnity.EventReference winPanelSfx;
    [Header("REferences")]
    [SerializeField] private GameObject deathPanelGO;
    public override void Start() {
        base.Start();
        bell.OnGameWon.AddListener(ShowWinPanel);
    }
    public override void PlayAgainButton() {
        // Instead of play again, this will be next level

        Debug.Log("Going to next level");
        FadeManager.instance?.FadeOutAndLoadScene(1);
        FindObjectOfType<KitchenMusic>()?.StopMusic();
        AllRecipeData.instance.NextLevel();

    }
    [ProButton]
    public void ShowWinPanel() {
        Debug.Log("showing win panel");
        var animCurveFunction = AnimationCurveHelper.GetAnimationCurve(curveFunction);
        StartCoroutine(ShowPanel(fadeInTime, animCurveFunction));
        deathPanelGO.SetActive(false); //setting false to make the winning panel interactive
    }

    public override void PlaySFX() {

    }
}
