using com.cyborgAssets.inspectorButtonPro;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{

    public const int MAX_HEALTH = 3;
    private int currentHealth = 3;
    [SerializeField] private float invicibilityTime = 1f;
    [SerializeField] private bool godMode = false;
   [HideInInspector] public UnityEvent OnDeath;
    [HideInInspector] public UnityEvent OnHurt;
    [HideInInspector] public UnityEvent OnHeal;

    [ProButton]
    public void Heal(int healAmount)
    {

        currentHealth += healAmount;
        OnHeal.Invoke();
        if (currentHealth > MAX_HEALTH)
        {
            currentHealth = MAX_HEALTH;
        }
    }
    public void Init()
    {
        currentHealth = MAX_HEALTH;
        godMode = false;
    }
    public void TakeDamage(int amt)
    {
        if (godMode) return;

        currentHealth -= amt;

        OnHurt.Invoke();
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
            return;
        }
        StartCoroutine(InvincibleAfterEffect());
    }
    private IEnumerator InvincibleAfterEffect() {
        godMode = true;
        yield return new WaitForSeconds(invicibilityTime);
        godMode = false;
    }

    public void ToggleGodMode()
    {
        godMode = !godMode;
    }

    public void InstaKill()
    {
        currentHealth = 0;
        Die();
    }
    private void Die()
    {

        // OnDeath event stuff
        OnDeath.Invoke();
    }

    public void Flash()
    {
        Material mat = GetComponent<Renderer>().material;

        if (mat)
        {
            mat.DOFloat(1f, "_HitEffectBlend", .1f).SetEase(Ease.InOutBack).SetLoops(4, LoopType.Yoyo).OnComplete(() =>
            {
                mat.SetFloat("_HitEffectBlend", 0f);
            });
        }

    }


}
