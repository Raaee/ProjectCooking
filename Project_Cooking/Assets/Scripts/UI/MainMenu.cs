using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
/// <summary>
/// The overall Main menu script. might be refactored into a singleton
/// </summary>
public class MainMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup mainMenuPanel;
    [SerializeField] [Range(1.0f, 3f)] float fadeTime = 1.5f;

    private void Start()
    {
        FadeInCanvasGroup(mainMenuPanel, fadeTime);
    }

    public void FadeInCanvasGroup(CanvasGroup cg, float fadeInTime)
    {
        cg.alpha = 0;
        cg.DOFade(1, fadeInTime);
    }

    public void FadeOutCanvasGroup(CanvasGroup cg, float fadeOutTime)
    {
        cg.alpha = 1;
        cg.DOFade(0, fadeOutTime);
    }

}
