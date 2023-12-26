using UnityEngine;
using UnityEngine.Events;
using System.Collections;
public class ScreechAbility : Ability
{
    // Default keybind: F [Keyboard]
    [SerializeField] private float enemyStunDuration = 5f;

    [Header("SCREECH REFERENCES")]
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private PlayerAnimation playerAnim;
    [SerializeField] private FMODUnity.EventReference screechAudio;

    public UnityEvent OnScreenAbility;
    public override void Awake()
    {
        base.Awake();
        actions.OnScreech.AddListener(PerformAbility);
    }
    public override void OnAbilityStart()
    {
        Debug.Log("SCREEEEECH");
        OnScreenAbility.Invoke();
        playerAnim.ToggleBatMode();
        playerAnim.EnableScreech();
        playerAnim.PlayScreechAnimation();
        StartCoroutine(FreezeAllEnemies());
        FMODUnity.RuntimeManager.PlayOneShot(screechAudio, transform.position);
    }
    public override void OnAbilityEnd()
    {
        PlayAbilityOneShot();
        playerAnim.ToggleBatMode();
        playerAnim.DisableScreech();
    }
    public override void OnNotEnoughBlood()
    {
        Debug.Log("Not Enough blood for SCREECH");
    }
    public override void OnCantPerform()
    {
        Debug.Log("NO");
    }
    public IEnumerator FreezeAllEnemies() {
        enemyManager.FreezeAllEnemies();
        yield return new WaitForSeconds(enemyStunDuration);
        enemyManager.UnFreezeAllEnemies();
    }
}
