using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;
using UnityEngine.Events;

public class AreaTimer : MonoBehaviour
{

    [SerializeField] private float kitchenTimeLength = 15f; //should probs be the same?
    [SerializeField] private float dungeonTimeLength = 15f;
    //no limbo time, that is made by the player
    private float roundOverTime;

    [Header("DEBUG")]
    [SerializeField] private float timer = 0f;
    [SerializeField] private bool isTimerPaused = false;

    public UnityEvent OnRoundOver;

    void Start()
    {
        timer = dungeonTimeLength;
    }

    void Update()
    {
        if 
            (isTimerPaused) return;
        else
            timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            OnRoundOver.Invoke();
            Debug.Log("timer done!");
            isTimerPaused = true;
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

}
