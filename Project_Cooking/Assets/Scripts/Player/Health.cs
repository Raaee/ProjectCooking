using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {

    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth = 3;
    [SerializeField] private bool godMode = false;
    private UnityEvent onDeath;

    private void Start() {
        onDeath.AddListener(OnDeath);
    }

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
        Debug.Log("Wow you suck. Get good");
        // OnDeath event stuff
    }
}
