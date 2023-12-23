using System.Collections;
using UnityEngine;


public abstract class PanelUI : MonoBehaviour {

    [SerializeField] protected Bell bell;
    [SerializeField] protected CanvasGroup canvasGroup;
    [SerializeField] [Range(0.01f, 2f)] protected float fadeInTime = 1.5f;
    [SerializeField] [Range(0.01f, 1f)] protected float delayBeforeDisplay = 0.5f;
    [SerializeField] protected EasingFunction curveFunction = EasingFunction.Linear;

    [Header("AUDIO")]
    [SerializeField] protected FMODUnity.EventReference restartGameSfx;
    [SerializeField] protected FMODUnity.EventReference backToMainMenuSfx;
  

    public virtual void Start() {
        HidePanel();
    }

    protected IEnumerator ShowPanel(float duration, AnimationCurve curve) {
        float startAlpha = 0f;
        float endAlpha = 1f;
        float timeElasped = 0f;

        yield return new WaitForSeconds(delayBeforeDisplay);
        PlaySFX();
        while (timeElasped < duration) {
            float normalizeTimed = timeElasped / duration;
            float alphaRatio = curve.Evaluate(normalizeTimed);
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, alphaRatio);
            timeElasped += Time.deltaTime;
            yield return null;
        }
        canvasGroup.interactable = true;
        canvasGroup.alpha = 1f;
    }
    protected void HidePanel() {
        Debug.Log("hiding");
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
    }

    public abstract void PlayAgainButton();
    public abstract void PlaySFX();

    public void GoToMainMenu() {
        Debug.Log("Moving To Main Menu");

    }
    public void PlayBackToMainMenuSfx() {
        FMODUnity.RuntimeManager.PlayOneShot(backToMainMenuSfx, transform.position);
    }
}
