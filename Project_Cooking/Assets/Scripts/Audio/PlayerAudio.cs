using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("AUDIO")]
    [SerializeField] private FMODUnity.EventReference playerHurtAudio;
    [SerializeField] private FMODUnity.EventReference playerWalkAudio;
    [SerializeField] private FMODUnity.EventReference playerDeathAudio;
    [Header("References")]
    [SerializeField] private Health playerHealth;
    [SerializeField] private Movement playerMovement;
    private bool playerDead = false;
    [Header("Walking Data")]
     private float minInterval = 0.25f;
    private float nextPlayTime;
    private void Awake()
    {
        playerHealth.OnHurt.AddListener(PlayPlayerHurtAudio);
        playerHealth.OnDeath.AddListener(PlayPlayerDeathAudio);
    }
    private void Start()
    {
        nextPlayTime = Time.time + minInterval; // Initialize last play time with offset to prevent immediate play
    }

    private void Update()
    {
        if(playerMovement.IsPlayerMoving() && (Time.time >= nextPlayTime))
        {
            PlayWalkAudio();
            nextPlayTime = Time.time + minInterval;
        }
    }
    public void PlayPlayerHurtAudio()
    {
        FMODUnity.RuntimeManager.PlayOneShot(playerHurtAudio, transform.position);
    }

    public void PlayWalkAudio()
    {
        FMODUnity.RuntimeManager.PlayOneShot(playerWalkAudio, transform.position);
    }
    public void PlayPlayerDeathAudio()
    {
        if(!playerDead)
            FMODUnity.RuntimeManager.PlayOneShot(playerDeathAudio, transform.position);
        playerDead = true;
    }

}
