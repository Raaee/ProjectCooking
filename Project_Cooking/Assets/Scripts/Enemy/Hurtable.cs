using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//drag and drop component that hurts the player using its tag
public class Hurtable : MonoBehaviour
{
   
    [SerializeField] private Game_Tag whoGetsHurt = Game_Tag.Player;
    [SerializeField] private int damageAmt = 1;
     private float knockbackForce = 250f;
   

    private void Awake()
    {
        //verify we have a box collider 2d 
        Collider2D col2D = GetComponent<Collider2D>();
        if (!col2D)
        {
            Debug.LogError("This gameobject needs a collider 2d if you plan on using this Hurtable component");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       

        if (!collision.CompareTag(whoGetsHurt.ToString())) 
            return;

        Health health = collision.gameObject.GetComponent<Health>();

        if (!health)
            return;

        health.TakeDamage(damageAmt);

      //  ApplyKnockback(collision);
       

    }

    private void ApplyKnockback(Collider2D collision)
    {
        Rigidbody2D rb2d = collision.gameObject.GetComponent<Rigidbody2D>();
        if (!rb2d)
            return;

       
        Vector2 direction = (collision.transform.position - transform.position).normalized;
        Vector2 knockback = knockbackForce * direction;
        rb2d.AddForce(knockback, ForceMode2D.Impulse);
        Debug.Log("apply knockback " + knockback + "to  " + collision.gameObject.name);
    }
}


public enum Game_Tag
{
    NONE,
    Player,
    Enemy
}