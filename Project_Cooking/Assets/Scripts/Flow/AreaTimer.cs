using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class AreaTimer : MonoBehaviour
{
    [SerializeField] private GameObject vignette;
    [SerializeField] private float kitchenTimeLength = 15f; //should probs be the same?
    [SerializeField] private float dungeonTimeLength = 15f;
   // [SerializeField] [Range(0.5f, 3f)] private float vignetteEffectSpeed = 1f;

    //no limbo time, that is made by the player
    private AreaTimerUI areaTimerUI;
    private bool timeAlmostDone = false;
    private Q_Vignette_Single vignetteScript;

    [Header("DEBUG")]
    [SerializeField] private float timer = 0f;
    [SerializeField] private bool isTimerPaused = false;
    [SerializeField] private bool startTimerPaused = false;
    [SerializeField] private bool isRoundOverInvoked = false;

    public UnityEvent OnRoundOver;

    private void Awake()
    {
        areaTimerUI = GetComponentInChildren<AreaTimerUI>();
        vignetteScript = vignette.GetComponent<Q_Vignette_Single>();
    }
    void Start()
    {
        float delayStartTime = 0.8f;
        dungeonTimeLength += delayStartTime;
        kitchenTimeLength += delayStartTime;
        timer = dungeonTimeLength;
        timeAlmostDone = true;
    }

    void Update()
    {
        if (startTimerPaused) return;

        if (isTimerPaused) return;
        else
            timer -= Time.deltaTime;

        if (timer <= areaTimerUI.GetTimeToChangeColor()) {
            if (timeAlmostDone) {
                timeAlmostDone = false;
                StartCoroutine(StartVignette());
            }
        }

        float roundOverBuffer = 0.3f;
        if (timer <= roundOverBuffer)
        {
            if(isRoundOverInvoked == false)
            {
                OnRoundOver.Invoke();
                isRoundOverInvoked = true;
            }
        }

    }
    private IEnumerator StartVignette() {
        float elapsedTime = 0f;
        int timeToFade = areaTimerUI.GetTimeToChangeColor();

        while (elapsedTime < timeToFade) {
            vignetteScript.mainScale = Mathf.Lerp(0f, vignetteScript.maxMainScale, elapsedTime / timeToFade);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        vignetteScript.mainScale = vignetteScript.maxMainScale;
    }

    public void ResetAreaTime(Current_Area currentArea)
    {
        timeAlmostDone = true;

        switch (currentArea)
        {
            case Current_Area.KITCHEN:
                timer = kitchenTimeLength;
                break;
            case Current_Area.DUNGEON:
                timer = dungeonTimeLength;
                break;
        }
        
        vignetteScript.mainScale = 0f;
        isTimerPaused = false;
        isRoundOverInvoked = false;
    }

    public float GetCurrentTime()
    {
        return timer;
    }
    public void PauseTimer() {
        isTimerPaused = true;
    }
    public void ResumeTimer() {
        isTimerPaused = false;
    }
    public void ActivateVignette(bool isActive) {
        vignette.SetActive(isActive);
    }

}
