using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        actions.OnAttack_Started_Context.AddListener(StartAttack);
        actions.OnAttack_Cancelled.AddListener(StopAttack);
        attackSpeed = 1f / attackSpeed;
    }
    private void Update() {
        if (!attacking) {
            return;
        }
        timer += Time.deltaTime;
        if (timer >= attackSpeed)
        {
           // SpawnKnife();
            timer = 0f;
        }
       
    }
    public void SpawnKnife(Vector2 dir) {
      

        var go = Instantiate(knifePrefab, this.transform.position, Quaternion.identity);
        go.GetComponent<Knife>().SetAttackDirection(GetEightDirection(dir));
        //get direction from mouse and this object

    }


    public void StartAttack(InputAction.CallbackContext context) {
        attacking = true;
        if (context.control.IsPressed()) // Check if the button is pressed
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector2 objectPosition = Camera.main.WorldToScreenPoint(transform.position);
           

            // Calculate normalized direction
            Vector2 direction = (mousePosition - objectPosition).normalized;

         
            SpawnKnife(direction);
        }
    }
    public void StopAttack() {
        attacking = false;
    }

    private AttackDirection GetEightDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
 

        angle = (angle + 360) % 360; // Ensure positive angle
        // Map angle to 8-direction enum
        if (angle >= 22.5f && angle < 67.5f)
            return AttackDirection.UP_RIGHT;
        else if (angle >= 67.5f && angle < 112.5f)
            return AttackDirection.UP;
        else if (angle >= 112.5f && angle < 157.5f)
            return AttackDirection.UP_LEFT;
        else if (angle >= 157.5f && angle < 202.5f)
            return AttackDirection.LEFT;
        else if (angle >= 202.5f && angle < 247.5f)
            return AttackDirection.DOWN_LEFT;
        else if (angle >= 247.5f && angle < 292.5f)
            return AttackDirection.DOWN;
        else if (angle >= 292.5f && angle < 337.5f)
            return AttackDirection.DOWN_RIGHT;
        else
            return AttackDirection.RIGHT;
    }

}
public enum AttackDirection
{
    UP,
    UP_RIGHT,
    RIGHT,
    DOWN_RIGHT,
    DOWN,
    DOWN_LEFT,
    LEFT,
    UP_LEFT
}