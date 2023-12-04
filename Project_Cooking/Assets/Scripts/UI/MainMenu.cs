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
    private CanvasGroup currentPanel;
    [SerializeField] [Range(1.0f, 3f)] private float fadeTime = 1.5f;

    private void Start()
    {
        currentPanel = mainMenuPanel;
        FadeInCanvasGroup(currentPanel);

    }

    public void GotoSettings()
    {
        FadeOutCanvasGroup(currentPanel);
        FadeInCanvasGroup(settingsPanel);
        currentPanel = settingsPanel;
    }

    public void GoToCredits()
    {
        FadeOutCanvasGroup(currentPanel);
        FadeInCanvasGroup(creditsPanel);
        currentPanel = creditsPanel;
    }

    public void GoBackToMenu()
    {
        FadeOutCanvasGroup(currentPanel);
        FadeInCanvasGroup(mainMenuPanel);
        currentPanel = mainMenuPanel;
    }


    private void FadeInCanvasGroup(CanvasGroup cg)
    {
        cg.gameObject.SetActive(true);
        cg.alpha = 0;
        cg.DOFade(1, fadeTime);
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
