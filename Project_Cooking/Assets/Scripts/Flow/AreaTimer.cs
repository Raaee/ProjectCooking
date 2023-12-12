using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class AreaTimer : MonoBehaviour
{
    [SerializeField] private GameObject vignette;
    [SerializeField] private float kitchenTimeLength = 15f; //should probs be the same?
    [SerializeField] private float dungeonTimeLength = 15f;
    [SerializeField] private float vignetteEffectSpeed = 1f;
    //no limbo time, that is made by the player
    private float roundOverTime;
    private AreaTimerUI areaTimerUI;

    [Header("DEBUG")]
    [SerializeField] private float timer = 0f;
    [SerializeField] private bool isTimerPaused = false;

    public UnityEvent OnRoundOver;

    void Start()
    {
        areaTimerUI = GetComponent<AreaTimerUI>();
        dungeonTimeLength += 0.8f;
        kitchenTimeLength += 0.8f;
        timer = dungeonTimeLength;
    }

    void Update()
    {
        if 
            (isTimerPaused) return;
        else
            timer -= Time.deltaTime;

        if (timer <= 0.3f)
        {
            OnRoundOver.Invoke();
            Debug.Log("timer done!");
        }

    }

    public void ResetAreaTime(Current_Area currentArea)
    {
        switch (currentArea)
        {
            case Current_Area.KITCHEN:
                timer = kitchenTimeLength;
                break;
            case Current_Area.DUNGEON:
                timer = dungeonTimeLength;
                break;
        }
        isTimerPaused = false;
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
