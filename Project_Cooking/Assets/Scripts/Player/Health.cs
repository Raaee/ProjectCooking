using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth = 3;
    [SerializeField] private bool godMode = false;
    
    public void Heal(int amt) {
        currentHealth += amt;
    }
    public void Heal() {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int amt) {
        currentHealth -= amt;
    }
    public void Kill() {
        currentHealth = 0;
    }
    public void OnDeath() {
        // OnDeath should have an event
    }
}
