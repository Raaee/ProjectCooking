using UnityEngine;

public class AutomaticPickup : MonoBehaviour
{
    private Transform playerTransform;
   private float pickUpSpeed = 3.5f;
    private ProgressBar progressBar;

    [SerializeField] private Rigidbody2D rb2d;
    private bool hasTarget = false;
    [SerializeField] private FMODUnity.EventReference bloodPickupAudio;

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
        rb2d.velocity = new Vector2(direction.x, direction.y) * pickUpSpeed;

        if (Vector2.Distance(playerTransform.position, transform.position) < 0.1f)
        {
            FMODUnity.RuntimeManager.PlayOneShot(bloodPickupAudio, transform.position);
            progressBar.Increase();
            this.gameObject.SetActive(false);

        }
    }

    public void SetTargetPosition(Transform newTransform)
    {
        GetComponent<RaeusBounce>().SetBounce(false);
        playerTransform = newTransform;
        hasTarget = true;
        GetComponentInChildren<BloodDropSpriteData>().highlightSprites();
    }
}
