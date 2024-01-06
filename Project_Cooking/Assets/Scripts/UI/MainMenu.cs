using DG.Tweening;
using UnityEngine;
/// <summary>
/// The overall Main menu script. might be refactored into a singleton
/// </summary>
public class MainMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup mainMenuPanel;
    [SerializeField] private CanvasGroup settingsPanel;
    [SerializeField] private CanvasGroup creditsPanel;
    [SerializeField] private CanvasGroup tutorialPanel;
    [SerializeField] private GameObject SlimeImages;
    private CanvasGroup currentPanel;
    [SerializeField] [Range(0.2f, 3f)] private float fadeTime = 1f;

    private void Start()
    {
        Time.timeScale = 1f;
        Init();
    }
    private void Init() {
        currentPanel = mainMenuPanel;
        currentPanel.alpha = 1f;
        SlimeImages.SetActive(true);
        settingsPanel.gameObject.SetActive(false);
        creditsPanel.gameObject.SetActive(false);
        tutorialPanel.gameObject.SetActive(false);
    }

    public void GotoSettings()
    {
        FadeOutCanvasGroup(currentPanel);
        FadeInCanvasGroup(settingsPanel);
        SlimeImages.SetActive(true);
        currentPanel = settingsPanel;
    }

    public void GoToCredits()
    {
        FadeOutCanvasGroup(currentPanel);
        FadeInCanvasGroup(creditsPanel);
        SlimeImages.SetActive(true);
        currentPanel = creditsPanel;
    }

    public void GoBackToMenu()
    {
        FadeOutCanvasGroup(currentPanel);
        FadeInCanvasGroup(mainMenuPanel);
        SlimeImages.SetActive(true);
        currentPanel = mainMenuPanel;
    }
    public void GoToTutorial() {
        SlimeImages.SetActive(false);
        FadeOutCanvasGroup(currentPanel);
        FadeInCanvasGroup(tutorialPanel);
        currentPanel = tutorialPanel;
    }
    public void QuitGame() {
        ES3.Save("musicVol", 0.75f);
        ES3.Save("sfxVol", 0.75f);
        Application.Quit();
    }
    private void FadeInCanvasGroup(CanvasGroup cg)
    {
        cg.gameObject.SetActive(true);
        cg.alpha = 0;
        cg.DOFade(1, fadeTime);
    }
    public void StartGame()
    {
        FindObjectOfType<FadeManager>().FadeOutAndLoadScene(1);

    }
    private void FadeOutCanvasGroup(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.DOFade(0, fadeTime).OnComplete(() =>
        {
            cg.gameObject.SetActive(false);
        }
        );
    }

}
