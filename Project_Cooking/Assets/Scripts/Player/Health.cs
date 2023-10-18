using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {

    [SerializeField] private const int MAX_HEALTH = 3;
    [SerializeField] private int currentHealth = 3;
    [SerializeField] private bool godMode = false;
    public UnityEvent OnDeath;

    private void Awake() {
       
    }

    public void Heal(int amt) {
        if ((currentHealth += amt) >= MAX_HEALTH) {
            currentHealth = MAX_HEALTH;
        } else {
            currentHealth += amt;
        }
    }
   
    public void TakeDamage(int amt) {
        if (godMode) {
            Debug.Log("Damage Negated. GooseMode Activated.");
            return;
        }
        
        if ((currentHealth -= amt) <= 0) {
            currentHealth = 0;
            Death();
        }
        else {
            currentHealth -= amt;
        }
    }
    // this is for dev mode: ******
    public void FullHeal()
    {
        currentHealth = MAX_HEALTH;
    }
    // ******
    public void InstaKill() {
        currentHealth = 0;
        Death();
    }
    public void Death() {
        Debug.Log("Wow you suck. Get good");
        // OnDeath event stuff
        OnDeath.Invoke();
    }
}
