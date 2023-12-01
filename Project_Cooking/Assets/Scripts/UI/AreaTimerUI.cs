using System.Collections;
using UnityEngine;
using TMPro;

public class AreaTimerUI : MonoBehaviour {

    [SerializeField] private AreaTimer areaTimer;
    [SerializeField] private TMP_Text areaTimerText;
    [SerializeField] private Color32 colorNormal;
    [SerializeField] private Color32 colorAlmostOver;
    [SerializeField] private int timeToChangeColor = 5;

    private void Update() {
        if (areaTimer.timer > (timeToChangeColor + 1)) {
            areaTimerText.color = colorNormal;
        }
        else {
            areaTimerText.color = colorAlmostOver;
        }
        UpdateTimer();
    }
    public void UpdateTimer() {
        float seconds = Mathf.FloorToInt(areaTimer.timer % 60);
        areaTimerText.text = string.Format("{0:00}:{1:00}", 0, seconds);
    }

}
