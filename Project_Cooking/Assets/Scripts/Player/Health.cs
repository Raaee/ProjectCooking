using com.cyborgAssets.inspectorButtonPro;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{

    public int maxHealth = 3;
    [SerializeField] private int currentHealth = 3;
    [Header("VARIABLE")]
    [SerializeField] private float invicibilityTime = 1f;
    
    [Header("DEBUG")]
    [SerializeField] private bool godMode = false;
    
    [HideInInspector] public UnityEvent OnDeath;
    [HideInInspector] public UnityEvent OnHurt;
    [HideInInspector] public UnityEvent OnHeal;
    private bool isDead = false;
    private void Start() {
        StartCoroutine(Invincibility(2f));
    }

    public void Heal(int amt)
    {
        currentHealth += amt;
        OnHeal.Invoke();
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    public void InitHealth()
    {
        currentHealth = maxHealth;
        godMode = false;
    }
    public void TakeDamage(int amt)
    {
        if (godMode) return;

        currentHealth -= amt;

       
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
            return;
        }
        OnHurt.Invoke();
        StartCoroutine(Invincibility(invicibilityTime));
    }
    private IEnumerator Invincibility(float time) {
        godMode = true;
        yield return new WaitForSeconds(time);
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
        OnDeath.Invoke();
        isDead = true;
    }
    public bool IsDead()
    {
        return isDead;
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
