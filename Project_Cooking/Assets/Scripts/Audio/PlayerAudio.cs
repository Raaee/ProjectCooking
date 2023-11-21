using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference playerHurtAudio;


    [SerializeField] private Health playerHealth;

    private void Awake()
    {
        playerHealth.OnHurt.AddListener(PlayPlayerHurtAudio);
    }

    public void PlayPlayerHurtAudio()
    {
        FMODUnity.RuntimeManager.PlayOneShot(playerHurtAudio, transform.position);
    }


}
