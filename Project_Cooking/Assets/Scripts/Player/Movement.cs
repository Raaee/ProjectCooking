using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour   {

    [SerializeField] private Input input;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float topSpeed = 8f;
    [SerializeField] private float accelerationRate = 16.0f;
    [SerializeField] private float decelerationRate = 20f;
    private Vector2 moveDirection = Vector2.zero;

    private void Update() {
        moveDirection = input.move.ReadValue<Vector2>();
    }

    private void FixedUpdate() {
        // acceleration increase when movement is not zero:
        if (moveDirection != Vector2.zero) {
            Vector2 deltaVelocity = (moveDirection * topSpeed) - rb.velocity;
            Vector2 accelerationVector = deltaVelocity.normalized * (accelerationRate * Time.fixedDeltaTime);

            if (accelerationVector.sqrMagnitude > deltaVelocity.sqrMagnitude) {
                accelerationVector = deltaVelocity;
            }

            rb.velocity += accelerationVector;
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, topSpeed);
        }
        // acceleration decrease when movement is zero:
        else {
            Vector2 decelerationVector = -rb.velocity.normalized * (decelerationRate * Time.fixedDeltaTime);
            rb.velocity += decelerationVector;

            if (Vector2.Dot(rb.velocity, decelerationVector) < -5f) {
                rb.velocity = Vector2.zero;
            }
        }        

    }

    private void SaveNormalSpeed()
    {

    }



}
