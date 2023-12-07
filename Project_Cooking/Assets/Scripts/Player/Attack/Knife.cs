using UnityEngine;

public class Knife : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject knifeObject;
    [SerializeField] private float maxLifeTime = 1.5f;
    [SerializeField] private float projectileSpeed = 1f;
    private float normalProjSpeed;
    private float timer = 0f;
    [SerializeField] private AttackDirection attackDirection = AttackDirection.RIGHT;
    private Vector2 moveDirection;

    private void Start()
    {
        ChangeRotationOnDirection();
        SetMoveDirection();
        normalProjSpeed = projectileSpeed;
    }
    private void FixedUpdate()
    {
        MoveKnife();
        timer += Time.deltaTime;
        if (timer >= maxLifeTime)
        {
            Destroy(this.gameObject);
        }
    }
    public void MoveKnife()
    {
        rb.velocity = moveDirection * projectileSpeed * Time.fixedDeltaTime;
    }
    public void SetMoveDirection()
    {
        switch (attackDirection)
        {
            case AttackDirection.UP:
                moveDirection = new Vector2(0f, 120f);
                break;
            case AttackDirection.UP_RIGHT:
                moveDirection = (Vector2.up + Vector2.right).normalized * 120f;
                break;
            case AttackDirection.RIGHT:
                moveDirection = new Vector2(120f, 0f);
                break;
            case AttackDirection.DOWN_RIGHT:
                moveDirection = (Vector2.down + Vector2.right).normalized * 120f;
                break;
            case AttackDirection.DOWN:
                moveDirection = new Vector2(0f, -120f);
                break;
            case AttackDirection.DOWN_LEFT:
                moveDirection = (Vector2.down + Vector2.left).normalized * 120f;
                break;
            case AttackDirection.LEFT:
                moveDirection = new Vector2(-120f, 0f);
                break;
            case AttackDirection.UP_LEFT:
                moveDirection = (Vector2.up + Vector2.left).normalized * 120f;
                break;
        }
    }
    private void ChangeRotationOnDirection()
    {
        switch (attackDirection)
        {
            case AttackDirection.UP:
                knifeObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
                break;
            case AttackDirection.UP_RIGHT:
                knifeObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 45f));
                break;
            case AttackDirection.RIGHT:
                knifeObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                break;
            case AttackDirection.DOWN_RIGHT:
                knifeObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, -45f));
                break;
            case AttackDirection.DOWN:
                knifeObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, -90f));
                break;
            case AttackDirection.DOWN_LEFT:
                knifeObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, -45f));
                break;
            case AttackDirection.LEFT:
                knifeObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
                break;
            case AttackDirection.UP_LEFT:
                knifeObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 45f));
                break;
        }
    }
    public void SetAttackDirection(AttackDirection newAttackDirection)
    {
        attackDirection = newAttackDirection;
    }
    
    public void IncreaseProjSpeed(float multiplier) {
        projectileSpeed *= multiplier;
    }
    public void NormalSpeed() {
        projectileSpeed = normalProjSpeed;
    }

}
