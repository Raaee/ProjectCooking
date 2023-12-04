using UnityEngine;

public class DungeonGameplayAudioUI : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference notEnoughBloodAudio;
    [SerializeField] private FMODUnity.EventReference onCooldownAudio;


    public void PlayNotEnoughBloodAudio()
    {
        FMODUnity.RuntimeManager.PlayOneShot(notEnoughBloodAudio, transform.position);
    }

    public void PlayOnCooldownAudio()
    {
        FMODUnity.RuntimeManager.PlayOneShot(onCooldownAudio, transform.position);
    }

}
