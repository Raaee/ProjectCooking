using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticPickup : MonoBehaviour
{
    private Transform playerTransform;
   private float speed = 3f;
    private ProgressBar progressBar;

  [SerializeField]  private Rigidbody2D rb2d;
    private bool hasTarget = false;
   
    private void Awake()
    {   
        progressBar = FindObjectOfType<ProgressBar>();
        rb2d.GetComponent<Rigidbody2D>();
    }
   

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!hasTarget)
            return;

        Vector3 direction = (playerTransform.position - transform.position).normalized;
        rb2d.velocity = new Vector2(direction.x, direction.y ) * speed;

        if (Vector2.Distance(playerTransform.position, transform.position) < 0.5f)
        {
            progressBar.Increase(4);
            Destroy(this.gameObject);

        }
    }

    public void SetTargetPosition(Transform newTransform)
    {
        playerTransform = newTransform;
        hasTarget = true;
        GetComponentInChildren<BloodDropSpriteData>().highlightSprites();
    }
}
