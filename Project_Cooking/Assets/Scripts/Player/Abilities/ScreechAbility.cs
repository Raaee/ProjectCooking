using System.Collections;
using UnityEngine;

public class ScreechAbility : Ability {
    // Default keybind: F [Keyboard]

    // [SerializeField] private float enemyStunDuration = 5f;
    [Header("Screech ABility References")]
    [SerializeField] private PlayerAnimation playerAnim;
    [SerializeField] private FMODUnity.EventReference screechAudio;

    public override void Awake() {
        base.Awake();
        actions.OnScreech.AddListener(PerformAbility);
    }
    public override void OnAbilityStart() {
        Debug.Log("SCREEEEECH");
        playerAnim.ToggleBatMode();
        FMODUnity.RuntimeManager.PlayOneShot(screechAudio, transform.position);
    }
    public override void OnAbilityEnd() {
        PlayAbilityOneShot();
        playerAnim.ToggleBatMode();
    }
    public override void OnNotEnoughBlood() {
        Debug.Log("Not Enough blood for SCREECH");
    }
    public override void OnCantPerform() {
        Debug.Log("NO");
    }
}
