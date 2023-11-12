using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour  {

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject knifeObject;
    [SerializeField] private float maxLifeTime = 1f;
    [SerializeField] private float projectileSpeed = 1f;
    private float timer = 0f;
    [SerializeField] private AttackDirection attackDirection = AttackDirection.RIGHT;
    private Vector2 moveDirection;

    private void Start() {
        ChangeRotationOnDirection();
        SetMoveDirection();
    }
    private void FixedUpdate() {
        MoveKnife();
        timer += Time.deltaTime;
        if (timer >= maxLifeTime) {
            Destroy(this.gameObject);
        }
    }
    public void MoveKnife() {
        rb.velocity = moveDirection * projectileSpeed * Time.fixedDeltaTime;
    }
    public void SetMoveDirection() {
        switch (attackDirection) {
            case AttackDirection.RIGHT:
                moveDirection = new Vector2(120f, 0f);
                break;
            case AttackDirection.LEFT:
                moveDirection = new Vector2(-120f, 0f);
                break;
            case AttackDirection.UP:
                moveDirection = new Vector2(0f, 120f);
                break;
            case AttackDirection.DOWN:
                moveDirection = new Vector2(0f, -120f);
                break;
        }
    }
    private void ChangeRotationOnDirection() {
        switch(attackDirection) {
            case AttackDirection.RIGHT:
                knifeObject.transform.Rotate(new Vector3(0f, 0f, 0f));                
                break;
            case AttackDirection.LEFT:
                knifeObject.transform.Rotate(new Vector3(180f, 0f, 180f));
                break;
            case AttackDirection.UP:
                knifeObject.transform.Rotate(new Vector3(0f, 0f, 90f));
                break;
            case AttackDirection.DOWN:
                knifeObject.transform.Rotate(new Vector3(180f, 0f, 90f));
                break;
        }
    }
    public void SetAttackDirection(AttackDirection newAttackDirection) {
        attackDirection = newAttackDirection;
    }
    
}
public enum AttackDirection {
    RIGHT, 
    LEFT,
    UP,
    DOWN
}
