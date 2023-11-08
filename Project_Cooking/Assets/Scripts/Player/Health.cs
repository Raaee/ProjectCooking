using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {

    [SerializeField] private const int MAX_HEALTH = 3;
    [SerializeField] private int currentHealth = 3;
    [SerializeField] private bool godMode = false;
    public UnityEvent OnDeath;
    public UnityEvent OnHurt;

    public void Heal(int amt) {
        if ((currentHealth + amt) >= MAX_HEALTH) {
            currentHealth = MAX_HEALTH;
        } else {
            currentHealth += amt;
        }
    }

    public void TakeDamage(int amt) {
        if (godMode) {
         
            return;
        }

        currentHealth -= amt;
     
        OnHurt.Invoke();
        if (currentHealth <= 0) {
            currentHealth = 0;
            Die();
        }
       
    }

    public void ToggleGodMode()
    {
        godMode = !godMode;
    }
   
    public void InstaKill() {
        currentHealth = 0;
        Die();
    }
    private void Die() {
    
        // OnDeath event stuff
        OnDeath.Invoke();
    }

    public void Flash()
    {
        Material mat = GetComponent<Renderer>().material;

        if(mat)
        {
            mat.DOFloat(1f, "_HitEffectBlend", .1f).SetEase(Ease.InOutBack).SetLoops(4, LoopType.Yoyo).OnComplete(() =>
            {
                mat.SetFloat("_HitEffectBlend", 0f);
            });
        }

    }

  
}
