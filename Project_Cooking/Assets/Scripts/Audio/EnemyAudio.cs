using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using System;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField] private EventReference enemyDefaultSoundRef;
    [SerializeField] private EventReference enemyDeathSoundRef;
    [SerializeField] private EventReference enemyDashSoundRef;
    [SerializeField] private GameObject enemyParentObj;
    private FMOD.Studio.EventInstance enemyDeathEvent;
    private FMOD.Studio.EventInstance enemyDashEvent;
    private FMOD.Studio.EventInstance enemyDefaultEvent;

    private void Awake()
    {
        ConnectFMODReferences();
        enemyParentObj.GetComponent<EnemyMovement>().OnEnemyCharge.AddListener(PlayEnemyDash);
        enemyParentObj.GetComponent<Health>().OnHurt.AddListener(PlayEnemyHurt);
        enemyParentObj.GetComponent<Health>().OnDeath.AddListener(PlayEnemyDeath);
    }
    private void Start()
    {
       
    }

    private void ConnectFMODReferences()
    {
        //Assign Instances
        enemyDeathEvent = RuntimeManager.CreateInstance(enemyDeathSoundRef);
        enemyDashEvent = RuntimeManager.CreateInstance(enemyDashSoundRef);
        enemyDefaultEvent = RuntimeManager.CreateInstance(enemyDefaultSoundRef);


        //Connect to 3d World space
        RuntimeManager.AttachInstanceToGameObject(enemyDeathEvent, enemyParentObj.GetComponent<Transform>(), enemyParentObj.GetComponent<Rigidbody>());
        RuntimeManager.AttachInstanceToGameObject(enemyDashEvent, enemyParentObj.GetComponent<Transform>(), enemyParentObj.GetComponent<Rigidbody>());
        RuntimeManager.AttachInstanceToGameObject(enemyDefaultEvent, enemyParentObj.GetComponent<Transform>(), enemyParentObj.GetComponent<Rigidbody>());
    }

    public void PlayEnemyDash()
    {
        enemyDashEvent.start();
    }

    public void PlayEnemyHurt()
    {
        enemyDefaultEvent.start();

    }
    public void PlayEnemyDeath()
    {
        enemyDeathEvent.start();
    }
}
