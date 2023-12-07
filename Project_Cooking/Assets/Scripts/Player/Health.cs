using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{

    public const int MAX_HEALTH = 3;
    private int currentHealth = 3;
    [SerializeField] private float invicibilityTime = 1f;
    [SerializeField] private bool godMode = false;
    [SerializeField] private bool invicible = false;
    public UnityEvent OnDeath;
    public UnityEvent OnHurt;
    public UnityEvent OnHeal;
    private float invicibilityTimer = 0f;

    private void Update() {
        if (!invicible) return;

        godMode = true;
        invicibilityTimer += Time.deltaTime;

        if (invicibilityTimer >= invicibilityTime) {
            invicibilityTimer = 0f;
            godMode = false;
            invicible = false;
        }
    }
    public void Heal(int amt)
    {

        currentHealth += amt;
        OnHeal.Invoke();
        if (currentHealth > MAX_HEALTH)
        {
            currentHealth = MAX_HEALTH;
        }
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
        }

    }
    public void EnableInvincibility() {
        invicible = true;
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
