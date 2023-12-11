using TMPro;
using UnityEngine;

public class AreaTimerUI : MonoBehaviour
{

    [SerializeField] private AreaTimer areaTimer;
    [SerializeField] private TMP_Text areaTimerText;
    [SerializeField] private Color32 colorNormal;
    [SerializeField] private Color32 colorAlmostOver;
    [SerializeField] private int timeToChangeColor = 5;

    private void Update()
    {
        if (areaTimer.GetCurrentTime() > (timeToChangeColor + 1))
        {
            areaTimerText.color = colorNormal;
        }
        else
        {
            areaTimerText.color = colorAlmostOver;
        }
        UpdateTimer();
    }
    public void UpdateTimer()
    {
        float seconds = Mathf.FloorToInt(areaTimer.GetCurrentTime() % 60);
        areaTimerText.text = string.Format("{0:00}:{1:00}", 0, seconds);
    }
    public int GetTimeToChangeColor() {
        return timeToChangeColor;
    }

}
