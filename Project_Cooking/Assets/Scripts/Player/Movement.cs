using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour   {

    [SerializeField] private Input input;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float moveSpeed = 30f;
    private float currentSpeed = 0f;
    private Vector2 moveDirection = Vector2.zero;

    private void Start()
    {
        SaveNormalSpeed();
    }
    private void Update() {
        moveDirection = input.move.ReadValue<Vector2>();
    }
    private void FixedUpdate() {
        if (moveDirection != Vector2.zero) {
            rb.velocity = moveDirection * moveSpeed * moveSpeed * Time.fixedDeltaTime;
        } else {
            rb.velocity = Vector2.zero;
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
