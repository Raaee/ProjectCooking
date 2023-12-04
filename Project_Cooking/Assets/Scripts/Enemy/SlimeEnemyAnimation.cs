using UnityEngine;

public class SlimeEnemyAnimation : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private bool isWalking = false;
    private const string WALK_BOOL_TAG = "Walk";
    private const string ATTACK_TRIGGER = "Attack";
    private void Update()
    {
        anim.SetBool(WALK_BOOL_TAG, isWalking);
    }

    public void SetWalkingState(bool _isWalking)
    {
        isWalking = _isWalking;
    }
    public void AttackAnimation()
    {
        anim.SetTrigger(ATTACK_TRIGGER);
    }
}
