using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour   {

    private Actions actions;
    private Movement movement;
    public bool attacking = false;
    [SerializeField] private GameObject knifePrefab;
    [SerializeField] private float attackSpeed = 2f;
    private Vector2 moveDir;
    private float timer = 0f;
    private void Awake() {
        actions = GetComponent<Actions>();
        movement = GetComponent<Movement>();
        actions.OnAttack_Started.AddListener(StartAttack);
        actions.OnAttack_Cancelled.AddListener(StopAttack);
        attackSpeed = 1f / attackSpeed;
    }
    private void Update() {
        if (!attacking) {
            return;
        }
        SpawnKnife();
    }
    public void SpawnKnife() {
        timer += Time.deltaTime;
        if (timer >= attackSpeed) {
            var go = Instantiate(knifePrefab, this.transform.position, Quaternion.identity);
            go.GetComponent<Knife>().SetAttackDirection(GetAttackDirectionFromMoveDirection());
            timer = 0f;
        }
    }
    public AttackDirection GetAttackDirectionFromMoveDirection() {
        moveDir = movement.GetMovementDirection();

        if (moveDir.y >= 0f) {
            return (moveDir.x > 0f) ? AttackDirection.RIGHT : ((moveDir.x < 0f) ? AttackDirection.LEFT : AttackDirection.UP);
        }
        else {
            return (moveDir.x > 0f) ? AttackDirection.RIGHT : ((moveDir.x < 0f) ? AttackDirection.LEFT : AttackDirection.DOWN);
        }

    }
    public void StartAttack() {
        attacking = true;
    }
    public void StopAttack() {
        attacking = false;
    }

}
