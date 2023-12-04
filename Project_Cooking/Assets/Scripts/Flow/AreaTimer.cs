using System.Collections;
using UnityEngine;
public class AreaTimer : MonoBehaviour {

    [SerializeField] private Current_Area currentArea = Current_Area.LIMBO;
    [SerializeField] float timeToSwitchInSeconds = 15f;
    public float timer = 0f;
    public bool pauseTimer = false;

    void Start() {
        currentArea = Current_Area.DUNGEON;
        timeToSwitchInSeconds += 0.5f;
        timer = timeToSwitchInSeconds;
    }

    void Update() {
        if (!pauseTimer) {
            if (timer > 0f) {
                timer -= Time.deltaTime;
            }
            if (timer <= 0) {
                DetermineCurrentArea();
                timer = timeToSwitchInSeconds;
                Debug.Log("Switched to: " + currentArea);
            }
        }
    }
    public void DetermineCurrentArea() {
        if (currentArea == Current_Area.DUNGEON) {
            currentArea = Current_Area.KITCHEN;
        }
        else {
            currentArea = Current_Area.DUNGEON;
        }
    }

    public Current_Area GetCurrentArea() {
        return currentArea;
    }
}
