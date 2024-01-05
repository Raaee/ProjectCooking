using System.Collections;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinningPanelUI : PanelUI {

    [Header("WINNING PANEL AUDIO")]
    [SerializeField] private FMODUnity.EventReference winPanelSfx;

    [Header("REFERENCES")]
    [SerializeField] private GameObject deathPanelGO;
    public bool playerWon = false;

    [Header("VISUALS")]
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text subText;
    [SerializeField] private Image centerImage;
    [SerializeField] private Sprite perLevel;
    [SerializeField] private Sprite perGame;
    [SerializeField] private GameObject nextLvlButton;
    [SerializeField] private GameObject mainMenuButton;

    public override void Start() {
        base.Start();
        bell.OnGameWon.AddListener(ShowWinPanel);
        playerWon = false;
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
        
        var animCurveFunction = AnimationCurveHelper.GetAnimationCurve(curveFunction);
        PanelVisualOnLevel();        
        StartCoroutine(ShowPanel(fadeInTime, animCurveFunction));
        deathPanelGO.SetActive(false); //setting false to make the winning panel interactive
        playerWon = true;

    }
    public void PanelVisualOnLevel() {
        if (AllRecipeData.instance.GetCurrLvlIndex() == AllRecipeData.instance.allRecipes.Count-1) {
            UpdateVisuals("The Slime has had enough", "you live to see another day", false, true, perGame);
        } else {
            UpdateVisuals("Slime is satisfied...", "but it's not over yet", true, false, perLevel);
        }
    }
    public void UpdateVisuals(string titleText, string subtextText, bool nextLvl, bool gameDone, Sprite sprite) {
        title.text = titleText;
        subText.text = subtextText;
        centerImage.sprite = sprite;
        nextLvlButton.SetActive(nextLvl);
        mainMenuButton.SetActive(gameDone);
    }

    public override void PlaySFX() {

    }
}
