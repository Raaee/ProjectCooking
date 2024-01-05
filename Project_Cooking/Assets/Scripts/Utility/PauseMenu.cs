using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PauseMenu : MonoBehaviour
{

    private Actions actions;
    public bool isPaused = false;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Sprite pausedSprite;
    [SerializeField] private Sprite unPausedSprite;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private CanvasGroup pausePanel;
    [SerializeField] private CanvasGroup settingsPanel;
    private CanvasGroup currentPanel;
    private void Awake() {
        actions = FindObjectOfType<Actions>();
        actions.OnPause.AddListener(DoOpenOrClose);
        ClosePauseMenu();
    }
    private void Init() {
        background.SetActive(false);
        pauseMenu.SetActive(false);
        settingsPanel.gameObject.SetActive(false);
        pausePanel.gameObject.SetActive(false);
    }
    public void DoOpenOrClose() {
        if (isPaused)
            ShowPauseMenu();
        else
            ClosePauseMenu();        
    }
    public void PauseGame() {
        // Pause or resume game here
        if (isPaused) {
            Time.timeScale = 0f;
        } else {
            Time.timeScale = 1f;
        }
    }
    public void ClosePauseMenu() {
        pauseButton.image.sprite = unPausedSprite;
        currentPanel = pausePanel;
        Init();
        PauseGame();
    }
    public void ShowPauseMenu() {
        PauseGame();
        pauseMenu.SetActive(true);
        background.SetActive(true);
        pauseButton.image.sprite = pausedSprite;
        ShowCanvasGroup(pausePanel);
    }
    public void ShowSettings() {
        ShowCanvasGroup(settingsPanel);
    }
    public void ShowCanvasGroup(CanvasGroup cg) {
        currentPanel.gameObject.SetActive(false);
        cg.gameObject.SetActive(true);
        currentPanel.alpha = 0f;
        cg.alpha = 1f;
        currentPanel = cg;
    }
    public void LoadMainMenu() {
        FindObjectOfType<FadeManager>().LoadMainMenu();
    }
}
