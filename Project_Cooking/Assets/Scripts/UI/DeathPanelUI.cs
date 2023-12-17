using com.cyborgAssets.inspectorButtonPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPanelUI : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    [SerializeField] [Range(0.01f, 2f)] private float fadeInTime = 1.5f;
    [SerializeField] [Range(0.01f, 1f)] private float delayBeforeDisplay = 0.5f;
    [SerializeField] private EasingFunction curveFunction = EasingFunction.Linear;
    [SerializeField] private Health playerHealth;

    [Header("AUDIO")]
    [SerializeField] private FMODUnity.EventReference deathPanelSfx;
    [SerializeField] private FMODUnity.EventReference restartGameSfx;
    [SerializeField] private FMODUnity.EventReference backToMainMenuSfx;
    private void Awake()
    {
        if (!playerHealth)
            Debug.LogError("Please assign Player Health in my inspector", this.gameObject);
        playerHealth.OnDeath.AddListener(ShowDeathPanel);
        canvasGroup = GetComponent<CanvasGroup>();
        
    }

    private void Start()
    {
        HideDeathPanel();
    }
    [ProButton]
    public void ShowDeathPanel()
    {
        var animCurveFunction = AnimationCurveHelper.GetAnimationCurve(curveFunction);
        if(!playerHealth.IsDead())
            StartCoroutine(ShowDeathPanel(fadeInTime, animCurveFunction));
    }

    private IEnumerator ShowDeathPanel(float duration, AnimationCurve curve)
    {
        float startAlpha = 0f;
        float endAlpha = 1f;
        float timeElasped = 0f;

        yield return new WaitForSeconds(delayBeforeDisplay);
        PlayDeathPanelSfx();
        while (timeElasped < duration)
        {
            float normalizeTimed = timeElasped / duration;
            float alphaRatio = curve.Evaluate(normalizeTimed);
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, alphaRatio);
            timeElasped += Time.deltaTime;
            yield return null;
        }
        canvasGroup.interactable = true;
        canvasGroup.alpha = 1f;
    }

    [ProButton]
    private void HideDeathPanel()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
    }

    public void PlayAgainButton()
    {
        Debug.Log("Let Vampire Chef Cook.");
    }

    public void GoToMainMenu()
    {
        Debug.Log("Moving To Main Menu");

    }

    private void PlayDeathPanelSfx()
    {
        FMODUnity.RuntimeManager.PlayOneShot(deathPanelSfx, transform.position);
    }
    public void PlayRestartGameSfx()
    {
        FMODUnity.RuntimeManager.PlayOneShot(restartGameSfx, transform.position);
    }
    public void PlayBackToMainMenuSfx()
    {
        FMODUnity.RuntimeManager.PlayOneShot(backToMainMenuSfx, transform.position);
    }
}
