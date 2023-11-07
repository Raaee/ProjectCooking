using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour   {

    [SerializeField] private Input input;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float moveSpeed = 30f;
    private float currentSpeed = 0f;
    private Vector2 moveDirection = Vector2.zero;
   [SerializeField] private PlayerAnimation playerAnim; 
  
    private void Start()
    {
        SaveNormalSpeed();

        playerAnim.PlayAnimation(PlayerAnimation.IDLE);
    }
    private void Update() {
        moveDirection = input.move.ReadValue<Vector2>();
    }
    private void FixedUpdate() {
        if (moveDirection != Vector2.zero) {
            rb.velocity = moveDirection * moveSpeed * moveSpeed * Time.fixedDeltaTime;
            HandleAnimationFromDirection(rb.velocity);
        } else {
            rb.velocity = Vector2.zero;
            playerAnim.PlayAnimation(PlayerAnimation.IDLE);
        }
    }

    private void HandleAnimationFromDirection(Vector2 playerDirection)
    {
        if(!playerAnim.GetIsInBatMode())
        {
            if (playerDirection.x > 0)
                playerAnim.PlayAnimation(PlayerAnimation.RIGHT_WALK);
            else
                playerAnim.PlayAnimation(PlayerAnimation.LEFT_WALK);
        }
        else
        {
            if (playerDirection.x > 0)
                playerAnim.PlayBatAnimation(PlayerAnimation.BAT_RIGHT);
            else
                playerAnim.PlayBatAnimation(PlayerAnimation.BAT_LEFT);
        }
      
    }

    private void SaveNormalSpeed()
    {
        currentSpeed = moveSpeed;
    }

    public void SpeedMode(float speedMultiplier)
    {
        moveSpeed *= speedMultiplier;
    }
    public void NormalSpeed()
    {
        moveSpeed = currentSpeed;
    }

}
