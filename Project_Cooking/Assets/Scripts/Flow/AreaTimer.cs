using System.Collections;
using UnityEngine;
public class AreaTimer : MonoBehaviour {

    [SerializeField] private Current_Area currentArea = Current_Area.LIMBO;
    [SerializeField] float timeToSwitchInSeconds = 15f;
    public float timer = 0f;
    public bool pauseTimer = false;

    void Start() {
        currentArea = Current_Area.DUNGEON;
        timer = 0f;
    }

    void Update() {
        if (!pauseTimer) {
            if (timer < timeToSwitchInSeconds) {
                timer += Time.deltaTime;
            }
            if (timer >= timeToSwitchInSeconds) {
                DetermineCurrentArea();
                timer = 0;
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
