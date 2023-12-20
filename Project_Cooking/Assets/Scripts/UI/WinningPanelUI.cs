using System.Collections;
using UnityEngine;


public class WinningPanelUI : PanelUI {

    [Header("WINNING PANEL AUDIO")]
    [SerializeField] private FMODUnity.EventReference winPanelSfx;

    private void Start() {
        bell.OnGameWon.AddListener(ShowWinPanel);
    }
    public override void PlayAgainButton() {
        // Instead of play again, this will be next level


    }
    public void ShowWinPanel() {
        var animCurveFunction = AnimationCurveHelper.GetAnimationCurve(curveFunction);
        StartCoroutine(ShowPanel(fadeInTime, animCurveFunction));
    }

    public override void PlaySFX() {

    }
}
