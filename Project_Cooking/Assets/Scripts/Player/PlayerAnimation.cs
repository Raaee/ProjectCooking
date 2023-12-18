using DG.Tweening;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Animators")]
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator batAnimator;
    [SerializeField] private Animator screechAnimator;

    [Header("Renders")]
    [SerializeField] private Renderer playerSpriteRenderer; //should be the spriterenderer?
    [SerializeField] private Renderer batSpriteRenderer; //should be the spriterenderer?
    [SerializeField] private GameObject screechObj;

    //PLAYER ANIMATION TAGS 
    public const string LEFT_WALK = "Player_LeftWalk";
    public const string RIGHT_WALK = "Player_RightWalk";
    public const string UP_WALK = "Player_UpWalk";
    public const string IDLE = "Idle";

    //BAT ANIMATION TAGS 
    public const string BAT_LEFT = "Bat_Left_Idle";
    public const string BAT_RIGHT = "BatRight_Idle";
    public const string BAT_SCREECH = "Screech";

    private bool isInBatMode = false;
    private string currentAnim;
    private Health playerHealth;
    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        playerHealth.OnHurt.AddListener(Flash);
        currentAnim = IDLE;
        PlayScreechAnimation();
    }

    public void PlayBatAnimation(string animTag)
    {
        batAnimator.Play(animTag);
    }

    public void PlayAnimation(string animTag)
    {
        if (animTag == currentAnim)
            return;

        playerAnimator.Play(animTag);

        currentAnim = animTag;
    }

    public void EnableBatMode()
    {
        if (isInBatMode)
            return;
        playerSpriteRenderer.gameObject.SetActive(false);
        batSpriteRenderer.gameObject.SetActive(true);
        isInBatMode = true;
    }

    public void EnablePlayerMode()
    {
        if (!isInBatMode)
            return;
        playerSpriteRenderer.gameObject.SetActive(true);
        batSpriteRenderer.gameObject.SetActive(false);
        isInBatMode = false;
    }

    public void ToggleBatMode()
    {

        bool _isInBatMode = GetIsInBatMode();
        _isInBatMode = !_isInBatMode;

        if (_isInBatMode)
            EnableBatMode();
        else
            EnablePlayerMode();
    }

    public bool GetIsInBatMode()
    {
        return isInBatMode;
    }
    public void PlayScreechAnimation() {
        EnableScreech();
        screechAnimator.Play(BAT_SCREECH);
    }
    public void EnableScreech() {
        screechObj.SetActive(true);
    }
    public void DisableScreech() {
        screechObj.SetActive(false);
    }


    /// <summary>
    /// This will be using its shader/renderer. Might have to refactor if it affects the actual animations
    /// </summary>
    public void Flash()
    {
        Material mat = playerSpriteRenderer.material;

        if (!mat) return;
        int amtOfFlashes = 4;
        float finalFlashAmt = 0.1f;
        float duration = 0.1f;
        string materialTag = "_HitEffectBlend";

        mat.DOFloat(finalFlashAmt, materialTag, duration).SetEase(Ease.InOutBack).SetLoops(amtOfFlashes, LoopType.Yoyo).OnComplete(() =>
        {
            mat.SetFloat(materialTag, 0f);
        });

    }
}
